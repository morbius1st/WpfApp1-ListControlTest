#region + Using Directives

using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;
using UtilityLibrary;
using WpfApp1_ListControlTest.TopoPtsData3.Support;
using WpfApp1_ListControlTest.TopoPtsData3.TopoPts3;
using WpfApp1_ListControlTest.TopoPtsData3.TopoPts3.Support;

#endregion


// projname: WpfApp1_ListControlTest.TopoPts
// itemname: TopoPointsTest
// username: jeffs
// created:  6/1/2019 7:19:31 AM


namespace WpfApp1_ListControlTest.TopoPtsData3
{
	public class TopoPointsTest3
	{
		private TopoPoint3 tp3Test = new TopoPoint3(new XYZ3(10201.0, 20201.0, 30201.0));

//		private double j = 10;
//		private double k = 50;
//		private double x;
//		private double y;
//		private double z;

		public TopoPoints3 Tpts3 { get; set; }
		public TopoPts3Mgr Tpts3Mgr { get; set; }

		public TopoPointsTest3(TopoPoints3 tps, TopoPts3Mgr tpts3Mgr)
		{
			this.Tpts3 = tps;
			this.Tpts3Mgr = tpts3Mgr;

			tps.Message = "initialized\n";
		}

	#region > misc tests


		public void ListTopoPtsTags()
		{
			foreach (Support.TopoPtsTags tag in Support.TopoPtsTags.Values())
			{
				Debug.Write("for tag name| ".PadLeft(20) + tag.Name.PadRight(11));
				Debug.Write("  ordinal| " + tag.Ordinal);
				Debug.Write("  axis name| " + tag.AxisName);
				Debug.Write("  amount| " + tag.Amount);
				Debug.Write(("  value| " + tag.Value).PadRight(11));
				Debug.Write(" location idx| " + (int) tag.LocationIndex);

				Debug.Write("\n");
			}
		}

	#endregion




		#region > Test All Tests

//		public void BtnTestAll_Click()
//		{
//			tps3.Append = "\n*** run all tests ***\n";
//			BtnAdd1_Click();
//			BtnInsert1_Click();
//			BtnChangeE_Click();
//			BtnChangeS_Click();
//			BtnChange4X_Click();
//			BtnChange4Y_Click();
//			BtnChange4Z_Click();
//		}

//		private double dx;
//		private double dy;
//		private double dz;
//
//		private string formatXYZ()
//		{
//			return $"{dx:F4}, {dy:F4}, {dz:F4}";
//		}
		public void DataNotLoaded()
		{
			Tpts3.Append = "\n*** FAIL: LOAD DATA FIRST ***\n";
		}


		public void BtnAdd10ToXofStart_Click()
		{
			if (!Tpts3Mgr.DataLoaded)
			{
				DataNotLoaded();
				return;
			}

			Tpts3.Append = "\n*** run test: Add 10 to start X ***\n\n";

			Tpts3[0].X+= 10;

			Tpts3.Append = "*** run test: Complete ***\n\n";
		}
		
		public void BtnAdd10ToXofEnd_Click()
		{
			if (!Tpts3Mgr.DataLoaded)
			{
				DataNotLoaded();
				return;
			}

			Tpts3.Append = "\n*** run test: Add 10 to start X ***\n\n";

			Tpts3[Tpts3.EndPointIndex].X+= 10;

			Tpts3.Append = "*** run test: Complete ***\n\n";
		}
		
		public void BtnAdd10ToZofStart_Click()
		{
			if (!Tpts3Mgr.DataLoaded)
			{
				DataNotLoaded();
				return;
			}

			Tpts3.Append = "\n*** run test: Add 10 to start Z ***\n\n";

			Tpts3[0].Z += 10;

			Tpts3.Append = "*** run test: Complete ***\n\n";
		}

		public void BtnAdd10ToZofEnd_Click()
		{
			if (!Tpts3Mgr.DataLoaded)
			{
				DataNotLoaded();
				return;
			}

			Tpts3.Append = "\n*** run test: Add 10 to end Z ***\n\n";

			Tpts3[Tpts3.EndPointIndex].Z += 10;

			Tpts3.Append = "*** run test: Complete ***\n\n";
		}

		public void BtnBatchAdd10ToYfrom3_Click()
		{
			if (!Tpts3Mgr.DataLoaded)
			{
				DataNotLoaded();
				return;
			}

			Tpts3.Append = "\n*** run test: batch Add 10 to [3+]Y ***\n";

			Tpts3Mgr.BatchIncreaseEachXyxByAmount(3, "Y", 10);

			Tpts3.Append = "\n*** run test: Complete ***\n\n";
		}
		
