#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



#endregion


// projname: ParameterVue.ListBoxManager.FamilyData
// itemname: TestData
// username: jeffs
// created:  8/19/2019 10:08:12 PM


namespace ParameterVue.FamilyManager.FamilyInfo
{

	public struct TestInfo
	{
		public string ParameterValue;
		public ParameterSpec ParamSpec;

		public TestInfo(string Value, string name, string storageType, string parameterType, string unitType, string dut)
		{
			ParameterValue = Value;
			ParamSpec = new ParameterSpec(name,  storageType,   parameterType,   unitType,   dut);
		}
	}

	public static class TestData
	{
		// key1=style name    // key2 = parameter value  // header is the definition for the value
		public static Dictionary<string, List<TestInfo>> TestFamilyData { get; private set; } 

		static TestData()
		{
			LoadData();
		}

		private static void LoadData()
		{
			TestFamilyData = new Dictionary<string, List<TestInfo>>();

			List<TestInfo> h = new List<TestInfo>();

			h.Add(new TestInfo("Arial"          , "Text Font"            , "string ", "Text ", "UT_Number", "(no unit type)"));
			h.Add(new TestInfo("3/32\""         , "Text Size"			 , "Double ", "105", "UT_SheetLength", "DUT_FRACTIONAL_INCHES"));
			h.Add(new TestInfo("1/2\" "         , "Tab Size"			 , "Double ", "105", "UT_SheetLength ", "DUT_FRACTIONAL_INCHES"));
			h.Add(new TestInfo("0"              , "Color"				 , "integer", "Invalid", "UT_Number", "(no unit type)"));
			h.Add(new TestInfo("1"              , "Line Weight"			 , "Integer", "Invalid", "UT_Number", "(no unit type)"));
			h.Add(new TestInfo("No "            , "Bold"				 , "Integer", "YesNo", "UT_Number", "(no unit type)"));
			h.Add(new TestInfo("No "            , "Italic"				 , "Integer", "YesNo", "UT_Number", "(no unit type)"));
			h.Add(new TestInfo("No "            , "Underline"			 , "Integer", "YesNo", "UT_Number", "(no unit type)"));
			h.Add(new TestInfo("Opaque "        , "Background"			 , "Integer", "Invalid", "UT_Number", "(no unit type)"));
			h.Add(new TestInfo("1"              , "Width Factor"		 , "Double ", "Number ", "UT_Number", "DUT_GENERAL "));
			h.Add(new TestInfo("No "            , "Show Border"			 , "Integer", "YesNo", "UT_Number", "(no unit type)"));
			h.Add(new TestInfo("1/128\" "       , "Leader/Border Offset" , "Double ", "105", "UT_SheetLength ", "DUT_FRACTIONAL_INCHES"));
			h.Add(new TestInfo("Arrow 30 Degree", "Leader Arrowhead"     , "ElementId", "Invalid", "UT_Number", "(no unit type)"));

			TestFamilyData.Add("3/32\" Arial", h);

			h = new List<TestInfo>();

			h.Add(new TestInfo("Arial"          , "Text Font"            ,"string ", "Text ", "UT_Number", "(no unit type)"));
			h.Add(new TestInfo("1/8\" "         , "Text Size"            ,"Double ", "105", "UT_SheetLength ", "DUT_FRACTIONAL_INCHES"));
			h.Add(new TestInfo("15/32\" "       , "Tab Size"             ,"Double ", "105", "UT_SheetLength ", "DUT_FRACTIONAL_INCHES"));
			h.Add(new TestInfo("0"              , "Color"                ,"integer", "Invalid", "UT_Number", "(no unit type)"));
			h.Add(new TestInfo("1"              , "Line Weight"          ,"Integer", "Invalid", "UT_Number", "(no unit type)"));
			h.Add(new TestInfo("No "            , "Bold"                 ,"Integer", "YesNo", "UT_Number", "(no unit type)"));
			h.Add(new TestInfo("No "            , "Italic"               ,"Integer", "YesNo", "UT_Number", "(no unit type)"));
			h.Add(new TestInfo("No "            , "Underline"            ,"Integer", "YesNo", "UT_Number", "(no unit type)"));
			h.Add(new TestInfo("Opaque "        , "Background"           ,"Integer", "Invalid", "UT_Number", "(no unit type)"));
			h.Add(new TestInfo("1"              , "Width Factor"         ,"Double ", "Number ", "UT_Number", "DUT_GENERAL "));
			h.Add(new TestInfo("No "            , "Show Border"          ,"Integer", "YesNo", "UT_Number", "(no unit type)"));
			h.Add(new TestInfo("5/64\""         , "Leader/Border Offset" ,"Double ", "105", "UT_SheetLength ", "DUT_FRACTIONAL_INCHES"));
			h.Add(new TestInfo("Arrow 30 Degree", "Leader Arrowhead"     ,"ElementId", "Invalid", "UT_Number", "(no unit type)"));
			
			TestFamilyData.Add("Schedule Default", h);
		}


	}
}











































