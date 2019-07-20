#region > Using Directives

using System.Linq.Expressions;
using WpfApp1_ListControlTest.TopoPts.Support;
using WpfApp1_ListControlTest.TopoPtsData3.Support;
using WpfApp1_ListControlTest.TopoPtsData3.TopoPts3;
using WpfApp1_ListControlTest.TopoPtsData3.TopoPts3.Support;

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
		private readonly TopoPts3MgrSupport _tpts3MgrSupport = new TopoPts3MgrSupport();

	#region constructor

		public TopoPts3Mgr()
		{
			Tpts3 = new TopoPoints3();

			Tpts3.ReIndexUpdateItem += reindexUpdateItem;
		}

	#endregion

	#region public properties

		public TopoPoints3 Tpts3 { get; set; }

		public bool DataLoaded { get; private set; } = false;

	#endregion

	#region delegated

		private void reindexUpdateItem(int idx, TopoPoint3 precedingtpt3)
		{
			Tpts3.Append = "\nafter| @TopoPts3Mgr| got updateItemAfterReindex"
				+ " (" + idx + ")\n";

			if (Tpts3[idx].XYZ.NeedsUpdatingX)
			{
				Tpts3.Append = "after| @TopoPts3Mgr| (" + idx + ")"
					+ " X | needs updating\n";
			}

			if (Tpts3[idx].XYZ.NeedsUpdatingY)
			{
				Tpts3.Append = "after| @TopoPts3Mgr| (" + idx + ")"
					+ " Y | needs updating\n";
			}

			if (Tpts3[idx].XYZ.NeedsUpdatingZ)
			{
				Tpts3.Append = "after| @TopoPts3Mgr| (" + idx + ")"
					+ " Z | needs updating\n";
			}

			TopoPoint3 tp3 = Tpts3[idx];

			if (double.IsNaN(tp3.X) || double.IsNaN(precedingtpt3.X)
				|| double.IsNaN(tp3.Y) || double.IsNaN(precedingtpt3.Y)
				|| double.IsNaN(tp3.Z) || double.IsNaN(precedingtpt3.Z))
			{
				tp3.Clear();
				return;
			}

			// update the index
			if (tp3.Index != idx) tp3.Index = idx;

			// update XΔ
			bool resultX = Update.UpdateXΔ(tp3, Tpts3[idx - 1]);

			// updateYΔ
			bool resultY = Update.UpdateYΔ(tp3, Tpts3[idx - 1]);

			// updateZΔ
			Update.UpdateZΔ(tp3, Tpts3[idx - 1]);

			// XYZ3 updated, XΔ, YΔ, ZΔ updated
			// need to update XYΔ?
			if (resultX || resultY)
			{
				Update.CalcXYΔ(tp3);
			}

			// assumption is that at least one
			// X or Y or Z changed
			Update.CalcSlope(tp3);

			//  all updated - adjust flags
			Tpts3[idx - 1].XYZ.NeedsUpdatingX = false;
			Tpts3[idx - 1].XYZ.NeedsUpdatingY = false;
			Tpts3[idx - 1].XYZ.NeedsUpdatingZ = false;

			Tpts3.Append = "\n";
		}

	#endregion

	#region management methods

		public void LoadData()
		{
			_tpts3MgrSupport.LoadDesignData(Tpts3);

			DataLoaded = true;
		}

		public void Undo(object tag, int idx)
		{
			int index = idx;

			switch (((TopoPtsTags) tag).Value)
			{
			case -1:
				{
					index = Tpts3.IndexOfEndPoint;
					break;
				}
			case 0:
				{
					index = 0;
					break;
				}
			}

			Tpts3[index].Undo((TopoPtsTags) tag);
		}

		public void Redo(object tag, int idx)
		{
			int index = idx;

			switch (((TopoPtsTags) tag).Value)
			{
			case -1:
				{
					index = Tpts3.IndexOfEndPoint;
					break;
				}
			case 0:
				{
					index = 0;
					break;
				}
			}

			Tpts3[index].Redo((TopoPtsTags) tag);
		}

	#endregion

	#region public methods

		public void BatchIncreaseEachXyxByAmount(int startIdx, string which, double amount)
		{
			if (startIdx > Tpts3.IndexOfEndPoint || startIdx < 1
				|| double.IsNaN(amount)
				)
			{
				return;
			}

			Tpts3.BatchBegin();

			for (int i = startIdx; i < Tpts3.Count; i++)
			{
				Tpts3[i][which] += amount;
			}

			Tpts3.BatchFinalize();
		}

	#endregion
	}
}