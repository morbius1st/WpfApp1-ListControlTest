#region + Using Directives

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using WpfApp1_ListControlTest.TopoPts.Support;
using WpfApp1_ListControlTest.TopoPtsData2.Support;

#endregion


// projname: WpfApp1_ListControlTest.TopoPts
// itemname: TopoPoint
// username: jeffs
// created:  5/21/2019 7:41:18 PM


namespace WpfApp1_ListControlTest.TopoPtsData2
{
#region	> special Topopoints

	public class TopoStartPoint : TopoPoint2
	{
		public TopoStartPoint(XYZ2 xyz)
		{
			if (!xyz.IsValid) { throw new ArgumentException("Invalid Start Point"); }

			base.index = 0;
			base.point = xyz;
			base.controlPoint = true;
		}
	}

	public class TopoEndPoint : TopoPoint2
	{
		public TopoEndPoint(XYZ2 xyz)
		{
			if (!xyz.IsValid) { throw new ArgumentException("Invalid End Point"); }

			base.index = -1;
			base.point = xyz;
			base.controlPoint = true;
		}
	}
#endregion

	public class TopoPoint2 : IEquatable<TopoPoint2>, ICloneable, INotifyPropertyChanged
	{
	#region > internal fields

		private UInt32 statusFlag = 0;

		// the index number for this item
		// and the list index number
		protected int index = -1;

		// flag - this TopoPoint is being edited
		private bool isBeingEdited = false;

		// management: is this a control point (start or end points)
		protected bool controlPoint = false;

		// the point's XYZ coordinates
		protected XYZ2 point = XYZ2.Empty;

		// the X or Y or Z distance change 
		// for the current point
		// from the prior point
		private double _xΔ = Double.NaN;
		private double _yΔ = Double.NaN;
		private double _zΔ = Double.NaN;

		// the XY distance change
		// for the current point
		// from the prior point
		private double _xyΔ = Double.NaN;

		// the XYZ distance change
		// for the current point
		// from the prior point
		private double _xyzΔ = Double.NaN;

		// the Slope
		// for the current point
		// from the prior point
		private double _slope = Double.NaN;

		// the saved XYZ (i.e. point) coordinates
		// for the current point
		// used to determining if the X or Y or Z
		// values have changed and used to prevent
		// extra events
//		private XYZ2 originalPointValue = XYZ2.Empty;

		private string message;

		private string[] PropertyEvents = {"Index", 
			"IsBeingEdited", "ControlPoint", "HasRevision", 
			"X", "Y", "Z", "XΔ", "YΔ", "ZΔ", "XYΔ", "XYZΔ",	
			"Slope" };

	#endregion

	#region > constructors

		public TopoPoint2()
		{
			point.PropertyChanged += PointPropertyChanged;
		}

		public TopoPoint2(XYZ2 xyz)
		{
			point = xyz;

			point.PropertyChanged += PointPropertyChanged;
		}

	#endregion

	#region > public properties

		// update XY and / or Z all at one time
		// this reduces the number of events 
		// will make the ui a bit faster
		public XYZ2 XYZ
		{
			get { return point; }
			set
			{
				if (!value.Equals(point))
				{
					OnPropertyChange("XYZstart");

					if (!double.IsNaN(value.X))
					{
						point.X = value.X;
						OnPropertyChange("X");
					}

					if (!double.IsNaN(value.Y))
					{
						point.Y = value.Y;
						OnPropertyChange("Y");
					}

					if (!double.IsNaN(value.Z))
					{
						point.Z = value.Z;
						OnPropertyChange("Z");
					}

					OnPropertyChange("XYZend");
				}
			}
		}


		// XYZ2 values can be set individually
		// however, since there is no prior point
		// cannot re-calculate values
		// just cause an event when changed
		public double X
		{
			get => point.X;
			set
			{
				if (!value.Equals(point.X))
				{
					Debug.WriteLine("     | @ TopoPoint2| @ pre-change X" + "\n");
					point.X = value;
					Debug.WriteLine("     | @ TopoPoint2| @ post-change X" + "\n");

					checkForNaN(value, (UInt32) BitFlag.Xflag);

					OnPropertyChange();
				}
			}
		}

		public double Y
		{
			get => point.Y;
			set
			{
				if (!value.Equals(point.Y))
				{
					point.Y = value;

					checkForNaN(value, (UInt32) BitFlag.Yflag);

					OnPropertyChange();
				}
			}
		}

		public double Z
		{
			get => point.Z;
			set
			{
				if (!value.Equals(point.Z))
				{
					point.Z = value;

					checkForNaN(value, (UInt32) BitFlag.Zflag);

					OnPropertyChange();
				}
			}
		}

		// these are management values
		// setting one of them just causes
		// a property change event
		public int Index
		{
			get => index;
			set
			{
				if (value == index) return;
				index = value;
				OnPropertyChange();
			}
		}

