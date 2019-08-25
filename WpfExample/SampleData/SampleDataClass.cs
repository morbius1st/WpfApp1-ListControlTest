#region + Using Directives

#endregion


// projname: WpfExample
// itemname: SampleDataClass
// username: jeffs
// created:  8/12/2018 2:11:02 PM


using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfExample.SampleData
{
	public class SampleDataClass : INotifyPropertyChanged
	{
		private string sheetNumber;
		private string sheetName;
		private string sheetData;
		private string sheetInfo;
		private string sheetInfo2;

		public string SheetNumber
		{
			get => sheetNumber;
			set  {
			sheetNumber = value;
			OnPropertyChange();
		}
		}

		public string SheetName
		{
			get => sheetName;
			set
			{ sheetName = value;
				OnPropertyChange();
			}
		}

		public string SheetData
		{
			get => sheetData;
			set
			{ sheetData = value;
				OnPropertyChange();
			}
		}

		public string SheetInfo
		{
			get => sheetInfo;
			set
			{ sheetInfo = value;
				OnPropertyChange();
			}
		}

		public string SheetInfo2
		{
			get => sheetInfo2;
			set
			{ sheetInfo2 = value;
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
