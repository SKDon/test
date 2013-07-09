using Alicargo.App_Start;
using Ninject;
using Ninject.Web.Common;

namespace Alicargo
{
	public class MvcApplication : NinjectHttpApplication
	{
		protected override IKernel CreateKernel()
		{
			var kernel = new StandardKernel();

			CompositionRoot.BindDataAccess(kernel);

			CompositionRoot.BindServices(kernel);

			CompositionRoot.RegisterConfigs(kernel);

			return kernel;
		}
	}
}