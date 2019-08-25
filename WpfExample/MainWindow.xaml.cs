using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfExample.SampleData;

namespace WpfExample
{
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		public ObservableCollection<SampleDataClass> Sxc { get; set; } = new ObservableCollection<SampleDataClass>();

		public string WinTitle { get; set; } = "Window Title";

		public string Title1 { get; set; } = "First";
		public string Title2 { get; set; } = "Second";
		public string Title3 { get; set; } = "Third";

		public SampleCollection sx { get; set; } = new SampleCollection();

		public bool StateTestBool1 { get; set; } = false;
		public bool StateTestBool2 { get; set; } = false;


		public MainWindow()
		{
			InitializeComponent();

			sx.Add(MakeData("A-01", "Sht 01", "Sheet Data"));
			sx.Add(MakeData("A-02", "Sht 02", "Sheet Data"));
			sx.Add(MakeData("A-03", "Sht 03", "Sheet Data"));
			sx.Add(MakeData("A-04", "Sht 04", "Sheet Data"));
			sx.Add(MakeData("A-05", "Sht 05", "Sheet Data"));

			// note:  do NOY make a new instance of the property Sxc - this loses the binding
			Sxc.Add(new SampleDataClass() { SheetNumber = "A-01b", SheetName = "name 01b", SheetData = "data 01b", SheetInfo = "info 01b", SheetInfo2 = "info2 01b"});
			Sxc.Add(new SampleDataClass() { SheetNumber = "A-02b", SheetName = "name 02b", SheetData = "data 02b", SheetInfo = "info 02b", SheetInfo2 = "info2 02b"});
			Sxc.Add(new SampleDataClass() { SheetNumber = "A-03b", SheetName = "name 03b", SheetData = "data 03b", SheetInfo = "info 03b", SheetInfo2 = "info2 03b"});
		}

		private SampleDataClass MakeData(string sNum, string sName, string sData)
		{
			return new SampleDataClass() { SheetNumber = sNum, SheetName = sName, SheetData = sData};
		}

		private void AddRows_Click(object sender, RoutedEventArgs e)
		{
			sx.Add(MakeData("A-11", "Sht 01", "Sheet Data"));
			sx.Add(MakeData("A-12", "Sht 02", "Sheet Data"));
			sx.Add(MakeData("A-13", "Sht 03", "Sheet Data"));
			sx.Add(MakeData("A-14", "Sht 04", "Sheet Data"));
			sx.Add(MakeData("A-15", "Sht 05", "Sheet Data"));
		}

		private void ChangeState1_Click(object sender, RoutedEventArgs e)
		{
			StateTestBool1 = !StateTestBool1;

			OnPropertyChange("StateTestBool1");
		}
		
		private void ChangeState2_Click(object sender, RoutedEventArgs e)
		{
			StateTestBool2 = !StateTestBool2;

			OnPropertyChange("StateTestBool2");
		}


		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

	}

	public class CustomProperties
	{
		public static readonly DependencyProperty GenericBoolOneProperty = DependencyProperty.RegisterAttached(
			"GenericBooleanOne", typeof(bool), typeof(CustomProperties),
			new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsParentArrange |
				FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsArrange |
				FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

	#region GenericBooleanOne

		public static void SetGenericBooleanOne(UIElement e, bool value)
		{
			e.SetValue(GenericBoolOneProperty, value);
		}

		public static bool GetGenericBooleanOne(UIElement e)
		{
			return (bool) e.GetValue(GenericBoolOneProperty);
		}

	#endregion
	}
}
