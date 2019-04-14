using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace WpfExample.PointsData
{

	public class PointsCollection : ObservableCollection<PointsClass>
	{
		public static PointsCollection Sx { get; set; } = new PointsCollection();
	}
}