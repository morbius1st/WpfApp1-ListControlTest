// Solution:     WpfApp1-ListControlTest
// Project:       WpfApp1-ListControlTest
// File:             TopoPoint3Support.cs
// Created:      -- ()

using System;
using System.Security.Cryptography;

namespace WpfApp1_ListControlTest.TopoPtsData3.TopoPts3.Support 
{
	public static class Update
	{
		internal static bool UpdateXΔ(TopoPoint3 tp3, TopoPoint3 precedingTp3)
		{
			if (!tp3.XYZ.NeedsUpdatingX 
				&& !precedingTp3.XYZ.NeedsUpdatingX
				) return false;
			tp3.XΔ = tp3.X - precedingTp3.X;
			return true;
		}

		internal static bool UpdateYΔ(TopoPoint3 tp3, TopoPoint3 precedingTp3)
		{
			if (!tp3.XYZ.NeedsUpdatingY
				&& !precedingTp3.XYZ.NeedsUpdatingY
				) return false;
			tp3.YΔ = tp3.Y - precedingTp3.Y;
			return true;
		}

		internal static bool UpdateZΔ(TopoPoint3 tp3, TopoPoint3 precedingTp3)
		{
			if (!tp3.XYZ.NeedsUpdatingZ
				&& !precedingTp3.XYZ.NeedsUpdatingZ
				) return false;
			tp3.ZΔ = tp3.Z - precedingTp3.Z;
			return true;
		}

		internal static void CalcXYΔ(TopoPoint3 tp3)
		{
			if (double.IsNaN(tp3.XΔ) || double.IsNaN(tp3.YΔ))
			{
				tp3.XYΔ = double.NaN;
				return;
			}

			if (tp3.XΔ.Equals(0))
			{
				tp3.XYΔ = tp3.YΔ;
			} else if (tp3.YΔ.Equals(0))
			{
				tp3.XYΔ = tp3.XΔ;
			}
			else
			{
				tp3.XYΔ = Math.Sqrt(tp3.XΔ * tp3.XΔ + tp3.YΔ * tp3.YΔ);
			}
		}

		internal static void CalcSlope(TopoPoint3 tp3)
		{
			if (double.IsNaN(tp3.ZΔ)
				|| double.IsNaN(tp3.XYΔ)
				|| tp3.XYΔ.Equals(0)
				)
			{
				tp3.XYZΔ = tp3.XYΔ;
				tp3.Slope = double.NaN;
				return;
			}

			if (tp3.ZΔ.Equals(0))
			{
				tp3.XYZΔ = tp3.XYΔ;
				tp3.Slope = 0;
				return;
			}

			tp3.XYZΔ = Math.Sqrt(tp3.XYΔ * tp3.XYΔ + tp3.ZΔ * tp3.ZΔ);

			tp3.Slope = tp3.ZΔ / tp3.XYΔ;
		}
	}

	public static class BitFlag
	{
		public const UInt32 Xflag = 0x1;
		public const UInt32 Yflag = 0x2;
		public const UInt32 Zflag = 0x4;
		public const UInt32 XΔflag = 0x8;
		public const UInt32 YΔflag = 0x10;
		public const UInt32 ZΔflag = 0x20;
		public const UInt32 XYΔflag = 0x40;
		public const UInt32 XYΔZflag = 0x80;
		public const UInt32 Slopeflag = 0x100;

		public static UInt32 AllSet => (UInt32) Xflag | Yflag | Zflag | XΔflag | YΔflag | ZΔflag | XYΔflag | XYΔZflag | Slopeflag;

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

	
}