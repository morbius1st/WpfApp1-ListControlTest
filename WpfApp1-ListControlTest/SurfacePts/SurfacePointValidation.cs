#region + Using Directives
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

#endregion


// projname: WpfApp1_ListControlTest.SurfacePts
// itemname: SurfacePointValidation
// username: jeffs
// created:  4/2/2019 9:42:24 PM


namespace WpfApp1_ListControlTest.SurfacePts
{
	public class SurfacePointValidation : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			if (((string) value).Length > 10)
			{
				return new ValidationResult(false, "string too long");
			}

			return new ValidationResult(true, null);
		}
	}
}
