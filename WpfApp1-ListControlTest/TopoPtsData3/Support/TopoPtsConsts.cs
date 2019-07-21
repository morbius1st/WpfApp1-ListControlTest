#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



#endregion


// projname: WpfApp1_ListControlTest.TopoPtsData3.Support
// itemname: TopoPtsConsts
// username: jeffs
// created:  7/21/2019 7:44:08 AM


namespace WpfApp1_ListControlTest.TopoPtsData3.Support
{
	public class TopoPtsConsts
	{
		// axes names
		public const string XaxisName  = "X";
		public const int    XaxisIndex = 0;

		public const string YaxisName = "Y";
		public const int    YaxisIndex = 1;

		public const string ZaxisName = "Z";
		public const int    ZaxisIndex = 2;


		// property names
		private const string StartPt = "StartPoint";

		public const string Idx = "Index";
		public const string Cp = "Cp";
		private const string X = "X";
		private const string Y = "Y";
		private const string Z = "Z";
		private const string XYZ = "XYZ";


		public const string StartPtIdx = StartPt + Idx;
		public const string StartPtCp = StartPt + Cp;
		public const string StartPtX = StartPt + X;
		public const string StartPtY = StartPt + Y;
		public const string StartPtZ = StartPt + Z;
		public const string StartPtXYZ = StartPt + XYZ;
	}
}
