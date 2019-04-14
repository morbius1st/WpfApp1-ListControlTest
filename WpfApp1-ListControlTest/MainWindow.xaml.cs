using System;
using System.Windows;
using WpfApp1_ListControlTest.SampleData;
using WpfApp1_ListControlTest.PointData;
using WpfApp1_ListControlTest.SurfacePts;
using WpfApp1_ListControlTest.ControlPtsWin;

namespace WpfApp1_ListControlTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
	{
		internal static MultiLineListBox Mlb { get; private set; } = new MultiLineListBox();
		internal static ListBoxWithXHeaderAndFooter Haf { get; private set; } = new ListBoxWithXHeaderAndFooter();
		internal static SurfacePoints Sp { get; private set; } = new SurfacePoints();
		internal static ControlPoints Cps { get; set; } = new ControlPoints();

		public static string nl = Environment.NewLine;

		public static bool loaded = false;

		public MainWindow()
        {
			
            InitializeComponent();

            SampleCollection.sx.Add(new SampleDataClass() { SheetNumber="01a", SheetName="name 01a", SheetData="data 01a", SheetInfo="info 01a", SheetInfo2="info2 01a"});
            SampleCollection.sx.Add(new SampleDataClass() { SheetNumber="02a", SheetName="name 02a", SheetData="data 02a", SheetInfo="info 02a", SheetInfo2="info2 02a"});
            SampleCollection.sx.Add(new SampleDataClass() { SheetNumber="03a", SheetName="name 03a", SheetData="data 03a", SheetInfo="info 03a", SheetInfo2="info2 03a"});
            SampleCollection.sx.Add(new SampleDataClass() { SheetNumber="04a", SheetName="name 04a", SheetData="data 04a", SheetInfo="info 04a", SheetInfo2="info2 04a"});
            SampleCollection.sx.Add(new SampleDataClass() { SheetNumber="05a", SheetName="name 05a", SheetData="data 05a", SheetInfo="info 05a", SheetInfo2="info2 05a"});
            SampleCollection.sx.Add(new SampleDataClass() { SheetNumber="06a", SheetName="name 06a", SheetData="data 06a", SheetInfo="info 06a", SheetInfo2="info2 06a"});
            SampleCollection.sx.Add(new SampleDataClass() { SheetNumber="07a", SheetName="name 07a", SheetData="data 07a", SheetInfo="info 07a", SheetInfo2="info2 07a"});
            SampleCollection.sx.Add(new SampleDataClass() { SheetNumber="08a", SheetName="name 08a", SheetData="data 08a", SheetInfo="info 08a", SheetInfo2="info2 08a"});
            SampleCollection.sx.Add(new SampleDataClass() { SheetNumber="09a", SheetName="name 09a", SheetData="data 09a", SheetInfo="info 09a", SheetInfo2="info2 09a"});
            SampleCollection.sx.Add(new SampleDataClass() { SheetNumber="10a", SheetName="name 10a", SheetData="data 10a", SheetInfo="info 10a", SheetInfo2="info2 10a"});
            SampleCollection.sx.Add(new SampleDataClass() { SheetNumber="11a", SheetName="name 11a", SheetData="data 11a", SheetInfo="info 11a", SheetInfo2="info2 11a"});
            SampleCollection.sx.Add(new SampleDataClass() { SheetNumber="12a", SheetName="name 12a", SheetData="data 12a", SheetInfo="info 12a", SheetInfo2="info2 12a"});
        }

		private void Button_Copy_Click(object sender, RoutedEventArgs e)
		{
			Mlb.ShowDialog();
			Mlb = new MultiLineListBox();
		}
		
		private void Button_Copy1_Click(object sender, RoutedEventArgs e)
		{
			Haf.ShowDialog();
			Haf = new ListBoxWithXHeaderAndFooter();
		}
		
		private void Button_Copy2_Click(object sender, RoutedEventArgs e)
		{
			Sp.ShowDialog();
			Sp = new SurfacePoints();
		}
				
		private void Button_Copy3_Click(object sender, RoutedEventArgs e)
		{
			Cps.ShowDialog();
			Cps = new ControlPoints();
		}

		private void BtnDone_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			loaded = true;
		}
	}
}
