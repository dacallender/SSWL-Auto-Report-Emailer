using System.IO;
using System.Linq;
using SiriusScientific.Core.Threading;

namespace SiriusScientific.Core.Persistence
{
	public abstract class FileWatcher : ThreadObject
	{
		public delegate void FileSyatemChanged(string fileName, WatcherChangeTypes systemEvent);

		public event FileSyatemChanged OnFilesystemChanged;

		public FileWatcher(string watchDirectory, string extension)
		{
			FileExtension += extension;

			WatchDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

			WatchDirectory = watchDirectory;

//			OnFilesystemChanged += WatcherOnFilesystemChanged;
		}

		public string FileExtension { get; set; }
		public string WatchDirectory { get; set; }

		public FileSystemWatcher Watcher { get; set; }

		public override void WorkerThread(object parameters)
		{
			var files = (from file in Directory.EnumerateFiles(WatchDirectory, FileExtension, SearchOption.AllDirectories) select file).ToList();

			foreach (string file in files)
			{
				OnFilesystemChanged?.Invoke(file, WatcherChangeTypes.Created);
			}
		}

		private void FileSystemEventHandler(object sender, FileSystemEventArgs fileSystemEventArgs)
		{
			OnFilesystemChanged?.Invoke(fileSystemEventArgs.FullPath, fileSystemEventArgs.ChangeType);
		}

		public override bool Start(object parameters)
		{
			if (!Directory.Exists(WatchDirectory))
			{
				Directory.CreateDirectory(WatchDirectory);
			}

			Watcher = new FileSystemWatcher()
			{
				Path = WatchDirectory,
				Filter = FileExtension
			};

			Watcher.Created += FileSystemEventHandler;
			Watcher.Deleted += FileSystemEventHandler;
			Watcher.Renamed += FileSystemEventHandler;
			Watcher.Changed += FileSystemEventHandler;

			Watcher.EnableRaisingEvents = true;

			return base.Start(parameters);
		}

		public override bool Stop()
		{
			Watcher.EnableRaisingEvents = false;

			Watcher.Created -= FileSystemEventHandler;
			Watcher.Deleted -= FileSystemEventHandler;
			Watcher.Renamed -= FileSystemEventHandler;
			Watcher.Changed -= FileSystemEventHandler;

			Watcher = null;

			return base.Stop();
		}

		public override void Dispose()
		{
			Stop();

			base.Dispose();
		}

		//public abstract void WatcherOnFilesystemChanged(string fileName, WatcherChangeTypes systemEvent);
	}
}
