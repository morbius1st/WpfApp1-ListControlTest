#region + Using Directives
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



#endregion


// projname: ParameterVue.WpfSupport
// itemname: ListBoxConfiguration
// username: jeffs
// created:  8/24/2019 12:57:41 PM


namespace ParameterVue.WpfSupport
{
    // information about the design / settings
    // for a listbox
	public class ListBoxConfiguration
	{
        public static readonly Font DefaultFont = new Font("Arial", 10.0f );

        public static Font HeaderFont { get; set; } = DefaultFont;
        public static Font RowHeaderFont { get; set; } = DefaultFont;
        public static Font ParameterFont { get; set; } = DefaultFont;



	}
}
