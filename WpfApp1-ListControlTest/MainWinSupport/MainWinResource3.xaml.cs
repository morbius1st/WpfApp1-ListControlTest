#region + Using Directives
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp1_ListControlTest.TopoPtsData2;
using static WpfApp1_ListControlTest.MainWindow;

#endregion


// projname: WpfApp1_ListControlTest.MainWinSupport
// itemname: MainWinResource3
// username: jeffs
// created:  6/15/2019 5:01:38 PM


namespace WpfApp1_ListControlTest.MainWinSupport
{
	

	public partial class MainWinResource3 : ResourceDictionary
	{
//		int PriorRowBeingEdited = 0;
//
//		private void ListBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
//		{
//			ListBox lb = sender as ListBox;
//
//			TopoPoint2 tp2 = (TopoPoint2) lb.Items[lb.SelectedIndex];
//
//			Debug.WriteLine("@ MainResource3| @selectionChanged|" 
//				+ " selected count| " + e.AddedItems.Count 
//				+ " selected idx| " + lb.SelectedIndex
//			);
//
//
//
//			if (!((TopoPoint2) lb.Items[PriorRowBeingEdited]).HasRevision)
//			{
//				((TopoPoint2) lb.Items[PriorRowBeingEdited]).IsBeingEdited = false;
//				tp2.IsBeingEdited = true;
//
//				PriorRowBeingEdited = lb.SelectedIndex;
//			}
//			else
//			{
//				lb.SelectedIndex = PriorRowBeingEdited;
//			}
//		}
//
//		private void Lb3BtnApply_Click(object sender, RoutedEventArgs e)
//		{
//			ClickInfo(sender, e, "Apply");
//		}
//
//		private void Lb3BtnUndo_Click(object sender, RoutedEventArgs e)
//		{
//			ClickInfo(sender, e, "Undo");
//		}
//
//		private void Lb3BtnApplyX_Click(object sender, RoutedEventArgs e)
//		{
//			ClickInfo(sender, e, "ApplyX");
//		}
//
//		private void Lb3BtnUndoX_Click(object sender, RoutedEventArgs e)
//		{
//			ClickInfo(sender, e, "UndoX");
//		}
//
//		private void Lb3BtnApplyY_Click(object sender, RoutedEventArgs e)
//		{
//			ClickInfo(sender, e, "ApplyY");
//		}
//
//		private void Lb3BtnUndoY_Click(object sender, RoutedEventArgs e)
//		{
//			ClickInfo(sender, e, "UndoY");
//		}
//
//		private void Lb3BtnApplyZ_Click(object sender, RoutedEventArgs e)
//		{
//			ClickInfo(sender, e, "ApplyZ");
//		}
//
//		private void Lb3BtnUndoZ_Click(object sender, RoutedEventArgs e)
//		{
//			ClickInfo(sender, e, "UndoZ");
//		}
//
//		private void ClickInfo(object sender, RoutedEventArgs e, string fromWho)
//		{
//			ListBox lb = MainWindow.Lb3;
//
//			TopoPoint2 tp2 = (TopoPoint2) MainWindow.Lb3.Items[lb.SelectedIndex];
//
//			Debug.WriteLine("@ MainResource3| @ClickInfo|"
//				+ " from| " + fromWho
//				+ " selected idx| " + lb.SelectedIndex
//				+ " xyz| " + tp2.ToString()
//				);
//		}
//
//		private void ListBox3_Loaded(object sender, RoutedEventArgs e)
//		{
//			MainWindow.Lb3 = (ListBox) sender;
//		}
	}
}
