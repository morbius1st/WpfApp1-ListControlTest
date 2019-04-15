using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WpfApp1_ListControlTest.PointData;
using WpfApp1_ListControlTest.PointsData;

namespace WpfApp1_ListControlTest.SurfacePts
{
    /// <summary>
    /// Interaction logic for SurfacePoints.xaml
    /// </summary>
    public partial class SurfacePoints : Window, INotifyPropertyChanged
	{
		private class SavedInfo
		{
			public SavedInfo()
			{
				Row = -1;
				X = new Saved();
				Y = new Saved();
				Z = new Saved();
				Slope = new Saved();
			}

			public int Row { get; set; }
			public Saved X;
			public Saved Y;
			public Saved Z;
			public Saved Slope;

			// true when all are valid
			public bool IsValid => X.IsRevisedValid && Y.IsRevisedValid && Z.IsRevisedValid && Slope.IsRevisedValid;

			// true when any has revised
			public bool HasRevised => X.HasRevised || Y.HasRevised || Z.HasRevised || Slope.HasRevised;

			// true when hasrevised is true and isvalid is true
			public bool IsRevisedAndValid => HasRevised && IsValid;

			public class Saved
			{
				public Saved()
				{
					originalValue = "";
					revisedValue = "";
					IsRevisedValid = true;
					HasOriginal = false;
					HasRevised = false;
				}

				private string originalValue;
				public string OriginalValue
				{
					get => originalValue;
					set
					{
						originalValue = value;
						revisedValue = value;
						IsRevisedValid = false;
						HasOriginal = true;
						HasRevised  = false;
					}
				}

				private string revisedValue;

				public string RevisedValue
				{
					get => revisedValue;
					set
					{
						revisedValue = value;
						HasRevised = !revisedValue.Equals(originalValue);
					}

				}
				public bool IsRevisedValid { get; set; }
				public bool HasOriginal { get; private set; }
				public bool HasRevised{ get; private set; }
			}
		}
	
		public string WinTitle { get; set; } = "Surface Points";

		private string nl = Environment.NewLine;

		public string MessageText { get; set; } = "Message Window\n";

#pragma warning disable CS0169 // The field 'SurfacePoints.savedValue' is never used
		private string savedValue;
#pragma warning restore CS0169 // The field 'SurfacePoints.savedValue' is never used
#pragma warning disable CS0169 // The field 'SurfacePoints.savedRow' is never used
		private int savedRow;
#pragma warning restore CS0169 // The field 'SurfacePoints.savedRow' is never used

		private SavedInfo savedInfo = new SavedInfo();


		public PointCollection Sx { get; set; } = new PointCollection();

        public SurfacePoints()
        {
            InitializeComponent();

#if DEBUG
			LoadData();
#endif
		}

		private void BtnDone_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void BtnTest_Click(object sender, RoutedEventArgs e)
		{
			LoadData();

		}

		private void BtnDebug_Click(object sender, RoutedEventArgs e)
		{
			string a;
			a =  "x| " + Sx[0].XValue + "  ";
			a += "y| " + Sx[0].YValue + "  ";
			a += "z| " + Sx[0].ZValue + "  ";
			a += "s| " + Sx[0].Slope;

			AppendMessage(a);

			AppendMessage("BtnDebug_Click");
		}