		public void BtnBatchAddAmtToYfrom3On_Click()
		{
			if (!Tpts3Mgr.DataLoaded)
			{
				DataNotLoaded();
				return;
			}

			int seed = 2;

			Tpts3.Append = "\n*** run test: batch Add Amt to [3+]Y ***\n";

			Tpts3.BatchBegin();

			for (int i = 3; i < Tpts3.Count; i++)
			{
				seed = (i * 3) + seed;

				Tpts3[i].Y += seed;
			}

			Tpts3.BatchFinalize();
			
			Tpts3.Append = "\n*** run test: Complete ***\n\n";
		}


		public void BtnBatchAdjustZByAmount_Click()
		{
			if (!Tpts3Mgr.DataLoaded)
			{
				DataNotLoaded();
				return;
			}

			Tpts3.Append = "\n*** run test: Batch Add amt to [3] Z to [end] ***\n";

			// change the Z delta for item3 to item 4
			int idx = 3;

			double adjDeltaZ = Tpts3[idx].ZΔ + 3;

			Tpts3.Append = "\n*** run test start: batch Adjust Z =  "
				+ adjDeltaZ.ToString() + "\n";

			bool result = AdjustZ(idx, adjDeltaZ);

			Tpts3.Append = "*** run test end: batch Adjust Z  = "
				+ result.ToString() + "\n\n";
		}

		public void BtnBatchAdjustZBySlope_Click()
		{
			if (!Tpts3Mgr.DataLoaded)
			{
				DataNotLoaded();
				return;
			}

			Tpts3.Append = "\n*** run test: batch Adjust Z [4] by Slope ***\n";

			// change the Z delta for item3 to item 4
			int idx = 4;

			double adjDeltaZ = Tpts3[idx].XYΔ * 0.05;

			Tpts3.Append = "\n*** run test start: batch Adjust Z (5%) =  "
				+ adjDeltaZ.ToString() + "\n";

			bool result = AdjustZ(idx, adjDeltaZ);

			Tpts3.Append = "*** run test end: batch Adjust Z = "
				+ result.ToString() + "\n\n";
		}

		// modify the Z value of [index] and
		// update the value of all subsequent Z values
		// by maintaining the original delta
		private bool AdjustZ(int index, double deltaZ)
		{
			if (index > Tpts3.IndexOfEndPoint ||
				index < 1 ||
				double.IsNaN(deltaZ)) return false;

			Tpts3.BatchBegin();

			// change the Z value of the indexed point
			// based on the Z value of the prior point
			// plus the new delta
			Tpts3[index].Z = Tpts3[index - 1].Z + deltaZ;

			// update the Z value of all subsequent points
			// based on the Z value of the prior point
			// plus the original delta
			for (int i = ++index; i < Tpts3.Count; i++)
			{
				Tpts3[i].Z = Tpts3[i - 1].Z + Tpts3[i].ZΔ;
			}

			Tpts3.BatchFinalize();

			return true;
		}

		private int a = 10;

		public void BtnChangeXof1_Click()
		{
			if (!Tpts3Mgr.DataLoaded)
			{
				DataNotLoaded();
				return;
			}

			Tpts3.Append = "\n*** run test: change X of [1] ***\n\n";

			Tpts3[1].X = 11101.0 + a;

			a += 10;
		}

