using System.Threading;

namespace SiriusScientific.Core.Threading
{
	public interface IThreadObject
	{
		bool Started { get; set; }

		ManualResetEvent PauseEvent { get; set; }

		bool Start(object parameters);

		bool Stop();

		void Pause(bool state);

		void WorkerThread(object parameters);
	}
}