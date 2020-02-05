﻿using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;
namespace SiriusScientific.Mvvm.CommandAction
{
	public class CustomCommandAction : TriggerAction<DependencyObject>
	{
		public static readonly DependencyProperty CommandParameterProperty =
			DependencyProperty.Register("CommandParameter", typeof(object), typeof(CustomCommandAction), new FrameworkPropertyMetadata(OnCommandParameterChanged)
			{
				BindsTwoWayByDefault = true
			});

		private static void OnCommandParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			
		}

		public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
			"Command", typeof(ICommand), typeof(CustomCommandAction), null);

		public ICommand Command
		{
			get
			{
				return (ICommand)GetValue(CommandProperty);
			}
			set
			{
				this.SetValue(CommandProperty, value);
			}
		}

		public object CommandParameter
		{
			get
			{
				return this.GetValue(CommandParameterProperty);
			}

			set
			{
				this.SetValue(CommandParameterProperty, value);
			}
		}

		protected override void Invoke(object parameter)
		{
			if (this.AssociatedObject != null)
			{
				ICommand command = this.Command;
				if (command != null)
				{
					if (this.CommandParameter != null)
					{
						if (command.CanExecute(this.CommandParameter))
						{
							command.Execute(this.CommandParameter);
						}
					}
					else
					{
						if (command.CanExecute(parameter))
						{
							command.Execute(parameter);
						}
					}
				}
			}
		}
	}
}
