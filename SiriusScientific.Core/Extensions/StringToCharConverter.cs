using System;
using System.Globalization;
using System.Windows.Data;

namespace SiriusScientific.Core.Extensions
{
	public class StringToCharConverter : IValueConverter
	{
		// ********************************************************************************
		/// <summary>
		/// Convert char value to string value
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns>return string</returns>
		/// <created>David,9/2/2017</created>
		/// <changed>David,9/2/2017</changed>
		// ********************************************************************************
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if ( value != null && value is char )
			{
				return value.ToString();
			}

			return null;
		}

		// ********************************************************************************
		/// <summary>
		/// Convert string value to char value
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns>return char</returns>
		/// <created>David,9/2/2017</created>
		/// <changed>David,9/2/2017</changed>
		// ********************************************************************************
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if ( value is string )
			{
				if (value.ToString() == "Yes") return 'Y';
			}

			return 'N';
		}
	}
}
