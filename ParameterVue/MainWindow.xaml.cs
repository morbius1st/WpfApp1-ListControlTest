using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using ParameterVue.FamilyManager;
using ParameterVue.FamilyManager.FamilyInfo;
using ParameterVue.WpfSupport;
using static ParameterVue.WpfSupport.WpfSupport;
using static ParameterVue.WpfSupport.ListBoxConfiguration;


namespace ParameterVue
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static string ColHeaderTbkSTyleName { get; set; } = "ColHeaderTbk";
		public static string RowHeaderTbxStyleName { get; set; } = "RowHeaderTbx";
		public static string FieldTbxStyleName { get; set; } = "FieldTbx";

		public static TextBlock tx;
		private int count = 0;

		public ListBoxConfiguration lbc { get;  set; } = new ListBoxConfiguration();

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

			AddColumns();

			listBox.UpdateLayout();

		}

		private void Button_Test2(object sender, RoutedEventArgs e)
		{
			Debug.WriteLine("At button Test 2");

			Fm.Fd[0].ParameterValues[0].ParamValue = "test " + count++;
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

			AddHeader(this, hg, h);

			int row = 0;

			string basePath = string.Format("ParameterValues[{0:D}]", col);
			string tbxName = string.Format("ParameterValues{0:D}", col);


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
			AddColumnAndData(Fm.Cd.ColumnSpecs[2], 2);

        }
    }

}
