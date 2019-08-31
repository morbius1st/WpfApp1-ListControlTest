#region + Using Directives

using System.ComponentModel;
using System.Windows.Media;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows;
using Brush = System.Windows.Media.Brush;
using Color = System.Drawing.Color;
using FontFamily = System.Windows.Media.FontFamily;
using FontStyle = System.Windows.FontStyle;

#endregion


// projname: ParameterVue.WpfSupport
// itemname: ListBoxConfiguration
// username: jeffs
// created:  8/24/2019 12:57:41 PM


namespace ParameterVue.WpfSupport
{
	// information about the design / settings
	// for a listbox
	public class ListBoxConfiguration : INotifyPropertyChanged
	{
		public static readonly Font DefaultFont = new Font("Arial");

		public static Font HeaderFont { get; set; } = new Font("Arial");
		public static Font SymbolFont { get; set; } = new Font("ArialUni");
		public static Font RowHeaderFont { get; set; } = new Font("Arial");
		public static Font ParameterFont { get; set; } = new Font("Arial");

		static ListBoxConfiguration()
		{
			HeaderFont.Foreground = new SolidColorBrush(Colors.Aqua);
			SymbolFont.Foreground = new SolidColorBrush(Colors.Red);
			RowHeaderFont.Foreground = new SolidColorBrush(Colors.DeepSkyBlue);
			ParameterFont.Foreground = new SolidColorBrush(Colors.MediumBlue);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}
	}

	public class Font : INotifyPropertyChanged
	{
		private FontFamily fontFamily;		
		private double fontSize;		
		private FontStyle fontStyle;
		private FontWeight fontWeight;
		private FontStretch fontStretch;
		private Brush foreground;

		public Font(string fontFamily = null, double fontSize = 10.0f,
			FontStyle fontStyle = default(FontStyle),
			FontWeight fontWeight = default(FontWeight),
			FontStretch fontStretch = default(FontStretch),
			Brush foreground = default(Brush)
			)
		{
			FontFamily = new FontFamily(fontFamily ?? "Arial");
			FontSize = fontSize;
			FontStyle = fontStyle;
			FontWeight = fontWeight;
			FontStretch = fontStretch;
			Foreground = foreground;
		}

		public FontFamily FontFamily
		{
			get => fontFamily;
			set
			{
				fontFamily = value;
				OnPropertyChange();
			}
		}

		public double FontSize
		{
			get => fontSize;
			set
			{
				fontSize = value;
				OnPropertyChange();
			}
		}

		public FontStyle FontStyle
		{
			get => fontStyle;
			set
			{
				fontStyle = value;
				OnPropertyChange();
			}
		}

		public FontWeight FontWeight
		{
			get => fontWeight;
			set
			{
				fontWeight = value;
				OnPropertyChange();
			}
		}

		public FontStretch FontStretch
		{
			get => fontStretch;
			set
			{
				fontStretch = value;
				OnPropertyChange();
			}
		}

		public Brush Foreground
		{
			get => foreground;
			set
			{
				foreground = value; 
				OnPropertyChange();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this,   new PropertyChangedEventArgs(memberName));
		}
	}


}