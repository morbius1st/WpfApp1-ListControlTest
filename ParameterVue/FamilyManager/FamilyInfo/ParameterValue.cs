#region + Using Directives

using System.ComponentModel;
using System.Runtime.CompilerServices;

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
		private string paramValue;
		private string paramOrigValue;
		private bool revised;

		public ParameterValue(string paramValue, string paramOrigValue)
		{
			ParamValue = paramValue;
			ParamOrigValue = paramOrigValue;
		}

		public ParameterValue(string paramValue)
		{
			ParamValue = paramValue;
			ParamOrigValue = paramValue;
		}

		public string ParamValue
		{
			get => paramValue;
			set
			{
				if (!value.Equals(paramValue))
				{
					paramValue = value;

					updateRevised();

					OnPropertyChange();
				}
			}
		}

		public string ParamOrigValue
		{
			get => paramOrigValue;
			set
			{
				if (!value.Equals(paramOrigValue))
				{
					paramOrigValue = value;

					updateRevised();

					OnPropertyChange();
				}
			}
		}

		private void updateRevised()
		{
			if (paramOrigValue == null || paramValue == null) return;

			Revised = !(paramOrigValue.Equals(paramValue));
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

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}
	}
}