		public void BtnDebugMarker_Click()
		{
			Tpts3.Append = "\n*** Debug Marker ***\n\n";
		}




//
//		// add 1
//		public void BtnAdd1_Click()
//		{
//			dx = x + j - 50;
//			dy = y + j - 50;
//			dz = z + j - 50;
//
//			tps3.Append = "\n*** run test add 1 (" + formatXYZ() + ") ***\n";
//			tps3.Add(new XYZ3(dx, dy, dz));
//
//			j += 10;
//		}
//
//		// insert 1
//		public void BtnInsert1_Click()
//		{
//			dx = 10501 + k;
//			dy = 20501 + k;
//			dz = 30501 + k;
//
//			tps3.Append = "\n*** run test insert 1 @ (5) (" + formatXYZ() + ")***\n";
//			tps3.Insert(5, new XYZ3(dx, dy, dz));
//
//			k -= 5;
//		}
//
//		// change end 1
//		public void BtnChangeE_Click()
//		{
//			tps3.Append = "\n*** run test change end ***\n";
//			tps3[tps3.EndPointIndex].XYZ = new XYZ3(10901.0, 20901.0, 30901.0);
//		}
//
//		// change start 1
//		public void BtnChangeS_Click()
//		{
//			tps3.Append = "\n*** run test change start ***\n";
//			tps3[0].XYZ = new XYZ3(9001.0, 19001.0, 29001.0);
//		}
//
//		// change value 1
//		public void BtnChange4X_Click()
//		{
//			tps3.Append = "\n*** run test change X @ (4) ***\n";
//			tps3[4].X = 10411.0;
//		}
//
//		// change end 2
//		public void BtnChange4Y_Click()
//		{
//			tps3.Append = "\n*** run test change Y @ (4) ***\n";
//			tps3[4].Y = 20421.0;
//		}
//
//		public void BtnChange4Z_Click()
//		{
//			tps3.Append = "\n*** run test change Z @ (4) ***\n";
//			tps3[4].Z = 30431.0;
//		}
//
		#endregion
//
//		public void BtnChangeStartX_Click()
//		{
//			tps3.Append = "\n*** run test change X @ (start) ***\n";
//			tps3[0].X = 8001.0;
//		}
//
//		public void BtnChangeEndX_Click()
//		{
//			tps3.Append = "\n*** run test change X @ (end) ***\n";
//			tps3[tps3.Count - 1].X = 13901.0;
//		}
//
//
//		public void BtnContains_Click()
//		{
//			tps3.Append = "\n*** run test contains ***\n";
//
//			bool result = tps3.Contains(testPoint);
//
//			if (result)
//			{
//				tps3.Append = "contains| found\n";
//			}
//			else
//			{
//				tps3.Append = "contains| NOT found\n";
//			}
//		}
//
//		public void BtnIndexOf_Click()
//		{
//			tps3.Append = "\n*** run test indexof ***\n";
//
//			int result = tps3.IndexOf(testPoint);
//
//			if (result >= 0)
//			{
//				tps3.Append = "contains| found| " + result.ToString() + "\n";
//			}
//			else
//			{
//				tps3.Append = "contains| NOT found\n";
//			}
//		}
//
//		public void BtnRemoveAt_Click()
//		{
//			int idx = 2;
//
//			tps3.Append = "\n*** run test: removeat (2) ***\n";
//
//			TopoPoint3 tp3 = tps3[idx];
//
//			tps3.RemoveAt(idx);
//
//			if (tps3.Contains(tp3))
//			{
//				tps3.Append = "remove at| NOT removed (fail)\n";
//			}
//			else
//			{
//				tps3.Append = "remove at| removed (pass)\n";
//			}
//		}
//
//		public void BtnRemove_Click()
//		{
//			tps3.Append = "\n*** run test: remove (2) ***\n";
//
//			bool result = tps3.Remove(testPoint);
//
//			if (!result)
//			{
//				tps3.Append = "remove at| NOT removed (fail)\n";
//			}
//			else
//			{
//				tps3.Append = "remove at| removed (pass)\n";
//			}
//		}
//
//		private TopoPoint3[] array;
//
//		public void BtnCopyTo_Click()
//		{
//			tps3.Append = "\n*** run test: copyto ***\n";
//
//			array = new TopoPoint3[tps3.Count];
//
//			tps3.CopyTo(array, 0);
//
//			StringBuilder sb = new StringBuilder("count| " + array.Length + "\n");
//
//			foreach (TopoPoint3 tp in array)
//			{
//				sb.AppendLine(
//					$"idx| {tp.Index,4:D}"
//					+ $" x| {tp.X,10:F2}"
//					+ $" y| {tp.Y,10:F2}"
//					+ $" z| {tp.Z,10:F2}"
//					+ $" Δx| {tp.XΔ,8:F2}"
//					+ $" Δy| {tp.YΔ,8:F2}"
//					+ $" Δz| {tp.ZΔ,8:F2}"
//					+ $" Δxy| {tp.XYΔ,10:F2}"
//					+ $" slope| {tp.Slope,10:F7}"
//					);
//			}
//
//			tps3.Append = sb.ToString();
//		}
//
//		public void BtnClear_Click()
//		{
//			tps3.Append = "\n*** run test: clear ***\n";
//
//			tps3.Clear();
//		}
//
//		public void BtnChange4XYZ_Click()
//		{
//			tps3.Append = "\n*** run test: change XYZ of [4] ***\n";
//
//			int idx = 4;
//
//			XYZ3 xyz = tps3[idx].XYZ;
//
//			xyz.X += 15;
//			xyz.Y += 17;
//			xyz.Z += 19;
//
//			tps3[idx].XYZ = xyz;
//		}
//
//		public void BtnBatch_Click()
//		{
//			tps3.Append = "\n*** run test: batch v1 (add 12 to each X starting at item 3 to item 5) ***\n";
//
//			int start = 3;
//			int end = 5;
//
//			XYZ3 xyz;
//
//			tps3.BatchBegin();
//
//			for (int i = start; i < end + 1; i++)
//			{
//				xyz = tps3[i].XYZ;
//
//				xyz.X += 12;
//
//				tps3[i].XYZ = xyz;
//			}
//
//			tps3.BatchFinalize();
//		}
//
//		public void BtnBatchv2_Click()
//		{
//			tps3.Append = "\n*** run test: batch v2 ***\n";
//
//			int start = 3;
//			int end = 4;
//
//			XYZ3 xyz;
//
//			tps3.BatchBegin();
//
//			for (int i = start; i < end + 1; i++)
//			{
//				xyz = tps3[i].XYZ;
//
//				xyz.X += 12;
//
//				tps3[i].XYZ = xyz;
//			}
//
//			tps3[4].Y = 22329.0;
//			tps3[6].Z = 33079.0;
//
//			tps3.BatchFinalize();
//		}

	}
}