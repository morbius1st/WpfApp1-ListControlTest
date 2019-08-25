using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using ParameterVue.FamilyManager;
using ParameterVue.FamilyManager.FamilyInfo;
using static ParameterVue.WpfSupport.WpfSupport;

namespace ParameterVue
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static string StyleName { get; set; } = "FieldTbx";
		public static string GridHeaderName { get; set; } = "FieldTbx";

		public static TextBlock tx;


		public FamilyMgr Fm { get; set; } = new FamilyManager.FamilyMgr();

		public MainWindow()
		{
			InitializeComponent();

			tx = textBlock;
		}

		private void Button_Test1(object sender, RoutedEventArgs e)
		{
			Debug.WriteLine("At button Test 1");

			VirtualizingStackPanel vp = GetVirtualizingStackPanel(listBox);

			removeOldHeaderElements();
			removeOldFieldElements();

			Fm.LoadFamilies();

			listBox.ItemsSource = Fm.Fd;
			listBox.UpdateLayout();

//			vp = GetVirtualizingStackPanel(listBox);
//
//			ParameterSpec p = new ParameterSpec("Parameter 1", null, null, null, null);
//			p.ColumnWidth = 100;
//
//			AddColumn(p);

			AddColumns();

			listBox.UpdateLayout();

//			AddData();
		}

		private int count = 0;

		private void Button_Test2(object sender, RoutedEventArgs e)
		{
			Debug.WriteLine("At button Test 2");

			Fm.Fd[0].Parameters[0].ParamValue = "test " + count++;

		}

		private void btnDebug_Click(object sender, RoutedEventArgs e)
		{
			

			Debug.WriteLine("At debug Click");
		}

		// based on previously loaded data, create all of
		// the columns for the grid
		private void AddColumns()
		{
			int col = 0;
			// add the first two standard columns
			foreach (ColumnSpec cs in Fm.Cd.ColumnSpecs)
			{
				AddColumnAndData(cs, col++);
			}
		}

		private void removeOldHeaderElements()
		{
			Grid g = (Grid) FindNamedVisualChild<Grid>(listBox, "header");

			RemoveOldElements(g, 2);
		}

		private void removeOldFieldElements()
		{
			VirtualizingStackPanel vp = GetVirtualizingStackPanel(listBox);

			foreach (object vpChild in vp.Children)
			{
				if (!(vpChild is ListBoxItem)) continue;



				Grid g = FindNamedVisualChild<Grid>((ListBoxItem) vpChild,
					"data");

				RemoveOldElements(g, 2);

			}
		}

		private void RemoveOldElements(Grid g, int start)
		{
			if (g.Children.Count > start)
			{
				g.Children.RemoveRange(start, g.ColumnDefinitions.Count - start);
			}

			if (g.ColumnDefinitions.Count > start)
			{
				g.ColumnDefinitions.RemoveRange(start, g.ColumnDefinitions.Count - start);
			}
		}

		public void AddColumnAndData(ColumnSpec h, int col)
		{
			Grid hg = (Grid) FindNamedVisualChild<Grid>(listBox, "header");

			AddHeader(hg, h);

			int row = 0;

			string basePath = string.Format("Parameters[{0:D}]", col);
			string tbxName = string.Format("Parameters{0:D}", col);


			VirtualizingStackPanel vp = GetVirtualizingStackPanel(listBox);

			foreach (object vpChild in vp.Children)
			{
				if (!(vpChild is ListBoxItem)) continue;

				Grid g = FindNamedVisualChild<Grid>((ListBoxItem) vpChild,
					"data");

				AddField(this, g, basePath, tbxName, h.ColumnWidth);
			}
		}

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
			AddColumnAndData(Fm.Cd.ColumnSpecs[0], 0);
			AddColumnAndData(Fm.Cd.ColumnSpecs[1], 1);
        }
    }

}
