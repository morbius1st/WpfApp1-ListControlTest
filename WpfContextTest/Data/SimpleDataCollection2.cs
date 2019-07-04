#region + Using Directives
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
	public class SimpleDataCollection2
	{

		private bool ChangeInProcess = false;

		public string Test2 { get; set; } = "test2";

		public ObservableCollection<SimpleData> Dx { get; set; }

		public delegate void AdjFirstItem(int newIdx, SimpleData currentSd);
		public delegate void AdjMiddleItems(int newIdx, SimpleData currentSd, SimpleData priorSd);
		public delegate void AdjLastItem(int newIdx, SimpleData currentSd, SimpleData priorSd);

		public SimpleDataCollection2()
		{
			LoadData2();
		}

		private void LoadData2()
		{
			Dx = new ObservableCollection<SimpleData>();

			SimpleData Sd;

			Sd = new SimpleData(0, "Uno", 100.1, 201.1, 1001, 2001);
			Sd.PropertyChanged += Sd_PropertyChanged;
			Dx.Add(Sd);

			Sd = new SimpleData(1, "Dos", 100.2, 201.2, 1002, 2002);
			Sd.PropertyChanged += Sd_PropertyChanged;
			Dx.Add(Sd);

			Sd = new SimpleData(2, "Tres", 100.3, 201.3, 1003, 2003);
			Sd.PropertyChanged += Sd_PropertyChanged;
			Dx.Add(Sd);
		}

		private AdjFirstItem adjFirstItem;
		private AdjMiddleItems adjMiddleItems;
		private AdjLastItem adjLastItem;

		public AdjFirstItem AdjustFirstItem
		{
			set => adjFirstItem = value;
		}
		
		public AdjMiddleItems AdjustMiddleItems
		{
			set => adjMiddleItems = value;
		}
		
		public AdjLastItem AdjustLastItem
		{
			set => adjLastItem = value;
		}



		public void Reindex()
		{
			int newIdx = Dx[0].Index + 10;

			ChangeInProcess = true;

			adjFirstItem?.Invoke(newIdx++, Dx[0]);

			SimpleData sdPrior = Dx[0];

			for (var i = 1; i < Dx.Count - 1; i++)
			{
				adjMiddleItems?.Invoke(newIdx++, Dx[i], sdPrior);

				sdPrior = Dx[i];
			}

			adjLastItem?.Invoke(newIdx, Dx[Dx.Count - 1], sdPrior);

			ChangeInProcess = false;
		}

		private void Sd_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (!ChangeInProcess) Reindex();
		}
		

	}
}
