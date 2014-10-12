using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Alicargo;
using Alicargo.Jobs;
using Alicargo.Jobs.Core;
using Alicargo.Mvc;
using Alicargo.Services;
using log4net;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using ILog = Alicargo.Core.Contracts.Common.ILog;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Startup), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Startup), "Stop")]

namespace Alicargo
{
	internal static class Startup
	{
		private static readonly string ConnectionString =
			ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString;

		private static readonly string FilesConnectionString =
			ConfigurationManager.ConnectionStrings["FilesDbConnectionString"].ConnectionString;

		private static readonly TimeSpan PausePeriod = TimeSpan.Parse(ConfigurationManager.AppSettings["JobPausePeriod"]);
		private static readonly ILog MainLogger = new Log4NetWrapper(LogManager.GetLogger("Logger"));
		private static readonly RunnerController RunnerController = new RunnerController();
		private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

		public static Action ApplicationStart = () =>
		{
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BinderConfig.RegisterBinders(ModelBinders.Binders);
			BundleConfig.RegisterBundles();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters, Bootstrapper.Kernel);
		};

		public static void Start()
		{
			DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
			DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));

			Bootstrapper.Initialize(CreateKernel);
		}

		public static void Stop()
		{
			try
			{
				var waitAll = RunnerController.StopAndWait(PausePeriod.Add(PausePeriod));

				MainLogger.Info(waitAll
					? "Jobs were stopped"
					: "One or more jobs were terminated with timeout");
			}
			catch(Exception e)
			{
				MainLogger.Error("One or more jobs failed", e);
				throw;
			}

			Bootstrapper.ShutDown();
		}

		private static IKernel CreateKernel()
		{
			var kernel = new StandardKernel();
			try
			{
				kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => Bootstrapper.Kernel);
				kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

				RegisterServices(kernel);

				return kernel;
			}
			catch
			{
				kernel.Dispose();
				throw;
			}
		}

		private static void RegisterServices(IKernel kernel)
		{
			CompositionRoot.BindConnection(kernel, ConnectionString);

			CompositionRoot.BindDataAccess(kernel, ConnectionString, FilesConnectionString);

			CompositionRoot.BindServices(kernel, MainLogger);

			CompositionJobsHelper.BindJobs(kernel, ConnectionString, FilesConnectionString);

			try
			{
				var runners = kernel.GetAll<IRunner>().ToArray();

				RunnerController.Run(runners);
			}
			catch(Exception e)
			{
				MainLogger.Error("Failed to start runners ", e);

				throw;
			}

			MainLogger.Info("Jobs are started");
		}
	}
}