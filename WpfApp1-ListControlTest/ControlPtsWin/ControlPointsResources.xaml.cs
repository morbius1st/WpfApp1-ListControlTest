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
	public partial class ControlPointsResources
	{

		private void TbxXYZ_OnError(object sender, ValidationErrorEventArgs e)
		{
			MainWindow.Cps.TbxXYZ_OnError(sender, e);
		}

		private void TbxSlope_OnError(object sender, ValidationErrorEventArgs e)
		{
			MainWindow.Cps.TbxSlope_OnError(sender, e);
		}

	}
}
