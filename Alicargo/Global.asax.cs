﻿using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Alicargo.App_Start;
using Alicargo.Jobs;
using Alicargo.Services;
using log4net;
using Ninject;
using Ninject.Web.Common;
using ILog = Alicargo.Core.Services.Abstract.ILog;

namespace Alicargo
{
	public /*sealed*/ class MvcApplication : NinjectHttpApplication
	{
		public static readonly TimeSpan PausePeriod = TimeSpan.Parse(ConfigurationManager.AppSettings["JobPausePeriod"]);
		private static readonly ILog MainLogger = new Log4NetWrapper(LogManager.GetLogger("Logger"));

		private readonly string _connectionString =
			ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString;

		private readonly string _filesConnectionString =
			ConfigurationManager.ConnectionStrings["FilesDbConnectionString"].ConnectionString;

		private readonly JobRunnerHelper _jobs = new JobRunnerHelper();

		private readonly StandardKernel _kernel = new StandardKernel();

		protected override IKernel CreateKernel()
		{
			CompositionRoot.BindConnection(_kernel, _connectionString, _filesConnectionString);

			CompositionRoot.BindDataAccess(_kernel, context => HttpContext.Current);

			CompositionRoot.BindServices(_kernel, MainLogger);

			CompositionJobsHelper.BindJobs(_kernel, _connectionString);

			RegisterConfigs(_kernel);

			return _kernel;
		}

		protected override void OnApplicationStarted()
		{
			try
			{
				_jobs.RunJobs(_kernel.GetAll<IJobRunner>().ToArray());
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
				var waitAll = _jobs.StopAndWait(PausePeriod.Add(PausePeriod));

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