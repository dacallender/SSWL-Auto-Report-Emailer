using System;
using SiriusScientific.Mvvm.Bootstrap;

namespace SiriusScientific.Mvvm.Mediator
{
	public class Mediator<TMessageType> 
	{
		private static Mediator<TMessageType> _singletonMmediatorInstance; 

		private readonly RegisteredVObjectContainer<TMessageType, Action<Object>> _registreeContainer;
		
		private static readonly object padlock = new object();

		public Mediator()
		{
			Bootstrapper.BootstrapInstance.CreateInstanceOfThis<RegisteredVObjectContainer<TMessageType, Action<Object>>,
				RegisteredVObjectContainer<TMessageType, Action<Object>>>();
			
			_registreeContainer = Bootstrapper.BootstrapInstance.GetInstanceOfThis<RegisteredVObjectContainer<TMessageType, Action<Object>>>();	
		}

		public static Mediator<TMessageType> MediatorInstance
		{
			get
			{
				lock ( padlock )
				{
					if ( _singletonMmediatorInstance == null )
					{
						_singletonMmediatorInstance = new Mediator<TMessageType>();

						return _singletonMmediatorInstance;
					}
					else
						return _singletonMmediatorInstance;
				}
			}
		}

		public void RegisterCallback(Action<Object> callback, TMessageType message)
		{
			_registreeContainer.AddValue(message, callback);
		}

		public void NotifyRegisterees(TMessageType message, object args)
		{
			if ( _registreeContainer.ContainsKey(message) )
			{
				foreach ( Action<object> callback in _registreeContainer[message] )
				{
					callback(args);
				}
			}
		}
	}
}
