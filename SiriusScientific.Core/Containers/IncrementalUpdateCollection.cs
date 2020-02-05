using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SiriusScientific.Core.Properties;

namespace SiriusScientific.Core.Containers
{
	public class IncrementalUpdateCollection<T> : System.Collections.CollectionBase
	{
		public delegate void NotifyIsDirty(int index, T value, CollectionChangeType change);

		public event NotifyIsDirty NotifyOnIsDirty;

		private object _padlock;

		public enum CollectionChangeType
		{
			Add,
			Delete,
			Insert,
			Update
		}

		public IncrementalUpdateCollection()
		{
			IsDirty = false;

			_padlock = new object();
		}

		public bool IsDirty { get; private set; }

		public void Add(T value)
		{
			lock (_padlock)
			{
				List.Add(value);

				IsDirty = true;

				if (NotifyOnIsDirty != null) NotifyOnIsDirty(List.Count - 1, value, CollectionChangeType.Add);

//				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, 1));
			}
		}

		public void AddAll(T[] collection)
		{
			int index = 0;

			foreach (T item in collection)
			{
				List[index] = item;

				index++;
			}

			if (NotifyOnIsDirty != null) NotifyOnIsDirty(List.Count - 1, collection[List.Count - 1], CollectionChangeType.Add);

//			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, 1));
		}

		public void Insert(int index, T value)
		{
			lock (_padlock)
			{
				List.Insert(index, value);

				IsDirty = true;

				if (NotifyOnIsDirty != null) NotifyOnIsDirty(index, value, CollectionChangeType.Insert);

//				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, 1));
			}
		}

		public virtual void Update(int index, T value)
		{
			lock (_padlock)
			{
				List[index] = value;

				IsDirty = true;

				if (NotifyOnIsDirty != null) NotifyOnIsDirty(index, value, CollectionChangeType.Update);

//				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, 1));
			}
		}

		public void Remove(int index)
		{
			lock (_padlock)
			{
				if (index > Count - 1 || index < 0)
				{
					throw new Exception("No items in list.");
				}
				else
				{
					T targetValue = (T) List[index];

					List.RemoveAt(index);

					IsDirty = true;

					if (NotifyOnIsDirty != null)
						NotifyOnIsDirty(index, targetValue, CollectionChangeType.Delete);

//					OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, 1));
				}
			}
		}

		public T Item(int index)
		{
			lock (_padlock)
			{
				return (T) List[index];
			}
		}

		public void ResetDirtyState()
		{
			lock (_padlock)
			{
				IsDirty = false;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			//if (CollectionChanged != null)
			//	CollectionChanged(this, e);
		}
	}
}
