using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfContextTest.Data;
using WpfData.Data;


namespace WpfContextTest
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		// level 1 data list
		public ObservableCollection<SimpleData> Sd { get; set; }
		public ObservableCollection<SimpleData3> Sdx { get; set; }

		// level 2 data list
		public SimpleDataMgr Mgr { get; set; }

		// level 3 data list
		public DataManager Mgr2 { get; set; }

		public MainWindow()
		{
			LoadData1();

			Mgr = new SimpleDataMgr();
			Mgr2 = new DataManager();

			InitializeComponent();

//			SetBinding();



		}

		public void LoadData1()
		{
			Sd = new ObservableCollection<SimpleData>();

			Sd.Add(new SimpleData() {PropertyS = "Fourth", PropertyD1 = 100.1, PropertyD2 = 201.1, PropertyI1 = 1001, PropertyI2 = 2001 }) ;
			Sd.Add(new SimpleData() {PropertyS = "Fifth", PropertyD1 = 100.2, PropertyD2 = 201.2, PropertyI1 = 1002, PropertyI2 = 2002 }) ;
			Sd.Add(new SimpleData() {PropertyS = "Sixth", PropertyD1 = 100.3, PropertyD2 = 201.3, PropertyI1 = 1003, PropertyI2 = 2003 }) ;
			
			Sdx = new ObservableCollection<SimpleData3>();

			Sdx.Add(new SimpleData3() {PropertyS = "Seventh", PropertyD1 = 100.1, PropertyD2 = 201.1, PropertyI1 = 1001, PropertyI2 = 2001 }) ;
			Sdx.Add(new SimpleData3() {PropertyS = "Eighth", PropertyD1 = 100.2, PropertyD2 = 201.2, PropertyI1 = 1002, PropertyI2 = 2002 }) ;
			Sdx.Add(new SimpleData3() {PropertyS = "Ninth", PropertyD1 = 100.3, PropertyD2 = 201.3, PropertyI1 = 1003, PropertyI2 = 2003 }) ;
		}

		private void ColorPicker_OnColorChanged(object sender,
			RoutedPropertyChangedEventArgs<Color> e
			)
		{

		}

		private void SetBinding()
		{
			ListBox lb3 = CustomListBox1.CustomLb;

//			lb3.DataContext = this;
//
//			Binding binding = new Binding();
//			binding.Source = this;
//			binding.Path = new PropertyPath("Sd");
//			binding.Mode = BindingMode.TwoWay;
//
//			BindingOperations.SetBinding(CustomListBox1, ListBox.ItemsSourceProperty, binding);



		}

		private void MainWindow_TargetUpdated(object sender, DataTransferEventArgs e)
		{

		}
	}
}
