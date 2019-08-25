using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1_ListControlTest.SampleData;

namespace WpfApp1_ListControlTest.ListBoxWithHdrAndFtr
{
	/// <summary>
	/// Interaction logic for ListBoxWithXHeaderAndFooter.xaml
	/// </summary>
	public partial class ListBoxWithXHeaderAndFooter : Window, INotifyPropertyChanged
	{
private string nl = Environment.NewLine;

		public string MessageText { get; set; } = "Message Window\n";

		public ObservableCollection<SampleDataClass> sxC { get; set; }
		public ObservableCollection<HeaderData> Hdx { get; set; }

		public ObservableCollection<ItemsData> Idx { get; set; }

//		private string savedValue;

        public ListBoxWithXHeaderAndFooter()
        {
            InitializeComponent();

			AddValues();
		}

		private void AddValues()
		{
			Hdx = new ObservableCollection<HeaderData>();
			Hdx.Add(new HeaderData(){ Name="Column 1x", DataType="Dt1", UnitType = "Ut1"});
			Hdx.Add(new HeaderData(){ Name="Column 2x", DataType="Dt2", UnitType = "Ut2"});
			Hdx.Add(new HeaderData(){ Name="Column 3x", DataType="Dt3", UnitType = "Ut3"});

			Idx = new ObservableCollection<ItemsData>();

			ItemsData id = new ItemsData();
			id.Items = new ItemData[4] {new ItemData("value 1.1", "ovalue 1.1"), new ItemData("value 1.2", "ovalue 1.2"), new ItemData("value 1.3", "ovalue 1.3"), new ItemData("value 1.4", "ovalue 1.4" )};
			id.Items = new ItemData[4] {new ItemData("value 2.1", "ovalue 2.1"), new ItemData("value 2.2", "ovalue 2.2"), new ItemData("value 2.3", "ovalue 2.3"), new ItemData("value 2.4", "ovalue 2.4" )};
			id.Items = new ItemData[4] {new ItemData("value 3.1", "ovalue 3.1"), new ItemData("value 3.2", "ovalue 3.2"), new ItemData("value 3.3", "ovalue 3.3"), new ItemData("value 3.4", "ovalue 3.4" )};


			sxC = new ObservableCollection<SampleDataClass>();

			sxC.Add(new SampleDataClass() { SheetNumber = "A-01b", SheetName = "name 01b", SheetData = "data 01b", SheetInfo = "info 01b", SheetInfo2 = "info2 01b"});
			sxC.Add(new SampleDataClass() { SheetNumber = "A-02b", SheetName = "name 02b", SheetData = "data 02b", SheetInfo = "info 02b", SheetInfo2 = "info2 02b"});
			sxC.Add(new SampleDataClass() { SheetNumber = "A-03b", SheetName = "name 03b", SheetData = "data 03b", SheetInfo = "info 03b", SheetInfo2 = "info2 03b"});
			sxC.Add(new SampleDataClass() { SheetNumber = "A-04b", SheetName = "name 04b", SheetData = "data 04b", SheetInfo = "info 04b", SheetInfo2 = "info2 04b"});



			SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-01b", SheetName="name 01b", SheetData="data 01b", SheetInfo="info 01b", SheetInfo2="info2 01b"});
            SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-02b", SheetName="name 02b", SheetData="data 02b", SheetInfo="info 02b", SheetInfo2="info2 02b"});
            SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-03b", SheetName="name 03b", SheetData="data 03b", SheetInfo="info 03b", SheetInfo2="info2 03b"});
            SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-04b", SheetName="name 04b", SheetData="data 04b", SheetInfo="info 04b", SheetInfo2="info2 04b"});
            SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-05b", SheetName="name 05b", SheetData="data 05b", SheetInfo="info 05b", SheetInfo2="info2 05b"});
            SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-06b", SheetName="name 06b", SheetData="data 06b", SheetInfo="info 06b", SheetInfo2="info2 06b"});
            SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-07b", SheetName="name 07b", SheetData="data 07b", SheetInfo="info 07b", SheetInfo2="info2 07b"});
            SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-08b", SheetName="name 08b", SheetData="data 08b", SheetInfo="info 08b", SheetInfo2="info2 08b"});
            SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-09b", SheetName="name 09b", SheetData="data 09b", SheetInfo="info 09b", SheetInfo2="info2 09b"});
            SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-10b", SheetName="name 10b", SheetData="data 10b", SheetInfo="info 10b", SheetInfo2="info2 10b"});
            SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-11b", SheetName="name 11b", SheetData="data 11b", SheetInfo="info 11b", SheetInfo2="info2 11b"});
            SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-12b", SheetName="name 12b", SheetData="data 12b", SheetInfo="info 12b", SheetInfo2="info2 12b"});



		}



		internal void AppendMessage(string message)
		{
			MessageText += message + nl;

			OnPropertyChange("MessageText");
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}


		private void TbxFirst_OnError(object sender, ValidationErrorEventArgs e)
		{
			if (e.Action == ValidationErrorEventAction.Added)
			{
				AppendMessage("tbxFirst error| " + ((TextBox) sender).Text + " :: Added :: " + e.Error.ErrorContent);
			}
			else
			{
				AppendMessage("tbxFirst error| " + ((TextBox) sender).Text + " :: Removed");
			}

		}

		private childItem FindVisualChild<childItem>(DependencyObject obj)
			where childItem : DependencyObject
		{
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(obj, i);
				if (child != null && child is childItem)
					return (childItem) child;
				else
				{
					childItem childOfChild = FindVisualChild<childItem>(child);
					if (childOfChild != null)
						return childOfChild;
				}
			}

			return null;
		}


		private void BtnDebug_Click(object sender, RoutedEventArgs e)
		{
			AddColumn();

			AppendMessage("BtnDebug_Click");
		}

		private void AddColumn()
		{
			TextBox tbx;

			Grid g = (Grid)	FindVisualChild<Grid>(ListBoxX);

			g.ColumnDefinitions[1].Width =  new GridLength(100);


			ColumnDefinition c = new ColumnDefinition();
			c.Width =new GridLength(1, GridUnitType.Star);

			g.ColumnDefinitions.Add(c);

			tbx = new TextBox();
			tbx.Text = "Column 3";
			tbx.HorizontalAlignment = HorizontalAlignment.Stretch;

			Grid.SetColumn(tbx, 2);
			Grid.SetRow(tbx, 0);

			g.Children.Add(tbx);
//			g.InvalidateVisual();
//			g.InvalidateArrange();
//			g.UpdateLayout();
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
		public ItemData[] Items { get; set; }
	}

	public class ItemData
	{
		public string Value { get; set; }
		public string OrigValue { get; set; }

		public ItemData(string V, string O)
		{
			Value = V;
			OrigValue = O;
		}
	}
}