		private void LoadData()
		{

			Sx.Insert(0, new PointClass() { XValue = " 111.0", YValue = " 121.0", ZValue = " 131.0", XyValue = " 141.0", XyzValue = " 151.0", ZDelta = " 161.0", Slope = " 1.0" });
			Sx.Insert(0, new PointClass() { XValue = " 211.0", YValue = " 221.0", ZValue = " 231.0", XyValue = " 241.0", XyzValue = " 251.0", ZDelta = " 261.0", Slope = " 2.0" });
            Sx.Insert(0, new PointClass() { XValue = " 311.0", YValue = " 321.0", ZValue = " 331.0", XyValue = " 341.0", XyzValue = " 351.0", ZDelta = " 361.0", Slope = " 3.0" });
            //Sx.Insert(0, new PointClass() { XValue = " 411.0", YValue = " 421.0", ZValue = " 431.0", XyValue = " 441.0", XyzValue = " 451.0", ZDelta = " 461.0", Slope = " 4.0" });
            //Sx.Insert(0, new PointClass() { XValue = " 511.0", YValue = " 521.0", ZValue = " 531.0", XyValue = " 541.0", XyzValue = " 551.0", ZDelta = " 561.0", Slope = " 5.0" });
            //Sx.Insert(0, new PointClass() { XValue = " 611.0", YValue = " 621.0", ZValue = " 631.0", XyValue = " 641.0", XyzValue = " 651.0", ZDelta = " 661.0", Slope = " 6.0" });
            //Sx.Insert(0, new PointClass() { XValue = " 711.0", YValue = " 721.0", ZValue = " 731.0", XyValue = " 741.0", XyzValue = " 751.0", ZDelta = " 761.0", Slope = " 7.0" });
            //Sx.Insert(0, new PointClass() { XValue = " 811.0", YValue = " 821.0", ZValue = " 831.0", XyValue = " 841.0", XyzValue = " 851.0", ZDelta = " 861.0", Slope = " 8.0" });
            //Sx.Insert(0, new PointClass() { XValue = " 911.0", YValue = " 921.0", ZValue = " 931.0", XyValue = " 941.0", XyzValue = " 951.0", ZDelta = " 961.0", Slope = " 9.0" });
            //Sx.Insert(0, new PointClass() { XValue = "1011.0", YValue = "1021.0", ZValue = "1031.0", XyValue = "1041.0", XyzValue = "1051.0", ZDelta = "1061.0", Slope = "10.0" });
            //Sx.Insert(0, new PointClass() { XValue = "1111.0", YValue = "1121.0", ZValue = "1131.0", XyValue = "1141.0", XyzValue = "1151.0", ZDelta = "1161.0", Slope = "11.0" });
            //Sx.Insert(0, new PointClass() { XValue = "1211.0", YValue = "1221.0", ZValue = "1231.0", XyValue = "1241.0", XyzValue = "1251.0", ZDelta = "1261.0", Slope = "12.0" });
			
		}

		public string StartX { get; private set; } = "110'-0\"";
		public string StartY { get; private set; } = "120'-0\"";
		public string StartZ { get; private set; } = "130'-0\"";

		public string EndX { get; private set; } = "210'-0\"";
		public string EndY { get; private set; } = "220'-0\"";
		public string EndZ { get; private set; } = "230'-0\"";

		public string EndDeltaXY { get; private set; }  = "310'-0\"";
		public string EndDeltaXYZ { get; private set; } = "320'-0\"";
		public string EndDeltaZ { get; private set; }   = "330'-0\"";
		public string EndSlope { get; private set; }    = "10%";

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

		private void TbxValue_OnError(object sender, ValidationErrorEventArgs e)
		{
			TextBox tbx = (TextBox) sender;

			if (e.Action != ValidationErrorEventAction.Added)
			{
				AppendMessage("TbxXvalue error| "
					+ tbx.Text + " :: tag| " + tbx.Tag.ToString() 
					+ " :: Removed");
				return;
			}

			AppendMessage("TbxXvalue error| "
				+ tbx.Text + " :: tag| " + tbx.Tag.ToString()
				+ " :: Added :: " + e.Error.ErrorContent);

			SavedInfo.Saved saved = GetSaved(tbx);

			saved.RevisedValue = tbx.Text;
			saved.IsRevisedValid = false;
		}


		private void TellMeLbx(ListBox lbx, string which)
		{
			AppendMessage(which + "| name| " + lbx.Name
				+ " :: idx (sender)| " + lbx.SelectedIndex
				+ " :: idx (listBox2)| " + listBox2.SelectedIndex
				);
		}

		private void ListBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			TellMeLbx(sender as ListBox, "SelectionChanged");

			AppendMessage("SelectionChanged| type|" + sender.GetType());
			AppendMessage("SelectionChanged| orig source| " + e.OriginalSource.GetType());
			AppendMessage("SelectionChanged| selected type| " + listBox2.SelectedItem.GetType());
			AppendMessage("SelectionChanged| selected type| " + listBox2.GetValue(ContentProperty).ToString());
			

			// use this method to prevent a selction change.
//			listBox2.SelectedIndex = 0;

			if (savedInfo.Row >= 0 && savedInfo.HasRevised)
			{
				AppendMessage("SelectionChanged| don't change row| row| " 
					+ savedInfo.Row 
					+ " :: hasrevised| " + savedInfo.HasRevised.ToString()
					);

				if (listBox2.SelectedIndex != savedInfo.Row)
				{
					listBox2.SelectedIndex = savedInfo.Row;
				}

				return;
			}

			savedInfo = new SavedInfo();

			savedInfo.Row = listBox2.SelectedIndex;

			AppendMessage("SelectionChanged| change row| row| "
				+ savedInfo.Row );
		}

