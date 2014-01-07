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
using Alicargo.Jobs.ApplicationEvents.Helpers;
using Alicargo.Jobs.Core;
using Alicargo.Jobs.Helpers;
using Alicargo.Jobs.Helpers.Abstract;
using Alicargo.Services;
using Alicargo.Services.Calculation;
using Alicargo.Services.Excel;
using log4net;
using Ninject;
using Ninject.Syntax;
using ILog = Alicargo.Core.Services.Abstract.ILog;
using TextBuilder = Alicargo.Jobs.Helpers.TextBuilder;

namespace Alicargo.App_Start.Jobs
{
	internal static class CompositionJobsHelper
	{
		public const int PartitionCount = 2;
		public const int PartitionIdForOtherMails = PartitionCount;
		private static readonly TimeSpan PausePeriod = TimeSpan.Parse(ConfigurationManager.AppSettings["JobPausePeriod"]);
		private static readonly ILog JobsLogger = new Log4NetWrapper(LogManager.GetLogger("JobsLogger"));

		public static void BindJobs(IKernel kernel, string connectionString, string filesConnectionString)
		{
			for(var i = 0; i < PartitionCount; i++)
			{
				var partitionId = i;
				var mainConnectionString = connectionString;

				BindStatelessJobRunner(kernel, () => RunBalaceJob(mainConnectionString, partitionId),
					"BalaceJob_" + partitionId);

				BindStatelessJobRunner(kernel, () => RunApplicationEventsJob(
					mainConnectionString, filesConnectionString, partitionId),
					"ApplicationMailCreatorJob_" + partitionId);

				BindStatelessJobRunner(kernel, () => RunMailSenderJob(mainConnectionString, partitionId),
					"MailSenderJob_" + partitionId);
			}

			BindStatelessJobRunner(kernel, () => RunMailSenderJob(connectionString, PartitionIdForOtherMails),
				"MailSenderJob_" + PartitionIdForOtherMails);
		}

		private static void BindStatelessJobRunner(IBindingRoot kernel, Action action,
			string jobName)
		{
			kernel.Bind<IRunner>()
				.ToMethod(context => new DefaultRunner(action, jobName, JobsLogger, PausePeriod))
				.InSingletonScope()
				.Named(jobName);
		}

		private static void RunBalaceJob(string connectionString, int partitionId)
		{
			using(var connection = new SqlConnection(connectionString))
			{
				var unitOfWork = new UnitOfWork(connection);
				var executor = new SqlProcedureExecutor(connectionString);
				var events = new EventRepository(executor);
				var eventEmailRecipient = new EventEmailRecipient(executor);
				var clientRepository = new ClientRepository(unitOfWork);
				var adminRepository = new AdminRepository(unitOfWork);
				var serializer = new Serializer();
				var recipientsFacade = new Alicargo.Jobs.Balance.RecipientsFacade(adminRepository, clientRepository,
					eventEmailRecipient);
				var awbRepository = new AwbRepository(unitOfWork);
				var applicationRepository = new ApplicationRepository(unitOfWork);
				var stateSettingsRepository = new StateSettingsRepository(executor);
				var clientCalculationPresenter = new ClientCalculationPresenter(applicationRepository, awbRepository,
					clientRepository, stateSettingsRepository);
				var templateRepository = new TemplateRepository(executor);
				var textBuilder = new TextBuilder();
				var excelClientCalculation = new ExcelClientCalculation();
				var templateRepositoryWrapper = new TemplateRepositoryHelper(templateRepository);
				var clientExcelHelper = new ClientExcelHelper(clientRepository, clientCalculationPresenter, excelClientCalculation);
				var messageBuilder = new Alicargo.Jobs.Balance.MessageBuilder(
					EmailsHelper.DefaultFrom,
					recipientsFacade,
					new ClientBalanceRepository(executor),
					clientRepository,
					serializer,
					textBuilder,
					clientExcelHelper,
					templateRepositoryWrapper);

				var emailingProcessor = new DefaultEmailingProcessor(
					new DbMailSender(partitionId, new EmailMessageRepository(executor), serializer),
					messageBuilder);

				var processors = new Dictionary<EventState, IEventProcessor>
				{
					{ EventState.Emailing, emailingProcessor }
				};

				new SequentialEventJob(events, partitionId,
					new Dictionary<EventType, IDictionary<EventState, IEventProcessor>>
					{
						{ EventType.BalanceDecreased, processors },
						{ EventType.BalanceIncreased, processors }
					}).Work();
			}
		}

