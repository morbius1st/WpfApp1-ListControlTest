#region + Using Directives

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using WpfApp1_ListControlTest.ControlPtsData;
using static WpfApp1_ListControlTest.MainWindow;

using static WpfApp1_ListControlTest.ControlPtsWin.ButtonClickCommandHandler;

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
//		public string MessageText { get; set; } = "Message Window version 2\n";
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
//
//			ErrorMsg = "no error";
//
//			_canExecute = true;
//
//			
//
//#if DEBUG
//			LoadData();
//#endif
//		}
//
//		// setup command / event handler for button click
//		private ICommand _btnClickCommand;
//
//		public ICommand ButtonClickCommand => _btnClickCommand ?? (_btnClickCommand = new ButtonClickCommandHandler(param => BtnClickEvent(param), _canExecute));
//
//		public void BtnClickEvent(object param)
//		{
//			MessageBox.Show("BtnClickEvent| who| " + (string)param);
//		}
//
//		// end of command handler for button click
//
//
//		// setup command / event handler for button click
//		private ICommand _validationErrorCommand;
//
//		public ICommand ValidationErrorCommand => _validationErrorCommand ?? (_validationErrorCommand = new ValidationErrorCommandHandler(param => ValidateErrorEvent(param), _canExecute));
//
//		private bool _gotError;
//
//		public bool GotError
//		{
//			get { return _gotError; }
//			set
//			{
//				_gotError = value;
//				MessageBox.Show("ValidationError");
//			}
//
//
//		}
//
//
//		public void ValidateErrorEvent(object param)
//		{
//			MessageBox.Show("ValidationErrorCommand| who| " + (string)param);
//		}
//
//		// end of command handler for button click
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
////
////		private void BtnDone_Click(object sender, RoutedEventArgs e)
////		{
////			Close();
////		}
////
////		private void BtnTest_Click(object sender, RoutedEventArgs e)
////		{
////			LoadData();
////		}
//
//		private void BtnDebugUndo_Click(object sender, RoutedEventArgs e)
//		{
//			//			((ControlPts)listBox2.Items[itemIndex])
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
//		public void LoadData()
//		{
//			Sx.Insert(0, new ControlPts() { XOrig = " 1011.0", YOrig = " 2011.0", ZOrig = " 3011.0", SlopeOrig = " 1.0", XyOrig = " 4011.0", XyzOrig = " 5011.0", ZDeltaOrig = " 6011.0" });
//			Sx.Insert(0, new ControlPts() { XOrig = " 1021.0", YOrig = " 2021.0", ZOrig = " 3021.0", SlopeOrig = " 1.0", XyOrig = " 4021.0", XyzOrig = " 5021.0", ZDeltaOrig = " 6021.0" });
//			Sx.Insert(0, new ControlPts() { XOrig = " 1031.0", YOrig = " 2031.0", ZOrig = " 3031.0", SlopeOrig = " 1.0", XyOrig = " 4031.0", XyzOrig = " 5031.0", ZDeltaOrig = " 6031.0" });
//			Sx.Insert(0, new ControlPts() { XOrig = " 1041.0", YOrig = " 2041.0", ZOrig = " 3041.0", SlopeOrig = " 1.0", XyOrig = " 4041.0", XyzOrig = " 5041.0", ZDeltaOrig = " 6041.0" });
//			Sx.Insert(0, new ControlPts() { XOrig = " 1051.0", YOrig = " 2051.0", ZOrig = " 3051.0", SlopeOrig = " 1.0", XyOrig = " 4051.0", XyzOrig = " 5051.0", ZDeltaOrig = " 6051.0" });
//			//			Sx.Insert(0, new ControlPts() { XOrig = " 1061.0", YOrig = " 2061.0", ZOrig = " 3061.0", SlopeOrig = " 1.0", XyOrig = " 4061.0", XyzOrig = " 5061.0", ZDeltaOrig = " 6061.0" });
//			//			Sx.Insert(0, new ControlPts() { XOrig = " 1071.0", YOrig = " 2071.0", ZOrig = " 3071.0", SlopeOrig = " 1.0", XyOrig = " 4071.0", XyzOrig = " 5071.0", ZDeltaOrig = " 6071.0" });
//			//			Sx.Insert(0, new ControlPts() { XOrig = " 1081.0", YOrig = " 2081.0", ZOrig = " 3081.0", SlopeOrig = " 1.0", XyOrig = " 4081.0", XyzOrig = " 5081.0", ZDeltaOrig = " 6081.0" });
//			//			Sx.Insert(0, new ControlPts() { XOrig = " 1091.0", YOrig = " 2091.0", ZOrig = " 3091.0", SlopeOrig = " 1.0", XyOrig = " 4091.0", XyzOrig = " 5091.0", ZDeltaOrig = " 6091.0" });
//
//		}
//
//		public string StartX
//		{
//			get => _startX;
//			set
//			{
//				_startX = value;
//				OnPropertyChange();
//			}
//		}
//		public string StartY
//		{
//			get => _startY;
//			set
//			{
//				_startY = value;
//				OnPropertyChange();
//			}
//		}
//		public string StartZ
//		{
//			get => _startZ;
//			set
//			{
//				_startZ = value;
//				OnPropertyChange();
//			}
//		}
//		public string EndX
//		{
//			get => _endX;
//			set
//			{
//				_endX = value;
//				OnPropertyChange();
//			}
//		}
//		public string EndY
//		{
//			get => _endY;
//			set
//			{
//				_endY = value;
//				OnPropertyChange();
//			}
//		}
//		public string EndZ
//		{
//			get => _endZ;
//			set
//			{
//				_endZ = value;
//				OnPropertyChange();
//			}
//		}
//		public string EndDeltaXY
//		{
//			get => _endDeltaXY;
//			set
//			{
//				_endDeltaXY = value;
//				OnPropertyChange();
//			}
//		}
//		public string EndDeltaXYZ
//		{
//			get => _endDeltaXYZ;
//			set
//			{
//				_endDeltaXYZ = value;
//				OnPropertyChange();
//			}
//		}
//		public string EndDeltaZ
//		{
//			get => _endDeltaZ;
//			set
//			{
//				_endDeltaZ = value;
//				OnPropertyChange();
//			}
//		}
//		public string EndSlope
//		{
//			get => _endSlope;
//			set
//			{
//				_endSlope = value;
//				OnPropertyChange();
//			}
//		}
//
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
////		private void TellMeLbx(ListBox lbx, string which)
////		{
////			AppendMessage(which + "| name| " + lbx.Name
////				+ " :: idx (sender)| " + lbx.SelectedIndex
////				+ " :: idx (listBox2)| " + listBox2.SelectedIndex
////				);
////		}
//
//		public event PropertyChangedEventHandler PropertyChanged;
//
//		private void OnPropertyChange([CallerMemberName] string memberName = "")
//		{
//			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
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
//		private bool clickTest = false;
//
//		public string LabelContent { get; set; } = "UN-CLICKED";
//
//		private void Testx_Click(object sender, RoutedEventArgs e)
//		{
//			clickTest = !clickTest;
//
//
//			if (clickTest)
//			{
//				LabelContent = "CLICKED";
//			}
//			else
//			{
//				LabelContent = "UN-CLICKED";
//			}
//		}

	}

//	public class PointTemplateSelector : DataTemplateSelector
//	{
//		public override DataTemplate SelectTemplate(object item, DependencyObject container)
//		{
//			FrameworkElement elemnt = container as FrameworkElement;
//			ControlPts pt = item as ControlPts;
//
//			if (pt.IsBeingEdited)
//			{
//				return elemnt.FindResource("pointDataTemplateEditing") as DataTemplate;
//			}
//			else
//			{
//				return elemnt.FindResource("pointDataTemplate") as DataTemplate;
//			}
//		}
//
//	}
}
