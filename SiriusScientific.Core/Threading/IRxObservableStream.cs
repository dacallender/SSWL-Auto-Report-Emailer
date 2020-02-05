using System;

namespace SiriusScientific.Core.Threading
{
	public interface IRxObservableStream<T>
	{
		IObservable<T> ColdStream
		{
			get;
		}

		void OnSubscribedEventNotification( T item );
	}
}
