#region + Using Directives
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



#endregion


// projname: WpfApp1_ListControlTest.TestRoutines
// itemname: ReIndexTest
// username: jeffs
// created:  5/28/2019 10:55:11 PM


namespace WpfApp1_ListControlTest.TestRoutines
{
	public class ReIndexTest
	{
		private class alpha
		{
			public int Index { get; set; }

			public alpha(int i)
			{
				Index = i;
			}
		}

		private List<alpha> Items = new List<alpha>(10);


		public void TestReIndex()
		{
			for (int i = 0; i < 5; i++) { Items.Add(new alpha(i)); }

			int idx = Items.Count - 1;

			Debug.WriteLine("before");
			ListItems();

			Items.Insert(idx, new alpha(idx));

			Debug.WriteLine("\nafter insert");
			ListItems();

			ReIndex(idx);

			Debug.WriteLine("\nafter reindex");
			ListItems();
		}

		private void ReIndex(int start = 1)
		{


			int i;
			int j = start + 2;

			j = j > Items.Count ? Items.Count : j;

			for (i = start; i < j; i++)
			{
				UpdateItem(i, i - 1);
			}

			for (; i < Items.Count; i++)
			{
				Debug.WriteLine("reindex| ( " + Items[i].Index + " ) => ( " + i + " )");
				Items[i].Index = i;
			}

		}

		private void UpdateItem(int i, int j)
		{
			Debug.WriteLine("update item| ( " + Items[i].Index + " ) => ( " + i + " )");
			Items[i].Index = i;
		}

		private  void ListItems()
		{
			for (int i = 0; i < Items.Count; i++)
			{
				Debug.WriteLine("item| (" + i + ") index| " + Items[i].Index);
			}
		}
	}
}
