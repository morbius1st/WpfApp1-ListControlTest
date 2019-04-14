#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

#endregion


// projname: WpfApp1_ListControlTest.WpfResources
// itemname: OnErrorValidation
// username: jeffs
// created:  3/27/2019 10:57:57 PM


namespace WpfApp1_ListControlTest.WpfResources
{
	public partial class ListBoxMultiLine1
	{
		public void TbxFirst_OnError(object sender, ValidationErrorEventArgs e)
		{
			if (e.Action == ValidationErrorEventAction.Added)
			{
				MainWindow.Haf.AppendMessage("tbxFirst error| " + ((TextBox) sender).Text + " :: Added :: " + e.Error.ErrorContent);
			}
			else
			{
				MainWindow.Haf.AppendMessage("tbxFirst error| " + ((TextBox) sender).Text + " :: Removed");
			}

		}
	}
}
