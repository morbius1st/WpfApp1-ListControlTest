using System;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Xaml;


namespace WpfData
{
	public partial class MainWindow : Window
	{
		public ObservableCollection<SampleDataClass> Sxc { get; set; }

		public ObservableCollection<HeaderData> Hdx { get; set; } = new ObservableCollection<HeaderData>();

		public ObservableCollection<ItemsData> Idx { get; set; }


		public MainWindow()
		{
			InitializeComponent();

			LoadData();
		}

		private void LoadData()
		{
			Hdx.Add(new HeaderData() { Name = "Column 1x", DataType = "Dt1", UnitType = "Ut1" });
			Hdx.Add(new HeaderData() { Name = "Column 2x", DataType = "Dt2", UnitType = "Ut2" });
			Hdx.Add(new HeaderData() { Name = "Column 3x", DataType = "Dt3", UnitType = "Ut3" });

			Idx = new ObservableCollection<ItemsData>();

			ItemsData id = new ItemsData();
			id.ItemsD = new ObservableCollection<ItemData>() { new ItemData("value 1.1", "ovalue 1.1"), new ItemData("value 1.2", "ovalue 1.2") };
			Idx.Add(id);

			id = new ItemsData();
			id.ItemsD = new ObservableCollection<ItemData>() { new ItemData("value 2.1", "ovalue 2.1"), new ItemData("value 2.2", "ovalue 2.2") };
			Idx.Add(id);

			id = new ItemsData();
			id.ItemsD = new ObservableCollection<ItemData>() { new ItemData("value 3.1", "ovalue 3.1"), new ItemData("value 3.2", "ovalue 3.2") };
			Idx.Add(id);

			listBox.ItemsSource = Idx;


			Sxc = new ObservableCollection<SampleDataClass>();

			Sxc.Add(new SampleDataClass() { SheetNumber = "A-01b", SheetName = "name 01b", SheetData = "data 01b", SheetInfo = "info 01b", SheetInfo2 = "info2 01b" });
			Sxc.Add(new SampleDataClass() { SheetNumber = "A-02b", SheetName = "name 02b", SheetData = "data 02b", SheetInfo = "info 02b", SheetInfo2 = "info2 02b" });
			Sxc.Add(new SampleDataClass() { SheetNumber = "A-03b", SheetName = "name 03b", SheetData = "data 03b", SheetInfo = "info 03b", SheetInfo2 = "info2 03b" });

			// note that when a "new" collection is created, must re-bind the source
			//			listBox.ItemsSource = Sxc;
		}

		private void Button_Test1(object sender, RoutedEventArgs e)
		{
			Debug.WriteLine("At button Test 1");

			Idx[1].ItemsD[0].OrigValue = "new value 0";
			Idx[2].ItemsD[3].Value = "new value 10";
		}
		
		private void Button_Test2(object sender, RoutedEventArgs e)
		{
			Debug.WriteLine("At button Test 2");

			Idx[1].ItemsD[0].Value = "new value 0";
			Idx[2].ItemsD[1].Value = "new value 2";
			Idx[0].ItemsD[1].Value = "new value 3";
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Debug.WriteLine("At button Click");

			AddToCollection();
		}

		private void AddToCollection()
		{
			AddHeaders();
			AddFields();
		}

		private void AddFields()
		{
			AddField();
			AddField();
		}

		private VirtualizingStackPanel GetVirtualizingStackPanel(DependencyObject dObj)
		{
			return FindVisualChild<VirtualizingStackPanel>(dObj);
		}

		// just adds the fields / data
		private bool AddField()
		{
			// add to the end
			int col = Idx[0].ItemsD.Count;

			string val = "value ";
			string oval = "ovalue ";
			double amt;

			ItemData iData;
			ItemsData id;

			// add data to every row
			for (var row = 0; row < Idx.Count; row++)
			{
				id = Idx[row];

				amt = (row + 1) + ((double) col) / 10;

				string v = val + amt.ToString("F1");
				string o = oval + amt.ToString("F1");

				iData = new ItemData(v, o);

				// add the new data item
				id.ItemsD.Add(iData);
			}

			AddAndBindControls(100, col);

			return true;
		}

		private void AddAndBindControls(int width, int col)
		{
			int row = 0;

			VirtualizingStackPanel vp = GetVirtualizingStackPanel(listBox);

			foreach (object vpChild in vp.Children)
			{
				if (!(vpChild is ListBoxItem)) continue;

				Grid g = FindNamedVisualChild<Grid>((ListBoxItem) vpChild, "data");

				AddAndBindControl(g, width, row++, col);
			}
		}

		private void AddAndBindControl(Grid g, int width, int row, int col)
		{
			TextBox tbx;

			g.ColumnDefinitions[col - 1].Width = new GridLength(width);

			ColumnDefinition c = new ColumnDefinition();
			c.Width = new GridLength(1, GridUnitType.Star);

			g.ColumnDefinitions.Add(c);

			tbx = new TextBox();
			tbx.HorizontalAlignment = HorizontalAlignment.Stretch;

			string basePath = "ItemsD[" + col.ToString("D") + "]";

			Binding b = new Binding(basePath + ".Value");
			b.Mode = BindingMode.TwoWay;
			b.Source = Idx[row];

			tbx.SetBinding(TextBox.TextProperty, b);
			tbx.HorizontalAlignment = HorizontalAlignment.Stretch;

			Style s = (Style) mainWindow.FindResource("tbx1");

			tbx.Style = s;

			tbx.SetBinding(CustomProperties.GenericBoolOneProperty, basePath + ".Revised");
			

			


			Grid.SetColumn(tbx, col);

			g.Children.Add(tbx);
		}

