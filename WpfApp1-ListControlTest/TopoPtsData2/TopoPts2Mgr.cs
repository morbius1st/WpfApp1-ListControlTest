#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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

		private TopoPts2MgrSupport TptSupport = new TopoPts2MgrSupport();

		public TopoPts2Mgr()
		{
			TpConsts = new TopoPtsConsts();

			Tpts2 = new TopoPoints2();

		}

		public bool DataLoaded { get; private set; } = false;

		public void LoadData()
		{
			TptSupport.LoadDesignData(Tpts2);

			DataLoaded = true;
		}

		public void Undo(object sender, int idx)
		{
			Button b = sender as Button;

			if (b == null) return;

			Tpts2[idx].Undo(b.Tag as string);
		}

		public void Redo(object sender, int idx)
		{
			Button b = sender as Button;

			if (b == null) return;

			Tpts2[idx].Redo(b.Tag as string);
		}

		public void Undo(object sender)
		{
			Button b = sender as Button;

			if (b == null) return;

			Tpts2[Tpts2.EndIdx].Undo(b.Tag as string);
		}

		public void Redo(object sender)
		{
			Button b = sender as Button;

			if (b == null) return;

			Tpts2[Tpts2.EndIdx].Redo(b.Tag as string);
		}

		public void BatchIncreaseEachXyxByAmount(int startIdx, string which, double amount)
		{
			if (startIdx > Tpts2.EndIdx || startIdx < 1
				|| double.IsNaN(amount)
				)
			{
				return;
			}

			Tpts2.BatchBegin();

			for (int i = startIdx; i < Tpts2.Count; i++)
			{
				Tpts2[i][which] += amount;
			}

			Tpts2.BatchFinalize();
		}
	}
}
