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
	public class TopoPtsTags : CsEnumBase<TopoPtsTags, byte, int>
	{
	#region > base enum

		private string tagName;

		private enum tags : byte
		{
			xStart = 0,
			yStart = 1,
			zStart = 2,
			x = 3,
			y = 4,
			z = 5,
			xEnd = 6,
			yEnd = 7,
			zEnd = 8
		}

	#endregion

	#region > ctor
		// v is the collection index

		private TopoPtsTags(tags x, int v, string s) : base(x, v)
		{
			tagName = s;

			members.Add(this);
		}

	#endregion

		// no enum specific properties

		// no enum specific functions

//		public override string ToString() => Enum.ToString();

		public new string Name => tagName;

	#region > members

		public static readonly TopoPtsTags XStartTag = new TopoPtsTags(tags.xStart, 0, "StartX");
		public static readonly TopoPtsTags YStartTag = new TopoPtsTags(tags.yStart, 0, "StartY");
		public static readonly TopoPtsTags ZStartTag = new TopoPtsTags(tags.zStart, 0, "StartZ");
		public static readonly TopoPtsTags XTag = new TopoPtsTags(tags.x, -1, "X");
		public static readonly TopoPtsTags YTag = new TopoPtsTags(tags.y, -1, "Y");
		public static readonly TopoPtsTags ZTag = new TopoPtsTags(tags.z, -1, "Z");
		public static readonly TopoPtsTags XEndTag = new TopoPtsTags(tags.xEnd, 0, "EndX");
		public static readonly TopoPtsTags YEndTag = new TopoPtsTags(tags.yEnd, 0, "EndY");
		public static readonly TopoPtsTags ZEndTag = new TopoPtsTags(tags.zEnd, 0, "EndZ");


	#endregion
	}




	public class Xyz : CsEnumBase<Xyz, byte, byte>
	{
	#region > base enum

		private enum xyz : byte
		{
			X = 0,
			Y = 1,
			Z = 2
		}

	#endregion

	#region > ctor

		private Xyz(xyz x, byte v) : base(x, v)
		{
			members.Add(this);
		}

	#endregion

		// no enum specific properties

		// no enum specific functions

		public override string ToString() => Enum.ToString();

	#region > members

		public static readonly Xyz XX = new Xyz(xyz.X, 0);
		public static readonly Xyz YY = new Xyz(xyz.Y, 1);
		public static readonly Xyz ZZ = new Xyz(xyz.Z, 2);
		

	#endregion
	}
}
