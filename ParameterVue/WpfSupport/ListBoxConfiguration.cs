#region + Using Directives
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
        public static readonly Font DefaultFont = new Font("Arial");

        public static Font HeaderFont { get; set; } = DefaultFont;
        public static Font SymbolrFont { get; set; } = new Font("ArialUni");
        public static Font RowHeaderFont { get; set; } = DefaultFont;
        public static Font ParameterFont { get; set; } = DefaultFont;
	}

	public class Font
	{
		public Font(string fontFamily = null, double fontSize = 10.0f, 
			FontStyle fontStyle = default(FontStyle), 
			FontWeight fontWeight = default(FontWeight), 
			FontStretch fontStretch = default(FontStretch))
		{
			FontFamily = new FontFamily(fontFamily ?? "Arial");
			FontSize = fontSize;
			FontStyle = fontStyle;
			FontWeight = fontWeight;
			FontStretch = fontStretch;
		}

		public FontFamily FontFamily { get; set; }
		public double FontSize { get; set; }
		public FontStyle FontStyle { get; set; }
		public FontWeight FontWeight { get; set; }
		public FontStretch FontStretch { get; set; }

	}

}