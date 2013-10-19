using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alicargo.Core.Services;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using Alicargo.Jobs;
using Alicargo.Jobs.Calculation;
using Alicargo.Services;
using Alicargo.Services.Email;
using log4net;
using Ninject;
using Ninject.Syntax;
using ILog = Alicargo.Core.Services.ILog;

namespace Alicargo.App_Start
{
	internal static class JobsHelper
	{
		private static readonly TimeSpan DeadTimeout = TimeSpan.FromMinutes(30);
		public static readonly TimeSpan PausePeriod = TimeSpan.Parse(ConfigurationManager.AppSettings["JobPausePeriod"]);
		public static readonly TimeSpan CancellationTimeout = PausePeriod.Add(PausePeriod);
		private static readonly ILog JobsLogger = new Log4NetWrapper(LogManager.GetLogger("JobsLogger"));

		public static Task[] RunJobs(IKernel kernel, CancellationTokenSource tokenSource)
		{
			var jobs = kernel.GetAll<IJobRunner>().Select(x => StartTask(x, tokenSource)).ToArray();

			JobsLogger.Info("Jobs are started");

			return jobs;
		}

		private static Task StartTask(IJobRunner runner, CancellationTokenSource tokenSource)
		{
			try
			{
				return Task.Factory.StartNew(state => runner.Run((CancellationTokenSource) state), tokenSource,
					CancellationToken.None);
			}
			catch (Exception e)
			{
				JobsLogger.Error("Failed to start the runner " + runner.Name, e);
				throw;
			}
		}

		public static void StopAndWait(Task[] jobs, CancellationTokenSource tokenSource)
		{
			tokenSource.Cancel(false);

			try
			{
				var waitAll = Task.WaitAll(jobs, CancellationTimeout);

				JobsLogger.Info(waitAll
					? "Jobs were stopped"
					: "One or more jobs were terminated with timeout");
			}
			catch (Exception e)
			{
				JobsLogger.Error("One or more jobs failed", e);
			}
		}

		private static void BindStatelessJobRunner(IBindingRoot kernel, Func<IDbConnection, IJob> getJob,
			string connectionString, string jobName)
		{
			kernel.Bind<IJobRunner>()
				  .ToMethod(context => new StatelessJobRunner(getJob, jobName, JobsLogger, connectionString, PausePeriod))
				  .InSingletonScope()
				  .Named(jobName);
		}

		public static void BindJobs(IKernel kernel, string connectionString)
		{
			const string calculationMailerJob = "CalculationMailerJob_";
			const string calculationWatcherJob = "CalculationWatcherJob_";

			BindStatelessJobRunner(kernel, GetCalculationMailerJob, connectionString, calculationMailerJob + 0);
			BindStatelessJobRunner(kernel, GetCalculationMailerJob, connectionString, calculationMailerJob + 1);
			BindStatelessJobRunner(kernel, GetCalculationWatcherJob, connectionString, calculationWatcherJob + 0);
			BindStatelessJobRunner(kernel, GetCalculationWatcherJob, connectionString, calculationWatcherJob + 1);
		}

		private static IJob GetCalculationWatcherJob(IDbConnection connection)
		{
			return new CalculationWatcherJob(DeadTimeout, new CalculationRepository(new UnitOfWork(connection)));
		}

		private static IJob GetCalculationMailerJob(IDbConnection connection)
		{
			var unitOfWork = new UnitOfWork(connection);
			var users = new UserRepository(unitOfWork, new PasswordConverter());
			var clients = new ClientRepository(unitOfWork);
			var calculations = new CalculationRepository(unitOfWork);
			var recipients = new Recipients(users);
			var mailSender = new SilentMailSender(new MailSender(), JobsLogger);
			var mailer = new CalculationMailer(mailSender, recipients, clients);

			return new CalculationMailerJob(calculations, mailer, JobsLogger);
		}
	}
}