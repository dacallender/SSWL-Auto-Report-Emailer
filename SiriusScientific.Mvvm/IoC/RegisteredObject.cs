using System;

namespace SiriusScientific.Mvvm.IoC
{
	public class RegisteredObject
	{
		public RegisteredObject(Type typeToResolve, Type concreteType, LifeCycle lifeCycle)
		{
			LifeCycycle = lifeCycle;
			TypeToResolve = typeToResolve;
			ConcreteType = concreteType;
		}

		public LifeCycle LifeCycycle { get; set; }

		public RegisteredObject(Type typeToResolve, Type concreteType, object targetObject)
		{
			LifeCycycle = LifeCycle.Singleton;
			TypeToResolve = typeToResolve;
			ConcreteType = concreteType;
			Instance = targetObject;
		}

		public Type TypeToResolve
		{
			get;
			private set;
		}

		public Type ConcreteType
		{
			get;
			private set;
		}

		public object Instance
		{
			get;
			private set;
		}

		public LifeCycle LifeCycle { get; set; }

		public void CreateInstance( params object[] args )
		{
			this.Instance = Activator.CreateInstance(this.ConcreteType, args);
		}
	}
}
