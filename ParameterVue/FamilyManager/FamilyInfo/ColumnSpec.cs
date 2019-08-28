#region + Using Directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

#endregion


// projname: ParameterVue.FamilyManager.FamilyInfo
// itemname: ColumnSpec
// username: jeffs
// created:  8/24/2019 11:41:10 AM


namespace ParameterVue.FamilyManager.FamilyInfo
{
	// holds the definition information about a column
	public class ColumnSpec : INotifyPropertyChanged
	{
		private bool selected = false;

		public ColumnSpec(ParameterSpec paramSpec)
		{
			ParamSpec = paramSpec;
		}

		public ColumnSpec() {}

		public bool Selected
		{
			get => selected;
			set
			{
				if (selected != value) return;
				selected = value;

				OnPropertyChange();
			}
		}

		// the defined width for this column
		public int ColumnWidth { get; set; } = 100;

		// the defined width for this column
		public HorizontalAlignment ColumnAlignment { get; set; } = HorizontalAlignment.Stretch;

		// the type of control for this column
		public FrameworkElement Control { get; set; } = new TextBox();

		// the parameter definition for this column
		public ParameterSpec ParamSpec { get; set; } = null;

		// for columns that have a choices, this is the list of choices
		public Choices Choices { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}
	}


	// holds the list of possible choices for the column
	public class Choices
	{
		public bool StringCaseMatters { get; set; }
		public bool CanUseCustomValue { get; set; }
		public bool AddCustomChoicetoList { get; set; }
		public bool HasChoices { get; set; }

		private List<string> choices = new List<string>();

		public string this[int index]
		{
			get => choices[index];
			set => choices[index] = value;
		}

		public bool Add(string item)
		{
			if (FindItemIndex(item) == -1) return false;

			choices.Add(item);

			return true;
		}

		public int FindItemIndex(string item)
		{
			if (!StringCaseMatters)
				return choices.FindIndex(x => x.ToUpper().Equals(item.ToUpper()));

			return choices.FindIndex(x => x.Equals(item));
		}
	}
}
