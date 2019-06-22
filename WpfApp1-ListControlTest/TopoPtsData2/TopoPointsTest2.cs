#region + Using Directives

using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;

#endregion


// projname: WpfApp1_ListControlTest.TopoPts
// itemname: TopoPointsTest
// username: jeffs
// created:  6/1/2019 7:19:31 AM


namespace WpfApp1_ListControlTest.TopoPtsData2
{
	public class TopoPointsTest2
	{
		private TopoPoint2 testPoint = new TopoPoint2(new XYZ2(10201.0, 20201.0, 30201.0));

//		private double j = 10;
//		private double k = 50;
//		private double x;
//		private double y;
//		private double z;

		public TopoPoints2 tps2 { get; set; }

		public TopoPointsTest2(TopoPoints2 tps)
		{
			this.tps2 = tps;

			tps.Message = "initialized\n";
		}
//
//		public void CreateData()
//		{
//			x = 12001.0;
//			y = 22001.0;
//			z = 32001.0;
//
//			tps2.Initialize(new TopoStartPoint(new XYZ2(x, y, z)));
//
//			NextPoint(100,10); // 1 (12101.0...)
//			NextPoint(100,10); // 2 (12201.0...)
//			NextPoint(100,10); // 3 (12301.0...)
//			NextPoint(100,10); // 4 (12401.0...)
//			NextPoint(100,10); // 5 (12501.0...)
//			NextPoint(100,10); // 6 (12601.0...)
//
//			tps2.Finalize(new TopoEndPoint(new XYZ2(12801.0, 22801.0, 32801.0)));
//		}
//
//		private void NextPoint(double xy_delta, double z_delta)
//		{
//			x += xy_delta;
//			y += xy_delta;
//			z += z_delta;
//
//			tps2.Add(new XYZ2(x, y, z));
//		}


		#region > Test All Tests

//		public void BtnTestAll_Click()
//		{
//			tps2.append = "\n*** run all tests ***\n";
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


		public void BtnBatchAdjustZByAmount_Click()
		{
			tps2.append = "\n*** run test: batch Adjust Z [3] by Amount ***\n";

			// change the Z delta for item3 to item 4
			int idx = 3;

			double adjDeltaZ = tps2[idx].ZΔ + 3;

			tps2.append = "\n*** run test: batch Adjust Z =  "
				+ adjDeltaZ.ToString();

			bool result = AdjustZ(idx, adjDeltaZ);

			tps2.append = "*** run test: batch Adjust Z  = "
				+ result.ToString();
		}

		public void BtnBatchAdjustZBySlope_Click()
		{
			tps2.append = "\n*** run test: batch Adjust Z [4] by Slope ***\n";

			// change the Z delta for item3 to item 4
			int idx = 4;

			double adjDeltaZ = tps2[idx].XYΔ * 0.05;

			tps2.append = "\n*** run test: batch Adjust Z (5%) =  "
				+ adjDeltaZ.ToString();

			bool result = AdjustZ(idx, adjDeltaZ);

			tps2.append = "*** run test: batch Adjust Z = "
				+ result.ToString();
		}

		// modify the Z value of [index] and
		// update the value of all subsequent Z values
		// by maintaining the original delta
		public bool AdjustZ(int index, double deltaZ)
		{
			if (index > tps2.EndIdx ||
				index < 1 ||
				double.IsNaN(deltaZ)) return false;

			tps2.BatchBegin();

			// change the Z value of the indexed point
			// based on the Z value of the prior point
			// plus the new delta
			tps2[index].Z = tps2[index - 1].Z + deltaZ;

			// update the Z value of all subsequent points
			// based on the Z value of the prior point
			// plus the original delta
			for (int i = ++index; i < tps2.Count; i++)
			{
				tps2[i].Z = tps2[i - 1].Z + tps2[i].ZΔ;
			}

			tps2.BatchFinalize();

			return true;
		}

		private int a = 10;

