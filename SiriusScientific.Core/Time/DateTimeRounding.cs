using System;

namespace SiriusScientific.Core.Time
{
	public class DateTimeRounding
	{
		public static DateTime ToNearestTimePeriod(DateTime time, TimePeriod timePeriod)
		{
			DateTime targetTime = time;

			int days = 0;
			int hours = 0;
			int minutes = 0;
			int seconds = 0;

			switch (timePeriod)
			{
				case TimePeriod.Second:
					{
						seconds = 1;
					}
					break;
				case TimePeriod.Minute:
					{
						minutes = 1;
					}
					break;
				case TimePeriod.Hour:
					{
						hours = 1;
					}
					break;
				case TimePeriod.Day:
					{
						days = 1;
					}
					break;
			}

			TimeSpan timeSpan = new TimeSpan(days, hours, minutes, seconds);

			long spanTicks = targetTime.Ticks / timeSpan.Ticks;

			return new DateTime(spanTicks * timeSpan.Ticks);
		}
	}
}
