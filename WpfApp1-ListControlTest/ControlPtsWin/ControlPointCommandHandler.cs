// Solution:     WpfApp1-ListControlTest
// Project:       WpfApp1-ListControlTest
// File:             ControlPointCommandHandler.cs
// Created:      -- ()

using System;
using System.Windows.Input;

namespace WpfApp1_ListControlTest.ControlPtsWin
{
	public class ButtonClickCommandHandler : ICommand
	{
		private Action<object> _action;
		private bool _canExecute;

		public ButtonClickCommandHandler(Action<object> action, bool canExecute)
		{
			_action     = action;
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute;
		}

	#pragma warning disable CS0067 // The event 'ControlPoints.ButtonClickCommandHandler.CanExecuteChanged' is never used
		public event EventHandler CanExecuteChanged;
	#pragma warning restore CS0067 // The event 'ControlPoints.ButtonClickCommandHandler.CanExecuteChanged' is never used

		public void Execute(object parameter)
		{
			_action(parameter);
		}
	}

	public class ValidationErrorCommandHandler : ICommand
	{
		private Action<object> _action;
		private bool _canExecute;

		public ValidationErrorCommandHandler(Action<object> action, bool canExecute)
		{
			_action = action;
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute;
		}
	#pragma warning disable CS0067 // The event 'ControlPoints.ButtonClickCommandHandler.CanExecuteChanged' is never used
		public event EventHandler CanExecuteChanged;
	#pragma warning restore CS0067 // The event 'ControlPoints.ButtonClickCommandHandler.CanExecuteChanged' is never used

		public void Execute(object parameter)
		{
			_action(parameter);
		}
	}
}