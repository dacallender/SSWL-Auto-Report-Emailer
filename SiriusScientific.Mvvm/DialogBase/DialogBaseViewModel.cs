using System.Windows.Forms;
using System.Windows.Input;
//using SiriusScientific.Mvvm.Bootstrap;
using SiriusScientific.Mvvm.ViewModelBase;

namespace SiriusScientific.Mvvm.DialogBase
{
	public abstract class DialogBaseViewModel : ViewModelBase.ViewModelBase, IDialogResult
	{
		private bool _okButtonVisibility;
		public DialogResult ReturnState { get; set; }
		public object ReturnValue { get; set; }

		public DialogBaseBehavior DialogBaseBehavior { get; set; }

		public DialogBaseViewModel()
		{
			Bootstrap();

			OkButtonVisibility = true;

			if(DialogBaseBehavior != null) PassedParameterValue = DialogBaseBehavior.DialogParameter;
		}

		public ICommand OnOkCommand
		{
			get
			{
				return RegisterOnOkCommand();				
			}
		}

		public ICommand OnCancelCommand
		{
			get
			{
				return RegisterOnCancelCommand();				
			}
		}

		protected virtual void Bootstrap()
		{
			//do this if the behavior has not been retrieved in the override.
//			DialogBaseBehavior = Bootstrapper.BootstrapInstance.GetInstanceOfThis<DialogBaseBehavior>();
		}

		protected virtual RelayCommand RegisterOnOkCommand()
		{
			return new RelayCommand(ExecuteOnOkCommand, CanExecuteOnOkCommand);;
		}

		protected virtual RelayCommand RegisterOnCancelCommand()
		{
			return new RelayCommand(ExecuteOnCancelCommand, CanExecuteOnCancelCommand);;
		}

		protected virtual void ExecuteOnOkCommand(object obj)
		{
			ReturnState = DialogResult.OK;

			var dialogResult = obj as IDialogResult;

			if (dialogResult != null) dialogResult.ReturnState = ReturnState;

			var result = obj as IDialogResult;

			if (result != null) result.ReturnValue = obj;

			DialogBaseBehavior.DialogResult = dialogResult;

			NotifyPropertyChanged(() => DialogBaseBehavior.DialogResult);

			DialogBaseBehavior.OnNotifyDialogClosingEventHandler(DialogBaseBehavior.DialogResult);
		}

		protected virtual void ExecuteOnCancelCommand(object obj)
		{
			ReturnState = DialogResult.Cancel;

			var dialogResult = obj as IDialogResult;

			if (dialogResult != null) dialogResult.ReturnState = ReturnState;

			var result = obj as IDialogResult;

			if (result != null) result.ReturnValue = null;

			DialogBaseBehavior.DialogResult = obj as IDialogResult;

			NotifyPropertyChanged(() => DialogBaseBehavior.DialogResult);

			DialogBaseBehavior.OnNotifyDialogClosingEventHandler(DialogBaseBehavior.DialogResult);
		}

		protected abstract bool CanExecuteOnCancelCommand(object obj);

		protected abstract bool CanExecuteOnOkCommand(object obj);
		public IDialogParameter PassedParameterValue { get; set; }

		public bool OkButtonVisibility
		{
			get { return _okButtonVisibility; }
			set
			{
				_okButtonVisibility = value; 
				NotifyPropertyChanged(()=>OkButtonVisibility);
			}
		}
	}
}
