using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace SiriusScientific.Mvvm.ViewModelBase
{
	public interface IViewModelBase : INotifyPropertyChanged, IDisposable
	{
		void NotifyPropertyChanged( string info );

		void NotifyPropertyChanged( object sender, string info );

		void NotifyPropertyChanged<T>( Expression<Func<T>> propertyExpression );

		void RaisePropertyChanged( string propertyName );

		void RaisePropertyChanged( object sender, PropertyChangedEventArgs e );

		string GetPropertyName( LambdaExpression expression );
	}
}
