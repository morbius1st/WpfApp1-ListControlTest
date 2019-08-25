#region + Using Directives

using System.Collections.ObjectModel;

#endregion


// projname: ParameterVue.Parameters
// itemname: HeaderMgr
// username: jeffs
// created:  8/18/2019 9:08:10 PM


namespace ParameterVue.FamilyManager.FamilyInfo
{
	public class ColumnData
	{
		public string SelectedColumn { get; private set; } = "Selected";
		public string NameColumn { get; private set; } = "Family Name";

		public ObservableCollection<ColumnSpec> ColumnSpecs { get; set; }

		public  ColumnData()
		{
			ColumnSpecs  = new ObservableCollection<ColumnSpec>();
		}

	}
}
