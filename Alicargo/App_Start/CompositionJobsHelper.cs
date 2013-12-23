using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Alicargo.Contracts.Helpers;
using Alicargo.Core.Services;
using Alicargo.Core.Services.Email;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using Alicargo.DataAccess.Repositories.User;
using Alicargo.Jobs;
using Alicargo.Jobs.ApplicationEvents;
using Alicargo.Jobs.ApplicationEvents.Abstract;
using Alicargo.Jobs.ApplicationEvents.Helpers;
using Alicargo.Jobs.Calculation;
using Alicargo.Jobs.Core;
using Alicargo.Services;
using log4net;
using Ninject;
using Ninject.Syntax;
using ILog = Alicargo.Core.Services.Abstract.ILog;

namespace Alicargo.App_Start
{
	internal static class CompositionJobsHelper
	{
		public const int PartitionIdForOtherMails = 2;
		private static readonly TimeSpan DeadTimeout = TimeSpan.FromMinutes(30);
		public static readonly TimeSpan PausePeriod = TimeSpan.Parse(ConfigurationManager.AppSettings["JobPausePeriod"]);
		private static readonly ILog JobsLogger = new Log4NetWrapper(LogManager.GetLogger("JobsLogger"));

		public static void BindJobs(IKernel kernel, string connectionString, string filesConnectionString)
		{
			const string calculationMailerJob = "CalculationMailerJob_";
			const string calculationWatcherJob = "CalculationWatcherJob_";
			const string applicationMailCreatorJob = "ApplicationMailCreatorJob_";
			const string applicationStateHistoryJob = "ApplicationStateHistoryJob_";
			const string mailSenderJobJob = "MailSenderJob_";

			// ReSharper disable ImplicitlyCapturedClosure
			BindStatelessJobRunner(kernel, () => GetCalculationMailerJob(connectionString), calculationMailerJob + 0);
			BindStatelessJobRunner(kernel, () => GetCalculationMailerJob(connectionString), calculationMailerJob + 1);
			BindStatelessJobRunner(kernel, () => GetCalculationWatcherJob(connectionString), calculationWatcherJob + 0);
			BindStatelessJobRunner(kernel, () => GetCalculationWatcherJob(connectionString), calculationWatcherJob + 1);

			BindStatelessJobRunner(kernel, () => GetApplicationMailCreatorJob(
				connectionString, filesConnectionString, new ShardSettings(0, 2)), applicationMailCreatorJob + 0);
			BindStatelessJobRunner(kernel, () => GetApplicationMailCreatorJob(
				connectionString, filesConnectionString, new ShardSettings(1, 2)), applicationMailCreatorJob + 1);
			BindStatelessJobRunner(kernel, () => GetApplicationStateHistoryJob(connectionString, new ShardSettings(0, 2)),
				applicationStateHistoryJob + 0);
			BindStatelessJobRunner(kernel, () => GetApplicationStateHistoryJob(connectionString, new ShardSettings(1, 2)),
				applicationStateHistoryJob + 1);

			BindStatelessJobRunner(kernel, () => GetMailSenderJob(connectionString, 0), mailSenderJobJob + 0);
			BindStatelessJobRunner(kernel, () => GetMailSenderJob(connectionString, 1), mailSenderJobJob + 1);
			BindStatelessJobRunner(kernel, () => GetMailSenderJob(connectionString, PartitionIdForOtherMails),
				mailSenderJobJob + 2);
			// ReSharper restore ImplicitlyCapturedClosure
		}

		private static void BindStatelessJobRunner(IBindingRoot kernel, Action action,
			string jobName)
		{
			kernel.Bind<IJobRunner>()
				.ToMethod(context => new StatelessJobRunner(action, jobName, JobsLogger, PausePeriod))
				.InSingletonScope()
				.Named(jobName);
		}

		private static void GetCalculationWatcherJob(string connectionString)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				var unitOfWork = new UnitOfWork(connection);

				var job = new CalculationWatcherJob(DeadTimeout, new CalculationRepository(unitOfWork));

				job.Run();
			}
		}

		private static void GetCalculationMailerJob(string connectionString)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				var unitOfWork = new UnitOfWork(connection);
				var clients = new ClientRepository(unitOfWork);
				var calculations = new CalculationRepository(unitOfWork);
				var mailSender = new SilentMailSender(new MailSender(), JobsLogger);
				var mailer = new CalculationMailer(mailSender,
					new CalculationMailBuilder(clients, new AdminRepository(unitOfWork), EmailsHelper.DefaultFrom));

				var job = new CalculationMailerJob(calculations, mailer, JobsLogger);

				job.Run();
			}
		}

		private static void GetApplicationMailCreatorJob(string connectionString, string filesConnectionString,
			ShardSettings shard)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				var serializer = new Serializer();
				var factory = GetMessageFactory(connection, connectionString, filesConnectionString, serializer);
				var executor = new SqlProcedureExecutor(connectionString);
				var events = new ApplicationEventRepository(executor);
				var emails = new EmailMessageRepository(executor);

				var job = new ApplicationMailCreatorJob(emails, factory, events, shard, serializer);

				job.Run();
			}
		}

		private static void GetApplicationStateHistoryJob(string connectionString, ShardSettings shard)
		{
			var executor = new SqlProcedureExecutor(connectionString);
			var events = new ApplicationEventRepository(executor);

			var job = new ApplicationStateHistoryJob(events, shard);

			job.Run();
		}

		private static void GetMailSenderJob(string connectionString, int partitionId)
		{
			var serializer = new Serializer();
			var executor = new SqlProcedureExecutor(connectionString);
			var messages = new EmailMessageRepository(executor);
			var sender = new MailSender();

			var job = new MailSenderJob(messages, partitionId, sender, serializer);

			job.Run();
		}

		private static IMessageFactory GetMessageFactory(IDbConnection connection, string connectionString,
			string filesConnectionString, Serializer serializer)
		{
			var unitOfWork = new UnitOfWork(connection);
			var passwordConverter = new PasswordConverter();
			var mainExecutor = new SqlProcedureExecutor(connectionString);
			var filesExecutor = new SqlProcedureExecutor(filesConnectionString);
			var states = new StateRepository(mainExecutor);
			var applications = new ApplicationRepository(unitOfWork);
			var awbs = new AwbRepository(unitOfWork);
			var files = new ApplicationFileRepository(filesExecutor);
			var filesFasade = new FilesFasade(serializer, awbs, files);
			var textBulder = new TextBulder(serializer, states, files);
			var stateSettings = new StateSettingsRepository(mainExecutor);
			var templates = new EmailTemplateRepository(mainExecutor);
			var recipientsFacade = new RecipientsFacade(
				awbs, serializer, stateSettings,
				new AdminRepository(unitOfWork),
				new SenderRepository(unitOfWork, passwordConverter, new SqlProcedureExecutor(connectionString)),
				new ClientRepository(unitOfWork),
				new ForwarderRepository(unitOfWork),
				new BrokerRepository(unitOfWork),
				templates);
			var templatesFacade = new TemplatesFacade(serializer, templates);

			return new MessageFactory(EmailsHelper.DefaultFrom, filesFasade, textBulder, recipientsFacade, templatesFacade, applications);
		}
	}
}