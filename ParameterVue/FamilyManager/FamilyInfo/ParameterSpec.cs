#region + Using Directives

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Autodesk.Revit.DB;

#endregion


// projname: ParameterVue
// itemname: Header
// username: jeffs
// created:  8/18/2019 9:07:11 PM


namespace ParameterVue.FamilyManager.FamilyInfo
{
	// holds the information about the parameter
	public class ParameterSpec : INotifyPropertyChanged
	{
		private string name;
		private StorageType storageType;
		private ParameterType parameterType;
		private UnitType unitType;
		private DisplayUnitType dut;


		public ParameterSpec(string name, StorageType storageType, ParameterType parameterType, UnitType unitType, DisplayUnitType dut)
		{
			this.name = name;
			this.storageType = storageType;
			this.parameterType = parameterType;
			this.unitType = unitType;
			this.dut = dut;
		}

		public string Name
		{
			get => name;
			set
			{
				if (name.Equals(value)) return;

//				string[] parts = name.Split(' ');
//
//				if (parts.Length > 1)
//				{
//
//				}
//				else
//				{
//					name = value;
//				}

				name = value;

				OnPropertyChange();
			}
		}

		public StorageType StorageType
		{
			get => storageType;
			set
			{
				if (storageType.Equals(value)) return;
				storageType = value;

				OnPropertyChange();
			}
		}

		public ParameterType ParameterType
		{
			get => parameterType;
			set
			{
				if (parameterType.Equals(value)) return;
				parameterType = value;

				OnPropertyChange();
			}
		}

		public UnitType UnitType
		{
			get => unitType;
			set
			{
				if (unitType.Equals(value)) return;
				unitType = value;

				OnPropertyChange();
			}
		}

		public DisplayUnitType DUT
		{
			get => dut;
			set
			{
				if (dut.Equals(value)) return;
				dut = value;

				OnPropertyChange();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}
	}
}
