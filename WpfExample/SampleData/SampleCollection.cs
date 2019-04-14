using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace WpfExample.SampleData
{

	public class SampleCollection : ObservableCollection<SampleDataClass>
	{
		public static SampleCollection sx { get; set; } = new SampleCollection();

	}

}