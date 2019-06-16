#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


	
#endregion


// projname: WpfApp1_ListControlTest.TopoPtsData2.Support
// itemname: TopoPts2MgrSupport
// username: jeffs
// created:  6/9/2019 12:49:49 PM


namespace WpfApp1_ListControlTest.TopoPtsData2.Support
{
	public class TopoPts2MgrSupport
	{
		private double lastX;
		private double lastY;
		private double lastZ;


		public void LoadDesignData(TopoPoints2 Tps2)
		{
			lastX = 11001;
			lastY = 21002;
			lastZ = 31003;

			Tps2.Initialize(new TopoStartPoint(new XYZ2(lastX, lastY, lastZ)));

			CreatePoints2(9, Tps2);

			Tps2.Finalize(new TopoEndPoint(new XYZ2(lastX, lastY, lastZ)));
		}

		private double CalcZ(double x, double y, double slope)
		{
			double xy = Math.Sqrt(x * x + y * y);

			return xy * slope;
		} 

		private void CreatePoints2(int qty, TopoPoints2 tps2)
		{
			int x = 100;
			int y = 150;
			double slope = 0.03;

			lastX += x;
			lastY += y;
			lastZ += CalcZ(x, y, slope);

			for (int i = 1; i < qty; i++)
			{
				tps2.Add(new XYZ2(lastX, lastY, lastZ));

				x += i;
				y += i;

				lastX += x;
				lastY += y;
				lastZ += CalcZ(x, y, 0.03);
			}
		}



	}
}
