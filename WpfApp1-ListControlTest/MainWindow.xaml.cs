using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using WpfApp1_ListControlTest.SampleData;
using WpfApp1_ListControlTest.ControlPtsWin;
using WpfApp1_ListControlTest.ListBoxWithHdrAndFtr;
using WpfApp1_ListControlTest.MultiLineLB;
using WpfApp1_ListControlTest.TopoPts;
using WpfApp1_ListControlTest.TopoPtsData;
using WpfApp1_ListControlTest.TopoPtsData2;
using WpfApp1_ListControlTest.TopoPts.Support;
//using WpfApp1_ListControlTest.TopoPtsData2.Support;
using WpfApp1_ListControlTest.TopoPtsData3;
using WpfApp1_ListControlTest.TopoPtsData3.TopoPts3;
using TopoPtsResources = WpfApp1_ListControlTest.TopoPts.Support.TopoPtsResources;


/* Todo
 * this relates to ListBox3 + TopoPoint(s)2
 * basic system is worked out.  
 * Investigations:
 * make this a user control?  does this make sense?
 *
 * need:
 * 1. "apply" for the whole listbox (eliminates any undo's / redo's)
 * 2. "undo" for the whole listbox (revert all undo's - one level undo)
 * 3. all each line to have a check box to allow user to select lines for
 *		deletion
 * 4. confirm that delete line works, add line works
 * 5. add tables below startpoint / add undo / redo buttons
 * 
 * next steps:
 * 1. depending on user control question, make this transferable
 * 2. add commands or events to disassociate this from manager
 * 3. make this self-contained
 * 4. make the width more variable - use min / max width rather than a set width
 *
 */


namespace WpfApp1_ListControlTest
{
	public partial class MainWindow : Window
	{
		public TopoPoints tps { get; set; }  = new TopoPoints();
		public TopoPointsTest TpTest { get; set; }

		public TopoPtsConsts TpConsts = new TopoPtsConsts();

		public TopoPts3Mgr TopoMgr { get; set; }

		public TopoPointsTest3 TopoPointTest { get; set; }

		internal static MultiLineLB.MultiLineListBox Mlb { get; private set; } = new MultiLineLB.MultiLineListBox();

		internal static ListBoxWithXHeaderAndFooter Haf { get; private set; } = new ListBoxWithXHeaderAndFooter();

		internal static ControlPoints Cps { get; set; } = new ControlPoints();
		internal static ControlPointsResources Cpr { get; set; }

		internal static TopoPtsWin Tpw { get; set; } = new TopoPtsWin();
		internal static TopoPtsResources Tpr { get; set; }

		public static string nl = Environment.NewLine;

		public static bool loaded = false;

		public static ListBox Lb3 { get; set; }

		public MainWindow()
		{
			TpTest = new TopoPointsTest(tps);

			TopoMgr = new TopoPts3Mgr();
			TopoPointTest = new TopoPointsTest3(TopoMgr.TopoPts, TopoMgr);
			InitializeComponent();

			CreateSampleData();
		}

		private void CreateSampleData()
		{
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
		}


		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			loaded = true;
		}

		int PriorRowBeingEdited = 0;

		private void ListBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListBox lb3 = sender as ListBox;

			if (lb3.SelectedIndex < 0) return;

			TopoPoint3 tpt = (TopoPoint3) lb3.Items[lb3.SelectedIndex];

			Debug.WriteLine("@ MainResource3| @selectionChanged|"
				+ " selected count| " + e.AddedItems.Count
				+ " selected idx| " + lb3.SelectedIndex
				);

