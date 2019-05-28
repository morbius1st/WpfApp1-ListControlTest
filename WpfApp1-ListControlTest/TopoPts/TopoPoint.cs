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

		public static UInt32 Set(this UInt32 value, UInt32 flag)
		{
			return value | (UInt32) flag;
		}

		public static UInt32 Clear(this UInt32 value, UInt32 flag)
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

	public static class XYZExtensions
	{
		public static XYZ Add(this XYZ xyz, double x, double y, double z)
		{
			return new XYZ(xyz.X + x, xyz.Y + y, xyz.Z + z);
		}
	}

	public class TopoPoint : IEquatable<TopoPoint>, INotifyPropertyChanged
	{
		private UInt32 statusFlag = 0;

		private int index = -1;
		private bool controlPoint = false;
		private XYZ point = XYZ.Empty;
		private double _xΔ = Double.NaN;
		private double _yΔ = Double.NaN;
		private double _zΔ = Double.NaN;
		private double xyΔ = Double.NaN;
		private double slope = Double.NaN;


		public XYZ XYZ => point;

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
			set
			{
				controlPoint = value;
				OnPropertyChange();
			}
		}
		public double X
		{
			get => point.X;
			set
			{
				point.X = value;
				OnPropertyChange();
			}
		}

		public double Y
		{
			get => point.Y;
			set
			{
				point.Y = value;
				OnPropertyChange();
			}
		}

		public double Z
		{
			get => point.Z;
			set
			{
				point.Z = value;
				OnPropertyChange();
			}
		}

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
			get => xyΔ;
			private set
			{
				xyΔ = value;
				OnPropertyChange();
			}
		}

		public double Slope
		{
			get => slope;
			private set
			{
				slope = value;
				OnPropertyChange();
			}
		}

		#region > constructors

		public TopoPoint(XYZ xyz, TopoPoint priorTp)
		{
			SetX(xyz.X, priorTp.X);
			SetY(xyz.Y, priorTp.Y);

			if (!SetZ(xyz.Z, priorTp.Z))
			{
				Clear();
			}
		}

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

		public bool IsValid => statusFlag == (UInt32)BitFlag.AllSet;
		public bool IsConfigured => statusFlag == (UInt32)BitFlag.AllSet && Index > -1;

		// update x values based on x = new x coordinate
		// and the prior point
		public bool SetX(double X, double priorX)
		{
			if (TestForNaN(X, priorX))
			{
				Clear();
				return false;
			}

			point.X = X;
			//			this.X = X;

			//			OnPropertyChange("X");

			StatusFlagSet(BitFlag.Xflag);

			SetXΔ(priorX);

			return CalcXYΔ() && CalcSlope();
		}

		public bool SetY(double Y, double priorY)
		{
			if (TestForNaN(Y, priorY))
			{
				Clear();
				return false;
			}

			point.Y = Y;
			//			this.Y = Y;

			//			OnPropertyChange("Y");

			StatusFlagSet(BitFlag.Yflag);

			SetYΔ(priorY);

			return CalcXYΔ() && CalcSlope();
		}

		public bool SetZ(double Z, double priorZ)
		{
			if (TestForNaN(Z, priorZ))
			{
				Clear();
				return false;
			}

			point.Z = Z;
			//			this.Z = Z;

			//			OnPropertyChange("Z");

			StatusFlagSet(BitFlag.Zflag);

			SetZΔ(priorZ);

			return CalcSlope();
		}

		// calc the x delta
		private void SetXΔ(double priorX)
		{
			XΔ = X - priorX;
			StatusFlagSet(BitFlag.XΔflag);

			//			OnPropertyChange("XΔ");
		}

		// calc the Y delta
		private void SetYΔ(double priorY)
		{
			YΔ = Y - priorY;
			StatusFlagSet(BitFlag.YΔflag);

			//			OnPropertyChange("YΔ");
		}

		// calc the Z delta
		private void SetZΔ(double priorZ)
		{
			ZΔ = Z - priorZ;
			StatusFlagSet(BitFlag.ZΔflag);

			//			OnPropertyChange("ZΔ");
		}

		public void Update(int index, TopoPoint priorTp)
		{
			// point (XYZ) is all that is valid
			// must update all other values

			Index = index;

			SetX(point.X, priorTp.X);
			SetY(point.Y, priorTp.Y);

			if (!SetZ(point.Z, priorTp.Z))
			{
				Clear();
			}
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

		private void StatusFlagSet(UInt32 b)
		{
			statusFlag = statusFlag.Set(b);
		}

		private bool TestForNaN(double c, double p)
		{
			return Double.IsNaN(c) || Double.IsNaN(p);
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

		#region > overriding members

		public override string ToString()
		{
			return (
				X.ToString("F4") + "," +
				Y.ToString("F4") + "," +
				Z.ToString("F4"));
		}

		public bool Equals(TopoPoint other)
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

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}
	}
}