using System;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.Windows;
using WpfApp1_ListControlTest.SampleData;
//using WpfApp1_ListControlTest.PointData;
//using WpfApp1_ListControlTest.SurfacePts;
using WpfApp1_ListControlTest.ControlPtsWin;
using WpfApp1_ListControlTest.ListBoxWithHdrAndFtr;
using WpfApp1_ListControlTest.TopoPts;

namespace WpfApp1_ListControlTest
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		internal static MultiLineLB.MultiLineListBox Mlb { get; private set; } = new MultiLineLB.MultiLineListBox();

		internal static ListBoxWithXHeaderAndFooter Haf { get; private set; } = new ListBoxWithXHeaderAndFooter();

//		internal static SurfacePoints Sp { get; private set; } = new SurfacePoints();
		internal static ControlPoints Cps { get; set; } = new ControlPoints();
//		internal static ControlPointsVm Cps { get; set; } = new ControlPointsVm();

		internal static ControlPointsResources Cpr { get; set; }

		public static string nl = Environment.NewLine;

		public static bool loaded = false;

		public TopoPoints tps { get; set; } = new TopoPoints();

		public MainWindow()
		{
			InitializeComponent();

			SampleCollection.sx.Add(new SampleDataClass() { SheetNumber = "01a", SheetName = "name 01a", SheetData = "data 01a", SheetInfo = "info 01a", SheetInfo2 = "info2 01a"});
			SampleCollection.sx.Add(new SampleDataClass() { SheetNumber = "02a", SheetName = "name 02a", SheetData = "data 02a", SheetInfo = "info 02a", SheetInfo2 = "info2 02a"});
			SampleCollection.sx.Add(new SampleDataClass() { SheetNumber = "03a", SheetName = "name 03a", SheetData = "data 03a", SheetInfo = "info 03a", SheetInfo2 = "info2 03a"});
			SampleCollection.sx.Add(new SampleDataClass() { SheetNumber = "04a", SheetName = "name 04a", SheetData = "data 04a", SheetInfo = "info 04a", SheetInfo2 = "info2 04a"});
			SampleCollection.sx.Add(new SampleDataClass() { SheetNumber = "05a", SheetName = "name 05a", SheetData = "data 05a", SheetInfo = "info 05a", SheetInfo2 = "info2 05a"});
			SampleCollection.sx.Add(new SampleDataClass() { SheetNumber = "06a", SheetName = "name 06a", SheetData = "data 06a", SheetInfo = "info 06a", SheetInfo2 = "info2 06a"});
			SampleCollection.sx.Add(new SampleDataClass() { SheetNumber = "07a", SheetName = "name 07a", SheetData = "data 07a", SheetInfo = "info 07a", SheetInfo2 = "info2 07a"});
			SampleCollection.sx.Add(new SampleDataClass() { SheetNumber = "08a", SheetName = "name 08a", SheetData = "data 08a", SheetInfo = "info 08a", SheetInfo2 = "info2 08a"});
			SampleCollection.sx.Add(new SampleDataClass() { SheetNumber = "09a", SheetName = "name 09a", SheetData = "data 09a", SheetInfo = "info 09a", SheetInfo2 = "info2 09a"});
			SampleCollection.sx.Add(new SampleDataClass() { SheetNumber = "10a", SheetName = "name 10a", SheetData = "data 10a", SheetInfo = "info 10a", SheetInfo2 = "info2 10a"});
			SampleCollection.sx.Add(new SampleDataClass() { SheetNumber = "11a", SheetName = "name 11a", SheetData = "data 11a", SheetInfo = "info 11a", SheetInfo2 = "info2 11a"});
			SampleCollection.sx.Add(new SampleDataClass() { SheetNumber = "12a", SheetName = "name 12a", SheetData = "data 12a", SheetInfo = "info 12a", SheetInfo2 = "info2 12a"});

			CreateData();

			tps.message = "initialized";
		}

		private void Button_Copy_Click(object sender, RoutedEventArgs e)
		{
			Mlb.ShowDialog();
			Mlb = new MultiLineLB.MultiLineListBox();
		}

		private void Button_Copy1_Click(object sender, RoutedEventArgs e)
		{
			Haf.ShowDialog();
			Haf = new ListBoxWithXHeaderAndFooter();
		}

//		private void Button_Copy2_Click(object sender, RoutedEventArgs e)
//		{
//			Sp.ShowDialog();
//			Sp = new SurfacePoints();
//		}

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

		private double j = 10;
		private double k = 50;
		private double x;
		private double y;
		private double z;

		// add 1
		private void BtnAdd1_Click(object sender, RoutedEventArgs e)
		{
			tps.Add(new XYZ(x + j - 50, y + j - 50, z + j - 50));

			j += 10;
		}

		// insert 1
		private void BtnInsert1_Click(object sender, RoutedEventArgs e)
		{
			tps.Insert(5, new XYZ(10501 + k, 20501 + k, 30501 + k));

			k -= 5;
		}

//		private void BtnChange1_Click(object sender, RoutedEventArgs e)
//		{
////			tps.Insert(3, new XYZ(1500,1500,1500));
//		}

		// change end 1
		private void BtnChangeE_Click(object sender, RoutedEventArgs e)
		{
			tps.SetEndPoint(new XYZ(10901.0, 20901.0, 30901.0));
		}

		// change start 1
		private void BtnChangeS_Click(object sender, RoutedEventArgs e)
		{
			tps.SetStartPoint(new XYZ(9001.0, 19001.0, 29001.0));
		}

		// change value 1
		private void BtnChange4X_Click(object sender, RoutedEventArgs e)
		{
			tps[4].X = 10411.0;
		}

		// change end 2
		private void BtnChange4Y_Click(object sender, RoutedEventArgs e)
		{
			tps[4].Y = 20421.0;
		}

		private void BtnChange4Z_Click(object sender, RoutedEventArgs e)
		{
			tps[4].Z = 30431.0;
		}


		private void CreateData()
		{
			x = 10001.0;
			y = 20001.0;
			z = 30001.0;

			tps.SetStartPoint(new XYZ(x, y, z));

			NextPoint(100); // 1 (10101.0...)
			NextPoint(100); // 2 (10201.0...)
			NextPoint(100); // 3 (10301.0...)
			NextPoint(100); // 4 (10401.0...)
			NextPoint(100); // 5 (10501.0...)
			NextPoint(100); // 6 (10601.0...)

			x += 100;
			y += 100;
			z += 100;


			tps.SetEndPoint(new XYZ(x, y, z));

			tps.Complete();

//			tps[0].Update(0, tps.StartPoint);
//
//			tps.SetStartPoint(new XYZ(1001.0, 2001.0, 3001.0));
//			tps[0].Update(0, tps.StartPoint);
//
//			tps.SetStartPoint(new XYZ(1001.0, 2001.0, 3001.0));
//			tps[0].Update(0, tps.StartPoint);
//
//			tps.SetStartPoint(new XYZ(1001.0, 2001.0, 3001.0));
//			tps[0].Update(0, tps.StartPoint);
//
//			tps.SetStartPoint(new XYZ(1001.0, 2001.0, 3001.0));
//			tps[0].Update(0, tps.StartPoint);
//
//			tps.SetStartPoint(new XYZ(1001.0, 2001.0, 3001.0));
//			tps[0].Update(0, tps.StartPoint);

//			tps.Complete();
		}

		private void NextPoint(double increase)
		{
			x += increase;
			y += increase;
			z += increase;

			tps.AddDefered(new XYZ(x, y, z));
		}
	}
}