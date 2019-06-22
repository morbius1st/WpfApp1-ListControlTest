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
	public class SimpleDataMgr2
	{
		public string Test2 { get; set; } = "test2";

		public ObservableCollection<SimpleData> Dx { get; set; }

		public SimpleDataMgr2()
		{
			LoadData2();
		}

		private void LoadData2()
		{
			Dx = new ObservableCollection<SimpleData>();

			Dx.Add(new SimpleData() {PropertyS = "Uno", PropertyD1 = 100.1, PropertyD2 = 201.1, PropertyI1 = 1001, PropertyI2 = 2001 }) ;
			Dx.Add(new SimpleData() {PropertyS = "Dos", PropertyD1 = 100.2, PropertyD2 = 201.2, PropertyI1 = 1002, PropertyI2 = 2002 }) ;
			Dx.Add(new SimpleData() {PropertyS = "Tres", PropertyD1 = 100.3, PropertyD2 = 201.3, PropertyI1 = 1003, PropertyI2 = 2003 }) ;
		}

	}
}
