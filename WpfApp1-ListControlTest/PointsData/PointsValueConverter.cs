#region + Using Directives
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

#endregion


// projname: WpfApp1_ListControlTest.PointsData
// itemname: PointsValueConverter
// username: jeffs
// created:  4/1/2019 10:07:28 PM


namespace WpfApp1_ListControlTest.PointsData
{
	[ValueConversion(typeof(double), typeof(string))]
	public class PointsValueConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			double input = (double) value;
			return "converted| " + input.ToString("N4");
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string input = value.ToString();
			double result;

			if (Double.TryParse(input, out result))
			{
				return result;
			}

			return double.NaN;
		}
	}
}
