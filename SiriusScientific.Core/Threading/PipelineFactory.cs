using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace SiriusScientific.Core.Threading
{
	public abstract class PipelineFactory<T> : ThreadObject, IDisposable where T:class
	{
		protected BlockingCollection<T> Blockingcollection;

		protected List<T> Collection;

		protected CancellationTokenSource Cts;

		public PipelineFactory()
		{
			Blockingcollection = new BlockingCollection<T>();

			Collection = new List<T>();

			Cts = new CancellationTokenSource();
		}

		public override bool Stop()
		{
			Cts.Cancel();

			Blockingcollection.Add(null);

			return base.Stop();
		}

		public void Add(T item)
		{
			if (Blockingcollection != null)
			{
				Blockingcollection.Add(item);

				Collection.Add(item);
			}
		}

		public override void Dispose()
		{
			Blockingcollection.Dispose();

			Blockingcollection = null;
		}

		public virtual void  ParseMessages<T1>(T message, T1 messageType)
		{
		}
	}
}