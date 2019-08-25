#region + Using Directives

using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

#endregion


// projname: WpfApp1_ListControlTest.ControlPtsWin
// itemname: ControlPointConverters
// username: jeffs
// created:  4/7/2019 7:38:50 PM


namespace WpfApp1_ListControlTest.MainWinSupport
{
	[ValueConversion(typeof(double), typeof(string))]
	public class TopoPoint2XYZConverter : IValueConverter
	{
		// convert from source (double) to target (string)
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string result;

			double feet = Math.Truncate((double) value);
			double fract = ((double) value) - feet;

			double inches = fract * 12;

			result = feet.ToString("F0") + "\'-" + inches.ToString("F2") + "\"";

			return result;
		}

		// convert back from (string) to (double)
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			double output = double.NaN;

			try
			{

				Regex r = new Regex(@"(\d*)\'\s*\-\s*(\d*\.\d*)");

				Match m = r.Match((string) value);

				if (m.Groups.Count != 3) return "0";

				double feet = double.Parse(m.Groups[1].Value);

				double inches = double.Parse(m.Groups[2].Value) / 12;

				output = feet + inches;
			}
			catch
			{
				return 0;
			}

			return output;
		}
	}
	
	[ValueConversion(typeof(int), typeof(bool?))]
	public class ConvertIntToNullableBool : IMultiValueConverter
	{
		// convert from (int) to (bool?)
		// using a maximum value
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			int val = (int) values[0];
			int max = (int) values[1];
			bool? result = null;

			if (val == 0)
				result = false;
			else if (val == max)
				result = true;

			return result;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
		
	[ValueConversion(typeof(double), typeof(string))]
	public class TopoPoint2SlopeConverter : IValueConverter
	{
		// convert from (double) to (percent string)
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string output = ((double)value).ToString("P3");
			return output;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			double output = double.Parse((string) value);
			return output;
		}
	}

	[ValueConversion(typeof(bool), typeof(bool))]
	public class NegateBooleanConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return !(bool)value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return !(bool) value;
		}
	}

}
