#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
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