		public bool HasRevision
		{
			get => point.IsRevised;
		}

		public bool IsBeingEdited
		{
			get => isBeingEdited;
			set
			{
				if (value == isBeingEdited) return;
				isBeingEdited = value;
				OnPropertyChange();
			}
		}

		public bool ControlPoint
		{
			get => controlPoint;
			private set { controlPoint = value; }
		}

		// values below are calculated and 
		// cannot be set from outside
		// event only insures UI gets the
		// updated value
		public double XΔ
		{
			get => _xΔ;
			private set
			{
				if (value.Equals(_xΔ)) return;
				_xΔ = value;
				OnPropertyChange();
			}
		}

		public double YΔ
		{
			get => _yΔ;
			private set
			{
				if (value.Equals(_yΔ)) return;
				_yΔ = value;
				OnPropertyChange();
			}
		}

		public double ZΔ
		{
			get => _zΔ;
			private set
			{
				if (value.Equals(_zΔ)) return;
				_zΔ = value;
				OnPropertyChange();
			}
		}

		public double XYΔ
		{
			get => _xyΔ;
			private set
			{
				if (value.Equals(_xyΔ)) return;
				_xyΔ = value;
				OnPropertyChange();
			}
		}

		public double XYZΔ
		{
			get => _xyzΔ;
			private set
			{
				if (value.Equals(_xyzΔ)) return;
				_xyzΔ = value;
				OnPropertyChange();
			}
		}

		public double Slope
		{
			get => _slope;
			private set
			{
				if (value.Equals(_slope)) return;
				_slope = value;
				OnPropertyChange();
			}
		}

		// all flags true - all values defined
		public bool IsConfigured => statusFlag == (UInt32) BitFlag.AllSet;

		public  string Message
		{
			get => message;
			private set
			{
				message = value;
				OnPropertyChange();
			}
		}

		public double this[string which]
		{
			get
			{
				switch (which)
				{
				case TopoPtsConsts.xTag:
					{
						return X;
					}
				case TopoPtsConsts.yTag:
					{
						return Y;
					}
				case TopoPtsConsts.zTag:
					{
						return Z;
					}
				}

				return double.NaN;
			}

			set
			{
				switch (which)
				{
				case TopoPtsConsts.xTag:
					{
						X = value;
						break;
					}
				case TopoPtsConsts.yTag:
					{
						Y = value;
						break;
					}
				case TopoPtsConsts.zTag:
					{
						Z = value;
						break;
					}
				}
			}
		}


	#endregion

	#region > public methods

		// update all of the computed values for this point
		// the XYZ2 values have been updated before this
		// adjusted to minimize property change events
		public void Update(int index, TopoPoint2 precedingPoint)
		{
			Debug.WriteLine("     | @ TopoPoint2| @ update|"
				+ " index| " + index + "\n");

			statusFlag = BitFlag.Reset();

			// any bad values - clear and return
			if (TestForNaN(point.X, precedingPoint.X) ||
				TestForNaN(point.Y, precedingPoint.Y) ||
				TestForNaN(point.Z, precedingPoint.Z))
			{
				Clear();
				return;
			}

			// update index
			if (Index != index) Index = index;

			//update X + XΔ  
			bool result1 = UpdateX(precedingPoint.X);

			//update Y + YΔ
			bool result2 = UpdateY(precedingPoint.Y);

			//update Z + ZΔ
			UpdateZ(precedingPoint.Z);

			// XYZ2 updated, XΔ, YΔ, ZΔ updated
			// need to update XYΔ?
			if (result1 || result2)
			{
				CalcXYΔ();
			}

			// assumption is that at least one
			// X or Y or Z changed
			CalcSlope();
		}

		public void Undo(string which)
		{
			point.Undo(which);

			OnPropertyChange(which);
		}
		
		public void Redo(string which)
		{
			point.Redo(which);

			OnPropertyChange(which);
		}

		public void Refresh()
		{
			foreach (string eventName in PropertyEvents)
			{
				OnPropertyChange(eventName);
			}
		}




	#endregion

	#region > private methods

		// update values based on the current
		// XYZ2 values and the prior point
		// and the prior point
		private bool UpdateX(double preceding)
		{
			if (point.X.Equals(preceding)) return false;

			StatusFlagSet(BitFlag.Xflag);
			UpdateXΔ(preceding);

			return true;
		}

		private bool UpdateY(double preceding)
		{
			if (point.Y.Equals(preceding)) return false;

			StatusFlagSet(BitFlag.Yflag);
			UpdateYΔ(preceding);

			return true;
		}

		private bool UpdateZ(double preceding)
		{
			if (point.Z.Equals(preceding)) return false;

			StatusFlagSet(BitFlag.Zflag);
			UpdateZΔ(preceding);

			return true;
		}

		// calc the x delta
		private void UpdateXΔ(double preceding)
		{
			XΔ = X - preceding;
			StatusFlagSet(BitFlag.XΔflag);
		}

