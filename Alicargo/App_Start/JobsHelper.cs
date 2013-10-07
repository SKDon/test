using System;
using System.Configuration;
using System.Data;
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
		private const string JobRunnerAggregator = "JobRunnerAggregator";
		private static readonly TimeSpan PausePeriod = TimeSpan.Parse(ConfigurationManager.AppSettings["JobPausePeriod"]);

		public static void RunJobs(IKernel kernel, CancellationTokenSource tokenSource)
		{
			Task.Factory.StartNew(state =>
			{
				var k = (IKernel) state;
				var runner = k.Get<IJobRunner>(JobRunnerAggregator);

				runner.Run(tokenSource);
			}, kernel, tokenSource.Token)
				.ContinueWith(task =>
				{
					// todo: test
					if (task.IsFaulted && task.Exception != null)
					{
						var log = kernel.Get<ILog>();

						log.Error("A job failed", task.Exception);
					}
				});
		}

		public static void BindJobs(IKernel kernel, string connectionString)
		{
			const string calculationMailerJob = "CalculationMailerJob";

			BindStatelessJobRunner(kernel, GetCalculationMailerJob, connectionString, calculationMailerJob);

			kernel.Bind<IJobRunner>().ToMethod(context =>
			{
				var mailer = context.Kernel.Get<IJobRunner>(calculationMailerJob);

				return new JobRunnerAggregator(mailer);
			}).InSingletonScope().Named(JobRunnerAggregator);
		}

		private static IJob GetCalculationMailerJob(IDbConnection connection)
		{
			var unitOfWork = new UnitOfWork(connection);
			var users = new UserRepository(unitOfWork, new PasswordConverter());
			var clients = new ClientRepository(unitOfWork);
			var calculations = new CalculationRepository(unitOfWork);
			var log = new Log4NetWrapper(LogManager.GetLogger("JobsLogger"));
			var recipients = new Recipients(users);
			var mailSender = new SilentMailSender(new MailSender(), log);
			var mailer = new CalculationMailer(mailSender, recipients, clients);

			return new CalculationMailerJob(calculations, mailer, log);
		}

		private static void BindStatelessJobRunner(IBindingRoot kernel, Func<IDbConnection, IJob> getJob,
												   string connectionString, string jobName)
		{
			kernel.Bind<IJobRunner>()
				  .ToMethod(context => new StatelessJobRunner(getJob, context.Kernel.Get<ILog>(), connectionString, PausePeriod))
				  .InSingletonScope()
				  .Named(jobName);
		}
	}
}