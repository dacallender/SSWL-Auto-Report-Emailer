using Ninject;
using SiriusScientific.Core.IoC;
using SSWLAutoReport.ViewModel;

namespace SSWLAutoReport.IoC
{
	public class MainViewServiceLocator
	{
		public MainWindowViewModel MainViewModel => IoCKernel.Kernel.Get<MainWindowViewModel>();

		public MainViewServiceLocator()
		{
			IoCKernel.Init(new MainViewServiceModule());
		}
	}
}