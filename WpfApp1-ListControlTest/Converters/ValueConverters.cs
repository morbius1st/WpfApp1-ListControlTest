﻿#region + Using Directives

using System;
using System.Globalization;
using System.Windows.Data;

#endregion


// projname: WpfApp1_ListControlTest
// itemname: ValueConverters
// username: jeffs
// created:  3/24/2019 7:38:20 PM


namespace WpfApp1_ListControlTest.Converters
{
	[ValueConversion(typeof(string), typeof(string))]
	public class ValueConverters : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
