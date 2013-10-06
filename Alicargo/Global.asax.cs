using System.Configuration;
using System.Threading;
using Alicargo.App_Start;
using Ninject;
using Ninject.Web.Common;

namespace Alicargo
{
	public /*sealed*/ class MvcApplication : NinjectHttpApplication
	{
		private StandardKernel _kernel;
		private CancellationTokenSource _tokenSource;
		readonly string _connectionString = ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString;

		protected override IKernel CreateKernel()
		{
			_kernel = new StandardKernel();

			CompositionRoot.BindDataAccess(_kernel, _connectionString);

			CompositionRoot.BindServices(_kernel);

			CompositionRoot.RegisterConfigs(_kernel);

			return _kernel;
		}

		protected override void OnApplicationStarted()
		{
			_tokenSource = new CancellationTokenSource();

			JobsHelper.BindJobs(_kernel, _connectionString);

			JobsHelper.RunJobs(_kernel, _tokenSource);
		}

		protected override void OnApplicationStopped()
		{
			_tokenSource.Cancel(false);
		}
	}
}