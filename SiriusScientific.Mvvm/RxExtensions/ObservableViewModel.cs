using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reflection;

namespace SiriusScientific.Mvvm.RxExtensions
{
	public class ObservableViewModel<T> : ViewModelBase.ViewModelBase, IObservableViewModel<T>
	{
		public IDisposable Subscribe(IObserver<T> observer)
		{
			throw new NotImplementedException();
		}

		public IObservable<TProperty> ToObservable<TSource, TProperty>( TSource source, Expression<Func<TSource, TProperty>> property )
				where TSource : INotifyPropertyChanged
		{
			var memberExpression = property.Body as MemberExpression;

			var propertyInfo = memberExpression.Member as PropertyInfo;

			string propertyName = propertyInfo.Name;

			Func<TSource, TProperty> propertySelector = property.Compile();

			return Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
				ev => source.PropertyChanged += RaisePropertyChanged,
				ev => source.PropertyChanged -= RaisePropertyChanged)
				.Where(ev => ev.EventArgs.PropertyName == propertyName)
				.Select(_ => propertySelector(source));
		}
	}
}
