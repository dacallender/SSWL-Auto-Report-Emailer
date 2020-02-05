using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace SiriusScientific.Mvvm.ViewModelBase
{
	public class ViewModelBase : FrameworkElement, IViewModelBase
	{
		public event PropertyChangedEventHandler PropertyChanged;

		#region External Notifiers/Invokers for Property ChanhesyChanged

		public void NotifyPropertyChanged(string info)
		{
			//NotifyPropertyChanged(this, info); does not work if invoking a call from multithreaded environment

			RaisePropertyChanged(info);
		}

		public void NotifyPropertyChanged(object sender, string info)
		{
			PropertyChangedEventHandler handler = PropertyChanged;

			if (handler != null)
				handler(this, new PropertyChangedEventArgs(info));
		}

		public void NotifyPropertyChanged<T>(Expression<Func<T>> propertyExpression)
		{
			string name = GetPropertyName(propertyExpression);

			RaisePropertyChanged(name);
		}

		#endregion

		#region Internal notofication handlers

		public void RaisePropertyChanged(string propertyName)
		{
			if (Application.Current == null || Application.Current.Dispatcher.CheckAccess())
			{
				NotifyPropertyChanged(this, propertyName);
			}
			else
			{
				Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
					new ThreadStart(() => NotifyPropertyChanged(propertyName)));
			}
		}

		public void RaisePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			RaisePropertyChanged(e.PropertyName);
		}

		#endregion

		public string GetPropertyName(LambdaExpression expression)
		{
			var memberExp = expression.Body as MemberExpression;

			if (memberExp == null)
			{
				var unaryExp = expression.Body as UnaryExpression;

				if (unaryExp == null)
					throw new ArgumentException("Invalid format", "expression");

				memberExp = unaryExp.Operand as MemberExpression;
				if (memberExp == null)
					throw new ArgumentException("Invalid format", "expression");
			}
			return memberExp.Member.Name;
		}

		public virtual void Dispose()
		{
		}
	}
}
