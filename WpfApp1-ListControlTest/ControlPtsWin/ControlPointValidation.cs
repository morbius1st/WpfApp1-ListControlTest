#region + Using Directives

using System.Globalization;
using System.Windows.Controls;

using static WpfApp1_ListControlTest.MainWindow;

#endregion


// projname: WpfApp1_ListControlTest.SurfacePts
// itemname: SurfacePointValidation
// username: jeffs
// created:  4/2/2019 9:42:24 PM


namespace WpfApp1_ListControlTest.ControlPtsWin
{
	public class ControlPointXYZValidation : ValidationRule
	{
		// validate the input value - this will be a string
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			double output;
			bool   result = double.TryParse((string) value, out output);

			Cps.AppendMessage("\n*** changed value to ("
				+ (string) value + ") ***"
				);

			Cps.TellMeTbx("at ValidationResult| (" + value + ")"
				+ " :: parse worked| " + result.ToString()
				);

			return new ValidationResult(result, null);
		}
	}
	
	public class ControlPointSlopeValidation : ValidationRule
	{
		// validate the input value - this will be a string
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			double output;
			bool   result = double.TryParse((string) value, out output);

			Cps.AppendMessage("\n*** changed value to ("
				+ (string) value + ") ***"
				);

			Cps.TellMeTbx("at ValidationResult| (" + value + ")"
				+ " :: parse worked| " + result.ToString()
				);

			return new ValidationResult(result, null);
		}
	}
}
