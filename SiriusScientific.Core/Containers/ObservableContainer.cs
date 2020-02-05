using System.Collections.ObjectModel;

namespace SiriusScientific.Core.Containers
{
	public class ObservableContainer<T> : ObservableCollection<T>
	{
		public ObservableContainer()
		{
			CurrentPointer = 0;
		}
		
		public object NextValue()
		{
			if ( CurrentPointer >= this.Count )
			{
				CurrentPointer--;

				return -1;
			}
			else
			{
				var value = this[CurrentPointer];

				CurrentPointer++;

				return value;
			}
		}

		public object PreviousValue()
		{
			if ( CurrentPointer <= 0 ) CurrentPointer = 0;
			
			else CurrentPointer--;
			
			return this[CurrentPointer];
		}

		public object FetchItem(int index)
		{
			if ( index >= this.Count )
				return -1;

			else return this[index];
		}

		public void ResetPointer()
		{
			CurrentPointer = 0;
		}

		public int CurrentPointer
		{
			get;
			private set;
		}
	}
}
