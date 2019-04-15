// Solution:     WpfApp1-ListControlTest
// Project:       WpfApp1-ListControlTest
// File:             ValidationRule.cs
// Created:      -- ()

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Controls;

namespace WpfApp1_ListControlTest.Validation
{
	public class TbxValueValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			if (((string) value).Length > 10)
			{
				return new ValidationResult(false, "string too long");
			}
			return new ValidationResult(true, null);
		}

	}

	public class ErrorRule : INotifyDataErrorInfo
	{
		public IEnumerable GetErrors(string propertyName)
		{
			yield break;
		}

		public bool HasErrors { get; }

#pragma warning disable CS0067 // The event 'ErrorRule.ErrorsChanged' is never used
		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
#pragma warning restore CS0067 // The event 'ErrorRule.ErrorsChanged' is never used
	}

}