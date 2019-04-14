using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace WpfApp1_ListControlTest.SampleData
{

	public class SampleCollection : ObservableCollection<SampleDataClass>
	{
		public static SampleCollection sx { get; set; } = new SampleCollection();

	}
//		public SampleCollection() : base()
//		{
//			sx.CollectionChanged += SampleCollection_CollectionChanged;
//		}


//		void SampleCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
//		{
//			MainWindow.Mlb.appendMessage("collection changed| # of items|" + e.NewItems.Count);
//		}
//	}
//

	// this class shows how to hook up events to the collection
	// however, these events don't seem to useful
	// also the below order of creation is needed to make the objects
	// correctly 
	public class SampleCollectionB : ObservableCollection<SampleDataClass>
	{
		public static SampleCollectionB sxB { get; set; } = null;

		static SampleCollectionB()
		{
			sxB = new SampleCollectionB();

//			sxB.HookEvents();
		}

//		public SampleCollectionB() :base()
//		{
//			
//		}

		private void HookEvents()
		{
			sxB.PropertyChanged += SxB_PropertyChanged;
			sxB.CollectionChanged += SampleCollectionB_CollectionChanged;
		}

		private void SxB_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			MainWindow.Mlb.AppendMessage("collection changed| prop changed|" + e.PropertyName);
		}

		void SampleCollectionB_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			MainWindow.Mlb.AppendMessage("collection changed| # of items|" + e.NewItems.Count);
		}

	}
//	}
//
//	public class TestX : ObservableCollection<SampleDataClass>
//	{
//		public static TestX tx { get; set; } = new TestX();
//
//		public TestX()
//		{
//			tx.CollectionChanged += TestX_CollectionChanged;
//		}
//
//		void TestX_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
//		{
//			MainWindow.Mlb.appendMessage("collection changed| # of items|" + e.NewItems.Count);
//		}
//	}
}