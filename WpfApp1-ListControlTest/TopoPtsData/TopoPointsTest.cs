#region + Using Directives

using System.Text;

#endregion


// projname: WpfApp1_ListControlTest.TopoPts
// itemname: TopoPointsTest
// username: jeffs
// created:  6/1/2019 7:19:31 AM


namespace WpfApp1_ListControlTest.TopoPtsData
{
	public class TopoPointsTest
	{
		private TopoPoint testPoint = new TopoPoint(new XYZ(10201.0, 20201.0, 30201.0));

		private double j = 10;
		private double k = 50;
		private double x;
		private double y;
		private double z;

		public TopoPoints tps { get; set; }

		public TopoPointsTest(TopoPoints tps)
		{
			this.tps = tps;

			tps.message = "initialized\n";
		}

		public void CreateData()
		{
			x = 10001.0;
			y = 20001.0;
			z = 30001.0;

			tps.Initialize(new TopoStartPoint(new XYZ(x, y, z)));

			NextPoint(100); // 1 (10101.0...)
			NextPoint(100); // 2 (10201.0...)
			NextPoint(100); // 3 (10301.0...)
			NextPoint(100); // 4 (10401.0...)
			NextPoint(100); // 5 (10501.0...)
			NextPoint(100); // 6 (10601.0...)

			tps.Finalize(new TopoEndPoint(new XYZ(10801.0, 20801.0, 30801.0)));
		}

		private void NextPoint(double increase)
		{
			x += increase;
			y += increase;
			z += increase;

			tps.Add(new XYZ(x, y, z));
		}


	#region > Test All Tests

		public void BtnTestAll_Click()
		{
			tps.append = "\n*** run all tests ***\n";
			BtnAdd1_Click    ();
			BtnInsert1_Click ();
			BtnChangeE_Click ();
			BtnChangeS_Click ();
			BtnChange4X_Click();
			BtnChange4Y_Click();
			BtnChange4Z_Click();
		}

		private double dx;
		private double dy;
		private double dz;

		private string formatXYZ()
		{
			return $"{dx:F4}, {dy:F4}, {dz:F4}";
		}
		
		// add 1
		public void BtnAdd1_Click()
		{
			dx = x + j - 50;
			dy = y + j - 50;
			dz = z + j - 50;

			tps.append = "\n*** run test add 1 (" + formatXYZ() + ") ***\n";
			tps.Add(new XYZ(dx,dy, dz));

			j += 10;
		}

		// insert 1
		public void BtnInsert1_Click()
		{
			dx = 10501 + k;
			dy = 20501 + k;
			dz = 30501 + k;

			tps.append = "\n*** run test insert 1 @ (5) (" + formatXYZ() + ")***\n";
			tps.Insert(5, new XYZ(dx, dy, dz));

			k -= 5;
		}

		// change end 1
		public void BtnChangeE_Click()
		{
			tps.append = "\n*** run test change end ***\n";
			tps[tps.EndPointIndex].XYZ  =  new XYZ(10901.0, 20901.0, 30901.0);

		}

		// change start 1
		public void BtnChangeS_Click()
		{
			tps.append = "\n*** run test change start ***\n";
			tps[0].XYZ = new XYZ(9001.0, 19001.0, 29001.0);
		}

		// change value 1
		public void BtnChange4X_Click()
		{
			tps.append = "\n*** run test change X @ (4) ***\n";
			tps[4].X    =  10411.0;
		}

		// change end 2
		public void BtnChange4Y_Click()
		{
			tps.append = "\n*** run test change Y @ (4) ***\n";
			tps[4].Y    =  20421.0;
		}

		public void BtnChange4Z_Click()
		{
			tps.append = "\n*** run test change Z @ (4) ***\n";
			tps[4].Z    =  30431.0;
		}

	#endregion

		public void BtnChangeStartX_Click()
		{
			tps.append = "\n*** run test change X @ (start) ***\n";
			tps[0].X    = 8001.0;
		}

