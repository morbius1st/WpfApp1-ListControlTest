#region + Using Directives

using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;

#endregion


// projname: WpfApp1_ListControlTest.TopoPts
// itemname: TopoPoint
// username: jeffs
// created:  5/21/2019 7:41:18 PM


namespace WpfApp1_ListControlTest.TopoPts
{
#region > preamble

	public static class BitFlag
	{
		public const UInt32 Xflag = 0x1;
		public const UInt32 Yflag = 0x2;
		public const UInt32 Zflag = 0x4;
		public const UInt32 XΔflag = 0x8;
		public const UInt32 YΔflag = 0x10;
		public const UInt32 ZΔflag = 0x20;
		public const UInt32 XYΔflag = 0x40;
		public const UInt32 Slopeflag = 0x80;

		private const UInt32 _allSet = Xflag | Yflag | Zflag | XΔflag | YΔflag | ZΔflag | XYΔflag | Slopeflag;

		public static UInt32 AllSet => (UInt32) _allSet;

		public static UInt32 Reset()
		{
			return 0;
		}

		public static UInt32 Set(this UInt32 value, UInt32 flag)
		{
			return value | (UInt32) flag;
		}

		public static UInt32 UnSet(this UInt32 value, UInt32 flag)
		{
			return value & ~(UInt32) flag;
		}

		public static bool IsSet(this UInt32 value, UInt32 flag)
		{
			return (value & (UInt32) flag) > 0;
		}
	}

	public struct XYZ : IEquatable<XYZ>
	{
		public double X;
		public double Y;
		public double Z;

		public XYZ(double x = Double.NaN, double y = Double.NaN, double z = Double.NaN)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public static XYZ Empty => new XYZ();

		public bool IsValid => !Double.IsNaN(X) &&
			!Double.IsNaN(Y) && !Double.IsNaN(Z);


		public bool Equals(XYZ other)
		{
			return
				X.Equals(other.X) &&
				Y.Equals(other.Y) &&
				Z.Equals(other.Z);
		}

		public override string ToString()
		{
			return (
				X.ToString("F4") + "," +
				Y.ToString("F4") + "," +
				Z.ToString("F4"));
		}
	}
	

#endregion

#region	> Specials

	public class TopoStartPoint : TopoPoint
	{
		public TopoStartPoint(XYZ xyz)
		{
			if (!xyz.IsValid) { throw new ArgumentException("Invalid Start Point"); }

			base.index = 0;
			base.point = xyz;
			base.controlPoint = true;
		}
	}

	public class TopoEndPoint : TopoPoint
	{
		public TopoEndPoint(XYZ xyz)
		{
			if (!xyz.IsValid) { throw new ArgumentException("Invalid End Point"); }

			base.index = -1;
			base.point = xyz;
			base.controlPoint = true;
		}
	}

#endregion

	public class TopoPoint : IEquatable<TopoPoint>, ICloneable, INotifyPropertyChanged
	{
	#region > internal fields

		private UInt32 statusFlag = 0;

		protected int index = -1;
		protected bool controlPoint = false;
		protected XYZ point = XYZ.Empty;
		private double _xΔ = Double.NaN;
		private double _yΔ = Double.NaN;
		private double _zΔ = Double.NaN;
		private double _xyΔ = Double.NaN;
		private double _slope = Double.NaN;

		private XYZ priorPoint = XYZ.Empty;

	#endregion

	#region > constructors

		public TopoPoint() { }

		public TopoPoint(XYZ xyz)
		{
			point = xyz;
		}

		//		public static  TopoPoint TerminationPoint(XYZ xyz)
		//		{
		//			return new TopoPoint(xyz);
		//		}

		//		public static TopoPoint TerminationPoint(XYZ xyz, TopoPoint priorTp)
		//		{
		//			return new TopoPoint(xyz, priorTp);
		//		}

	#endregion

	#region > public properties

