using System;
using System.Globalization;
using System.Windows.Data;

namespace SiriusScientific.Core.Extensions
{
	public class ReadOnlyConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if ((value is bool ? (bool) value : false) == true) return false;

			return true;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if ((value is bool ? (bool) value : false) == true) return false;

			return true;
		}
	}
}