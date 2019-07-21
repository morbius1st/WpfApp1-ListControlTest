#region + Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UtilityLibrary;

#endregion


// projname: WpfApp1_ListControlTest.TopoPtsData3.Support
// itemname: XYZEnum
// username: jeffs
// created:  7/7/2019 10:50:56 PM


namespace WpfApp1_ListControlTest.TopoPtsData3.Support
{
	public enum Axes
	{
		X,
		Y,
		Z
	}



	public class TopoPtsTags : CsEnumBase<TopoPtsTags, byte, int>
	{
	#region > base enum

		// these names must match the member names
		private enum tags : byte
		{
			x = Axes.X,
			y = Axes.Y,
			z = Axes.Z,
			xStart = 3,
			yStart = 4,
			zStart = 5,
			xEnd = 6,
			yEnd = 7,
			zEnd = 8,
		}

	#endregion

	#region > ctor

		// v is the collection index

		private TopoPtsTags(tags x, int v, string p, Axes axis) : base(x, v)
		{
			base.Add(this);
			PropertyName = p;
			Axis = axis;
		}

	#endregion

		// no enum specific properties

		// no enum specific functions

		public string PropertyName { get; }

		public Axes Axis { get; private set; }

	#region > members

		public static readonly TopoPtsTags XStartTag = new TopoPtsTags(tags.xStart, 0, "X", Axes.X);
		public static readonly TopoPtsTags YStartTag = new TopoPtsTags(tags.yStart, 0, "Y", Axes.Y);
		public static readonly TopoPtsTags ZStartTag = new TopoPtsTags(tags.zStart, 0, "Z", Axes.Z);
		public static readonly TopoPtsTags XTag =      new TopoPtsTags(tags.x, 1, "X", Axes.X);
		public static readonly TopoPtsTags YTag =      new TopoPtsTags(tags.y, 1, "Y", Axes.Y);
		public static readonly TopoPtsTags ZTag =      new TopoPtsTags(tags.z, 1, "Z", Axes.Z);
		public static readonly TopoPtsTags XEndTag =   new TopoPtsTags(tags.xEnd, -1, "X", Axes.X);
		public static readonly TopoPtsTags YEndTag =   new TopoPtsTags(tags.yEnd, -1, "Y", Axes.Y);
		public static readonly TopoPtsTags ZEndTag =   new TopoPtsTags(tags.zEnd, -1, "Z", Axes.Z);

//		public static readonly TopoPtsTags XStartTag = new TopoPtsTags(nameof(XStartTag), tags.xStart, 0, "X", Axes.X);
//		public static readonly TopoPtsTags YStartTag = new TopoPtsTags(nameof(YStartTag), tags.yStart, 0, "Y", Axes.Y);
//		public static readonly TopoPtsTags ZStartTag = new TopoPtsTags(nameof(ZStartTag), tags.zStart, 0, "Z", Axes.Z);
//		public static readonly TopoPtsTags XTag =      new TopoPtsTags(nameof(XTag), tags.x, 1, "X", Axes.X);
//		public static readonly TopoPtsTags YTag =      new TopoPtsTags(nameof(YTag), tags.y, 1, "Y", Axes.Y);
//		public static readonly TopoPtsTags ZTag =      new TopoPtsTags(nameof(ZTag), tags.z, 1, "Z", Axes.Z);
//		public static readonly TopoPtsTags XEndTag =   new TopoPtsTags(nameof(XEndTag), tags.xEnd, -1, "X", Axes.X);
//		public static readonly TopoPtsTags YEndTag =   new TopoPtsTags(nameof(YEndTag), tags.yEnd, -1, "Y", Axes.Y);
//		public static readonly TopoPtsTags ZEndTag =   new TopoPtsTags(nameof(ZEndTag), tags.zEnd, -1, "Z", Axes.Z);

	#endregion
	}
}