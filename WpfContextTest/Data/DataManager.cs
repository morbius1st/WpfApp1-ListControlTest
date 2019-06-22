#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



#endregion


// projname: WpfContextTest.Data
// itemname: DataManager
// username: jeffs
// created:  6/18/2019 6:00:37 AM


namespace WpfContextTest.Data
{
	public class DataManager
	{
		public string TestDm { get; set; }  = "TestDm";

		public SimpleDataMgr2 Sm2 { get; set; }

		public DataManager()
		{
			Sm2 = new SimpleDataMgr2();
		}
	}
}
