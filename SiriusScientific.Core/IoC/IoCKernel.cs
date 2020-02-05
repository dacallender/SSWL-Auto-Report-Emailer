using System.Collections.Generic;
using Ninject;
using Ninject.Modules;

namespace SiriusScientific.Core.IoC
{
	public class IoCKernel : StandardKernel
	{
		public static IKernel Kernel { get; set; }

		public static List<INinjectModule> NinjectModules { get; set; }

		static IoCKernel()
		{
			NinjectModules = new List<INinjectModule>();
		}

		public static IKernel Init(INinjectModule module)
		{
			//
			NinjectModules.Add(module);

			Kernel = new StandardKernel(NinjectModules.ToArray());

			return Kernel;
		}

		public static T Get<T>()
		{
			return Kernel.Get<T>();
		}
	}
}