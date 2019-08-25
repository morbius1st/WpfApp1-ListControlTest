#region + Using Directives

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

#endregion


// projname: ParameterVue.Parameters
// itemname: Header
// username: jeffs
// created:  8/18/2019 9:07:11 PM


namespace ParameterVue.FamilyManager.FamilyInfo
{
	// holds the information about the parameter
	public class ParameterSpec : INotifyPropertyChanged
	{
		private string name;
		private string storageType;
		private string parameterType;
		private string unitType;
		private string dut;


		public ParameterSpec(string name, string storageType, string parameterType, string unitType, string dut)
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
				name = value;

				OnPropertyChange();
			}
		}

		public string StorageType
		{
			get => storageType;
			set
			{
				if (storageType.Equals(value)) return;
				storageType = value;

				OnPropertyChange();
			}
		}

		public string ParameterType
		{
			get => parameterType;
			set
			{
				if (parameterType.Equals(value)) return;
				parameterType = value;

				OnPropertyChange();
			}
		}

		public string UnitType
		{
			get => unitType;
			set
			{
				if (unitType.Equals(value)) return;
				unitType = value;

				OnPropertyChange();
			}
		}

		public string DUT
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
