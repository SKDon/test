using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Core.Services;
using Alicargo.Core.Services.Email;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using Alicargo.DataAccess.Repositories.Application;
using Alicargo.DataAccess.Repositories.User;
using Alicargo.Jobs;
using Alicargo.Jobs.ApplicationEvents;
using Alicargo.Jobs.ApplicationEvents.Abstract;
using Alicargo.Jobs.ApplicationEvents.Helpers;
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
		public const int PartitionCount = 2;
		public const int PartitionIdForOtherMails = PartitionCount;
		private static readonly TimeSpan PausePeriod = TimeSpan.Parse(ConfigurationManager.AppSettings["JobPausePeriod"]);
		private static readonly ILog JobsLogger = new Log4NetWrapper(LogManager.GetLogger("JobsLogger"));

		public static void BindJobs(IKernel kernel, string connectionString, string filesConnectionString)
		{
			const string calculationJob = "CalculationJob_";
			const string applicationMailCreatorJob = "ApplicationMailCreatorJob_";
			const string mailSenderJobJob = "MailSenderJob_";

			for (var i = 0; i < PartitionCount; i++)
			{
				var partitionId = i;
				var mainConnectionString = connectionString;

				BindStatelessJobRunner(kernel, () => RunCalculationJob(mainConnectionString, partitionId),
					calculationJob + partitionId);

				BindStatelessJobRunner(kernel, () => RunApplicationEventsJob(
					mainConnectionString, filesConnectionString, partitionId), applicationMailCreatorJob + partitionId);

				BindStatelessJobRunner(kernel, () => RunMailSenderJob(mainConnectionString, partitionId),
					mailSenderJobJob + partitionId);
			}

			BindStatelessJobRunner(kernel, () => RunMailSenderJob(connectionString, PartitionIdForOtherMails),
				mailSenderJobJob + PartitionIdForOtherMails);
		}

		private static void BindStatelessJobRunner(IBindingRoot kernel, Action action,
			string jobName)
		{
			kernel.Bind<IRunner>()
				.ToMethod(context => new DefaultRunner(action, jobName, JobsLogger, PausePeriod))
				.InSingletonScope()
				.Named(jobName);
		}

		private static void RunCalculationJob(string connectionString, int partitionId)
		{
			var executor = new SqlProcedureExecutor(connectionString);
			var events = new EventRepository(executor);

			//var job = new CalculationJob(events, partitionId);

			//job.Work();
		}

		private static void RunApplicationEventsJob(string connectionString, string filesConnectionString,
			int partitionId)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				var serializer = new Serializer();
				var factory = GetMessageFactory(connection, connectionString, filesConnectionString, serializer);
				var executor = new SqlProcedureExecutor(connectionString);
				var events = new EventRepository(executor);
				var emails = new EmailMessageRepository(executor);
				var mailCreatorProcessor = new ApplicationMailCreatorProcessor(partitionId, factory, emails, events, serializer);

				var processors = new Dictionary<EventState, IEventProcessor>
				{
					{ EventState.Emailing, mailCreatorProcessor },
					{ EventState.StateHistorySaving, new ApplicationStateHistoryProcessor(events) }
				};

				new DefaultEventJob(events, partitionId,
					new Dictionary<EventType, IDictionary<EventState, IEventProcessor>>
					{
						{ EventType.ApplicationCreated, processors },
						{ EventType.ApplicationSetState, processors },
						{ EventType.SetDateOfCargoReceipt, processors },
						{ EventType.SetTransitReference, processors },
						{ EventType.CPFileUploaded, processors },
						{ EventType.InvoiceFileUploaded, processors },
						{ EventType.PackingFileUploaded, processors },
						{ EventType.SwiftFileUploaded, processors },
						{ EventType.DeliveryBillFileUploaded, processors },
						{ EventType.Torg12FileUploaded, processors }
					}).Work();
			}
		}

		private static void RunMailSenderJob(string connectionString, int partitionId)
		{
			var serializer = new Serializer();
			var executor = new SqlProcedureExecutor(connectionString);
			var messages = new EmailMessageRepository(executor);
			var sender = new MailSender();

			var job = new MailSenderJob(messages, partitionId, sender, serializer);

			job.Work();
		}

		private static IMessageFactory GetMessageFactory(IDbConnection connection, string connectionString,
			string filesConnectionString, ISerializer serializer)
		{
			var unitOfWork = new UnitOfWork(connection);
			var passwordConverter = new PasswordConverter();
			var mainExecutor = new SqlProcedureExecutor(connectionString);
			var filesExecutor = new SqlProcedureExecutor(filesConnectionString);
			var states = new StateRepository(mainExecutor);
			var applications = new ApplicationRepository(unitOfWork);
			var awbs = new AwbRepository(unitOfWork);
			var files = new ApplicationFileRepository(filesExecutor);
			var filesFasade = new FilesFacade(serializer, awbs, files);
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

			return new MessageFactory(EmailsHelper.DefaultFrom, filesFasade, textBulder, recipientsFacade, templatesFacade,
				applications);
		}
	}
}