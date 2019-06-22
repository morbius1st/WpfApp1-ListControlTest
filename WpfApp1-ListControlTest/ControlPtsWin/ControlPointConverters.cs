#region + Using Directives

using System;
using System.Globalization;
using System.Windows.Data;

#endregion


// projname: WpfApp1_ListControlTest.ControlPtsWin
// itemname: ControlPointConverters
// username: jeffs
// created:  4/7/2019 7:38:50 PM


namespace WpfApp1_ListControlTest.ControlPtsWin
{
	[ValueConversion(typeof(double), typeof(string))]
	public class ControlPointXYZConverter : IValueConverter
	{
		// convert from source (double) to target (string)
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string output = ((double)value).ToString("F3");

			MainWindow.Cps.TellMeTbx("at Convert| (" + output + ")");

			return output;
		}

		// convert back from (string) to (double)
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			double output = double.Parse((string) value);

			MainWindow.Cps.TellMeTbx("at Convert back| (" + value + ")");

			return output;
		}
	}
	
	[ValueConversion(typeof(double), typeof(string))]
	public class ControlPointSlopeConverter : IValueConverter
	{
		// convert from (double) to (percent string)
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string output = ((double)value).ToString("P3");

			MainWindow.Cps.TellMeTbx("at Convert| (" + output + ")");

			return output;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			double output = double.Parse((string) value);

			MainWindow.Cps.TellMeTbx("at Convert back| (" + value + ")");

			return output;
		}
	}
}