		public void BtnChangeEndX_Click()
		{
			tps.append = "\n*** run test change X @ (end) ***\n";
			tps[tps.Count - 1].X    = 11901.0;
		}


		public void BtnContains_Click()
		{
			tps.append = "\n*** run test contains ***\n";

			bool result = tps.Contains(testPoint);

			if (result)
			{
				tps.append = "contains| found\n";
			}
			else
			{
				tps.append = "contains| NOT found\n";
			}
		}		

		public void BtnIndexOf_Click()
		{
			tps.append = "\n*** run test indexof ***\n";

			int result = tps.IndexOf(testPoint);

			if (result >= 0)
			{
				tps.append = "contains| found| " + result.ToString() + "\n";
			}
			else
			{
				tps.append = "contains| NOT found\n";
			}
		}

		public void BtnRemoveAt_Click()
		{
			int idx = 2;

			tps.append = "\n*** run test: removeat (2) ***\n";

			TopoPoint tx = tps[idx];

			tps.RemoveAt(idx);

			if (tps.Contains(tx))
			{
				tps.append = "remove at| NOT removed (fail)\n";
			}
			else
			{
				tps.append = "remove at| removed (pass)\n";
			}
		}

		public void BtnRemove_Click()
		{
			tps.append = "\n*** run test: remove (2) ***\n";

			bool result = tps.Remove(testPoint);

			if (!result)
			{
				tps.append = "remove at| NOT removed (fail)\n";
			}
			else
			{
				tps.append = "remove at| removed (pass)\n";
			}
		}

		private TopoPoint[] array;

		public void BtnCopyTo_Click()
		{
			tps.append = "\n*** run test: copyto ***\n";

			array = new TopoPoint[tps.Count];

			tps.CopyTo(array, 0);

			StringBuilder sb = new StringBuilder("count| " + array.Length + "\n");

			foreach (TopoPoint tp in array)
			{
				sb.AppendLine(
					$"idx| {tp.Index     ,4:D}"
					+ $" x| {tp.X        ,10:F2}"
					+ $" y| {tp.Y        ,10:F2}"
					+ $" z| {tp.Z        ,10:F2}"
					+ $" Δx| {tp.XΔ      ,8:F2}"
					+ $" Δy| {tp.YΔ      ,8:F2}"
					+ $" Δz| {tp.ZΔ      ,8:F2}"
					+ $" Δxy| {tp.XYΔ    ,10:F2}"
					+ $" slope| {tp.Slope,10:F7}"
					);
			}
			tps.append = sb.ToString();
		}

		public void BtnClear_Click()
		{
			tps.append = "\n*** run test: clear ***\n";

			tps.Clear();
		}

		public void BtnChange4XYZ_Click()
		{
			tps.append = "\n*** run test: change XYZ of [4] ***\n";

			int idx = 4;

			XYZ xyz = tps[idx].XYZ;

			xyz.X += 15;
			xyz.Y += 17;
			xyz.Z += 19;

			tps[idx].XYZ = xyz;

		}

		public void BtnBatch_Click()
		{
			tps.append = "\n*** run test: batch v1 (add 12 to each X starting at item 3 to item 5) ***\n";

			int start = 3;
			int end = 5;

			XYZ xyz;

			tps.BatchBegin();

			for (int i = start; i < end + 1; i++)
			{
				xyz = tps[i].XYZ;

				xyz.X += 12;

				tps[i].XYZ = xyz;
			}
			tps.BatchFinalize();
		}

		public void BtnBatchv2_Click()
		{
			tps.append = "\n*** run test: batch v2 ***\n";

			int start = 3;
			int end = 4;

			XYZ xyz;

			tps.BatchBegin();

			for (int i = start; i < end + 1; i++)
			{
				xyz = tps[i].XYZ;

				xyz.X += 12;

				tps[i].XYZ = xyz;
			}

			tps[4].Y = 20329.0;
			tps[6].Z = 37079.0;

			tps.BatchFinalize();
		}




	}
}
