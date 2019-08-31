#region + Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

#endregion


// projname: ParameterVue.ListBoxManager.FamilyData
// itemname: TestData
// username: jeffs
// created:  8/19/2019 10:08:12 PM


namespace ParameterVue.FamilyManager.FamilyInfo
{

//	public struct TestInfo
//	{
//		public string ParameterValue;
//		public ParameterSpec ParamSpec;
//
//		public TestInfo(string Value, string name, string storageType, string parameterType, string unitType, string dut)
//		{
//			ParameterValue = Value;
//			ParamSpec = new ParameterSpec(name,  storageType,   parameterType,   unitType,   dut);
//		}
//	}

	public static class TestData
	{
		public static Dictionary<string, List<Parameter>> TestFamilyData = new Dictionary<string, List<Parameter>>();

		// key1=style name    // key2 = parameter value  // header is the definition for the value
//		public static Dictionary<string, List<TestInfo>> TestFamilyData { get; private set; } 

		static TestData()
		{
			LoadData();
		}

		private static void LoadData()
		{
			TestFamilyData = new Dictionary<string, List<Parameter>>();

			List<Autodesk.Revit.DB.Parameter> px = new List<Parameter>();

			//                               0                 1                         2           3          4                  5
			//                                               parameter                 storage     parameter   unit             display unit  
			//                             value             name                      type        type        type             type
			px.Add(DefineParameter(new [] {"Arial"          , "Text Font"            , "string "  , "Text "  , "UT_Number"      , "(no unit type)"       }));
			px.Add(DefineParameter(new [] {"3/32\""         , "Text Size"			 , "Double "  , "105"    , "UT_SheetLength" , "DUT_FRACTIONAL_INCHES"}));
			px.Add(DefineParameter(new [] {"1/2\" "         , "Tab Size"			 , "Double "  , "105"    , "UT_SheetLength ", "DUT_FRACTIONAL_INCHES"}));
			px.Add(DefineParameter(new [] {"0"              , "Color"				 , "integer"  , "Invalid", "UT_Number"      , "(no unit type)"       }));
			px.Add(DefineParameter(new [] {"1"              , "Line Weight"			 , "Integer"  , "Invalid", "UT_Number"      , "(no unit type)"       }));
			px.Add(DefineParameter(new [] {"No "            , "Bold"				 , "Integer"  , "YesNo"  , "UT_Number"      , "(no unit type)"       }));
			px.Add(DefineParameter(new [] {"No "            , "Italic"				 , "Integer"  , "YesNo"  , "UT_Number"      , "(no unit type)"       }));
			px.Add(DefineParameter(new [] {"No "            , "Underline"			 , "Integer"  , "YesNo"  , "UT_Number"      , "(no unit type)"       }));
			px.Add(DefineParameter(new [] {"Opaque "        , "Background"			 , "Integer"  , "Invalid", "UT_Number"      , "(no unit type)"       }));
			px.Add(DefineParameter(new [] {"1"              , "Width Factor"		 , "Double "  , "Number ", "UT_Number"      , "DUT_GENERAL "         }));
			px.Add(DefineParameter(new [] {"No "            , "Show Border"			 , "Integer"  , "YesNo"  , "UT_Number"      , "(no unit type)"       }));
			px.Add(DefineParameter(new [] {"1/128\" "       , "Leader/Border Offset" , "Double "  , "105"    , "UT_SheetLength ", "DUT_FRACTIONAL_INCHES"}));
			px.Add(DefineParameter(new [] {"Arrow 30 Degree", "Leader Arrowhead"     , "ElementId", "Invalid", "UT_Number"      , "(no unit type)"       }));

			TestFamilyData.Add("3/32\" Arial", px);

			px = new List<Parameter>();


			px.Add(DefineParameter(new [] {"Arial"          , "Text Font"            , "string "  , "Text "  , "UT_Number"      , "(no unit type)"       }));
			px.Add(DefineParameter(new [] {"1/8\" "         , "Text Size"            , "Double "  , "105"    , "UT_SheetLength ", "DUT_FRACTIONAL_INCHES" }));
			px.Add(DefineParameter(new [] {"15/32\" "       , "Tab Size"             , "Double "  , "105"    , "UT_SheetLength ", "DUT_FRACTIONAL_INCHES" }));
			px.Add(DefineParameter(new [] {"0"              , "Color"                , "integer"  , "Invalid", "UT_Number"      , "(no unit type)"        }));
			px.Add(DefineParameter(new [] {"1"              , "Line Weight"          , "Integer"  , "Invalid", "UT_Number"      , "(no unit type)"        }));
			px.Add(DefineParameter(new [] {"No "            , "Bold"                 , "Integer"  , "YesNo"  , "UT_Number"      , "(no unit type)"        }));
			px.Add(DefineParameter(new [] {"No "            , "Italic"               , "Integer"  , "YesNo"  , "UT_Number"      , "(no unit type)"        }));
			px.Add(DefineParameter(new [] {"No "            , "Underline"            , "Integer"  , "YesNo"  , "UT_Number"      , "(no unit type)"        }));
			px.Add(DefineParameter(new [] {"Opaque "        , "Background"           , "Integer"  , "Invalid", "UT_Number"      , "(no unit type)"        }));
			px.Add(DefineParameter(new [] {"1"              , "Width Factor"         , "Double "  , "Number ", "UT_Number"      , "DUT_GENERAL "          }));
			px.Add(DefineParameter(new [] {"No "            , "Show Border"          , "Integer"  , "YesNo"  , "UT_Number"      , "(no unit type)"        }));
			px.Add(DefineParameter(new [] {"5/64\""         , "Leader/Border Offset" , "Double "  , "105"    , "UT_SheetLength ", "DUT_FRACTIONAL_INCHES" }));
			px.Add(DefineParameter(new [] {"Arrow 30 Degree", "Leader Arrowhead"     , "ElementId", "Invalid", "UT_Number"      , "(no unit type)"        }));

			TestFamilyData.Add("Schedule Default", px);

			px = new List<Parameter>();

		}

		public static Parameter DefineParameter(string[] pd)
		{
			bool result;
			Parameter p = new Parameter();
			p.Definition = new InternalDefinition();

			p.Definition.Name = pd[1];

			StorageType st;
			if (Enum.TryParse(pd[2], true, out st))
				p.StorageType = st;
			else
				p.StorageType = StorageType.None;

			ParameterType pt;
			if (pd[3].Equals("105"))
			{
				p.Definition.ParameterType = ParameterType.Length;
			}
			else
			{
				if (Enum.TryParse(pd[3], true, out pt))
					p.Definition.ParameterType = pt;
				else
					p.Definition.ParameterType = ParameterType.Invalid;
			}

			UnitType ut;
			if (Enum.TryParse(pd[4], true, out ut))
				p.Definition.UnitType = ut;
			else
				p.Definition.UnitType = UnitType.UT_Undefined;

			DisplayUnitType dut;
			if (Enum.TryParse(pd[5], true, out dut))
				p.DisplayUnitType = dut;
			else
				p.DisplayUnitType = DisplayUnitType.DUT_UNDEFINED;

			p.SetValueString(pd[0]);

			return p;
		}


	}
}











































