#region + Using Directives
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1_ListControlTest.ControlPtsData;

#endregion


// projname: WpfApp1_ListControlTest.ControlPtsWin
// itemname: ControlPointsVm
// username: jeffs
// created:  5/17/2019 9:49:56 PM


namespace WpfApp1_ListControlTest.ControlPtsWin
{
	// this is the holder / provider of the data
	// this holds the actions as a result of data adjustment events
	// this does not hold the data validation actions
	// this does not hold the data conversion actions (see the converters)
	// this does hold the events for the view
	// this will be the hub that connect the M & V
	public class ControlPointsVm 
	{
//		public string WinTitle { get; set; } = "Control Points";
//
//		public string MessageText { get; set; } = "Message Window\n";
//
//		private int itemIndex = -1;
//		private string _errorMsg = "error message 1";
//		private string _endSlope = "10%";
//		private string _endDeltaZ = "330'-0\"";
//		private string _endDeltaXYZ = "320'-0\"";
//		private string _endDeltaXY = "310'-0\"";
//		private string _endZ = "230'-0\"";
//		private string _endY = "220'-0\"";
//		private string _endX = "210'-0\"";
//		private string _startZ = "130'-0\"";
//		private string _startY = "120'-0\"";
//		private string _startX = "110'-0000\"";
//
//		public ControlPointsConsts cpConsts { get; private set; }
//			= new ControlPointsConsts();
//
//		public ObservableCollection<ControlPts> Sx { get; set; }
//			= new ObservableCollection<ControlPts>();
//
//		public ControlPointsResources cpr { get; set; }
//
//		private bool _canExecute;
//
//		public ControlPointsVm()
//		{
//			_canExecute = true;
//
//			MainWindow.Cps = this;
//
//			if (MainWindow.Cpr != null)
//			{
//				cpr = MainWindow.Cpr;
//
//				cpr.BtnRestoreXClick += CommandClickHandler2;
//				cpr.XYZ_OnError += ValidationOnErrorHandler2;
//
//			}
//
//			this.AddHandler(Button.ClickEvent, new RoutedEventHandler(CommandClickHandler1));
//
//			System.Windows.Controls.Validation.AddErrorHandler(this, new EventHandler<ValidationErrorEventArgs>(ValidationOnErrorHandler1));
//#if DEBUG
//			LoadData();
//#endif
//		}
//
//
//		private ICommand _clickCommand;
//
//		public ICommand ClickCommand => _clickCommand ?? (_clickCommand = new ControlPoints.CommandHandler(MyAction, _canExecute));
//
//		public void MyAction()
//		{
//			MessageBox.Show("my action");
//		}
//
//		public class CommandHandler : ICommand
//		{
//			private Action _action;
//			private bool _canExecute;
//			public CommandHandler(Action action, bool canExecute)
//			{
//				_action = action;
//				_canExecute = canExecute;
//			}
//
//			public bool CanExecute(object parameter)
//			{
//				return _canExecute;
//			}
//
//			public event EventHandler CanExecuteChanged;
//
//			public void Execute(object parameter)
//			{
//				_action();
//			}
//		}
//
//
//		private void ValidationOnErrorHandler1(object sender, ValidationErrorEventArgs e)
//		{
//			MessageBox.Show("bubble validation message");
//		}
//
//		private void ValidationOnErrorHandler2(object sender, ValidationErrorEventArgs e)
//		{
//			MessageBox.Show("indirect validation message");
//		}
//
//		private void CommandClickHandler1(object sender, RoutedEventArgs e)
//		{
//			MessageBox.Show("bubble button click event message");
//		}
//
//		private void CommandClickHandler2(object sender, RoutedEventArgs e)
//		{
//			MessageBox.Show("indirect button click event message");
//		}
//
//
//		private void BtnDone_Click(object sender, RoutedEventArgs e)
//		{
//			Close();
//		}
//
//		private void BtnTest_Click(object sender, RoutedEventArgs e)
//		{
//			LoadData();
//		}
//
//		private void BtnDebugUndo_Click(object sender, RoutedEventArgs e)
//		{
//			ControlPts cps = Sx[itemIndex];
//			cps.Undo(((Button)sender).Tag as string);
//
//			AppendMessage("@ BtnDebugUndo_Click| "
//				+ " :: revised?| " + cps.IsRevised
//				);
//			list(itemIndex);
//		}
//
//		private void BtnDebugSl_Click(object sender, RoutedEventArgs e)
//		{
//			//			Sx[itemIndex]._Scp.IsRevisedValid = true;
//			Sx[itemIndex].Slope = -10000.0;
//			AppendMessage("@ BtnDebugSl_Click");
//			list(itemIndex);
//		}
//
//		private void BtnDebugX_Click(object sender, RoutedEventArgs e)
//		{
//			Sx[itemIndex].X = -20000.0;
//			AppendMessage("@ BtnDebugX_Click");
//			list(itemIndex);
//		}
//
//		//		private void BtnDebugReset_Click(object sender, RoutedEventArgs e)
//		//		{
//		//			Sx[1] = new ControlPts()  { XOrig = " 1081.0", YOrig = " 2081.0", ZOrig = " 3081.0", SlopeOrig = " 1.0", XyValue = " 4081.0", XyzValue = " 5081.0", ZDelta = " 6081.0"};
//		//			AppendMessage("@ BtnDebugReset_Click");
//		//			list(1);
//		//		}
//
//		private void BtnDebug_Click(object sender, RoutedEventArgs e)
//		{
//			Sx[itemIndex].X = -20000.0;
//
//			StartX = "asdfZ";
//			EndX = "asdf";
//
//
//			AppendMessage("BtnDebug_Click");
//			list(itemIndex);
//
//			//			(listBox2.Items[1] as ControlPts).Slope = "asdf";
//			//			Sx[1]._Scp.IsRevisedValid = true;
//			//			Sx[1].Slope = "asdf";
//
//		}
//
//		private void list(int idx)
//		{
//			string a;
//			a = "x| " + Sx[idx].X + "  ";
//			a += "y| " + Sx[idx].Y + "  ";
//			a += "z| " + Sx[idx].Z + "  ";
//			a += "s| " + Sx[idx].Slope;
//
//			AppendMessage(a);
//		}
//
//		public string StartX
//		{
//			get => _startX;
//			private set
//			{
//				_startX = value;
//				OnPropertyChange();
//			}
//		}
//		public string StartY
//		{
//			get => _startY;
//			private set
//			{
//				_startY = value;
//				OnPropertyChange();
//			}
//		}
//		public string StartZ
//		{
//			get => _startZ;
//			private set
//			{
//				_startZ = value;
//				OnPropertyChange();
//			}
//		}
//		public string EndX
//		{
//			get => _endX;
//			private set
//			{
//				_endX = value;
//				OnPropertyChange();
//			}
//		}
//		public string EndY
//		{
//			get => _endY;
//			private set
//			{
//				_endY = value;
//				OnPropertyChange();
//			}
//		}
//		public string EndZ
//		{
//			get => _endZ;
//			private set
//			{
//				_endZ = value;
//				OnPropertyChange();
//			}
//		}
//		public string EndDeltaXY
//		{
//			get => _endDeltaXY;
//			private set
//			{
//				_endDeltaXY = value;
//				OnPropertyChange();
//			}
//		}
//		public string EndDeltaXYZ
//		{
//			get => _endDeltaXYZ;
//			private set
//			{
//				_endDeltaXYZ = value;
//				OnPropertyChange();
//			}
//		}
//		public string EndDeltaZ
//		{
//			get => _endDeltaZ;
//			private set
//			{
//				_endDeltaZ = value;
//				OnPropertyChange();
//			}
//		}
//		public string EndSlope
//		{
//			get => _endSlope;
//			private set
//			{
//				_endSlope = value;
//				OnPropertyChange();
//			}
//		}
//		public string ErrorMsg
//		{
//			get => _errorMsg;
//
//			set
//			{
//				_errorMsg = value;
//				OnPropertyChange();
//			}
//		}
//
//		internal void AppendMessage(string message)
//		{
//			MessageText += message + nl;
//
//			OnPropertyChange("MessageText");
//		}
//
//		public void TellMeTbx(string which, TextBox tbx = null)
//		{
//			if (tbx != null)
//			{
//				AppendMessage(nl + which + "| " + tbx.Name
//					+ " :: text| (" + tbx.Text + ")"
//					+ " :: tag| (" + (string)tbx.Tag + ")"
//					);
//			}
//			else
//			{
//				AppendMessage(nl + which);
//			}
//		}
//
//		public void TbxXYZ_OnError(object sender, ValidationErrorEventArgs e)
//		{
//			TextBox tbx = (TextBox)sender;
//
//			TellMeTbx("at TbxXYZ_OnError| ", tbx);
//			AppendMessage("*** on error action| "
//				+ e.Action.ToString()
//				);
//
//			//			OnErrorEx();
//		}
//
//		public void TbxSlope_OnError(object sender, ValidationErrorEventArgs e)
//		{
//			TextBox tbx = (TextBox)sender;
//
//			TellMeTbx("at TbxSlope_OnError| ", tbx);
//			AppendMessage("*** on error action| "
//				+ e.Action.ToString()
//				);
//
//			//			OnErrorEx();
//		}
//
//
//		public event PropertyChangedEventHandler PropertyChanged;
//
//		private void OnPropertyChange([CallerMemberName] string memberName = "")
//		{
//			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
//		}
//
//
//
//
//
//		private void ListBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
//		{
//			//			TellMeTbx("at ListBox2_SelectionChanged");
//
//			//			// uses the selectedindex property to get the correct index
//			//			TellMeLbx(sender as ListBox, "SelectionChanged");
//
//			if (itemIndex < 0 || !Sx[itemIndex].IsRevised)
//			{
//				if (itemIndex >= 0)
//				{
//					((ControlPts)listBox2.Items[itemIndex]).IsBeingEdited = false;
//				}
//
//				// begin editing a new item
//				// save the index of this item
//				itemIndex = listBox2.SelectedIndex;
//
//				((ControlPts)listBox2.SelectedItem).CurrentIndex = itemIndex;
//				((ControlPts)listBox2.SelectedItem).IsBeingEdited = true;
//
//				return;
//			}
//
//			// the current item is has changes - prevent them from switching to another item
//			// use this method to prevent a selection change.
//			listBox2.SelectedIndex = itemIndex;
//
//			//SelectionChangedEx();
//		}
//
//		private bool clickTest = false;
//
//		private void Testx_Click(object sender, RoutedEventArgs e)
//		{
//			clickTest = !clickTest;
//
//
//			if (clickTest)
//			{
//				label.Content = "CLICKED";
//			}
//			else
//			{
//				label.Content = "UN-CLICKED";
//			}
//		}
//
//#pragma warning disable CS0108 // 'ControlPoints.LostFocus(object, RoutedEventArgs)' hides inherited member 'UIElement.LostFocus'. Use the new keyword if hiding was intended.
//		private void LostFocus(object sender, RoutedEventArgs e)
//#pragma warning restore CS0108 // 'ControlPoints.LostFocus(object, RoutedEventArgs)' hides inherited member 'UIElement.LostFocus'. Use the new keyword if hiding was intended.
//		{
//			if (itemIndex >= 0 && !Sx[itemIndex].IsRevised)
//			{
//				Sx[itemIndex].IsBeingEdited = false;
//
//				listBox2.SelectedIndex = -1;
//			}
//		}
//
//
//
//
//
//
//
//
//
//
//
//
//		private void LoadData()
//		{
//			Sx.Insert(0, new ControlPts() { XOrig = " 1011.0", YOrig = " 2011.0", ZOrig = " 3011.0", SlopeOrig = " 1.0", XyOrig = " 4011.0", XyzOrig = " 5011.0", ZDeltaOrig = " 6011.0" });
//			Sx.Insert(0, new ControlPts() { XOrig = " 1021.0", YOrig = " 2021.0", ZOrig = " 3021.0", SlopeOrig = " 1.0", XyOrig = " 4021.0", XyzOrig = " 5021.0", ZDeltaOrig = " 6021.0" });
//			Sx.Insert(0, new ControlPts() { XOrig = " 1031.0", YOrig = " 2031.0", ZOrig = " 3031.0", SlopeOrig = " 1.0", XyOrig = " 4031.0", XyzOrig = " 5031.0", ZDeltaOrig = " 6031.0" });
//			Sx.Insert(0, new ControlPts() { XOrig = " 1041.0", YOrig = " 2041.0", ZOrig = " 3041.0", SlopeOrig = " 1.0", XyOrig = " 4041.0", XyzOrig = " 5041.0", ZDeltaOrig = " 6041.0" });
//			//			Sx.Insert(0, new ControlPts() { XOrig = " 1051.0", YOrig = " 2051.0", ZOrig = " 3051.0", SlopeOrig = " 1.0", XyOrig = " 4051.0", XyzOrig = " 5051.0", ZDeltaOrig = " 6051.0" });
//			//			Sx.Insert(0, new ControlPts() { XOrig = " 1061.0", YOrig = " 2061.0", ZOrig = " 3061.0", SlopeOrig = " 1.0", XyOrig = " 4061.0", XyzOrig = " 5061.0", ZDeltaOrig = " 6061.0" });
//			//			Sx.Insert(0, new ControlPts() { XOrig = " 1071.0", YOrig = " 2071.0", ZOrig = " 3071.0", SlopeOrig = " 1.0", XyOrig = " 4071.0", XyzOrig = " 5071.0", ZDeltaOrig = " 6071.0" });
//			//			Sx.Insert(0, new ControlPts() { XOrig = " 1081.0", YOrig = " 2081.0", ZOrig = " 3081.0", SlopeOrig = " 1.0", XyOrig = " 4081.0", XyzOrig = " 5081.0", ZDeltaOrig = " 6081.0" });
//			//			Sx.Insert(0, new ControlPts() { XOrig = " 1091.0", YOrig = " 2091.0", ZOrig = " 3091.0", SlopeOrig = " 1.0", XyOrig = " 4091.0", XyzOrig = " 5091.0", ZDeltaOrig = " 6091.0" });
//
//		}
//	}
//
//
//	public class CommandHandler : ICommand
//	{
//		private Action _action;
//		private bool _canExecute;
//		public CommandHandler(Action action, bool canExecute)
//		{
//			_action = action;
//			_canExecute = canExecute;
//		}
//
//		public bool CanExecute(object parameter)
//		{
//			return _canExecute;
//		}
//
//		public event EventHandler CanExecuteChanged;
//
//		public void Execute(object parameter)
//		{
//			_action();
//		}
	}
}
