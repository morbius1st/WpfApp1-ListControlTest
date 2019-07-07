using System;
using System.Collections;
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
using WpfData.Data;

namespace WpfCustomControlLibrary
{
	/// <summary>
	/// Interaction logic for CustomListBox1.xaml
	/// </summary>
	public partial class CustomListBox1 : UserControl
	{
		public static  DependencyProperty ItemSourceProperty;

		public ObservableCollection<SimpleData3> Sd3 { get; set; }

		static CustomListBox1()
		{
			ItemSourceProperty = DependencyProperty.Register(
				"ItemSource", typeof(IEnumerable), typeof(CustomListBox1));
		}

		public CustomListBox1()
		{
			InitializeComponent();

			SetItemSourceBinding();

			LoadData();

		}

		private void LoadData()
		{
			Sd3 = new ObservableCollection<SimpleData3>();

//			PART_CustomLB.DataContext = this;
//
//			Binding b = new Binding();
//			b.Source = this;
//			b.Path = new PropertyPath("ItemSource");
//
//			BindingOperations.SetBinding(PART_CustomLB, ListBox.ItemsSourceProperty, b);

			Sd3.Add(new SimpleData3() {PropertyS = "First", PropertyD1 = 100.1, PropertyD2 = 201.1, PropertyI1 = 1001, PropertyI2 = 2001 }) ;
			Sd3.Add(new SimpleData3() {PropertyS = "Second", PropertyD1 = 100.2, PropertyD2 = 201.2, PropertyI1 = 1002, PropertyI2 = 2002 }) ;
			Sd3.Add(new SimpleData3() {PropertyS = "Third", PropertyD1 = 100.3, PropertyD2 = 201.3, PropertyI1 = 1003, PropertyI2 = 2003 }) ;

		}

		private void SetItemSourceBinding()
		{
//			PART_CustomLB.DataContext = this;

			Binding b = new Binding();
			b.Source = this;
			b.Path = new PropertyPath("ItemSource");

			BindingOperations.SetBinding(PART_CustomLB, ListBox.ItemsSourceProperty, b);
		}

		public ListBox CustomLb
		{
			get => PART_CustomLB;
		}

		public IEnumerable ItemSource
		{
			get => (IEnumerable) GetValue(ItemSourceProperty);
			set => SetValue(ItemSourceProperty, value);
		}

		


	}
}
