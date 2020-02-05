using Ninject.Modules;
using SSWLAutoReport.ViewModel;

namespace SSWLAutoReport.IoC
{
	public class MainViewServiceModule : NinjectModule
	{
		public override void Load()
		{
			Bind<MainWindowViewModel>().ToSelf().InSingletonScope();
			
		}
	}
}