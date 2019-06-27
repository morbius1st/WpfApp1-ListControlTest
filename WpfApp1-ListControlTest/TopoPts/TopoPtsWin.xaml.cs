using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1_ListControlTest.TopoPts.Support;
using WpfApp1_ListControlTest.TopoPtsData;
using static WpfApp1_ListControlTest.MainWindow;

namespace WpfApp1_ListControlTest.TopoPts
{
	/// <summary>
	/// Interaction logic for TopoPtsWin.xaml
	/// </summary>
	public partial class TopoPtsWin : Window, INotifyPropertyChanged
	{
		public string WinTitle { get; } = "TopoPoints";

		public string Message { get; set; }

		public string ErrorMsg { get; set; } = "No Error";

		public TopoPtsConsts TpConsts { get; } = new TopoPtsConsts();

		public TopoPtsData.TopoPoints Tpx { get; set; } = new TopoPoints();

		public TopoPtsWin()
		{
			InitializeComponent();

//			LoadData();

			Message = "Message Window\n";
		}


		private void LbxTopoPoints_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		//	if (itemIndex < 0 || !((ControlPts)listBox2.Items[itemIndex]).IsRevised)
		//	{
		//	if (itemIndex >= 0)
		//	{
		//	((ControlPts)listBox2.Items[ itemIndex]).IsBeingEdited = false;
		//	}
		//
		//	// begin editing a new item
		//	// save the index of this item
		//	itemIndex = listBox2.SelectedIndex;
		//
		//	((ControlPts)listBox2.SelectedItem).CurrentIndex = itemIndex;
		//	((ControlPts)listBox2.SelectedItem).IsBeingEdited = true;

		//	return;
		//}

		// the current item is has changes - prevent them from switching to another item
		// use this method to prevent a selection change.
		//			listBox2.SelectedIndex = itemIndex;

		//SelectionChangedEx();
		}

		private double x = 10001.0;
		private double y = 20002.0;
		private double z = 20003.0;


		private void LoadData()
		{
			Tpx.Initialize(new TopoStartPoint(new XYZ(x, y, z)));
			Tpx.Add(Next(100));
			Tpx.Add(Next(100));
			Tpx.Add(Next(100));
			Tpx.Finalize(new TopoEndPoint(Next(500)));
		}

		private XYZ Next(double increment)
		{
			x += increment;
			y += increment;
			z += increment;

			return new XYZ(x, y, z);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

	#region > debug utilities

		public void Append(string msg)
		{
			Message += msg;
		}

	#endregion
	}
}