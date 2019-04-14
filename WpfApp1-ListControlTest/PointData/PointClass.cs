#region + Using Directives

#endregion


// projname: WpfApp1_ListControlTest
// itemname: PointsClass
// username: jeffs
// created:  8/12/2018 2:11:02 PM


using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1_ListControlTest.PointData
{
	public class PointClass : INotifyPropertyChanged
	{
		private string xValue;

		public string XValue
		{
			get => xValue;
			set
			{
				xValue = value;
				NotifyPropertyChanged();
			}
		}

		private string yValue;

		public string YValue
		{
			get => yValue;
			set { yValue = value; }
		}

		private string zValue;

		public string ZValue
		{
			get => zValue;
			set
			{
				zValue = value;
				NotifyPropertyChanged();
			}
		}

		public string XyValue { get; set; }

		public string XyzValue { get; set; }

		public string ZDelta { get; set; }

		private string slope;
		public string Slope
		{
			get => slope;
			set
			{
				slope = value;
				NotifyPropertyChanged();
			}
		}

		public int Index { get; }

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged([CallerMemberName] string p = "")
		{
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
		}
	}
}
