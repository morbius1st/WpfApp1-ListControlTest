using System.ComponentModel;
using System.Windows;
using WpfExample.SampleData;

namespace WpfExample
{
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		public string WinTitle { get; set; } = "Window Title";

		public string Title1 { get; set; } = "First";
		public string Title2 { get; set; } = "Second";
		public string Title3 { get; set; } = "Third";

		public SampleCollection sx { get; set; } = new SampleCollection();


		public MainWindow()
		{
			InitializeComponent();

			sx.Add(MakeData("A-01", "Sht 01", "Sheet Data"));
			sx.Add(MakeData("A-02", "Sht 02", "Sheet Data"));
			sx.Add(MakeData("A-03", "Sht 03", "Sheet Data"));
			sx.Add(MakeData("A-04", "Sht 04", "Sheet Data"));
			sx.Add(MakeData("A-05", "Sht 05", "Sheet Data"));
		}

		private SampleDataClass MakeData(string sNum, string sName, string sData)
		{
			return new SampleDataClass() { SheetNumber = sNum, SheetName = sName, SheetData = sData};
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			sx.Add(MakeData("A-11", "Sht 01", "Sheet Data"));
			sx.Add(MakeData("A-12", "Sht 02", "Sheet Data"));
			sx.Add(MakeData("A-13", "Sht 03", "Sheet Data"));
			sx.Add(MakeData("A-14", "Sht 04", "Sheet Data"));
			sx.Add(MakeData("A-15", "Sht 05", "Sheet Data"));
		}

#pragma warning disable CS0067 // The event 'MainWindow.PropertyChanged' is never used
		public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067 // The event 'MainWindow.PropertyChanged' is never used
	}
}
