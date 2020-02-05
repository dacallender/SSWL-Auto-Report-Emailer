using System;
using System.Windows.Input;

namespace SiriusScientific.Mvvm.ViewModelBase
{
	public class RelayCommand : ICommand
	{
		private readonly Action<object> _execute;

		private readonly Predicate<object> _canExecute;

		private event EventHandler CanExecuteChangedInternal;

		public RelayCommand()
		{
			_execute = null;
			_execute= null;
		}
		public RelayCommand(Action<object> execute)
			: this(execute, null)
		{
			if (execute != null) _execute = execute;
		}

		public RelayCommand(Action<object> execute, Predicate<object> canExecute)
		{
			if (execute != null) _execute = execute;
			_canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
				CanExecuteChangedInternal += value;
			}
			remove
			{
				CommandManager.RequerySuggested -= value;

				//CommandManager.InvalidateRequerySuggested();

				CanExecuteChangedInternal -= value;
			}
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute == null ? true : _canExecute(parameter);
		}

		public void Execute(object parameter)
		{
			_execute(parameter);
		}

		protected virtual void OnCanExecuteChangedInternal()
		{
			var handler = CanExecuteChangedInternal;
			if (handler != null) handler(this, EventArgs.Empty);
		}
	}
}