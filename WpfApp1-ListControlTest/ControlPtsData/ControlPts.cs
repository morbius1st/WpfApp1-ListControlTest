#region + Using Directives
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using WpfApp1_ListControlTest.ControlPtsWin;

#endregion


// projname: WpfApp1_ListControlTest.ControlPointsData
// itemname: ControlPoints
// username: jeffs
// created:  4/7/2019 5:45:50 PM




namespace WpfApp1_ListControlTest.ControlPtsData
{

	public class ControlPts : INotifyPropertyChanged
	{
		public static int index = 0;
		public ControlPts()
		{
			_Xcp.PropertyChanged += PropertyChangedX;
			_Ycp.PropertyChanged += PropertyChangedY;
			_Zcp.PropertyChanged += PropertyChangedZ;
			_Scp.PropertyChanged += PropertyChangedS;

		}

		private void PropertyChangedX(object sender, PropertyChangedEventArgs e)
		{

			if (MainWindow.loaded)
			{
				MainWindow.Cps.AppendMessage("prop change x");
				MainWindow.Cps.AppendMessage("index| " + index++);

			}
			OnPropertyChange("HasRevisionX");
		}
		
		private void PropertyChangedY(object sender, PropertyChangedEventArgs e)
		{
			OnPropertyChange("HasRevisionY");
		}
		
		private void PropertyChangedZ(object sender, PropertyChangedEventArgs e)
		{
			OnPropertyChange("HasRevisionZ");
		}
		
		private void PropertyChangedS(object sender, PropertyChangedEventArgs e)
		{
			OnPropertyChange("HasRevisionS");
		}

		private ControlPointsConsts cpConsts = new ControlPointsConsts();

		private int currentIndex;
		private bool isBeingEdited;

		public int CurrentIndex
		{
			get => currentIndex;
			set
			{
				currentIndex = value;
				Update();
			}
		}

		public bool IsBeingEdited
		{
			get => isBeingEdited;
			set
			{
				isBeingEdited = value;
				Update();
			}
		}

		public double X
		{
			get
			{
				MainWindow.Cps.TellMeTbx("at get X| (" + _Xcp.ToString() + ")");
				return _Xcp.Value;
			}
			set
			{
				_Xcp.RevisedValue = value;
				MainWindow.Cps.TellMeTbx("at set X| (" + value + ")"
					+ " has revisions| " + _Xcp.HasRevision
					+ " is revised| " + IsRevised.ToString() );
				MainWindow.Cps.AppendMessage("index| " + index++);
				Update();
			}
		}
		
		public double Y
		{
			get => _Ycp.Value;
			set
			{
				_Ycp.RevisedValue = value;
				Update();
			}
		}
				
		public double Z
		{
			get => _Zcp.Value;
			set
			{
				_Zcp.RevisedValue = value;
				Update();
			}
		}
						
		public double Slope
		{
			get => _Scp.Value;
			set
			{
				_Scp.RevisedValue = value;
				Update();
			}
		}

		public bool HasRevisionX
		{
			get
			{
				if (MainWindow.loaded)
				{
					MainWindow.Cps.AppendMessage("getting HasRevisionsX| "
						+ _Xcp. HasRevision
						);
					MainWindow.Cps.AppendMessage("index| " + index++);
				}

				return _Xcp. HasRevision;
			}
		}

		public bool HasRevisionY
		{
			get
			{
				return _Ycp.HasRevision;
			}
		}

		public bool HasRevisionZ
		{
			get
			{
				return _Zcp.HasRevision;
			}
		}

		public bool HasRevisionS
		{
			get
			{
				return _Scp.HasRevision;
			}
		}

		public string XOrig
		{
			get => _Xcp.OriginalValue.ToString();
			set
			{
				_Xcp.OriginalValue = double.Parse(value);
				Update();
			}
		}

		public string YOrig
		{
			get => _Ycp.OriginalValue.ToString();
			set
			{
				_Ycp.OriginalValue = double.Parse(value);
				Update();
			}
		}

		public string ZOrig
		{
			get => _Zcp.OriginalValue.ToString();
			set
			{
				_Zcp.OriginalValue = double.Parse(value);
				Update();
			}
		}

		public string SlopeOrig
		{
			get => _Scp.OriginalValue.ToString();
			set
			{
				_Scp.OriginalValue = double.Parse(value);
				Update();
			}
		}

		public string XyOrig
		{
			set { XyValue = double.Parse(value); }
		}
		
		public string XyzOrig
		{
			set { XyzValue = double.Parse(value); }
		}
		
		public string ZDeltaOrig
		{
			set { ZDelta = double.Parse(value); }
		}

		public void UndoSl()
		{
			_Scp.Reset();
			Update();
		}

		public void SetRevisedValue(string which, double value)
		{
			MainWindow.Cps.MessageText += "at SetRevisedValue (" + which + ")" + MainWindow.nl;
			ControlPt cp = SelectCp(which);

			cp.RevisedValue = value;
			Update(which);
		}

