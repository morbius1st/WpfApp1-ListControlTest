// Solution:     WpfApp1-ListControlTest
// Project:       ParameterVue
// File:             CustomProperties.cs
// Created:      -- ()

using System.Windows;

namespace ParameterVue.WpfSupport.CustomXAMLProperties 
{

	public class CustomProperties
	{
		public static readonly DependencyProperty GenericBoolOneProperty = DependencyProperty.RegisterAttached(
			"GenericBoolOne", typeof(bool), typeof(CustomProperties),
			new PropertyMetadata(false));

	#region GenericBoolOne

		public static void SetGenericBoolOne(UIElement e, bool value)
		{
			e.SetValue(GenericBoolOneProperty, value);
		}

		public static bool GetGenericBoolOne(UIElement e)
		{
			return (bool) e.GetValue(GenericBoolOneProperty);
		}

	#endregion
	}
}