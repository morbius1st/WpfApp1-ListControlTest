#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1_ListControlTest.TopoPts.Support;
using WpfApp1_ListControlTest.TopoPtsData2.Support;

#endregion


// projname: WpfApp1_ListControlTest.TopoPtsData2
// itemname: TopoPts2Mgr
// username: jeffs
// created:  6/9/2019 12:46:14 PM


namespace WpfApp1_ListControlTest.TopoPtsData2
{
	// this manager (wrapper) will hold the topopoints collection and
	// all of the routines to work with the collection
	public class TopoPts2Mgr
	{
		public TopoPtsConsts TpConsts { get; private set; }
		public TopoPoints2 Tpts2 { get; set; }

		public TopoPts2MgrSupport TptSupport = new TopoPts2MgrSupport();

		public TopoPts2Mgr()
		{
			TpConsts = new TopoPtsConsts();

			Tpts2 = new TopoPoints2();
			
			TptSupport.LoadDesignData(Tpts2);

		}
	}
}
