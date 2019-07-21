#region + Using Directives
using UtilityLibrary;

using static WpfApp1_ListControlTest.TopoPtsData3.Support.TopoPtsConsts;

#endregion


// projname: WpfApp1_ListControlTest.TopoPtsData3.Support
// itemname: XYZEnum
// username: jeffs
// created:  7/7/2019 10:50:56 PM


namespace WpfApp1_ListControlTest.TopoPtsData3.Support
{

//	public class Axis : CsEnumBase<Axis, int, int>
//	{
//	#region > base enum
//
//		// these names must match the member names
//		public enum axis : int
//		{
//			X  = 0,
//			Y  = 1,
//			Z  = 2
//
//		}
//
//	#endregion
//
//	#region > ctor
//
//		// v is the collection index
//
//		private Axis(axis x, int v, string p) : base(x, v)
//		{
//			base.Add(this);
//			AxisName = p;
//		}
//
//	#endregion
//
//		// no enum specific properties
//
//		// no enum specific functions
//
//		public string  AxisName { get; private set; }
//
//	#region > members
//
//		public static readonly Axis X = new Axis(axis.X, (int) axis.X, "X");
//		public static readonly Axis Y = new Axis(axis.Y, (int) axis.Y, "Y");
//		public static readonly Axis Z = new Axis(axis.Z, (int) axis.Z, "Z");
//
//
//	#endregion
//	}
	
	public class TopoPtsTags : CsEnumBase<TopoPtsTags, byte, int>
	{
		public enum LocationIdx
		{
			START = -1,
			FIELD = 0,
			END   = 1
		}


	#region > base enum

		// these names must match the member names
		private enum tags : byte
		{
			XTag      = XaxisIndex,
			YTag      = YaxisIndex,
			ZTag      = ZaxisIndex,
			XStartTag = 3,
			YStartTag = 4,
			ZStartTag = 5,
			XEndTag   = 6,
			YEndTag   = 7,
			ZEndTag   = 8,
		}

	#endregion

	#region > ctor

		// v is the collection index

		private TopoPtsTags(tags x, int v, string p, LocationIdx r) : base(x, v)
		{
			base.Add(this);
			AxisName = p;
			LocationIndex = r;
		}

	#endregion

		// no enum specific properties

		// no enum specific functions

		public string AxisName { get; }

		public LocationIdx LocationIndex { get; }

	#region > members

		public static readonly TopoPtsTags XStartTag = new TopoPtsTags(tags.XStartTag, XaxisIndex, XaxisName, LocationIdx.START);
		public static readonly TopoPtsTags YStartTag = new TopoPtsTags(tags.YStartTag, YaxisIndex, YaxisName, LocationIdx.START);
		public static readonly TopoPtsTags ZStartTag = new TopoPtsTags(tags.ZStartTag, ZaxisIndex, ZaxisName, LocationIdx.START);
		public static readonly TopoPtsTags XTag      = new TopoPtsTags(tags.XTag,      XaxisIndex, XaxisName, LocationIdx.FIELD);
		public static readonly TopoPtsTags YTag      = new TopoPtsTags(tags.YTag,      YaxisIndex, YaxisName, LocationIdx.FIELD);
		public static readonly TopoPtsTags ZTag      = new TopoPtsTags(tags.ZTag,      ZaxisIndex, ZaxisName, LocationIdx.FIELD);
		public static readonly TopoPtsTags XEndTag   = new TopoPtsTags(tags.XEndTag,   XaxisIndex, XaxisName, LocationIdx.END);
		public static readonly TopoPtsTags YEndTag   = new TopoPtsTags(tags.YEndTag,   YaxisIndex, YaxisName, LocationIdx.END);
		public static readonly TopoPtsTags ZEndTag   = new TopoPtsTags(tags.ZEndTag,   ZaxisIndex, ZaxisName, LocationIdx.END);

	#endregion
	}


}