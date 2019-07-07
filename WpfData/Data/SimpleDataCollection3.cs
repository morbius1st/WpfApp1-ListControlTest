#region + Using Directives

using System.Collections.ObjectModel;

#endregion


// projname: WpfContextTest.Data
// itemname: SimpleData3Mgr
// username: jeffs
// created:  6/17/2019 9:49:36 PM


namespace WpfData.Data
{
	public class SimpleDataCollection3
	{

		private bool ChangeInProcess = false;

		public string Test2 { get; set; } = "test2";

		public ObservableCollection<SimpleData3> Dx3 { get; set; }

		public delegate void AdjFirstItem(int newIdx, SimpleData3 currentSd);
		public delegate void AdjMiddleItems(int newIdx, SimpleData3 currentSd, SimpleData3 priorSd);
		public delegate void AdjLastItem(int newIdx, SimpleData3 currentSd, SimpleData3 priorSd);

		public SimpleDataCollection3()
		{
			LoadData2();
		}

		private void LoadData2()
		{
			Dx3 = new ObservableCollection<SimpleData3>();

			SimpleData3 Sd3;

			Sd3 = new SimpleData3(0, "Uno", 100.1, 201.1, 1001, 2001);
			Sd3.PropertyChanged += Sd_PropertyChanged;
			Dx3.Add(Sd3);

			Sd3 = new SimpleData3(1, "Dos", 100.2, 201.2, 1002, 2002);
			Sd3.PropertyChanged += Sd_PropertyChanged;
			Dx3.Add(Sd3);

			Sd3 = new SimpleData3(2, "Tres", 100.3, 201.3, 1003, 2003);
			Sd3.PropertyChanged += Sd_PropertyChanged;
			Dx3.Add(Sd3);
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
			int newIdx = Dx3[0].Index + 10;

			ChangeInProcess = true;

			adjFirstItem?.Invoke(newIdx++, Dx3[0]);

			SimpleData3 sdPrior = Dx3[0];

			for (var i = 1; i < Dx3.Count - 1; i++)
			{
				adjMiddleItems?.Invoke(newIdx++, Dx3[i], sdPrior);

				sdPrior = Dx3[i];
			}

			adjLastItem?.Invoke(newIdx, Dx3[Dx3.Count - 1], sdPrior);

			ChangeInProcess = false;
		}

		private void Sd_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (!ChangeInProcess) Reindex();
		}
		

	}
}
