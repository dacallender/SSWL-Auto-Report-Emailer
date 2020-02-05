using System;
using System.Globalization;

namespace SiriusScientific.Mvvm.ViewModelBase
{
	public class AppInfo
	{
		public AppInfo(string appName)
		{
			AppId = Guid.NewGuid().ToString();

			StartTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);

			AppName = appName;
		}

		public string AppId { get; private set; }

		public string StartTime { get; private set; }

		public string AppName { get; private set; }
	}
}
