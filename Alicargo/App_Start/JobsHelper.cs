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
				return Task.Factory.StartNew(state => runner.Run((CancellationTokenSource)state), tokenSource,
											 CancellationToken.None);
			}
			catch (Exception e)
			{
				JobsLogger.Error("Failed to start a runner", e);
				throw;
			}
		}

		public static void StopAndWait(Task[] jobs, CancellationTokenSource tokenSource)
		{
			tokenSource.Cancel(false);

			try
			{
				Task.WaitAll(jobs, CancellationTimeout);

				JobsLogger.Info("Jobs were stopped");
			}
			catch (Exception e)
			{
				JobsLogger.Error("Failed to stop runners", e);
			}
		}

		private static void BindStatelessJobRunner(IBindingRoot kernel, Func<IDbConnection, IJob> getJob,
												   string connectionString, string jobName)
		{
			kernel.Bind<IJobRunner>()
				  .ToMethod(context => new StatelessJobRunner(getJob, context.Kernel.Get<ILog>(), connectionString, PausePeriod))
				  .InSingletonScope()
				  .Named(jobName);
		}

		public static void BindJobs(IKernel kernel, string connectionString)
		{
			const string calculationMailerJob = "CalculationMailerJob";
			const string clientExcelUpdaterJob = "ClientExcelUpdaterJob";

			BindStatelessJobRunner(kernel, GetCalculationMailerJob, connectionString, calculationMailerJob);

			BindStatelessJobRunner(kernel, GetClientExcelUpdaterJob, connectionString, clientExcelUpdaterJob);
		}

		private static IJob GetClientExcelUpdaterJob(IDbConnection connection)
		{
			return new ClientExcelUpdaterJob();
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