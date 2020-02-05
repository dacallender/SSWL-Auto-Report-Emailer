using System;
using System.Collections.Generic;
using System.Linq;

namespace SiriusScientific.Mvvm.IoC
{
	public sealed class ObjectContainer : IObjectContainer
	{
		private  static ObjectContainer _objectContainer;

		private readonly IList<RegisteredObject> _registeredObjectList;

		private readonly Dictionary<string, RegisteredObject> _registeredNamedObjectList;
		
		private static readonly object padlock = new object();

		private ObjectContainer()
		{
			_registeredNamedObjectList = new Dictionary<string, RegisteredObject>();

			_registeredObjectList = new List<RegisteredObject>();
		}

		public static ObjectContainer IoCObjectContainerInstance
		{
			get
			{
				lock ( padlock )
				{
					if ( _objectContainer == null )
					{
						_objectContainer = new ObjectContainer();

						return _objectContainer;
					}
					else
						return _objectContainer;
				}
			}
		}

		public void RegisterObject<TTypeToResolve, TConcrete>()
		{
			RegisterObject<TTypeToResolve, TConcrete>(LifeCycle.Singleton);
		}

		public void RegisterObject<TTypeToResolve, TConcrete>( LifeCycle lifeCycle )
		{
			_registeredObjectList.Add(new RegisteredObject(typeof(TTypeToResolve), typeof(TConcrete), lifeCycle));
		}

		public void RegisterNamedObject<TTypeToResolve, TConcrete>( string name )
		{
			RegisterNamedObject<TTypeToResolve, TConcrete>(LifeCycle.Singleton, name);
		}

		public void RegisterNamedObject<TTypeToResolve, TConcrete>( LifeCycle lifeCycle, string name )
		{
			_registeredNamedObjectList.Add(name, new RegisteredObject(typeof(TTypeToResolve), typeof(TConcrete), lifeCycle));
		}

		public void AddUnregisteredObject<TTypeToResolve, TConcrete>( TTypeToResolve unregisteredObject )
		{
			_registeredObjectList.Add(new RegisteredObject(typeof(TTypeToResolve), typeof(TConcrete), unregisteredObject));
		}

		public void AddUnregisteredNamedObject<TTypeToResolve, TConcrete>( TTypeToResolve unregisteredObject, string name )
		{
			_registeredNamedObjectList.Add(name, new RegisteredObject(typeof(TTypeToResolve), typeof(TConcrete), unregisteredObject));
		}

		public TTypeToResolve ResolveObject<TTypeToResolve>()
		{
			return (TTypeToResolve)ResolveObject(typeof(TTypeToResolve));
		}

		private object ResolveObject( Type typeToResolve )
		{
			var registeredObject = _registeredObjectList.FirstOrDefault(o => o.TypeToResolve == typeToResolve);

			if ( registeredObject == null )
			{
				//throw new TypeNotRegisteredException(string.Format(
				//	"The type {0} has not been registered", typeToResolve.Name));

				return null;
			}
			return GetInstance(registeredObject);
		}


		public TTypeToResolve ResolveNamedObject<TTypeToResolve>(string name)
		{
			return (TTypeToResolve)ResolveNamedObject(typeof(TTypeToResolve), name);
		}

		private object ResolveNamedObject( Type typeToResolve, string name)
		{
			RegisteredObject registeredObject = null;

			_registeredNamedObjectList.TryGetValue(name, out registeredObject);

			if ( registeredObject == null )
			{
				throw new TypeNotRegisteredException(string.Format(
					"The type {0} has not been registered", typeToResolve.Name));
			}
			return GetInstance(registeredObject);
		}


		private object GetInstance( RegisteredObject registeredObject )
		{
			if ( registeredObject.Instance == null ||
				registeredObject.LifeCycle == LifeCycle.Transient )
			{
				var parameters = ResolveConstructorParameters(registeredObject);
				registeredObject.CreateInstance(parameters.ToArray());
			}
			return registeredObject.Instance;
		}

		private IEnumerable<object> ResolveConstructorParameters( RegisteredObject registeredObject )
		{
			var constructorInfo = registeredObject.ConcreteType.GetConstructors().First();
			foreach ( var parameter in constructorInfo.GetParameters() )
			{
				yield return ResolveObject(parameter.ParameterType);
			}
		}
	}
}
