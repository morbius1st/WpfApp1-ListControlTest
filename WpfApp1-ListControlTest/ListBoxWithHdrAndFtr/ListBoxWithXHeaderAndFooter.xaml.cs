using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using WpfApp1_ListControlTest.SampleData;

namespace WpfApp1_ListControlTest.ListBoxWithHdrAndFtr
{
	/// <summary>
	/// Interaction logic for ListBoxWithXHeaderAndFooter.xaml
	/// </summary>
	public partial class ListBoxWithXHeaderAndFooter : Window, INotifyPropertyChanged
	{
private string nl = Environment.NewLine;

		public string MessageText { get; set; } = "Message Window\n";

#pragma warning disable CS0169 // The field 'ListBoxWithXHeaderAndFooter.savedValue' is never used
		private string savedValue;
#pragma warning restore CS0169 // The field 'ListBoxWithXHeaderAndFooter.savedValue' is never used

        public ListBoxWithXHeaderAndFooter()
        {
            InitializeComponent();

			AddValues();
		}

		private void AddValues()
		{
			SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-01b", SheetName="name 01b", SheetData="data 01b", SheetInfo="info 01b", SheetInfo2="info2 01b"});
            SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-02b", SheetName="name 02b", SheetData="data 02b", SheetInfo="info 02b", SheetInfo2="info2 02b"});
            SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-03b", SheetName="name 03b", SheetData="data 03b", SheetInfo="info 03b", SheetInfo2="info2 03b"});
            SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-04b", SheetName="name 04b", SheetData="data 04b", SheetInfo="info 04b", SheetInfo2="info2 04b"});
            SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-05b", SheetName="name 05b", SheetData="data 05b", SheetInfo="info 05b", SheetInfo2="info2 05b"});
            SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-06b", SheetName="name 06b", SheetData="data 06b", SheetInfo="info 06b", SheetInfo2="info2 06b"});
            SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-07b", SheetName="name 07b", SheetData="data 07b", SheetInfo="info 07b", SheetInfo2="info2 07b"});
            SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-08b", SheetName="name 08b", SheetData="data 08b", SheetInfo="info 08b", SheetInfo2="info2 08b"});
            SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-09b", SheetName="name 09b", SheetData="data 09b", SheetInfo="info 09b", SheetInfo2="info2 09b"});
            SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-10b", SheetName="name 10b", SheetData="data 10b", SheetInfo="info 10b", SheetInfo2="info2 10b"});
            SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-11b", SheetName="name 11b", SheetData="data 11b", SheetInfo="info 11b", SheetInfo2="info2 11b"});
            SampleCollectionB.sxB.Add(new SampleDataClass() { SheetNumber="A-12b", SheetName="name 12b", SheetData="data 12b", SheetInfo="info 12b", SheetInfo2="info2 12b"});
		}



		internal void AppendMessage(string message)
		{
			MessageText += message + nl;

			OnPropertyChange("MessageText");
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}


		private void TbxFirst_OnError(object sender, ValidationErrorEventArgs e)
		{
			if (e.Action == ValidationErrorEventAction.Added)
			{
				AppendMessage("tbxFirst error| " + ((TextBox) sender).Text + " :: Added :: " + e.Error.ErrorContent);
			}
			else
			{
				AppendMessage("tbxFirst error| " + ((TextBox) sender).Text + " :: Removed");
			}

		}

		private void BtnDebug_Click(object sender, RoutedEventArgs e)
		{
			AppendMessage("BtnDebug_Click");
		}
	}
}
