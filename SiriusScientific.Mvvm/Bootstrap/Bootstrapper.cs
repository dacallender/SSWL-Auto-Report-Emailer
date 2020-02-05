using SiriusScientific.Mvvm.IoC;

namespace SiriusScientific.Mvvm.Bootstrap
{
	public class Bootstrapper
	{
		private static Bootstrapper _singleBootstrap; 
		
		private static readonly object padlock = new object();

		private Bootstrapper()
		{
		}

		public static Bootstrapper BootstrapInstance
		{
			get
			{
				lock (padlock)
				{
					if ( _singleBootstrap == null )
					{
						_singleBootstrap = new Bootstrapper();

						return _singleBootstrap;
					}
					else
						return _singleBootstrap;
				}
			}
		}

		public ObjectContainer IoC
		{
		get
			{
				return ObjectContainer.IoCObjectContainerInstance;
			}
		}

		public void CreateInstanceOfThis<TTargetType, TConcreteType>()
		{
			IoC.RegisterObject<TTargetType, TConcreteType>();
		}

		public void CreateNamedInstanceOfThis<TTargetType, TConcreteType>(string name)
		{
			IoC.RegisterNamedObject<TTargetType, TConcreteType>(name);
		}

		public TTargetType GetInstanceOfThis<TTargetType>()
		{
			return IoC.ResolveObject<TTargetType>();
		}

		public TTargetType GetNamedInstanceOfThis<TTargetType>(string name)
		{
			return IoC.ResolveNamedObject<TTargetType>(name);
		}

		public void RegisterThisUngrgisteredInstance<TTargetType, TConcreteType>( TTargetType instance )
		{
			IoC.AddUnregisteredObject<TTargetType, TConcreteType>(instance);
		}

		public void RegisterThisNamedUngrgisteredInstance<TTargetType, TConcreteType>( TTargetType instance, string name )
		{
			IoC.AddUnregisteredNamedObject<TTargetType, TConcreteType>(instance, name);
		}

		public TTargetType GetThisParentWindowHandle<TTargetType>()
		{
			return IoC.ResolveObject<TTargetType>();
		}
	}
}
