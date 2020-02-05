using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;
//using System.Windows.Interactivity;
//using SiriusScientific.Mvvm.Bootstrap;

namespace SiriusScientific.Mvvm.DialogBase
{
	public abstract class DialogBaseBehavior : Behavior<Control>
	{
		public static readonly DependencyProperty DialogResultProperty = DependencyProperty.Register("DialogResult", typeof(IDialogResult),
			typeof(DialogBaseBehavior), new FrameworkPropertyMetadata(default(IDialogResult), OnResultPropertyChangedCallback) {BindsTwoWayByDefault = true});

		public static readonly DependencyProperty DialogParameterProperty = DependencyProperty.Register("DialogParameter", typeof(IDialogParameter), 
			typeof(DialogBaseBehavior), new FrameworkPropertyMetadata(default(IDialogResult), OnParameterPropertyChangedCallback) { BindsTwoWayByDefault = true });

		public static readonly DependencyProperty DialogContentProperty = DependencyProperty.Register("DialogContent", typeof(object), 
			typeof(DialogBaseBehavior), new FrameworkPropertyMetadata(default(IDialogResult), OnDialogContentPropertyChangedCallback) { BindsTwoWayByDefault = true });

		private static void OnDialogContentPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			
		}

		public object DialogContent
		{
			get { return (object) GetValue(DialogContentProperty); }
			set { SetValue(DialogContentProperty, value); }
		}

		public DialogBaseBehavior()
		{
			Owner = Application.Current.MainWindow;

			Modality = DialogKind.Modal;

			WindowStartupLocation = WindowStartupLocation.CenterOwner;
		}

		public IDialogConfig DialogConfig { get; set; }

		public virtual void OnNotifyDialogClosingEventHandler(IDialogResult dialogResult)
		{
			//Close the dialog box.
			OnClosing(dialogResult);

			Dialog.Close();
		}

		private static void OnResultPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
		}

		private static void OnParameterPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			
		}

		public IDialogResult DialogResult
		{
			get { return (IDialogResult)GetValue(DialogResultProperty); }
			set { SetValue(DialogResultProperty, value); }
		}

		public IDialogParameter DialogParameter
		{
			get { return (IDialogParameter)GetValue(DialogParameterProperty); }
			set { SetValue(DialogParameterProperty, value); }
		}

		protected virtual void AssociatedControlToDisplayDialog(object sender, RoutedEventArgs routedEventArgs)
		{
//			Bootstrapper.BootstrapInstance.RegisterThisUngrgisteredInstance<DialogBaseBehavior, DialogBaseBehavior>(this);

			Dialog = CreateDialogInstance();

			SetupDialogProperties();

			if (DialogConfig != null) Modality = DialogConfig.DialogKind;

			if (Dialog != null)
			{
				Dialog.Owner = Owner;

				Dialog.WindowStartupLocation = WindowStartupLocation;

				if(Modality == DialogKind.Modal) DisplayModal();

				else DisplayModeless();
			}
		}


		// ********************************************************************************
		/// <summary>
		/// Display Modal dialog via Dialog.ShowDialog()
		/// </summary>
		/// <created>David,5/1/2017</created>
		/// <changed>David,5/1/2017</changed>
		// ********************************************************************************
		public abstract void DisplayModal();

		// ********************************************************************************
		/// <summary>
		/// Display modeless dialog via Dialog.Show()
		/// </summary>
		/// <created>David,5/1/2017</created>
		/// <changed>David,5/1/2017</changed>
		// ********************************************************************************
		public abstract void DisplayModeless();
		//{
		//	if(Modality == DialogKind.Modal) Dialog.ShowDialog();

		//	else Dialog.Show();
		//}

		/// <summary>
		/// overriden by the child behavior implementation.
		/// </summary>
		/// <param name="obj"></param>
		protected virtual void OnClosing(object obj)
		{
		}

		protected abstract Window CreateDialogInstance();

		protected abstract void SetupDialogProperties();

		protected Window Dialog { get; set; }

		protected DialogKind Modality { get; set; }

		protected Window Owner { get; set; }

		protected WindowStartupLocation WindowStartupLocation { get; set; }
	}
}