		private void TellMeTbx(TextBox tbx, string which)
		{
			AppendMessage(which + "| " + tbx.Name 
				+ " :: text| " + tbx.Text
				);
		}


		private void tbxDetail_OnGotFocus(object sender, RoutedEventArgs e)
		{
			TextBox tbx = sender as TextBox;
//
//			
//
//			DependencyObject parent = tbx.Parent;
//			AppendMessage("got focus| tbx parent 1| " + parent.GetType());
//
//			parent = ((FrameworkElement) parent).Parent;
//			AppendMessage("got focus| tbx parent 2| " + parent.GetType());
//
//			parent = ((FrameworkElement) parent).Parent;
//			AppendMessage("got focus| tbx parent 3| " + parent.GetType());
//
//			parent = ((FrameworkElement) parent).Parent;
//			AppendMessage("got focus| tbx parent 4| " + parent.GetType());
//
//			parent = ((FrameworkElement) parent).Parent;
//			AppendMessage("got focus| tbx parent 5| " + parent.GetType());


		

			TellMeTbx(tbx, "got focus");

			SavedInfo.Saved saved = GetSaved(tbx);

			if (saved.HasOriginal) return;

			saved.OriginalValue = tbx.Text;
		}

		private void TbxSlope_OnSourceUpdated(object sender, DataTransferEventArgs e)
		{
			TextBox tbx = sender as TextBox;
			
			TellMeTbx(tbx, "source updated");

			SavedInfo.Saved saved = GetSaved(tbx);

			saved.RevisedValue = tbx.Text;
			saved.IsRevisedValid = true;
		}

		private SavedInfo.Saved GetSaved(TextBox tbx)
		{
			SavedInfo.Saved saved = new SavedInfo.Saved();

			switch (tbx.Tag)
			{
			case "Slope":
				{
					saved = savedInfo.Slope;
					break;
				}
			case ("X"):
				{
					saved = savedInfo.X;
					break;
				}

			case ("Y"):
				{
					saved = savedInfo.Y;
					break;
				}

			case ("Z"):
				{
					saved = savedInfo.Z;
					break;
				}
			}

			return saved;
		}


	}

	public class PointTemplateSelector : DataTemplateSelector
	{
		public override DataTemplate SelectTemplate(object item, DependencyObject container) 
		{
			FrameworkElement elemnt = container as FrameworkElement;
			PointClass pt = item as PointClass;
			if(pt.Slope.Equals(" 1.0"))
			{
				return elemnt.FindResource("detailDataTemplate2") as DataTemplate;
			}
			else
			{
				return elemnt.FindResource("detailDataTemplate") as DataTemplate;
			}
		}

	}

}


		// these also did not work - never fired
//		private void ListBox2_SourceUpdated(object sender, DataTransferEventArgs e)
//		{
//			AppendMessage("ListBox2 SourceUpdated");
//		}
//
//		private void ListBox2_TargetUpdated(object sender, DataTransferEventArgs e)
//		{
//			AppendMessage("ListBox2 TargetUpdated");
//		}

		// using got focus / lost focus methods does not work - this
		// does not invervene between the list box and the collection
//		private void TbxFirst_OnGotFocus(object sender, RoutedEventArgs e)
//		{
//			savedValue = ((TextBox) sender).Text;
//		}
//
//		private void TbxFirst_LostFocus(object sender, RoutedEventArgs e)
//		{
//			AppendMessage("tbxFirst changed| " + ((TextBox) sender).Text);
//
//			((TextBox) sender).Text = savedValue;
//
//			e.Handled = true;
//		}
//
//
//		private void TbxSecond_OnGotFocus(object sender, RoutedEventArgs e)
//		{
//			savedValue = ((TextBox) sender).Text;
//		}
//
//
//		private void TbxSecond_LostFocus(object sender, RoutedEventArgs e)
//		{
//			AppendMessage("tbxSecond changed| " + ((TextBox) sender).Text);
//
//			((TextBox) sender).Text = savedValue;
//
//			e.Handled = true;
//		}
//
//
//
//		private void TbxThird_OnGotFocus(object sender, RoutedEventArgs e)
//		{
//			savedValue = ((TextBox) sender).Text;
//		}
//
//		private void TbxThird_LostFocus(object sender, RoutedEventArgs e)
//		{
//			AppendMessage("tbxThird changed| " + ((TextBox) sender).Text);
//
//			((TextBox) sender).Text = savedValue;
//
//			e.Handled = true;
//		}

