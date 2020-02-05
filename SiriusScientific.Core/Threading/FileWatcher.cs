using System;
using System.Collections.Concurrent;
using System.IO;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace SiriusScientific.Core.Threading
{
	internal class FileWatcher : ThreadObject, IDisposable
	{
		private FileSystemWatcher _fileSystemWatcher;

		private readonly TaskFactory _taskPileline;

		private readonly BlockingCollection<FileSystemEventArgs> _fileathCollection;

		public delegate void NewPluginHandler(FileSystemEventArgs fileSystem);
		public event NewPluginHandler OnNewPlugin;

		public FileWatcher(string filter)
		{
			FileFilter = filter;

			_taskPileline = new TaskFactory(TaskCreationOptions.LongRunning, TaskContinuationOptions.None);

			_fileathCollection = new BlockingCollection<FileSystemEventArgs>();
		}
	
		public override bool Start(object parameters)
		{
			_taskPileline.StartNew(FormatFilePathPipeline);

			base.Start(parameters);

			return true;
		}

		public override bool Stop()
		{
			StopWatchingFolder();

			return true;
		}

		protected override void WorkerThread(object parameters)
		{
			BeginWatchingFolder();
		}

		private bool _formatPipeline = true;
		void FormatFilePathPipeline()
		{
			while (_formatPipeline)
			{
				foreach (FileSystemEventArgs pluginFilesystem in _fileathCollection.GetConsumingEnumerable())
				{
					if (OnNewPlugin != null) OnNewPlugin(pluginFilesystem); 
				}
			}
		}

		[PermissionSet(SecurityAction.Demand, Name="FullTrust")]
		void BeginWatchingFolder()
		{
			_fileSystemWatcher = new FileSystemWatcher();

			_fileSystemWatcher.Path = Directory.GetCurrentDirectory();

			_fileSystemWatcher.Filter = FileFilter;

			_fileSystemWatcher.Created += OnCreated;
			_fileSystemWatcher.Deleted += OnDeleted;

			_fileSystemWatcher.EnableRaisingEvents = true;
		}

		public string FileFilter { get; set; }

		void StopWatchingFolder()
		{
			_fileSystemWatcher.Created -= OnCreated;
			_fileSystemWatcher.Deleted -= OnDeleted;

			_fileSystemWatcher.EnableRaisingEvents = false;

			_formatPipeline = false;
		}

		void OnCreated(object source, FileSystemEventArgs e)
		{
			_fileathCollection.Add(e);
		}

		void OnDeleted(object source, FileSystemEventArgs e)
		{
		}

		public void Dispose()
		{
			if (_fileSystemWatcher != null)
			{
				_fileSystemWatcher.Dispose();
				_fileSystemWatcher = null;
			}
		}
	}
}
