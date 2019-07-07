#region + Using Directives

using System.ComponentModel;
using System.Runtime.CompilerServices;

#endregion


// projname: WpfContextTest.Data
// itemname: SimpleData2
// username: jeffs
// created:  6/17/2019 9:07:49 PM


namespace WpfData.Data
{
	public class SimpleData3 : INotifyPropertyChanged
	{
		private int index = 0;
		private string propertyS = "void";
		private double propertyD1 = 100.1;
		private double propertyD2 = 200.2;
		private int propertyI1 = 1001;
		private int propertyI2 = 2002;

		public SimpleData3(
			int index, string propertyS, double propertyD1,
			double propertyD2, int propertyI1, int propertyI2
			)
		{
			this.index = index;
			this.propertyS = propertyS;
			this.propertyD1 = propertyD1;
			this.propertyD2 = propertyD2;
			this.propertyI1 = propertyI1;
			this.propertyI2 = propertyI2;
		}

		public SimpleData3() { }

		public int Index
		{
			get => index;
			set
			{
				if (value == index) return;

				index = value;

				OnPropertyChange();
			}
		}

		public string PropertyS
		{
			get => propertyS;
			set
			{
				if (value.Equals(propertyS)) return;

				propertyS = value;

				OnPropertyChange();
			}
		}

		public double PropertyD1
		{
			get => propertyD1;
			set
			{
				if (value.Equals(propertyD1)) return;

				propertyD1 = value;

				OnPropertyChange();
			}
		}

		public double PropertyD2
		{
			get => propertyD2;
			set
			{
				if (value.Equals(propertyD2)) return;

				propertyD2 = value;

				OnPropertyChange();
			}
		}

		public int PropertyI1
		{
			get => propertyI1;
			set
			{
				if (value == propertyI1) return;

				propertyI1 = value;

				OnPropertyChange();
			}
		}

		public int PropertyI2
		{
			get => propertyI2;
			set
			{
				if (value == propertyI2) return;

				propertyI2 = value;

				OnPropertyChange();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}
	}
}
