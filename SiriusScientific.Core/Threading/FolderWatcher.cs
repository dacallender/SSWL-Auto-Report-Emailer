using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace SiriusScientific.Core.Threading
{
	public class FolderWatcher : ThreadObject, IDisposable
	{
		private FileSystemWatcher _fileSystemWatcher;

		private readonly TaskFactory _taskPileline;

		private readonly BlockingCollection<FileSystemEventArgs> _filePathCollection;

		public delegate void ExistingPluginHandler(List<string> preExistingFiles);
		public event ExistingPluginHandler OnExistingPlugin;

		public delegate void NewPluginHandler(FileSystemEventArgs fileSystem);
		public event NewPluginHandler OnNewPlugin;

		public delegate void RemovedPluginHandler(FileSystemEventArgs fileSystem);
		public event NewPluginHandler OnRemovedPlugin;

		public FolderWatcher(string folderPath, string filter)
		{
			FolderPath = folderPath;

			FileFilter = filter;

			_taskPileline = new TaskFactory(TaskCreationOptions.LongRunning, TaskContinuationOptions.None);

			_filePathCollection = new BlockingCollection<FileSystemEventArgs>();
		}

		public override bool Start(object parameters)
		{
			ThreadPool.QueueUserWorkItem(GetExistingFiles);

			_taskPileline.StartNew(FormatFilePathPipeline);

			base.Start(parameters);

			return true;
		}

		public override bool Stop()
		{
			StopWatchingFolder();

			return true;
		}

		public override void WorkerThread(object parameters)
		{
			BeginWatchingFolder();
		}

		private bool _formatPipeline = true;
		void FormatFilePathPipeline()
		{
			while (_formatPipeline)
			{
				foreach (FileSystemEventArgs pluginFilesystem in _filePathCollection.GetConsumingEnumerable())
				{
					if (OnNewPlugin != null) OnNewPlugin(pluginFilesystem); 
				}
			}
		}

		[PermissionSet(SecurityAction.Demand, Name="FullTrust")]
		void BeginWatchingFolder()
		{
			_fileSystemWatcher = new FileSystemWatcher();

			_fileSystemWatcher.Path = FolderPath;

			_fileSystemWatcher.Filter = FileFilter;

			_fileSystemWatcher.Created += OnCreated;

			_fileSystemWatcher.Deleted += OnDeleted;

			_fileSystemWatcher.EnableRaisingEvents = true;
		}

		void GetExistingFiles(object state)
		{
			var preExistingFiles = Directory.EnumerateFiles(FolderPath, FileFilter, SearchOption.TopDirectoryOnly).ToList();

			if (OnExistingPlugin != null) OnExistingPlugin(preExistingFiles); 
		}

		public string FileFilter { get; set; }

		public string FolderPath { get; set; }

		void StopWatchingFolder()
		{
			_fileSystemWatcher.Created -= OnCreated;
			_fileSystemWatcher.Deleted -= OnDeleted;

			_fileSystemWatcher.EnableRaisingEvents = false;

			_formatPipeline = false;
		}

		void OnCreated(object source, FileSystemEventArgs e)
		{
			_filePathCollection.Add(e);
		}

		void OnDeleted(object source, FileSystemEventArgs e)
		{
			if (OnRemovedPlugin != null) OnRemovedPlugin(e); 
		}

		public override void Dispose()
		{
			if (_fileSystemWatcher != null)
			{
				_fileSystemWatcher.Dispose();
				_fileSystemWatcher = null;
			}
		}
	}
}
