#region + Using Directives
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



#endregion


// projname: WpfContextTest.Data
// itemname: SimpleDataMgr
// username: jeffs
// created:  6/17/2019 9:49:36 PM


namespace WpfContextTest.Data
{
	public class SimpleDataMgr
	{
		public string Test1 { get; set; } = "test1";

		public ObservableCollection<SimpleData> Sx { get; set; }

		public SimpleDataMgr()
		{
			LoadData1();
		}

		private void LoadData1()
		{
			Sx = new ObservableCollection<SimpleData>();

			Sx.Add(new SimpleData() {PropertyS = "Alpha", PropertyD1 = 100.1, PropertyD2 = 201.1, PropertyI1 = 1001, PropertyI2 = 2001 }) ;
			Sx.Add(new SimpleData() {PropertyS = "Beta", PropertyD1 = 100.2, PropertyD2 = 201.2, PropertyI1 = 1002, PropertyI2 = 2002 }) ;
			Sx.Add(new SimpleData() {PropertyS = "Delta", PropertyD1 = 100.3, PropertyD2 = 201.3, PropertyI1 = 1003, PropertyI2 = 2003 }) ;
		}

	}
}
