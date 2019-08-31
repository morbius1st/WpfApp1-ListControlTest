#region + Using Directives

using System.ComponentModel;
using System.Runtime.CompilerServices;
using Autodesk.Revit.DB;

#endregion


// projname: ParameterVue
// itemname: ParameterValue
// username: jeffs
// created:  8/18/2019 9:08:35 PM


namespace ParameterVue.FamilyManager.FamilyInfo
{
	// holds a single parameter item - data & header info
	public class ParameterValue : INotifyPropertyChanged
	{
//		private string paramValue;
		private string paramOrigValue;
		private bool revised;
		private bool invalid;

		private Parameter parameter;

		public ParameterValue (Parameter parameter)
		{
			this.parameter = parameter;

			paramOrigValue = ParamValue;

			revised = false;
			invalid = false;
		}

		public string ParamValue
		{
			get => parameter.AsValueString();
			set
			{
				if (string.IsNullOrWhiteSpace(value)) return;

				string priorValue = ParamValue;

				bool result = parameter.SetValueString(value);

				if (!result) // invalid value entered
				{
					invalid = true;
					return;
				}

				string newValue = parameter.AsValueString();

				if (!newValue.Equals(priorValue))
				{
					Revised = true;
					OnPropertyChange();
				}
				else
				{
					Revised = false;
				}
			}
		}

		public string ParamOrigValue
		{
			get => paramOrigValue;
//			private set
//			{
//				if (!value.Equals(paramOrigValue))
//				{
//					paramOrigValue = value;
//
//					updateRevised();
//
//					OnPropertyChange();
//				}
//			}
		}

		public bool Revised
		{
			get => revised;
			set
			{
				if (value != revised)
				{
					revised = value;
					OnPropertyChange();
				}
			}
		}

		public bool Invalid
		{
			get => invalid;
			set
			{
				invalid = value;
				OnPropertyChange();
			}
		}

		public void Commit()
		{
			if (revised)
			{
				paramOrigValue = parameter.AsValueString();

				Revised = false;
				Invalid = false;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}
	}
}