		// calc the Y delta
		private void UpdateYΔ(double preceding)
		{
			YΔ = Y - preceding;
			StatusFlagSet(BitFlag.YΔflag);
		}

		// calc the Z delta
		private void UpdateZΔ(double preceding)
		{
			ZΔ = Z - preceding;
			StatusFlagSet(BitFlag.ZΔflag);
		}

		private bool CalcXYΔ()
		{
			if (TestForNaN(XΔ, YΔ))
			{
				XYΔ = Double.NaN;
				return false;
			}

			if (XΔ.Equals(0))
			{
				XYΔ = YΔ;
			}
			else if (YΔ.Equals(0))
			{
				XYΔ = XΔ;
			}
			else
			{
				XYΔ = Math.Sqrt(XΔ * XΔ + YΔ * YΔ);
			}

			StatusFlagSet(BitFlag.XYΔflag);

			return true;
		}

		private bool CalcSlope()
		{
			XYZΔ = XYΔ;

			StatusFlagSet(BitFlag.XYΔZflag);

			if (Double.IsNaN(ZΔ) || Double.IsNaN(XYΔ) || XYΔ.Equals(0))
			{
				Slope = Double.NaN;
				return false;
			}

			StatusFlagSet(BitFlag.Slopeflag);

			if (ZΔ.Equals(0))
			{

				Slope = 0;
				return true;
			}

			XYZΔ = Math.Sqrt(XYΔ * XYΔ + ZΔ * ZΔ);

			Slope = ZΔ / XYΔ;

			return true;
		}

		public void Clear()
		{
			Index = -1;
			point = new XYZ2();
			XΔ = Double.NaN;
			YΔ = Double.NaN;
			ZΔ = Double.NaN;
			XYΔ = Double.NaN;
			XYZΔ = Double.NaN;
			Slope = Double.NaN;

			isBeingEdited = false;
			controlPoint = false;
			statusFlag = 0;
		}

	#endregion

	#region > utility methods

		private void StatusFlagSet(UInt32 b)
		{
			statusFlag = statusFlag.Set(b);
		}

		private void StatusFlagUnSet(UInt32 b)
		{
			statusFlag = statusFlag.UnSet(b);
		}

		private void checkForNaN(double value, UInt32 b)
		{
			if (Double.IsNaN(value))
			{
				StatusFlagUnSet(b);
			}
			else
			{
				StatusFlagSet(b);
			}
		}

		private bool TestForNaN(double c, double p)
		{
			return Double.IsNaN(c) || Double.IsNaN(p);
		}

	#endregion

	#region > overriding members

		public override string ToString()
		{
			return (
				X.ToString("F4") + "," +
				Y.ToString("F4") + "," +
				Z.ToString("F4"));
		}

		public object Clone()
		{
			TopoPoint2 tp = new TopoPoint2();

			tp.X = point.X;
			tp.Y = point.Y;
			tp.Z = point.Z;

			tp.Index = index;

			tp.ControlPoint = controlPoint;

			tp.XΔ = _xΔ;
			tp.YΔ = _yΔ;
			tp.ZΔ = _zΔ;

			tp.XYΔ = _xyΔ;
			tp.XYZΔ = _xyzΔ;

			tp.Slope = _slope;

			tp.statusFlag = statusFlag;

			return tp;
		}

		public bool Equals(TopoPoint2 other)
		{
			return
				point.X.Equals(other.point.X) &&
				point.Y.Equals(other.point.Y) &&
				point.Z.Equals(other.point.Z);
		}

		public bool FullyEquals(TopoPoint2 other)
		{
			return
				point.X.Equals(other.point.X) &&
				point.Y.Equals(other.point.Y) &&
				point.Z.Equals(other.point.Z) &&
				XΔ.Equals(other.XΔ) &&
				YΔ.Equals(other.YΔ) &&
				ZΔ.Equals(other.ZΔ) &&
				XYΔ.Equals(other.XYΔ) &&
				Slope.Equals(other.Slope);
		}

	#endregion

	#region > event handler

	#if DEBUG
		private void PropChangedReceivedMessage(string who, string propname)
		{
			Debug.WriteLine("(get ) @ TopoPoint2.PropChanged| (" + who + " [" + propname + "] )");
		}
	#endif

		private void PointPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
	#if DEBUG
			PropChangedReceivedMessage("general", e.PropertyName);
	#endif

			OnPropertyChange(e.PropertyName);
		}

	#endregion

	#region > event providers

	#if DEBUG
		private void PropChangedSendMessage(string who, string membername)
		{
			Debug.WriteLine("(send) @ TopoPoint2.OnPropertyChange| (" + who + " [" + membername + "] )");
		}
	#endif

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
	#if DEBUG
			PropChangedSendMessage("general", memberName);
	#endif
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

	#endregion
	}
}