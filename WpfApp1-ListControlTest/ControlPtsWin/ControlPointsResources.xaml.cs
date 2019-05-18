#region + Using Directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

#endregion


// projname: WpfApp1_ListControlTest.ControlPtsWin
// itemname: ControlPointsResources
// username: jeffs
// created:  5/11/2019 6:41:02 PM


namespace WpfApp1_ListControlTest.ControlPtsWin
{
	public delegate void Cpw_OnError(object source, ValidationErrorEventArgs e);

	public delegate void Cpw_BtnClick(object source, RoutedEventArgs e);


	public partial class ControlPointsResources
	{

		public string test = "this is a test";

		public ControlPointsResources()
		{
			MainWindow.Cpr = this;

			if (MainWindow.Cps != null)
			{
				MainWindow.Cps.cpr = this;
			}
		}

		public event Cpw_BtnClick BtnRestoreXClick;

		public void BtnRestoreX_Click(object sender, RoutedEventArgs e)
		{
			BtnRestoreXClick?.Invoke(sender, e);



		}

		public event Cpw_OnError XYZ_OnError;

		public void TbxXYZ_OnError(object sender, ValidationErrorEventArgs e)
		{
			XYZ_OnError?.Invoke(sender, e);
		}

		//		private void TbxSlope_OnError(object sender, ValidationErrorEventArgs e)
		//		{
		////			MainWindow.Cps.TbxSlope_OnError(sender, e);
		//		}



	}
}
