#region + Using Directives




#endregion


// projname: ParameterVue.FamilyManager.FamilyInfo
// itemname: Parameter
// username: jeffs
// created:  8/25/2019 6:45:13 PM


using System;
using System.CodeDom;
using System.Drawing.Text;
using System.Dynamic;

namespace Autodesk.Revit.DB
{
	public static class DocumentUnit
	{
//		public static Units LengthUnits { get; private set; }
//
//		static DocumentUnit()
//		{
//			LengthUnits = new Units(UnitSystem.Imperial);
//
//			FormatOptions x = new FormatOptions(DisplayUnitType.DUT_BARS);
//
//			FormatOptions f = new FormatOptions()
//			{
//				Accuracy = .0001,
//				DisplayUnits = DisplayUnitType.DUT_UNDEFINED,
//				SuppressLeadingZeros = true,
//				SuppressTrailingZeros = false,
//				SuppressSpaces = true,
//				UnitSymbol = UnitSymbolType.UST_FOOT_SINGLE_QUOTE,
//				RoundingMethod = RoundingMethod.Nearest
//			};
//
//
//			LengthUnits.SetFormatOptions(UnitType.UT_Length, f);
//		}
	}

	// this is a faux revit parameter
	public class Parameter
	{
		private dynamic internalValue = null;

		public InternalDefinition Definition { get; set; }
		public DisplayUnitType DisplayUnitType { get; set; }
		private StorageType storageType;

		public Parameter()
		{
			internalValue = null;
		}

		public bool HasValue
		{
			get => internalValue != null;
		}

		public StorageType StorageType
		{
			get => storageType;
			set
			{ 
				storageType = value;

			}
		}

		public bool set(double value)
		{
			if (StorageType != StorageType.Double) return false;

			internalValue = value;

			return true;
		}

		public bool set(int value)
		{
			if (StorageType != StorageType.Integer) return false;

			internalValue = value;

			return true;
		}

		public bool set(string value)
		{
			if (StorageType != StorageType.String) return false;

			internalValue = value;

			return true;
		}
		
		public bool set(ElementId value)
		{
			if (StorageType != StorageType.ElementId) return false;

			internalValue = value;

			return true;
		}

		public double AsDouble()
		{
			if (StorageType != StorageType.Double) return Double.NaN;

			return internalValue;
		}


		public int AsInteger()
		{
			if (StorageType != StorageType.Integer) return -1;

			return internalValue;
		}

		public ElementId AsElementId()
		{
			if (StorageType != StorageType.ElementId) return ElementId.InvalidElementId;

			return internalValue;
		}

		public string AsString()
		{
			if (StorageType != StorageType.String) return null;

			return internalValue;
		}
		
		public string AsValueString()
		{

			return internalValue;
		}

		public bool SetValueString(string value)
		{
			internalValue = value;

			return true;

		}
//
//			bool result = false;
//			try
//			{
//				if (StorageType == StorageType.Double)
//				{
//					double answer;
//
//					result = UnitFormatUtils.TryParse(DocumentUnit.LengthUnits, 
//						Definition.UnitType, value, out answer);
//
//					if (result)
//					{
//						set(answer);
//					}
//				} 
//				else if (StorageType == StorageType.String)
//				{
//					set(value);
//				} 
//				else if (StorageType == StorageType.Integer)
//				{
//					double answer;
//
//					result = UnitFormatUtils.TryParse(DocumentUnit.LengthUnits,
//						Definition.UnitType, value, out answer);
//
//					set(int.Parse(value));
//				} 
//				else if (StorageType == StorageType.ElementId)
//				{
//					result = set(ElementId.InvalidElementId);
//				}
//				else
//				{
//					result = false;
//				}
//			}
//			catch
//			{
//				result = false;
//			}
//
//			return result;
//		}
	}

	public class InternalDefinition
	{
		public string Name { get; set; }
		public UnitType UnitType { get; set; }
		public ParameterType ParameterType { get; set; }
	}

	public class ElementId
	{
		public static readonly ElementId InvalidElementId = 
			new ElementId() { IntegerValue = -1 };
		public int IntegerValue { get; set; }
	}

	public enum DisplayUnitType
	{
		DUT_UNDEFINED = -1,
		DUT_FRACTIONAL_INCHES,
		DUT_GENERAL
	}

	public enum StorageType
	{
		None = 0,
		String,
		Double,
		ElementId,
		Integer
	}

	public enum UnitType
	{
		UT_Undefined = -1,
		UT_Number = 1,
		UT_SheetLength
	}

	public  enum ParameterType
	{
		Invalid = -1,
		Text = 1,
		YesNo,
		Number,
		Length
		
	}
}
