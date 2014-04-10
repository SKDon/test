using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Alicargo.Jobs;
using Alicargo.Jobs.Core;
using Alicargo.Mvc;
using Alicargo.Services;
using log4net;
using Ninject;
using Ninject.Web.Common;
using ILog = Alicargo.Core.Contracts.Common.ILog;

namespace Alicargo
{
	public /*sealed*/ class MvcApplication : NinjectHttpApplication
	{
		public static readonly TimeSpan PausePeriod = TimeSpan.Parse(ConfigurationManager.AppSettings["JobPausePeriod"]);
		private static readonly ILog MainLogger = new Log4NetWrapper(LogManager.GetLogger("Logger"));

		public static readonly string Version =
			Assembly.GetExecutingAssembly().GetName().Version.ToString();

		private readonly string _connectionString =
			ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString;

		private readonly string _filesConnectionString =
			ConfigurationManager.ConnectionStrings["FilesDbConnectionString"].ConnectionString;

		private readonly RunnerHelper _runnerHelper = new RunnerHelper();

		private readonly StandardKernel _kernel = new StandardKernel();

		static MvcApplication() { }

		protected override IKernel CreateKernel()
		{
			CompositionRoot.BindConnection(_kernel, _connectionString);

			CompositionRoot.BindDataAccess(_kernel, _connectionString, _filesConnectionString, context => HttpContext.Current);

			CompositionRoot.BindServices(_kernel, MainLogger);

			CompositionJobsHelper.BindJobs(_kernel, _connectionString, _filesConnectionString);

			RegisterConfigs(_kernel);

			return _kernel;
		}

		protected override void OnApplicationStarted()
		{
			try
			{
				var runners = _kernel.GetAll<IRunner>().ToArray();

				//_runnerHelper.RunJobs(runners);
			}
			catch (Exception e)
			{
				MainLogger.Error("Failed to start runners ", e);

				throw;
			}

			MainLogger.Info("Jobs are started");
		}

		protected override void OnApplicationStopped()
		{
			try
			{
				var waitAll = _runnerHelper.StopAndWait(PausePeriod.Add(PausePeriod));

				MainLogger.Info(waitAll
					? "Jobs were stopped"
					: "One or more jobs were terminated with timeout");
			}
			catch (Exception e)
			{
				MainLogger.Error("One or more jobs failed", e);
				throw;
			}
		}

		private static void RegisterConfigs(IKernel kernel)
		{
			AreaRegistration.RegisterAllAreas();

			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters, kernel);

			RouteConfig.RegisterRoutes(RouteTable.Routes);

			BinderConfig.RegisterBinders(ModelBinders.Binders);
		}
	}
}