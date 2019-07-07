#region + Using Directives

using System.Diagnostics;
using WpfData.Data;

#endregion


// projname: WpfContextTest.Data
// itemname: DataManager3
// username: jeffs
// created:  6/18/2019 6:00:37 AM


namespace WpfData.Data
{
	public class DataManager3
	{
		public string TestDm { get; set; }  = "TestDm";

		public SimpleDataCollection3 Sdc3 { get; set; }

		public DataManager3()
		{
			Sdc3 = new SimpleDataCollection3();

			Sdc3.AdjustFirstItem = AdjustFirstItem;
			Sdc3.AdjustMiddleItems = AdjustMiddleItems;
			Sdc3.AdjustLastItem = AdjustLastItem;

		}


		// proof of concept.  using a delegate to define a method that
		// modifies each element in the collection
		public static void AdjustMiddleItems(int newIdx, SimpleData3 currentSd, SimpleData3 priorSd)
		{
			Debug.WriteLine("(before) Index| " + currentSd.Index + "  name| " + currentSd.PropertyS);

			currentSd.Index = newIdx;

			if (priorSd == null)
			{
				Debug.WriteLine(" (after) Index| is null\n");
				return;
			}

			currentSd.PropertyS += ((char) ('!' + newIdx)).ToString();
			currentSd.PropertyD1 = priorSd.PropertyD1 + newIdx;
			currentSd.PropertyD2 = priorSd.PropertyD2 + newIdx;
			currentSd.PropertyI1 = priorSd.PropertyI1 + newIdx;
			currentSd.PropertyI2 = priorSd.PropertyI2 + newIdx;

			Debug.WriteLine(" (after) Index| " + currentSd.Index + "  name| " + currentSd.PropertyS + "\n");
		}

		private static double propertyD1 = 101.1;
		private static double propertyD2 = 102.2;
		private static int propertyI1 = 1001;
		private static int propertyI2 = 1002;


		public static void AdjustFirstItem(int newIdx, SimpleData3 currentSd)
		{
			propertyD1 += newIdx;
			propertyD2 += newIdx;
			propertyI1 += newIdx;
			propertyI2 += newIdx;

			currentSd.Index = newIdx;
			currentSd.PropertyS += ((char) ('!' + newIdx)).ToString();
			currentSd.PropertyD1 = propertyD1;
			currentSd.PropertyD2 = propertyD2;
			currentSd.PropertyI1 = propertyI1;
			currentSd.PropertyI2 = propertyI2;
		}
		
		public static void AdjustLastItem(int newIdx, SimpleData3 currentSd, SimpleData3 priorSd)
		{
			AdjustMiddleItems(newIdx, currentSd, priorSd);

			currentSd.PropertyS = "last " + currentSd.PropertyS;
		}



	}
}
