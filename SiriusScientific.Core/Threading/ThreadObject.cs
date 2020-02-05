using System;
using System.Threading;

namespace SiriusScientific.Core.Threading
{
	public abstract class ThreadObject : IThreadObject, IDisposable
	{
		public bool Started { get; set; }

		public ManualResetEvent PauseEvent { get; set; }

		protected ThreadObject()
		{
			PauseEvent = new ManualResetEvent(true);
		}
		
		public virtual bool Start(object parameters)
		{
			Started = true;

			Thread.Sleep(10);

			ThreadPool.QueueUserWorkItem(WorkerThread, parameters);

			return true;
		}

		public virtual bool Stop()
		{
			Started = false;

			PauseEvent.Set();

			return true;
		}

		public virtual void Pause(bool state)
		{
			if ( state )
				PauseEvent.Reset();
			else
				PauseEvent.Set();
		}

		public abstract void WorkerThread(object parameters);
		
		public virtual void Dispose()
		{
		}
	}
}
