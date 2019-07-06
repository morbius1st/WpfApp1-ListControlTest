#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


	
#endregion


// projname: WpfApp1_ListControlTest.TopoPtsData3.Support
// itemname: TopoPts3MgrSupport
// username: jeffs
// created:  6/9/2019 12:49:49 PM


namespace WpfApp1_ListControlTest.TopoPtsData3.Support
{
	public class TopoPts3MgrSupport
	{
		private double lastX;
		private double lastY;
		private double lastZ;

		private int xStart = 1001;
		private int yStart = 1101;

		public void LoadDesignData(TopoPoints3 Tps3)
		{
			lastX = 10299 + xStart;
			lastY = 20299 + yStart;
			lastZ = 31300;

			Tps3.Initialize(new TopoStartPoint(new XYZ3(lastX, lastY, lastZ)));

			CreatePoints3(9, Tps3);

			Tps3.Finalize(new TopoEndPoint(new XYZ3(lastX, lastY, lastZ)));
		}

		private double CalcZ(double x, double y, double slope)
		{
			double xy = Math.Sqrt(x * x + y * y);

			return xy * slope;
		} 

		private void CreatePoints3(int qty, TopoPoints3 tps3)
		{
			int x = xStart;
			int y = yStart;
			double slope = 0.03;

			lastX += x;
			lastY += y;
			lastZ += CalcZ(x, y, slope);

			for (int i = 1; i < qty; i++)
			{
				tps3.Add(new XYZ3(lastX, lastY, lastZ));

				lastX += x;
				lastY += y;
				lastZ += CalcZ(x, y, 0.03);
			}
		}



	}
}
