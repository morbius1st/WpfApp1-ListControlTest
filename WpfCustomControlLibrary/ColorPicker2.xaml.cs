using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WpfCustomControlLibrary
{
	/// <summary>
	/// Interaction logic for ColorPicker22.xaml
	/// </summary>
	public partial class ColorPicker2 : UserControl
	{
		public static DependencyProperty ColorProperty;

		public static DependencyProperty AlphaChannelProperty;
		public static DependencyProperty RedProperty;
		public static DependencyProperty BlueProperty;
		public static DependencyProperty GreenProperty;

		public static DependencyProperty SliderMarginProperty;
		public static DependencyProperty RectangleMarginProperty;

		public static readonly RoutedEvent ColorChangedEvent;

		private Color? prevColor;

	#region ctor

		public ColorPicker2()
		{
			InitializeComponent();
		}

		static ColorPicker2()
		{
			SliderMarginProperty = DependencyProperty.Register(
				"SliderMargin", typeof(Thickness), typeof(ColorPicker2),
				new FrameworkPropertyMetadata(new Thickness(0, 0, 0, 0)) );
			RectangleMarginProperty = DependencyProperty.Register(
				"RectangleMargin", typeof(Thickness), typeof(ColorPicker2),
				new FrameworkPropertyMetadata(new Thickness(0, 0, 0, 0)) );

			AlphaChannelProperty = DependencyProperty.Register(
				"Alpha", typeof(byte), typeof(ColorPicker2),
				new FrameworkPropertyMetadata(
					new PropertyChangedCallback(OnColorARGBChanged)));
			RedProperty = DependencyProperty.Register(
				"Red", typeof(byte), typeof(ColorPicker2),
				new FrameworkPropertyMetadata(
					new PropertyChangedCallback(OnColorARGBChanged)));
			GreenProperty = DependencyProperty.Register(
				"Green", typeof(byte), typeof(ColorPicker2),
				new FrameworkPropertyMetadata(
					new PropertyChangedCallback(OnColorARGBChanged)));
			BlueProperty = DependencyProperty.Register(
				"Blue", typeof(byte), typeof(ColorPicker2),
				new FrameworkPropertyMetadata(
					new PropertyChangedCallback(OnColorARGBChanged)));
			ColorProperty = DependencyProperty.Register(
				"Color", typeof(Color), typeof(ColorPicker2),
				new FrameworkPropertyMetadata(
					new PropertyChangedCallback(OnColorChanged)));

			ColorChangedEvent = EventManager.RegisterRoutedEvent(
				"ColorChanged", RoutingStrategy.Bubble,
				typeof(RoutedPropertyChangedEventHandler<Color>), typeof(ColorPicker2));

			CommandManager.RegisterClassCommandBinding(typeof(ColorPicker2),
				new CommandBinding(ApplicationCommands.Undo,
					UndoCommand_Executed, UndoCommand_CanExecute));

//			DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPicker2),
//				new FrameworkPropertyMetadata(typeof(ColorPicker2)));
		}

	#endregion

		private static void UndoCommand_CanExecute(object sender,
			CanExecuteRoutedEventArgs e
			)
		{
			e.CanExecute = ((ColorPicker2) sender).prevColor.HasValue;
		}

		private static void UndoCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			ColorPicker2 ColorPicker2 = ((ColorPicker2) sender);

			Color currentColor = ColorPicker2.Color;

			ColorPicker2.Color = (Color) ColorPicker2.prevColor;
		}

	#region properties

		public Color Color
		{
			get => (Color) GetValue(ColorProperty);
			set => SetValue(ColorProperty, value);
		}

		public byte Alpha
		{
			get => (byte) GetValue(AlphaChannelProperty);
			set => SetValue(AlphaChannelProperty, value);
		}

		public byte Red
		{
			get => (byte) GetValue(RedProperty);
			set => SetValue(RedProperty, value);
		}

		public byte Green
		{
			get => (byte) GetValue(GreenProperty);
			set => SetValue(GreenProperty, value);
		}

		public byte Blue
		{
			get => (byte) GetValue(BlueProperty);
			set => SetValue(BlueProperty, value);
		}

		public Thickness SliderMargin
		{
			get => (Thickness) GetValue(SliderMarginProperty);
			set => SetValue(SliderMarginProperty, value);
		}

		public Thickness RectangleMargin
		{
			get => (Thickness) GetValue(RectangleMarginProperty);
			set => SetValue(RectangleMarginProperty, value);
		}

	#endregion

	#region event handlers

		private static void OnColorARGBChanged(DependencyObject sender,
			DependencyPropertyChangedEventArgs e
			)
		{
			ColorPicker2 ColorPicker2 = (ColorPicker2) sender;
			Color color = ColorPicker2.Color;
			if (e.Property == AlphaChannelProperty)
				color.A = (byte) e.NewValue;
			else if (e.Property == RedProperty)
				color.R = (byte) e.NewValue;
			else if (e.Property == GreenProperty)
				color.G = (byte) e.NewValue;
			else if (e.Property == BlueProperty)
				color.B = (byte) e.NewValue;

			ColorPicker2.Color = color;
		}

		private static void OnColorChanged(DependencyObject sender,
			DependencyPropertyChangedEventArgs e
			)
		{
			Color newColor = (Color) e.NewValue;
			Color oldColor = (Color) e.OldValue;

			ColorPicker2 ColorPicker2 = (ColorPicker2) sender;
			ColorPicker2.Red = newColor.R;
			ColorPicker2.Green = newColor.G;
			ColorPicker2.Blue = newColor.B;

			ColorPicker2.prevColor = (Color) e.OldValue;

			RoutedPropertyChangedEventArgs<Color> args =
				new RoutedPropertyChangedEventArgs<Color>(oldColor, newColor);
			args.RoutedEvent = ColorPicker2.ColorChangedEvent;

			ColorPicker2.RaiseEvent(args);
		}

		public event RoutedPropertyChangedEventHandler<Color> ColorChanged
		{
			add { AddHandler(ColorChangedEvent, value); }
			remove { RemoveHandler(ColorChangedEvent, value); }
		}

	#endregion
	}
}