		public void BtnChangeXof1_Click()
		{
			tps2.append = "\n*** run test: change X of [1] ***\n\n";

			tps2[1].X = 11101.0 + a;

			a += 10;
		}



//
//		// add 1
//		public void BtnAdd1_Click()
//		{
//			dx = x + j - 50;
//			dy = y + j - 50;
//			dz = z + j - 50;
//
//			tps2.append = "\n*** run test add 1 (" + formatXYZ() + ") ***\n";
//			tps2.Add(new XYZ2(dx, dy, dz));
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
//			tps2.append = "\n*** run test insert 1 @ (5) (" + formatXYZ() + ")***\n";
//			tps2.Insert(5, new XYZ2(dx, dy, dz));
//
//			k -= 5;
//		}
//
//		// change end 1
//		public void BtnChangeE_Click()
//		{
//			tps2.append = "\n*** run test change end ***\n";
//			tps2[tps2.EndPointIndex].XYZ = new XYZ2(10901.0, 20901.0, 30901.0);
//		}
//
//		// change start 1
//		public void BtnChangeS_Click()
//		{
//			tps2.append = "\n*** run test change start ***\n";
//			tps2[0].XYZ = new XYZ2(9001.0, 19001.0, 29001.0);
//		}
//
//		// change value 1
//		public void BtnChange4X_Click()
//		{
//			tps2.append = "\n*** run test change X @ (4) ***\n";
//			tps2[4].X = 10411.0;
//		}
//
//		// change end 2
//		public void BtnChange4Y_Click()
//		{
//			tps2.append = "\n*** run test change Y @ (4) ***\n";
//			tps2[4].Y = 20421.0;
//		}
//
//		public void BtnChange4Z_Click()
//		{
//			tps2.append = "\n*** run test change Z @ (4) ***\n";
//			tps2[4].Z = 30431.0;
//		}
//
		#endregion
//
//		public void BtnChangeStartX_Click()
//		{
//			tps2.append = "\n*** run test change X @ (start) ***\n";
//			tps2[0].X = 8001.0;
//		}
//
//		public void BtnChangeEndX_Click()
//		{
//			tps2.append = "\n*** run test change X @ (end) ***\n";
//			tps2[tps2.Count - 1].X = 13901.0;
//		}
//
//
//		public void BtnContains_Click()
//		{
//			tps2.append = "\n*** run test contains ***\n";
//
//			bool result = tps2.Contains(testPoint);
//
//			if (result)
//			{
//				tps2.append = "contains| found\n";
//			}
//			else
//			{
//				tps2.append = "contains| NOT found\n";
//			}
//		}
//
//		public void BtnIndexOf_Click()
//		{
//			tps2.append = "\n*** run test indexof ***\n";
//
//			int result = tps2.IndexOf(testPoint);
//
//			if (result >= 0)
//			{
//				tps2.append = "contains| found| " + result.ToString() + "\n";
//			}
//			else
//			{
//				tps2.append = "contains| NOT found\n";
//			}
//		}
//
//		public void BtnRemoveAt_Click()
//		{
//			int idx = 2;
//
//			tps2.append = "\n*** run test: removeat (2) ***\n";
//
//			TopoPoint2 tx = tps2[idx];
//
//			tps2.RemoveAt(idx);
//
//			if (tps2.Contains(tx))
//			{
//				tps2.append = "remove at| NOT removed (fail)\n";
//			}
//			else
//			{
//				tps2.append = "remove at| removed (pass)\n";
//			}
//		}
//
//		public void BtnRemove_Click()
//		{
//			tps2.append = "\n*** run test: remove (2) ***\n";
//
//			bool result = tps2.Remove(testPoint);
//
//			if (!result)
//			{
//				tps2.append = "remove at| NOT removed (fail)\n";
//			}
//			else
//			{
//				tps2.append = "remove at| removed (pass)\n";
//			}
//		}
//
//		private TopoPoint2[] array;
//
//		public void BtnCopyTo_Click()
//		{
//			tps2.append = "\n*** run test: copyto ***\n";
//
//			array = new TopoPoint2[tps2.Count];
//
//			tps2.CopyTo(array, 0);
//
//			StringBuilder sb = new StringBuilder("count| " + array.Length + "\n");
//
//			foreach (TopoPoint2 tp in array)
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
//			tps2.append = sb.ToString();
//		}
//
//		public void BtnClear_Click()
//		{
//			tps2.append = "\n*** run test: clear ***\n";
//
//			tps2.Clear();
//		}
//
//		public void BtnChange4XYZ_Click()
//		{
//			tps2.append = "\n*** run test: change XYZ of [4] ***\n";
//
//			int idx = 4;
//
//			XYZ2 xyz = tps2[idx].XYZ;
//
//			xyz.X += 15;
//			xyz.Y += 17;
//			xyz.Z += 19;
//
//			tps2[idx].XYZ = xyz;
//		}
//
//		public void BtnBatch_Click()
//		{
//			tps2.append = "\n*** run test: batch v1 (add 12 to each X starting at item 3 to item 5) ***\n";
//
//			int start = 3;
//			int end = 5;
//
//			XYZ2 xyz;
//
//			tps2.BatchBegin();
//
//			for (int i = start; i < end + 1; i++)
//			{
//				xyz = tps2[i].XYZ;
//
//				xyz.X += 12;
//
//				tps2[i].XYZ = xyz;
//			}
//
//			tps2.BatchFinalize();
//		}
//
//		public void BtnBatchv2_Click()
//		{
//			tps2.append = "\n*** run test: batch v2 ***\n";
//
//			int start = 3;
//			int end = 4;
//
//			XYZ2 xyz;
//
//			tps2.BatchBegin();
//
//			for (int i = start; i < end + 1; i++)
//			{
//				xyz = tps2[i].XYZ;
//
//				xyz.X += 12;
//
//				tps2[i].XYZ = xyz;
//			}
//
//			tps2[4].Y = 22329.0;
//			tps2[6].Z = 33079.0;
//
//			tps2.BatchFinalize();
//		}

	}
}