		public void Undo(string which)
		{
			MainWindow.Cps.MessageText += "at undo (" + which + ")" + MainWindow.nl;

			ControlPts cps = null;

			if (which == ControlPointsConsts.aTag)
			{
				_Xcp.Reset();
				Update(ControlPointsConsts.xTag);
				_Ycp.Reset();
				Update(ControlPointsConsts.yTag);
				_Zcp.Reset();
				Update(ControlPointsConsts.zTag);
				_Scp.Reset();
				Update(ControlPointsConsts.sTag);
			}
			else
			{
				ControlPt cp = SelectCp(which);
				cp.Reset();
				Update(which);
			}
		}

		public ControlPt SelectCp(string which)
		{
			ControlPt cp = null;

			switch (which)
			{
			case ControlPointsConsts.xTag:
				{
				cp = _Xcp;
				break;
				}
			case ControlPointsConsts.yTag:
				{
				cp = _Ycp;
				break;
				}
			case ControlPointsConsts.zTag:
				{
				cp = _Zcp;
				break;
				}
			case ControlPointsConsts.sTag:
				{
				cp = _Scp;
				break;
				}
			}

			return cp;
		}

		private void Update([CallerMemberName] string memberName = "")
		{
			OnPropertyChange(memberName);
//			OnPropertyChange("IsRevisedAndValid");
		}

		public ControlPt _Xcp { get; set; } = new ControlPt();
		public ControlPt _Ycp { get; set; } = new ControlPt();
		public ControlPt _Zcp { get; set; } = new ControlPt();
		public ControlPt _Scp { get; set; } = new ControlPt();

		public double XyValue { get; set; } = 0;
		public double XyzValue { get; set; } = 0;
		public double ZDelta { get; set; } = 0;

		// true when any has revised
		public bool IsRevised => _Xcp.HasRevision || _Ycp.HasRevision || _Zcp.HasRevision || _Scp.HasRevision;

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}
	}

	public class ControlPt : INotifyPropertyChanged
	{
		private double originalValue;
		private double revisedValue;
		private bool _hasRevision;

		public ControlPt()
		{
			originalValue = double.NaN;
			revisedValue = double.NaN;
			HasOriginal = false;
			HasRevision = false;
		}

		public void Reset()
		{
			if (HasRevision)
			{
				OriginalValue = originalValue;
			}
		}

		public bool HasOriginal { get; private set; }
		public bool HasRevision
		{
			get => _hasRevision;
			private set
			{
				if (MainWindow.loaded)
				{
					MainWindow.Cps.AppendMessage("has revision change to| " + value.ToString());
					MainWindow.Cps.AppendMessage("index| " + ControlPts.index++);
				}

				OnPropertyChange();
				_hasRevision = value;
			}
		}

		public double OriginalValue
		{
			get => originalValue;
			set
			{
				originalValue = value;
				revisedValue = value;
				HasOriginal = true;
				HasRevision = false;
				OnPropertyChange();
			}
		}

		public double RevisedValue
		{
			get => revisedValue;
			set
			{

				revisedValue = value;
				HasRevision = !revisedValue.Equals(originalValue);

				if (MainWindow.loaded)
				{
					MainWindow.Cps.AppendMessage("at RevisedValue has revision change to| " 
						+ HasRevision.ToString()
						+ " :: (" + value + ")"
						);
					MainWindow.Cps.AppendMessage("index| " + ControlPts.index++);
				}
				OnPropertyChange();
			}
		}

		public double Value
		{
			get
			{
				if (HasRevision) return revisedValue;

				return originalValue;
			}
		}

		public override string ToString()
		{
			if (HasRevision) return RevisedValue.ToString();

			return OriginalValue.ToString();
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			if (MainWindow.loaded)
			{
				MainWindow.Cps.AppendMessage(
					"at controlPt.OnPropertyChange| membername| " + memberName
					);
				MainWindow.Cps.AppendMessage("index| " + ControlPts.index++);
			}

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}
	}
}
//
//
//public class ControlPt : INotifyPropertyChanged
//{
//	private bool isRevisedValid;
//
//	public bool IsRevisedValid
//	{
//		get => isRevisedValid;
//		set
//		{
//			isRevisedValid = value;
//			OnPropertyChange();
//		}
//	}
//
//	public bool HasOriginal { get; private set; }
//	public bool IsRevised  { get; private set; }
//
//	public ControlPt()
//	{
//		originalValue  = "";
//		revisedValue   = "";
//		IsRevisedValid = true;
//		HasOriginal    = false;
//		IsRevised     = false;
//	}
//
//	private string originalValue;
//
//	public string OriginalValue
//	{
//		get => originalValue;
//		set
//		{
//			originalValue  = value;
//			revisedValue   = value + "_rev";
//			IsRevisedValid = false;
//			HasOriginal    = true;
//			IsRevised     = false;
//			OnPropertyChange();
//		}
//	}
//
//	private string revisedValue;
//
//	public string RevisedValue
//	{
//		get => revisedValue;
//		set
//		{
//			revisedValue = value;
//			IsRevised   = !revisedValue.Equals(originalValue);
//			OnPropertyChange();
//		}
//	}
//
//	public override string ToString()
//	{
//		if (IsRevisedValid) return RevisedValue;
//
//		return OriginalValue;
//	}
//

//}
//
//
