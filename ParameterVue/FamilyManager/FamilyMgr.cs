#region + Using Directives

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ParameterVue.FamilyManager.FamilyInfo;

#endregion


// projname: ParameterVue.ListBoxManager
// itemname: ListBoxMgr
// username: jeffs
// created:  8/19/2019 9:55:52 PM


namespace ParameterVue.FamilyManager
{
	public class FamilyMgr
	{

		// data chart
		// parametervalue = data structure: the value for a single parameter
		// parameterdata = data structure: the data associated with a parameter

		// familydata = the collection of parameters for a single family + management info: selected property & family name parameter

		// familymanager = the manager of the collections for familydata's and parameterdata's

		public ObservableCollection<FamilyData> Fd { get; set; } = new ObservableCollection<FamilyData>();
		public ColumnData Cd { get; private set; } = new ColumnData();

		public FamilyMgr()
		{
		#if DEBUG
			LoadDesignData();
		#endif
		}

	#if DEBUG

		private void LoadDesignData()
		{
			ObservableCollection<ParameterValue> p;

			Fd.Add(new FamilyData("Family Name 1"));
			p = Fd[Fd.Count - 1].Parameters;
			p.Add(new ParameterValue("Value 1.1x", "Value 1.1x"));
			p.Add(new ParameterValue("Value 1.2x", "Value 1.2x"));

			Fd.Add(new FamilyData("Family Name 2"));
			p = Fd[Fd.Count - 1].Parameters;
			p.Add(new ParameterValue("Value 2.1x", "Value 2.1x"));
			p.Add(new ParameterValue("Value 2.2x", "Value 2.2x"));

			Fd.Add(new FamilyData("Family Name 3"));
			p = Fd[Fd.Count - 1].Parameters;
			p.Add(new ParameterValue("Value 3.1x", "Value 3.1x"));
			p.Add(new ParameterValue("Value 3.2x", "Value 3.2x"));

			Cd.ColumnSpecs.Add(new ColumnSpec(new ParameterSpec("Parameter 1", null, null, null, null)));
			Cd.ColumnSpecs.Add(new ColumnSpec(new ParameterSpec("Parameter 2", null, null, null, null)));


		}

		// load a family's data
		public void LoadFamilies()
		{
			Cd = new ColumnData();
			Fd = new ObservableCollection<FamilyData>();

			FamilyData fd;

			// get the first record
			KeyValuePair<string, List<TestInfo>> k = TestData.TestFamilyData.First();

			// setup the parameter specifications
			foreach (TestInfo ti in k.Value)
			{
				ColumnSpec cs = new ColumnSpec(ti.ParamSpec);

				Cd.ColumnSpecs.Add(cs);
			}

			// get the value for each parameter
			foreach (KeyValuePair<string, List<TestInfo>> kvp in TestData.TestFamilyData)
			{
				fd = new FamilyData(kvp.Key);
				fd.Selected = false;

				foreach (TestInfo ti in kvp.Value)
				{
					fd.Parameters.Add(new ParameterValue(ti.ParameterValue));
				}

				Fd.Add(fd);
			}
		}


	#endif

	}
}
