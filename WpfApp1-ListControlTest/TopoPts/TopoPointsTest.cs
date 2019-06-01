#region + Using Directives
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

#endregion


// projname: WpfApp1_ListControlTest.TopoPts
// itemname: TopoPointsTest
// username: jeffs
// created:  6/1/2019 7:19:31 AM


namespace WpfApp1_ListControlTest.TopoPts
{
	public class TopoPointsTest
	{

		private double j = 10;
		private double k = 50;
		private double x;
		private double y;
		private double z;

		private TopoPoints tps;

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

			x += 100;
			y += 100;
			z += 100;

			tps.Finalize(new TopoEndPoint(new XYZ(x, y, z)));
		}

		private void NextPoint(double increase)
		{
			x += increase;
			y += increase;
			z += increase;

			tps.Add(new XYZ(x, y, z));
		}

		
		// add 1
		public void BtnAdd1_Click()
		{
			
			tps.message += "\n*** run test add 1 ***\n";
			tps.Add(new XYZ(x + j - 50, y + j - 50, z + j - 50));

			j += 10;
		}

		// insert 1
		public void BtnInsert1_Click()
		{
			tps.message += "\n*** run test insert 1 @ (5) ***\n";
			tps.Insert(5, new XYZ(10501 + k, 20501 + k, 30501 + k));

			k -= 5;
		}

		// change end 1
		public void BtnChangeE_Click()
		{
			tps.message += "\n*** run test change end ***\n";
			tps.EndXYZ = new XYZ(10901.0, 20901.0, 30901.0);

		}

		// change start 1
		public void BtnChangeS_Click()
		{
			tps.message += "\n*** run test change start ***\n";
			Debug.Write("\n*** run test change start ***\n");
			tps.StartXYZ = new XYZ(9001.0, 19001.0, 29001.0);
		}

		// change value 1
		public void BtnChange4X_Click()
		{
			tps.message += "\n*** run test change X @ (4) ***\n";
			tps[4].X = 10411.0;
		}

		// change end 2
		public void BtnChange4Y_Click()
		{
			tps.message += "\n*** run test change Y @ (4) ***\n";
			tps[4].Y = 20421.0;
		}

		public void BtnChange4Z_Click()
		{
			tps.message += "\n*** run test change Z @ (4) ***\n";
			tps[4].Z = 30431.0;
		}

		public void BtnChangeStartX_Click()
		{
			tps.message += "\n*** run test change X @ (start) ***\n";
			tps[0].X    = 8001.0;
		}

		public void BtnChangeEndX_Click()
		{
			tps.message += "\n*** run test change X @ (end) ***\n";
			tps[tps.Count - 1].X    = 11901.0;
		}

		public void BtnTestAll_Click()
		{
			tps.message += "\n*** run all tests ***\n";
			BtnAdd1_Click    ();
			BtnInsert1_Click ();
			BtnChangeE_Click ();
			BtnChangeS_Click ();
			BtnChange4X_Click();
			BtnChange4Y_Click();
			BtnChange4Z_Click();
		}

	}
}