		// update XY and / or Z all at one time
		// this reduces the number of events 
		// will make the ui a bit faster
		public XYZ XYZ
		{
			get { return point; }
			set
			{
				if (!value.Equals(point))
				{
					priorPoint = point;

					OnPropertyChange("XYZstart");

					if (!Double.IsNaN(value.X))
					{
						point.X = value.X;
						OnPropertyChange("X");
					}

					if (!Double.IsNaN(value.Y))
					{
						point.Y = value.Y;
						OnPropertyChange("Y");
					}

					if (!Double.IsNaN(value.Z))
					{
						point.Z = value.Z;
						OnPropertyChange("Z");
					}

					OnPropertyChange("XYZend");
				}
			}
		}


		// XYZ values can be set individually
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
					priorPoint = point;

					point.X = value;

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
					priorPoint = point;

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
					priorPoint = point;

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
				index = value;
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
				_xΔ = value;
				OnPropertyChange();
			}
		}

		public double YΔ
		{
			get => _yΔ;
			private set
			{
				_yΔ = value;
				OnPropertyChange();
			}
		}

		public double ZΔ
		{
			get => _zΔ;
			private set
			{
				_zΔ = value;
				OnPropertyChange();
			}
		}

		public double XYΔ
		{
			get => _xyΔ;
			private set
			{
				_xyΔ = value;
				OnPropertyChange();
			}
		}

		public double Slope
		{
			get => _slope;
			private set
			{
				_slope = value;
				OnPropertyChange();
			}
		}

		// all flags true - all values defined
		public bool IsConfigured => statusFlag == (UInt32) BitFlag.AllSet;

	#endregion

	#region > public methods

		// update all of the computed values for this point
		// the XYZ values have been updated before this
		// adjusted to minimize property change events
		public void Update(int index, TopoPoint precedingPoint)
		{
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

			// XYZ updated, XΔ, YΔ, ZΔ updated
			// need to update XYΔ?
			if (result1 || result2)
			{
				CalcXYΔ();
			}

			// assumption is that at least one
			// X or Y or Z changed
			CalcSlope();

			// clear the prior point
			priorPoint = XYZ.Empty;
		}

	#endregion

	#region > private methods

		// update values based on the current
		// XYZ values and the prior point
		// and the prior point
		private bool UpdateX(double preceding)
		{
			if (point.X.Equals(priorPoint.X)) return false;

			StatusFlagSet(BitFlag.Xflag);
			UpdateXΔ(preceding);

			return true;
		}

		private bool UpdateY(double preceding)
		{
			if (point.Y.Equals(priorPoint.Y)) return false;

			StatusFlagSet(BitFlag.Yflag);
			UpdateYΔ(preceding);

			return true;
		}

		private bool UpdateZ(double preceding)
		{
			if (point.Z.Equals(priorPoint.Z)) return false;

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

			//			OnPropertyChange("XYΔ");

			return true;
		}

		private bool CalcSlope()
		{
			if (ZΔ.Equals(0))
			{
				Slope = 0;
				return true;
			}

			if (Double.IsNaN(ZΔ) || Double.IsNaN(XYΔ) || XYΔ.Equals(0))
			{
				Slope = Double.NaN;
				return false;
			}

			Slope = ZΔ / XYΔ;

			//			OnPropertyChange("Slope");

			StatusFlagSet(BitFlag.Slopeflag);

			return true;
		}

		public void Clear()
		{
			Index = -1;
			point.X = Double.NaN;
			point.Y = Double.NaN;
			point.Z = Double.NaN;
			XΔ = Double.NaN;
			YΔ = Double.NaN;
			ZΔ = Double.NaN;
			Slope = Double.NaN;

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
			TopoPoint tp = new TopoPoint();

			tp.X = point.X;
			tp.Y = point.Y;
			tp.Z = point.Z;

			tp.Index = index;

			tp.ControlPoint = controlPoint;

			tp.XΔ = _xΔ;
			tp.YΔ = _yΔ;
			tp.ZΔ = _zΔ;

			tp.XYΔ = _xyΔ;

			tp.Slope = _slope;

			tp.statusFlag = statusFlag;

			return tp;
		}

		public bool Equals(TopoPoint other)
		{
			return
				point.X.Equals(other.point.X) &&
				point.Y.Equals(other.point.Y) &&
				point.Z.Equals(other.point.Z);
		}

		public bool FullyEquals(TopoPoint other)
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

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

	#endregion
	}
}