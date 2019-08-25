using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using WpfApp1_ListControlTest.SampleData;
using WpfApp1_ListControlTest.ControlPtsWin;
using WpfApp1_ListControlTest.ListBoxWithHdrAndFtr;
using WpfApp1_ListControlTest.MainWinSupport;
using WpfApp1_ListControlTest.MultiLineLB;
using WpfApp1_ListControlTest.TopoPts;
using WpfApp1_ListControlTest.TopoPtsData;
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
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		public double GlyphActHeight { get; set; }

		public TopoPoints tps { get; set; }  = new TopoPoints();
		public TopoPointsTest TpTest { get; set; }

		public TopoPts.Support.TopoPtsConsts TpConsts = new TopoPts.Support.TopoPtsConsts();

		public TopoPts3Mgr Tpts3Mgr { get; set; }

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

		public static MainWindow Me { get; private set; }

		private string message;

		public MainWindow()
		{
			TpTest = new TopoPointsTest(tps);

			Tpts3Mgr = new TopoPts3Mgr();
			TopoPointTest = new TopoPointsTest3(Tpts3Mgr.Tpts3, Tpts3Mgr);

			InitializeComponent();

			CreateSampleData();

			ListBox3.SelectedIndex = 1;

//			TopoPointTest.ListTopoPtsTags();


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

			Me = this;

			Me.Append = "Main Win Loaded\n";

//			Glyphs G = CheckBox1.Template.FindName("Glyphs1", CheckBox1) as Glyphs;

		}

		private void ListViewTest()
		{
			
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
			ClickInfo(sender, e, "Undo");

			Tpts3Mgr.Undo(((Button) sender)?.Tag, Lb3.SelectedIndex);
		}

		private void Lb3BtnRedo_Click(object sender, RoutedEventArgs e)
		{
			ClickInfo(sender, e, "Redo");

			Tpts3Mgr.Redo(((Button) sender)?.Tag, Lb3.SelectedIndex);
		}

		private void ClickInfo(object sender, RoutedEventArgs e, string fromWho)
		{
			ListBox lb = MainWindow.Lb3;

			TopoPoint3 tp = (TopoPoint3) MainWindow.Lb3.Items[lb.SelectedIndex];

			Debug.WriteLine("\n@ MainResource3| @button pressed|"
				+ " from| " + fromWho
				+ " selected idx| " + lb.SelectedIndex
				+ " xyz| " + tp.ToString()
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
			Tpts3Mgr.LoadData();

			ListBox3.SelectedIndex = -1;
		}

		private void BtnDebugMarker_Click(object sender, RoutedEventArgs e)
		{
			TopoPointTest.BtnDebugMarker_Click();
		}

		private void BtnAdd10ToXofStart_Click(object sender, RoutedEventArgs e)
		{
			TopoPointTest.BtnAdd10ToXofStart_Click();
		}
		
		private void BtnAdd10ToXofEnd_Click(object sender, RoutedEventArgs e)
		{
			TopoPointTest.BtnAdd10ToXofEnd_Click();
		}
		
		private void BtnAdd10ToZofStart_Click(object sender, RoutedEventArgs e)
		{
			TopoPointTest.BtnAdd10ToZofStart_Click();
		}
		
		private void BtnAdd10ToZofEnd_Click(object sender, RoutedEventArgs e)
		{
			TopoPointTest.BtnAdd10ToZofEnd_Click();
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

		private void BtnListMultiSelect_Click(object sender, RoutedEventArgs e)
		{
			TopoPointTest.BtnListMultiSelect_Click(ListBox3);
		}
		
		private void BtnSelectAndListMultiSelect_Click(object sender, RoutedEventArgs e)
		{
			TopoPointTest.BtnSelectAndListMultiSelect_Click(ListBox3);
		}


	#endregion

		public string Message
		{
			get => message;
			set
			{
				message = value;
				OnPropertyChange();
			}
		}

		public string Append
		{
			set
			{
				Message += value;
				Debug.Write(value);
			}
		}

		public void ClearMessage()
		{
			Message = "";
		}


		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

		private int SomeSelected = 0;
		private int NextSelect = 0;
		private bool[] CurrentSelected;

		private void CheckBox1_Click(object sender, RoutedEventArgs e)
		{
			Debug.WriteLine("master row selector clicked|");
			// three options
			// nothing selected - select everything
			// all selected - select none
			// some selected - select all then none then some

			if (ListBox3.SelectedItems.Count > 0 &&
				ListBox3.SelectedItems.Count < ListBox3.Items.Count
				&& SomeSelected == 0
				)
			{
				Debug.WriteLine("master row selector| saving someselected|");
				// some, but not all, selected
				// save which are selected
				CurrentSelected = new bool[ListBox3.Items.Count];

				foreach (TopoPoint3 item in ListBox3.SelectedItems)
				{
					CurrentSelected[item.Index] = true;
					SomeSelected =  1; // using the some selected process
					NextSelect = 1;
				}
			}


			// nextselection mode is 3 and doing someselected
			if (SomeSelected == 1 && NextSelect == 3)
			{
				Debug.WriteLine("master row selector| restore somselected| ");
				ListBox3.SelectedItems.Clear();

				for (var i = 0; i < CurrentSelected.Length; i++)
				{
					if (CurrentSelected[i])
					{
						ListBox3.SelectedItems.Add(ListBox3.Items[i]);
					}
				}

				NextSelect = 1;

				// all selected or nextselection mode is 2
			} else if ((SomeSelected == 1 && NextSelect == 2) || ListBox3.SelectedItems.Count == ListBox3.Items.Count)
			{
				Debug.WriteLine("master row selector| clear all selected| ");

				ListBox3.SelectedItems.Clear();

				NextSelect = 3;

				// none selected or just saved the list of some selected
				// do this to select all
			} else if ((SomeSelected == 1 && NextSelect == 1) || ListBox3.SelectedItems.Count == 0)
			{
				Debug.WriteLine("master row selector| select all| ");
				ListBox3.SelectedItems.Clear();

				for (var i = 0; i < ListBox3.Items.Count; i++)
				{
					ListBox3.SelectedItems.Add(ListBox3.Items[i]);
				}

				NextSelect = 2;
			}



		}

		private void RowSelector_Click(object sender, RoutedEventArgs e)
		{
			Debug.WriteLine("row selector clicked| reset master row selector");
			NextSelect = 0;
			SomeSelected = 0;
		}
	}



	public class CustomProperties
	{


		public static readonly DependencyProperty GenericBoolOneProperty = DependencyProperty.RegisterAttached(
			"GenericBooleanOne", typeof(bool), typeof(CustomProperties),
			new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsParentArrange |
				FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsArrange |
				FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

		public static readonly DependencyProperty  GenericBoolTwoProperty = DependencyProperty.RegisterAttached(
			"GenericBooleanTwo", typeof(bool), typeof(CustomProperties),
			new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsParentArrange |
				FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsArrange |
				FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

		public static readonly DependencyProperty  GenericDoubleOneProperty = DependencyProperty.RegisterAttached(
			"GenericDoubleOne", typeof(double), typeof(CustomProperties),
			new FrameworkPropertyMetadata(0.0));

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

	#region GenericBooleanTwo

		public static void SetGenericBooleanTwo(UIElement e, bool value)
		{
			e.SetValue(GenericBoolTwoProperty, value);
		}

		public static bool GetGenericBooleanTwo(UIElement e)
		{
			return (bool) e.GetValue(GenericBoolTwoProperty);
		}

	#endregion

	#region GenericDoubleOne

		public static void SetGenericDoubleOne(UIElement e, double value)
		{
			e.SetValue(GenericDoubleOneProperty, value);
		}

		public static double GetGenericDoubleOne(UIElement e)
		{
			return (double) e.GetValue(GenericDoubleOneProperty);
		}

	#endregion
	}
}