			((TopoPoint3) lb3.Items[PriorRowBeingEdited]).IsBeingEdited = false;
			tpt.IsBeingEdited = true;
			PriorRowBeingEdited = lb3.SelectedIndex;
		}

		private void Lb3BtnUndo_Click(object sender, RoutedEventArgs e)
		{
			ClickInfo(sender, e, "UndoX");

			TopoMgr.Undo(((Button) sender)?.Tag as string, Lb3.SelectedIndex);
		}

		private void Lb3BtnRedo_Click(object sender, RoutedEventArgs e)
		{
			ClickInfo(sender, e, "UndoX");

			TopoMgr.Redo(((Button) sender)?.Tag as string, Lb3.SelectedIndex);
		}

		private void Lb3BtnUndoEndPt_Click(object sender, RoutedEventArgs e)
		{
			ClickInfo(sender, e, "UndoX");

			TopoMgr.Undo(((Button) sender)?.Tag as string);
		}

		private void Lb3BtnRedoEndPt_Click(object sender, RoutedEventArgs e)
		{
			ClickInfo(sender, e, "UndoX");

			TopoMgr.Redo(((Button) sender)?.Tag as string);
		}

		private void ClickInfo(object sender, RoutedEventArgs e, string fromWho)
		{
//			((TextBox) sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();

			ListBox lb = MainWindow.Lb3;

			TopoPoint3 tp2 = (TopoPoint3) MainWindow.Lb3.Items[lb.SelectedIndex];

			Debug.WriteLine("\n@ MainResource3| @button pressed|"
				+ " from| " + fromWho
				+ " selected idx| " + lb.SelectedIndex
				+ " xyz| " + tp2.ToString()
				+ "\n"
				);
		}

		private void ListBox3_Loaded(object sender, RoutedEventArgs e)
		{
			MainWindow.Lb3 = (ListBox) sender;
		}

	#region > control buttons

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

		private void Button_Copy3_Click(object sender, RoutedEventArgs e)
		{
			Cps.ShowDialog();
			Cps = new ControlPoints();
		}

		private void Button_Copy4_Click(object sender, RoutedEventArgs e)
		{
			Tpw.ShowDialog();
			Tpw = new TopoPtsWin();
		}

		private void BtnDone_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

	#endregion

	#region > listbox 2 buttons

		// add 1
		private void BtnAdd1_Click(object sender, RoutedEventArgs e)
		{
			TpTest.BtnAdd1_Click();
		}

		// insert 1
		private void BtnInsert1_Click(object sender, RoutedEventArgs e)
		{
			TpTest.BtnInsert1_Click();
		}

		// change end 1
		private void BtnChangeE_Click(object sender, RoutedEventArgs e)
		{
			TpTest.BtnChangeE_Click();
		}

		// change start 1
		private void BtnChangeS_Click(object sender, RoutedEventArgs e)
		{
			TpTest.BtnChangeS_Click();
		}

		// change value 1
		private void BtnChange4X_Click(object sender, RoutedEventArgs e)
		{
			TpTest.BtnChange4X_Click();
		}

		// change end 2
		private void BtnChange4Y_Click(object sender, RoutedEventArgs e)
		{
			TpTest.BtnChange4Y_Click();
		}

		private void BtnChange4Z_Click(object sender, RoutedEventArgs e)
		{
			TpTest.BtnChange4Z_Click();
		}

		private void BtnChangeStartX_Click(object sender, RoutedEventArgs e)
		{
			TpTest.BtnChangeStartX_Click();
		}

		private void BtnChangeEndX_Click(object sender, RoutedEventArgs e)
		{
			TpTest.BtnChangeEndX_Click();
		}

		private void BtnTestAll_Click(object sender, RoutedEventArgs e)
		{
			TpTest.BtnTestAll_Click();
		}

		private void BtnContains_Click(object sender, RoutedEventArgs e)
		{
			TpTest.BtnContains_Click();
		}

		private void BtnIndexOf_Click(object sender, RoutedEventArgs e)
		{
			TpTest.BtnIndexOf_Click();
		}

		private void BtnRemoveAt_Click(object sender, RoutedEventArgs e)
		{
			TpTest.BtnRemoveAt_Click();
		}

		private void BtnRemove_Click(object sender, RoutedEventArgs e)
		{
			TpTest.BtnRemove_Click();
		}

		private void BtnClear_Click(object sender, RoutedEventArgs e)
		{
			TpTest.BtnClear_Click();
		}

		private void BtnCopyTo_Click(object sender, RoutedEventArgs e)
		{
			TpTest.BtnCopyTo_Click();
		}

		private void BtnChange4XYZ_Click(object sender, RoutedEventArgs e)
		{
			TpTest.BtnChange4XYZ_Click();
		}

		private void BtnBatch_Click(object sender, RoutedEventArgs e)
		{
			TpTest.BtnBatch_Click();
		}

		private void BtnBatchv2_Click(object sender, RoutedEventArgs e)
		{
			TpTest.BtnBatchv2_Click();
		}

	#endregion

	#region > listbox3 buttons

		private void BtnInitialize_Click(object sender, RoutedEventArgs e)
		{
			TopoMgr.LoadData();
		}

		private void BtnAdd10ToZofStart_Click(object sender, RoutedEventArgs e)
		{
			TopoPointTest.BtnAdd10ToZofStart_Click();
		}

		private void BtnBatchAdd10ToYfrom3_Click(object sender, RoutedEventArgs e)
		{
			TopoPointTest.BtnBatchAdd10ToYfrom3_Click();
		}

		private void BtnBatchAddAmtToYfrom3On_Click(object sender, RoutedEventArgs e)
		{
			TopoPointTest.BtnBatchAddAmtToYfrom3On_Click();
		}

		private void BtnBatchAdjustZByAmount_Click(object sender, RoutedEventArgs e)
		{
			TopoPointTest.BtnBatchAdjustZByAmount_Click();
		}

		private void BtnBatchAdjustZBySlope_Click(object sender, RoutedEventArgs e)
		{
			TopoPointTest.BtnBatchAdjustZBySlope_Click();
		}

		private void BtnChangeXof1_Click(object sender, RoutedEventArgs e)
		{
			TopoPointTest.BtnChangeXof1_Click();
		}

		private void BtnDebugMarker_Click(object sender, RoutedEventArgs e)
		{
			TopoPointTest.BtnDebugMarker_Click();
		}

	#endregion
	}
}