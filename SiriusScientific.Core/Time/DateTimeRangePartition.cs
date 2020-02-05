using System;
using System.Collections.ObjectModel;

namespace SiriusScientific.Core.Time
{
	public class DateTimeRangePartition : ObservableCollection<DateTimeRangePartition>
	{
		protected DateTimeRangePartition()
		{
		}

		public DateTimeRangePartition(DateTime start, DateTime end)
		{
			Start = start;

			End = end;
		}

		public DateTimeRangePartition(DateTime start, DateTime end, TimeSpan timeSpan)
		{
			Start = start;

			End = end;

			Interval = timeSpan;

			Partition();
		}

		protected void Partition()
		{
			DateTime newStartDate = Start;

			DateTime newEndDate;

			do
			{
				newEndDate = newStartDate + Interval;

				Add(new DateTimeRangePartition(newStartDate, newEndDate));

				newStartDate = newEndDate;

			} while (newEndDate <= End);
		}

		public static int IntervalCount(DateTime startTime, DateTime endTime, TimeSpan interval)
		{
			int intervalCount = 0;

			DateTime newStartDate = startTime;

			DateTime newEndDate;

			do
			{
				newEndDate = newStartDate + interval;

				intervalCount++;

				newStartDate = newEndDate;

			} while (newEndDate < endTime);

			return intervalCount;
		}

		public DateTime Start { get; private set; }

		public DateTime End { get; private set; }

		public TimeSpan Interval { get; set; }
	}
}
