#region + Using Directives

using System.Collections.ObjectModel;

#endregion


// projname: ParameterVue
// itemname: FamilyData
// username: jeffs
// created:  8/18/2019 9:56:59 PM


namespace ParameterVue.FamilyManager.FamilyInfo
{

	// holds all of the information for a single
	// family item (style)
	public class FamilyData
	{
		public bool Selected { get; set; } = false;
		public string Name { get; set; }

		public ObservableCollection<ParameterValue> ParameterValues { get; set; }

		public FamilyData(string name)
		{
			Name = name;

			ParameterValues = new ObservableCollection<ParameterValue>();
		}

	}
}