		private void AddHeaders()
		{
			Grid g = (Grid) FindNamedVisualChild<Grid>(listBox, "header");

			if (g == null) return;

			AddHeader(g, 100, "Column 3x");
			AddHeader(g, 100, "Column 4x");
		}

		private void AddHeader(Grid g, int width, string title)
		{
			TextBlock tbk;

			int n = g.ColumnDefinitions.Count;

			g.ColumnDefinitions[n - 1].Width = new GridLength(width);

			ColumnDefinition c = new ColumnDefinition();
			c.Width = new GridLength(1, GridUnitType.Star);

			g.ColumnDefinitions.Add(c);

			tbk = new TextBlock();

			tbk.Text = title;
			tbk.HorizontalAlignment = HorizontalAlignment.Stretch;

			Grid.SetColumn(tbk, n);

			g.Children.Add(tbk);
		}


		private childItem FindNamedVisualChild<childItem>(DependencyObject obj, string name)
			where childItem : FrameworkElement
		{
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(obj, i);
				if (child != null && child is childItem && ((FrameworkElement) child).Name.Equals(name))
				{
					return (childItem) child;
				}
				else
				{
					childItem childOfChild = FindNamedVisualChild<childItem>(child, name);
					if (childOfChild != null)
						return childOfChild;
				}
			}

			return null;
		}

		private childItem FindVisualChild<childItem>(DependencyObject obj)
			where childItem : DependencyObject
		{
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(obj, i);
				if (child != null && child is childItem)
				{
					return (childItem) child;
				}
				else
				{
					childItem childOfChild = FindVisualChild<childItem>(child);
					if (childOfChild != null)
						return childOfChild;
				}
			}

			return null;
		}
	}

	public class HeaderData
	{
		public string Name { get; set; }
		public string DataType { get; set; }
		public string UnitType { get; set; }
	}

	public class ItemsData
	{
		public ObservableCollection<ItemData> ItemsD { get; set; }
	}

	public class ItemData : INotifyPropertyChanged
	{
		private string value;
		private string origValue;
		private bool revised;

		public string Value
		{
			get => value;
			set
			{
				if (!value.Equals(this.value))
				{
					ChkRevised();

					this.value = value;
					OnPropertyChange();
				}
			}
		}

		public string OrigValue
		{
			get => origValue;
			set
			{
				if (!value.Equals(origValue))
				{
					ChkRevised();

					origValue = value;
					OnPropertyChange();
				}
			}
		}

		private void ChkRevised()
		{
			if (origValue == null || value == null) return;

			Revised = !(origValue.Equals(value));
		}

		public bool Revised
		{
			get => revised;
			set
			{
				if (!(value == revised))
				{
					revised = value;
					OnPropertyChange();
				}
			}
		}

		public ItemData(string V, string O)
		{
			Value = V;
			OrigValue = O;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}
	}

	public class SampleDataClass
	{
		public string SheetNumber { get; set; }
		public string SheetName { get; set; }
		public string SheetData { get; set; }
		public string SheetInfo { get; set; }
		public string SheetInfo2 { get; set; }
	}

	public class CustomProperties
	{
		public static readonly DependencyProperty GenericBoolOneProperty = DependencyProperty.RegisterAttached(
			"GenericBooleanOne", typeof(bool), typeof(CustomProperties),
			new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsParentArrange |
				FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsArrange |
				FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

//		public static readonly DependencyProperty  GenericBoolTwoProperty = DependencyProperty.RegisterAttached(
//			"GenericBooleanTwo", typeof(bool), typeof(CustomProperties),
//			new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsParentArrange |
//				FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsArrange |
//				FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
//
//		public static readonly DependencyProperty  GenericDoubleOneProperty = DependencyProperty.RegisterAttached(
//			"GenericDoubleOne", typeof(double), typeof(CustomProperties),
//			new FrameworkPropertyMetadata(0.0));

	#region GenericBooleanOne

		public static void SetGenericBooleanOne(UIElement e, bool value)
		{
			e.SetValue(GenericBoolOneProperty, value);
		}

		public static bool GetGenericBooleanOne(UIElement e)
		{
			return (bool) e.GetValue(GenericBoolOneProperty);
		}

	#endregion
//
//	#region GenericBooleanTwo
//
//		public static void SetGenericBooleanTwo(UIElement e, bool value)
//		{
//			e.SetValue(GenericBoolTwoProperty, value);
//		}
//
//		public static bool GetGenericBooleanTwo(UIElement e)
//		{
//			return (bool) e.GetValue(GenericBoolTwoProperty);
//		}
//
//	#endregion
//
//	#region GenericDoubleOne
//
//		public static void SetGenericDoubleOne(UIElement e, double value)
//		{
//			e.SetValue(GenericDoubleOneProperty, value);
//		}
//
//		public static double GetGenericDoubleOne(UIElement e)
//		{
//			return (double) e.GetValue(GenericDoubleOneProperty);
//		}
//
//	#endregion
	}
}