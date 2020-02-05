Parent Dialog Implementation

1) Include the SiriusScientific.Mvvm and System.Windows.Interactivity.WPF framework.

2) Create a Behavior that inherits DialogBehavior directly from the base class SiriusScientific.Mvvm.DialogBase.

3) Override the OnAttached method and link the Control event Handler to activate the child dialog to "AssociatedControlToDisplayDialog" in DialogBase.

5) Create an instance of the child Dialog inside the CreateDialogInstance() method.

6) Override the Modality, Owner and WindowStartupLocation inside the SetupDialogProperties if necessary. Additional sttings may be overriden by using the "Dialog" property.

7) Bind the DialogParameter and DialogResult properties on the behavior in xaml implementation. Note that each binded property must call NotifyOnPropertyChanged in the viewmodel to see the DialogResult or pass a DialogParameter.

8) To pass parameters to the child dialog and obtain returned results to the host viewmodel, let the host viewmodel inherit IDialogReturn, IDialogPassParameter. This will set up the necessary properties to store these results.


Child Dialog implementation

1) ChildDialog ViewModel must inherit from DialogBaseViewModel or DialogExtendedBaseViewModel if a DialogResult is to be returned or a DialogParameter is to be passed otherwise, inherit from ViewModelBase.

2) Do: <DataModel>.ReturnValue = <desied object to return> to send the result back to the parent dialog.

3) In xaml, for OK and Candel buttons, bind the Command properties as follows: Command="{Binding OnOkCommand}" and Command="{Binding OnCancelCommand}".

4) Optionally, if DialogExtendedBaseViewModel i inherited, do the same as 3 for OnAbortCommand, OnRetryCommand, OIgnoreCommand, OnNoneCommand, OnYesCommand and OnNoCommand.

4) Implement all relevant abstract CanExecute overrides for ech implemented button in viewmodel.

5) Implement all relavent virtual Eecute overrides for implemented button in viewmodel.

6) In each ecute override, do base.ExecuteOn<...>Command((IDialogResult)Model);

7) Model may inherit from IDialogResult and/or IDialogParameter to setup necessary return fields that can then be passed as IDialogResult or IDialogParameter types.
 


