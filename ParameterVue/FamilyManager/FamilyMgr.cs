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

			Fd.Add(new FamilyData("Family 1"));
			p = Fd[Fd.Count - 1].ParameterValues;
			p.Add(new ParameterValue(
				TestData.DefineParameter(new []{"3/32\"" , "Text Size" , "Double "  , "105"    , "UT_SheetLength" , "DUT_FRACTIONAL_INCHES"})));
			p.Add(new ParameterValue(
				TestData.DefineParameter(new []{"Arial1" , "Text Font" , "string "  , "Text "  , "UT_Number"      , "(no unit type)" })));
			p.Add(new ParameterValue(
				TestData.DefineParameter(new []{"1/2\" " , "Tab Size"  , "Double "  , "105"    , "UT_SheetLength ", "DUT_FRACTIONAL_INCHES"})));

			Fd.Add(new FamilyData("Family 2"));
			p = Fd[Fd.Count - 1].ParameterValues;
			p.Add(new ParameterValue(
				TestData.DefineParameter(new [] {"4/32\"" , "Text Size" , "Double "  , "105"    , "UT_SheetLength" , "DUT_FRACTIONAL_INCHES"})));
			p.Add(new ParameterValue(
				TestData.DefineParameter(new [] {"Arial2" , "Text Font" , "string "  , "Text "  , "UT_Number"      , "(no unit type)" })));
			p.Add(new ParameterValue(
				TestData.DefineParameter(new [] {"1/2\" " , "Tab Size"  , "Double "  , "105"    , "UT_SheetLength ", "DUT_FRACTIONAL_INCHES"})));

			Fd.Add(new FamilyData("Family 3"));
			p = Fd[Fd.Count - 1].ParameterValues;
			p.Add(new ParameterValue(
				TestData.DefineParameter(new [] {"5/32\"" , "Text Size" , "Double "  , "105"    , "UT_SheetLength" , "DUT_FRACTIONAL_INCHES"})));
			p.Add(new ParameterValue(
				TestData.DefineParameter(new [] {"Arial3" , "Text Font" , "string "  , "Text "  , "UT_Number"      , "(no unit type)" })));
			p.Add(new ParameterValue(
				TestData.DefineParameter(new [] {"1/2\" " , "Tab Size"  , "Double "  , "105"    , "UT_SheetLength ", "DUT_FRACTIONAL_INCHES"})));

			Cd.ColumnSpecs.Add(new ColumnSpec("Parameter 1"));
			Cd.ColumnSpecs.Add(new ColumnSpec("Parameter 2"));
			Cd.ColumnSpecs.Add(new ColumnSpec("Parameter 3"));

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
					fd.ParameterValues.Add(new ParameterValue(p));

					col++;
				}

				Fd.Add(fd);
			}
		}

		private ColumnSpec CreateColumnSpec(Parameter p)
		{
			ColumnSpec cs = new ColumnSpec();

			cs.Choices = null;
			cs.ColumnTitle = p.Definition.Name;
			cs.ColumnAlignment = HorizontalAlignment.Left;
			cs.ColumnWidth = 100;
			cs.Control = null;

			return cs;
		}


	#endif

	}
}
