#region + Using Directives

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Text;
using System.Linq;
using System.Windows;
using Autodesk.Revit.DB;
using ParameterVue.FamilyManager.FamilyInfo;
using ParameterValue = ParameterVue.FamilyManager.FamilyInfo.ParameterValue;

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
			p = Fd[Fd.Count - 1].ParameterValues;
			p.Add(new ParameterValue("Value 1.1x", "Value 1.1x"));
			p.Add(new ParameterValue("Value 1.2x", "Value 1.2x"));

			Fd.Add(new FamilyData("Family Name 2"));
			p = Fd[Fd.Count - 1].ParameterValues;
			p.Add(new ParameterValue("Value 2.1x", "Value 2.1x"));
			p.Add(new ParameterValue("Value 2.2x", "Value 2.2x"));

			Fd.Add(new FamilyData("Family Name 3"));
			p = Fd[Fd.Count - 1].ParameterValues;
			p.Add(new ParameterValue("Value 3.1x", "Value 3.1x"));
			p.Add(new ParameterValue("Value 3.2x", "Value 3.2x"));

			Cd.ColumnSpecs.Add(new ColumnSpec(
				new ParameterSpec("Parameter 1", StorageType.None, 
					ParameterType.Invalid, UnitType.UT_Undefined, 
					DisplayUnitType.DUT_UNDEFINED)));
			Cd.ColumnSpecs.Add(new ColumnSpec(
				new ParameterSpec("Parameter 2", StorageType.None,
					ParameterType.Invalid, UnitType.UT_Undefined,
					DisplayUnitType.DUT_UNDEFINED)));
		}

		// load a family's data
		public void LoadFamilies()
		{
			Cd = new ColumnData();
			Fd = new ObservableCollection<FamilyData>();

			FamilyData fd;

			// get the first record
			KeyValuePair<string, List<Parameter>> k = TestData.TestFamilyData.First();

			// setup the initial column specifications
			foreach (Parameter p in k.Value)
			{
				ColumnSpec cs = CreateColumnSpec(p);

				Cd.ColumnSpecs.Add(cs);
			}

			int col;

			// get the value for each parameter
			foreach (KeyValuePair<string, List<Parameter>> kvp in TestData.TestFamilyData)
			{
				col = 0;

				fd = new FamilyData(kvp.Key);
				fd.Selected = false;

				foreach (Parameter p in kvp.Value)
				{
					fd.ParameterValues.Add(new ParameterValue(p.AsValueString()));

					col++;
				}

				Fd.Add(fd);
			}
		}

		private ColumnSpec CreateColumnSpec(Parameter p)
		{
			ColumnSpec cs = new ColumnSpec();

			ParameterSpec ps = new ParameterSpec(
				p.Definition.Name, p.StorageType, p.Definition.ParameterType,
				p.Definition.UnitType, p.DisplayUnitType);

			cs.ParamSpec = ps;
			cs.Choices = null;
			cs.ColumnAlignment = HorizontalAlignment.Left;
			cs.ColumnWidth = 100;
			cs.Control = null;

			return cs;
		}


	#endif

	}
}
