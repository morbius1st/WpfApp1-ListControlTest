﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
	/// Interaction logic for ColorPicker.xaml
	/// </summary>
	public class ColorPicker : System.Windows.Controls.Control
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

		static ColorPicker()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPicker), 
				new FrameworkPropertyMetadata(typeof(ColorPicker)));


			SliderMarginProperty = DependencyProperty.Register(
				"SliderMargin", typeof(Thickness), typeof(ColorPicker),
				new FrameworkPropertyMetadata(new Thickness(0, 0, 0, 0)) );
			RectangleMarginProperty = DependencyProperty.Register(
				"RectangleMargin", typeof(Thickness), typeof(ColorPicker),
				new FrameworkPropertyMetadata(new Thickness(0, 0, 0, 0)) );
			ColorProperty = DependencyProperty.Register(
				"Color", typeof(Color), typeof(ColorPicker),
				new FrameworkPropertyMetadata(Colors.Black,
					new PropertyChangedCallback(OnColorChanged)));
			AlphaChannelProperty = DependencyProperty.Register(
				"Alpha", typeof(byte), typeof(ColorPicker),
				new FrameworkPropertyMetadata((byte) 255,
					new PropertyChangedCallback(OnColorARGBChanged)));
			RedProperty = DependencyProperty.Register(
				"Red", typeof(byte), typeof(ColorPicker),
				new FrameworkPropertyMetadata(
					new PropertyChangedCallback(OnColorARGBChanged)));
			GreenProperty = DependencyProperty.Register(
				"Green", typeof(byte), typeof(ColorPicker),
				new FrameworkPropertyMetadata(
					new PropertyChangedCallback(OnColorARGBChanged)));
			BlueProperty = DependencyProperty.Register(
				"Blue", typeof(byte), typeof(ColorPicker),
				new FrameworkPropertyMetadata(
					new PropertyChangedCallback(OnColorARGBChanged)));

			ColorChangedEvent = EventManager.RegisterRoutedEvent(
				"ColorChanged", RoutingStrategy.Bubble,
				typeof(RoutedPropertyChangedEventHandler<Color>), typeof(ColorPicker));

			CommandManager.RegisterClassCommandBinding(typeof(ColorPicker),
				new CommandBinding(ApplicationCommands.Undo,
					UndoCommand_Executed, UndoCommand_CanExecute));
		}

		public ColorPicker()
		{
			
		}

	#endregion

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			SetBinding("PART_AlphaSlider", "Alpha");
			SetBinding("PART_RedSlider", "Red");
			SetBinding("PART_GreenSlider", "Green");
			SetBinding("PART_BlueSlider", "Blue");

			SolidColorBrush brush = GetTemplateChild("PART_Swatch") as SolidColorBrush;

			if (brush != null)
			{
				// bind to the red property in the control using a two-way binding
				Binding binding = new Binding("Color");
				binding.Source = brush;
				binding.Mode = BindingMode.OneWayToSource;

				this.SetBinding(ColorPicker.ColorProperty, binding);
			}
		}

		private static void UndoCommand_CanExecute(object sender,
			CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = ((ColorPicker) sender).prevColor.HasValue;
		}

		private static void UndoCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			ColorPicker colorPicker = ((ColorPicker) sender);

			Color currentColor = colorPicker.Color;

			colorPicker.Color = (Color) colorPicker.prevColor;
		}

		private void SetBinding(string childPartName, string propertyName)
		{
			RangeBase slider = GetTemplateChild(childPartName) as RangeBase;

			if (slider != null)
			{
				// bind to the red property in the control using a two-way binding
				Binding binding = new Binding(propertyName);
				binding.Source = this;
				binding.Mode = BindingMode.TwoWay;

				slider.SetBinding(RangeBase.ValueProperty, binding);
			}
		}



	#region properties

		public Color Color
		{
			get { return (Color) GetValue(ColorProperty); }
			set { SetValue(ColorProperty, value); }
		}


		public byte Alpha
		{
			get { return (byte) GetValue(AlphaChannelProperty); }
			set { SetValue(AlphaChannelProperty, value); }
		}
		
		public byte Red
		{
			get { return (byte) GetValue(RedProperty); }
			set { SetValue(RedProperty, value); }
		}

		public byte Green
		{
			get { return (byte) GetValue(GreenProperty); }
			set { SetValue(GreenProperty, value); }
		}

		public byte Blue
		{
			get { return (byte) GetValue(BlueProperty); }
			set { SetValue(BlueProperty, value); }
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
			ColorPicker colorPicker = (ColorPicker) sender;
			Color color = colorPicker.Color;
			if (e.Property == AlphaChannelProperty)
				color.A = (byte) e.NewValue;
			else if (e.Property == RedProperty)
				color.R = (byte) e.NewValue;
			else if (e.Property == GreenProperty)
				color.G = (byte) e.NewValue;
			else if (e.Property == BlueProperty)
				color.B = (byte) e.NewValue;
			colorPicker.Color = color;
		}

		private static void OnColorChanged(DependencyObject sender,
			DependencyPropertyChangedEventArgs e
			)
		{
			Color newColor = (Color) e.NewValue;
			Color oldColor = (Color) e.OldValue;

			ColorPicker colorPicker = (ColorPicker) sender;
			colorPicker.Red = newColor.R;
			colorPicker.Green = newColor.G;
			colorPicker.Blue = newColor.B;

			colorPicker.prevColor = (Color) e.OldValue;

			RoutedPropertyChangedEventArgs<Color> args =
				new RoutedPropertyChangedEventArgs<Color>(oldColor, newColor);
			args.RoutedEvent = ColorPicker.ColorChangedEvent;

			colorPicker.RaiseEvent(args);
		}

	#endregion

		public event RoutedPropertyChangedEventHandler<Color> ColorChanged
		{
			add { AddHandler(ColorChangedEvent, value); }
			remove { RemoveHandler(ColorChangedEvent, value); }
		}
	}
}