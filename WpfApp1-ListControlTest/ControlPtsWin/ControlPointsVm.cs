#region + Using Directives
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		public string WinTitle { get; set; } = "Control Points";

		public string MessageText { get; set; } = "Message Window\n";

		private int itemIndex = -1;
		private string _errorMsg = "error message 1";
		private string _endSlope = "10%";
		private string _endDeltaZ = "330'-0\"";
		private string _endDeltaXYZ = "320'-0\"";
		private string _endDeltaXY = "310'-0\"";
		private string _endZ = "230'-0\"";
		private string _endY = "220'-0\"";
		private string _endX = "210'-0\"";
		private string _startZ = "130'-0\"";
		private string _startY = "120'-0\"";
		private string _startX = "110'-0000\"";

		public ControlPointsConsts cpConsts { get; private set; }
			= new ControlPointsConsts();

		public ObservableCollection<ControlPts> Sx { get; set; }
			= new ObservableCollection<ControlPts>();


		public ControlPointsVm()
		{

			LoadData();
		}


		private void LoadData()
		{
			Sx.Insert(0, new ControlPts() { XOrig = " 1011.0", YOrig = " 2011.0", ZOrig = " 3011.0", SlopeOrig = " 1.0", XyOrig = " 4011.0", XyzOrig = " 5011.0", ZDeltaOrig = " 6011.0" });
			Sx.Insert(0, new ControlPts() { XOrig = " 1021.0", YOrig = " 2021.0", ZOrig = " 3021.0", SlopeOrig = " 1.0", XyOrig = " 4021.0", XyzOrig = " 5021.0", ZDeltaOrig = " 6021.0" });
			Sx.Insert(0, new ControlPts() { XOrig = " 1031.0", YOrig = " 2031.0", ZOrig = " 3031.0", SlopeOrig = " 1.0", XyOrig = " 4031.0", XyzOrig = " 5031.0", ZDeltaOrig = " 6031.0" });
			Sx.Insert(0, new ControlPts() { XOrig = " 1041.0", YOrig = " 2041.0", ZOrig = " 3041.0", SlopeOrig = " 1.0", XyOrig = " 4041.0", XyzOrig = " 5041.0", ZDeltaOrig = " 6041.0" });
			//			Sx.Insert(0, new ControlPts() { XOrig = " 1051.0", YOrig = " 2051.0", ZOrig = " 3051.0", SlopeOrig = " 1.0", XyOrig = " 4051.0", XyzOrig = " 5051.0", ZDeltaOrig = " 6051.0" });
			//			Sx.Insert(0, new ControlPts() { XOrig = " 1061.0", YOrig = " 2061.0", ZOrig = " 3061.0", SlopeOrig = " 1.0", XyOrig = " 4061.0", XyzOrig = " 5061.0", ZDeltaOrig = " 6061.0" });
			//			Sx.Insert(0, new ControlPts() { XOrig = " 1071.0", YOrig = " 2071.0", ZOrig = " 3071.0", SlopeOrig = " 1.0", XyOrig = " 4071.0", XyzOrig = " 5071.0", ZDeltaOrig = " 6071.0" });
			//			Sx.Insert(0, new ControlPts() { XOrig = " 1081.0", YOrig = " 2081.0", ZOrig = " 3081.0", SlopeOrig = " 1.0", XyOrig = " 4081.0", XyzOrig = " 5081.0", ZDeltaOrig = " 6081.0" });
			//			Sx.Insert(0, new ControlPts() { XOrig = " 1091.0", YOrig = " 2091.0", ZOrig = " 3091.0", SlopeOrig = " 1.0", XyOrig = " 4091.0", XyzOrig = " 5091.0", ZDeltaOrig = " 6091.0" });

		}
	}


	public class CommandHandler : ICommand
	{
		private Action _action;
		private bool _canExecute;
		public CommandHandler(Action action, bool canExecute)
		{
			_action = action;
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute;
		}

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			_action();
		}
	}
}