		private static void RunApplicationEventsJob(string connectionString, string filesConnectionString,
			int partitionId)
		{
			using(var connection = new SqlConnection(connectionString))
			{
				var serializer = new Serializer();
				var messageBuilder = GetMessageBuilder(connection, connectionString, filesConnectionString, serializer);
				var executor = new SqlProcedureExecutor(connectionString);
				var events = new EventRepository(executor);
				var emails = new EmailMessageRepository(executor);
				var mailSender = new DbMailSender(partitionId, emails, serializer);
				var mailCreatorProcessor = new DefaultEmailingProcessor(mailSender, messageBuilder);

				var processors = new Dictionary<EventState, IEventProcessor>
				{
					{ EventState.Emailing, mailCreatorProcessor }
				};

				var processorsForApplicationSetState = new Dictionary<EventState, IEventProcessor>
				{
					{ EventState.Emailing, mailCreatorProcessor },
					{ EventState.StateHistorySaving, new ApplicationStateHistoryProcessor() }
				};

				new SequentialEventJob(events, partitionId,
					new Dictionary<EventType, IDictionary<EventState, IEventProcessor>>
					{
						{ EventType.ApplicationCreated, processors },
						{ EventType.ApplicationSetState, processorsForApplicationSetState },
						{ EventType.SetDateOfCargoReceipt, processors },
						{ EventType.SetTransitReference, processors },
						{ EventType.CPFileUploaded, processors },
						{ EventType.InvoiceFileUploaded, processors },
						{ EventType.PackingFileUploaded, processors },
						{ EventType.SwiftFileUploaded, processors },
						{ EventType.DeliveryBillFileUploaded, processors },
						{ EventType.Torg12FileUploaded, processors },
						{ EventType.Calculate, processors },
						{ EventType.CalculationCanceled, processors }
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

		internal static IMessageBuilder GetMessageBuilder(IDbConnection connection, string connectionString,
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
			var textBulder = new Alicargo.Jobs.ApplicationEvents.Helpers.TextBuilder(
				serializer, states, files, new ClientBalanceRepository(mainExecutor), new TextBuilder());
			var stateSettings = new StateSettingsRepository(mainExecutor);
			var templates = new TemplateRepository(mainExecutor);
			var clientRepository = new ClientRepository(unitOfWork);
			var recipientsFacade = new RecipientsFacade(
				awbs, serializer, stateSettings,
				new AdminRepository(unitOfWork),
				new SenderRepository(unitOfWork, passwordConverter, mainExecutor),
				clientRepository,
				new ForwarderRepository(unitOfWork),
				new BrokerRepository(unitOfWork),
				new EventEmailRecipient(mainExecutor));
			var wrapper = new TemplateRepositoryHelper(templates);
			var applicationEventTemplates = new ApplicationEventTemplates(wrapper, templates, serializer);
			var excelClientCalculation = new ExcelClientCalculation();
			var clientCalculationPresenter = new ClientCalculationPresenter(applications, awbs,
				clientRepository, stateSettings);
			var clientExcelHelper = new ClientExcelHelper(clientRepository, clientCalculationPresenter, excelClientCalculation);

			return new MessageBuilder(
				EmailsHelper.DefaultFrom,
				filesFasade,
				textBulder,
				recipientsFacade,
				applicationEventTemplates,
				applications,
				clientExcelHelper,
				serializer);
		}
	}
}