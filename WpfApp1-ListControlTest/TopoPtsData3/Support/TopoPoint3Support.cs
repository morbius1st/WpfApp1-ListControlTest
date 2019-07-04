// Solution:     WpfApp1-ListControlTest
// Project:       WpfApp1-ListControlTest
// File:             TopoPoint3Support.cs
// Created:      -- ()

using System;

namespace WpfApp1_ListControlTest.TopoPtsData3.Support 
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