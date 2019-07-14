#region > Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1_ListControlTest.TopoPts.Support;
using WpfApp1_ListControlTest.TopoPtsData3.Support;
using WpfApp1_ListControlTest.TopoPtsData3.TopoPts3;

#endregion

// projname: WpfApp1_ListControlTest.TopoPtsData3
// itemname: TopoPts3Mgr
// username: jeffs
// created:  6/9/2019 12:46:14 PM


namespace WpfApp1_ListControlTest.TopoPtsData3
{
	// this manager (wrapper) will hold the topopoints collection and
	// all of the routines to work with the collection
	public class TopoPts3Mgr
	{
		public TopoPtsConsts TpConsts { get; private set; }

		public TopoPoints3 TopoPts { get; set; }

		private TopoPts3MgrSupport TptSupport = new TopoPts3MgrSupport();

		public TopoPts3Mgr()
		{
			TpConsts = new TopoPtsConsts();

			TopoPts = new TopoPoints3();

			TopoPts.AfterReindexItem += updateItemAfterReindex;
		}

		private void updateItemAfterReindex(int idx, TopoPoint3 precedingtpt3)
		{
			TopoPts.Append = "\nafter| @TopoPts3Mgr| got updateItemAfterReindex"
				+ " (" + idx + ")\n";


			if (TopoPts[idx].XYZ.NeedsUpdatingX ||
				TopoPts[idx].XYZ.NeedsUpdatingY ||
				TopoPts[idx].XYZ.NeedsUpdatingZ
				)
			{
				if (TopoPts[idx].XYZ.NeedsUpdatingX)
				{
					TopoPts.Append = "after| @TopoPts3Mgr| (" + idx + ")"
						+ " X | needs updating\n";

					TopoPts[idx].XYZ.NeedsUpdatingX = false;
				}

				if (TopoPts[idx].XYZ.NeedsUpdatingY)
				{
					TopoPts.Append = "after| @TopoPts3Mgr| (" + idx + ")"
						+ " Y | needs updating\n";

					TopoPts[idx].XYZ.NeedsUpdatingY = false;
				}

				if (TopoPts[idx].XYZ.NeedsUpdatingZ)
				{
					TopoPts.Append = "after| @TopoPts3Mgr| (" + idx + ")"
						+ " Z | needs updating\n";

					TopoPts[idx].XYZ.NeedsUpdatingZ = false;
				}
			}
			else
			{
				TopoPts.Append = "after| @TopoPts3Mgr| (" + idx + ")"
					+ " nothing needs updating\n";
			}

			TopoPts.Append = "\n";
		}

		public bool DataLoaded { get; private set; } = false;

		public void LoadData()
		{
			TptSupport.LoadDesignData(TopoPts);

			DataLoaded = true;
		}

		public void Undo(string tag, int idx)
		{
//			Button b = sender as Button;
//
//			if (b == null) return;

			TopoPts[idx].Undo(tag);
		}

		public void Redo(string tag, int idx)
		{
//			Button b = sender as Button;
//
//			if (b == null) return;

			TopoPts[idx].Redo(tag);
		}

		public void Undo(string tag)
		{
//			Button b = sender as Button;
//
//			if (b == null) return;

			TopoPts[TopoPts.EndIdx].Undo(tag);
		}

		public void Redo(string tag)
		{
//			Button b = sender as Button;
//
//			if (b == null) return;

			TopoPts[TopoPts.EndIdx].Redo(tag);
		}

		public void BatchIncreaseEachXyxByAmount(int startIdx, string which, double amount)
		{
			if (startIdx > TopoPts.EndIdx || startIdx < 1
				|| double.IsNaN(amount)
				)
			{
				return;
			}

			TopoPts.BatchBegin();

			for (int i = startIdx; i < TopoPts.Count; i++)
			{
				TopoPts[i][which] += amount;
			}

			TopoPts.BatchFinalize();
		}
	}
}
