using System.Windows.Forms;
using System.Windows.Input;
using SiriusScientific.Mvvm.ViewModelBase;

namespace SiriusScientific.Mvvm.DialogBase
{
	public abstract class DialogExtendedBaseViewModel : DialogBaseViewModel
	{
		public ICommand OnAbortCommand
		{
			get
			{
				return new RelayCommand(ExecuteOnAbortCommand, CanExecuteOnAbortCommand);
			}
		}

		public ICommand OnRetryCommand
		{
			get
			{
				return new RelayCommand(ExecuteOnRetryCommand, CanExecuteOnRetryCommand);
			}
		}

		public ICommand OIgnoreCommand
		{
			get
			{
				return new RelayCommand(ExecuteOnIgnoreCommand, CanExecuteOnIgnoreCommand);
			}
		}

		public ICommand OnNoneCommand
		{
			get
			{
				return new RelayCommand(ExecuteOnNoneCommand, CanExecuteOnNoneCommand);
			}
		}

		public ICommand OnYesCommand
		{
			get
			{
				return new RelayCommand(ExecuteOnYesCommand, CanExecuteOnYesCommand);
			}
		}

		public ICommand OnNoCommand
		{
			get
			{
				return new RelayCommand(ExecuteOnNoCommand, CanExecuteOnNoCommand);
			}
		}

		protected virtual void ExecuteOnAbortCommand(object obj)
		{
			ReturnState = DialogResult.Abort;

			DialogBaseBehavior.DialogResult = obj as IDialogResult;

			NotifyPropertyChanged(() => DialogBaseBehavior.DialogResult);
		}

		protected virtual void ExecuteOnRetryCommand(object obj)
		{
			ReturnState = DialogResult.Retry;

			DialogBaseBehavior.DialogResult = obj as IDialogResult;

			NotifyPropertyChanged(() => DialogBaseBehavior.DialogResult);
		}

		protected virtual void ExecuteOnIgnoreCommand(object obj)
		{
			ReturnState = DialogResult.Ignore;

			DialogBaseBehavior.DialogResult = obj as IDialogResult;

			NotifyPropertyChanged(() => DialogBaseBehavior.DialogResult);
		}

		protected virtual void ExecuteOnNoneCommand(object obj)
		{
			ReturnState = DialogResult.None;

			DialogBaseBehavior.DialogResult = obj as IDialogResult;

			NotifyPropertyChanged(() => DialogBaseBehavior.DialogResult);
		}

		protected virtual void ExecuteOnYesCommand(object obj)
		{
			ReturnState = DialogResult.Yes;

			DialogBaseBehavior.DialogResult = obj as IDialogResult;

			NotifyPropertyChanged(() => DialogBaseBehavior.DialogResult);
		}

		protected virtual void ExecuteOnNoCommand(object obj)
		{
			ReturnState = DialogResult.No;

			DialogBaseBehavior.DialogResult = obj as IDialogResult;

			NotifyPropertyChanged(() => DialogBaseBehavior.DialogResult);
		}

		protected abstract bool CanExecuteOnAbortCommand(object obj);
		protected abstract bool CanExecuteOnRetryCommand(object obj);
		protected abstract bool CanExecuteOnIgnoreCommand(object obj);
		protected abstract bool CanExecuteOnNoneCommand(object obj);
		protected abstract bool CanExecuteOnYesCommand(object obj);
		protected abstract bool CanExecuteOnNoCommand(object obj);
		
	}
}
