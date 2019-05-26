using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using WpfApp1_ListControlTest.SampleData;

namespace WpfApp1_ListControlTest.MultiLineLB
{
    /// <summary>
    /// Interaction logic for MultiLineListBox.xaml
    /// </summary>
    public partial class MultiLineListBox : Window, INotifyPropertyChanged
	{
		private string nl = Environment.NewLine;

		public string MessageText { get; set; } = "Message Window\n";

#pragma warning disable CS0169 // The field 'MultiLineListBox.savedValue' is never used
		private string savedValue;
#pragma warning restore CS0169 // The field 'MultiLineListBox.savedValue' is never used

        public MultiLineListBox()
        {
            InitializeComponent();
			
		}

		private void BtnTest_Click(object sender, RoutedEventArgs e)
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

			e.Handled = true;
		}

		private void BtnDebug_Click(object sender, RoutedEventArgs e)
		{
			AppendMessage("BtnDebug_Click");
		}
	}
}


		// these also did not work - never fired
//		private void ListBox2_SourceUpdated(object sender, DataTransferEventArgs e)
//		{
//			AppendMessage("ListBox2 SourceUpdated");
//		}
//
//		private void ListBox2_TargetUpdated(object sender, DataTransferEventArgs e)
//		{
//			AppendMessage("ListBox2 TargetUpdated");
//		}

		// using got focus / lost focus methods does not work - this
		// does not invervene between the list box and the collection
//		private void TbxFirst_OnGotFocus(object sender, RoutedEventArgs e)
//		{
//			savedValue = ((TextBox) sender).Text;
//		}
//
//		private void TbxFirst_LostFocus(object sender, RoutedEventArgs e)
//		{
//			AppendMessage("tbxFirst changed| " + ((TextBox) sender).Text);
//
//			((TextBox) sender).Text = savedValue;
//
//			e.Handled = true;
//		}
//
//
//		private void TbxSecond_OnGotFocus(object sender, RoutedEventArgs e)
//		{
//			savedValue = ((TextBox) sender).Text;
//		}
//
//
//		private void TbxSecond_LostFocus(object sender, RoutedEventArgs e)
//		{
//			AppendMessage("tbxSecond changed| " + ((TextBox) sender).Text);
//
//			((TextBox) sender).Text = savedValue;
//
//			e.Handled = true;
//		}
//
//
//
//		private void TbxThird_OnGotFocus(object sender, RoutedEventArgs e)
//		{
//			savedValue = ((TextBox) sender).Text;
//		}
//
//		private void TbxThird_LostFocus(object sender, RoutedEventArgs e)
//		{
//			AppendMessage("tbxThird changed| " + ((TextBox) sender).Text);
//
//			((TextBox) sender).Text = savedValue;
//
//			e.Handled = true;
//		}

