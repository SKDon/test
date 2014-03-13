using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Alicargo.Core.Email;
using Alicargo.Core.Excel.Client;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using Alicargo.DataAccess.Repositories.Application;
using Alicargo.DataAccess.Repositories.User;
using Alicargo.Jobs;
using Alicargo.Jobs.Application;
using Alicargo.Jobs.Application.Helpers;
using Alicargo.Jobs.Awb;
using Alicargo.Jobs.Client;
using Alicargo.Jobs.Client.Balance;
using Alicargo.Jobs.Client.ClientAdd;
using Alicargo.Jobs.Core;
using Alicargo.Jobs.Helpers;
using Alicargo.Jobs.Helpers.Abstract;
using Alicargo.Services;
using Alicargo.Utilities;
using log4net;
using Ninject;
using Ninject.Syntax;
using ILog = Alicargo.Core.Contracts.Common.ILog;
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

				BindStatelessJobRunner(kernel,
					() => RunBalaceJob(mainConnectionString, partitionId),
					"BalaceJob_" + partitionId);

				BindStatelessJobRunner(kernel,
					() => RunApplicationEventsJob(
						mainConnectionString,
						filesConnectionString,
						partitionId),
					"ApplicationMailCreatorJob_" + partitionId);

				BindStatelessJobRunner(kernel,
					() => RunMailSenderJob(mainConnectionString, partitionId),
					"MailSenderJob_" + partitionId);

				BindStatelessJobRunner(kernel, () => RunClientJob(mainConnectionString, partitionId), "ClientJob_" + partitionId);

				BindStatelessJobRunner(kernel, () => RunAwbJob(mainConnectionString, partitionId), "AwbJob_" + partitionId);
			}

			BindStatelessJobRunner(kernel,
				() => RunMailSenderJob(connectionString, PartitionIdForOtherMails),
				"MailSenderJob_ForOtherMails");
		}

		private static void BindStatelessJobRunner(
			IBindingRoot kernel, Action action,
			string jobName)
		{
			kernel.Bind<IRunner>()
				.ToMethod(context => new DefaultRunner(action, jobName, JobsLogger, PausePeriod))
				.InSingletonScope()
				.Named(jobName);
		}

		private static DefaultEmailingProcessor GetDefaultEmailingProcessor(
			int partitionId, ISqlProcedureExecutor executor,
			ISerializer serializer, IMessageBuilder messageBuilder)
		{
			var emails = new EmailMessageRepository(executor);
			var mailSender = new DbMailSender(partitionId, emails, serializer);

			return new DefaultEmailingProcessor(mailSender, messageBuilder);
		}

		private static CommonEventMessageBuilder GetMessageBuilder(
			ISqlProcedureExecutor executor, ISerializer serializer, IClientExcelHelper fileHelper,
			ILocalizedDataHelper localizedDataHelper, IRecipientsFacade recipientsFacade, IUnitOfWork unitOfWork)
		{
			var templateRepository = new TemplateRepository(executor);
			var textBuilder = new TextBuilder();
			var templateRepositoryWrapper = new TemplateRepositoryHelper(templateRepository);
			var awbFiles = new AwbFileRepository(unitOfWork);

			return new CommonEventMessageBuilder(
				EmailsHelper.DefaultFrom,
				recipientsFacade,
				awbFiles,
				serializer,
				textBuilder,
				fileHelper,
				localizedDataHelper,
				templateRepositoryWrapper);
		}

		private static void RunApplicationEventsJob(
			string connectionString, string filesConnectionString,
			int partitionId)
		{
			using(var connection = new SqlConnection(connectionString))
			{
				var serializer = new Serializer();
				var executor = new SqlProcedureExecutor(connectionString);
				var events = new EventRepository(executor);

				var messageBuilder = GetMessageBuilder(connection, connectionString, filesConnectionString, serializer);
				var mailCreatorProcessor = GetDefaultEmailingProcessor(partitionId, executor, serializer, messageBuilder);

				var processors = new Dictionary<EventState, IEventProcessor>
				{
					{ EventState.Emailing, mailCreatorProcessor }
				};

				var processorsForApplicationSetState = new Dictionary<EventState, IEventProcessor>
				{
					{ EventState.Emailing, mailCreatorProcessor },
					{ EventState.StateHistorySaving, new ApplicationStateHistoryProcessor() }
				};

				new SequentialEventJob(events,
					partitionId,
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
						{ EventType.SetSender, processors },
						{ EventType.SetForwarder, processors },
						{ EventType.SetCarrier, processors },
						{ EventType.SetAwb, processors },
						{ EventType.CalculationCanceled, processors }
					}).Work();
			}
		}

		private static void RunAwbJob(string mainConnectionString, int partitionId)
		{
			using(var connection = new SqlConnection(mainConnectionString))
			{
				var executor = new SqlProcedureExecutor(mainConnectionString);
				var serializer = new Serializer();
				var events = new EventRepository(executor);

				var unitOfWork = new UnitOfWork(connection);
				var adminRepository = new AdminRepository(unitOfWork);
				var brokerRepository = new BrokerRepository(unitOfWork);
				var clientExcelHelper = new ClientExcelHelperStub();
				var awbs = new AwbRepository(unitOfWork);
				var localizedDataHelper = new AwbEventLocalizedDataHelper(awbs);
				var eventEmailRecipient = new EventEmailRecipient(executor);
				var recipientsFacade = new AwbEventRecipientsFacade(adminRepository, brokerRepository, awbs, eventEmailRecipient);

				var messageBuilder = GetMessageBuilder(
					executor,
					serializer,
					clientExcelHelper,
					localizedDataHelper,
					recipientsFacade,
					unitOfWork);
				var emailingProcessor = GetDefaultEmailingProcessor(partitionId, executor, serializer, messageBuilder);

				var processors = new Dictionary<EventState, IEventProcessor>
				{
					{ EventState.Emailing, emailingProcessor }
				};

				new SequentialEventJob(events,
					partitionId,
					new Dictionary<EventType, IDictionary<EventState, IEventProcessor>>
					{
						{ EventType.AwbCreated, processors },
						{ EventType.SetBroker, processors },
						{ EventType.GTDFileUploaded, processors },
						{ EventType.GTDAdditionalFileUploaded, processors },
						{ EventType.AwbPackingFileUploaded, processors },
						{ EventType.AwbInvoiceFileUploaded, processors },
						{ EventType.AWBFileUploaded, processors },
						{ EventType.DrawFileUploaded, processors }
					}).Work();
			}
		}

		private static void RunBalaceJob(string connectionString, int partitionId)
		{
			using(var connection = new SqlConnection(connectionString))
			{
				var executor = new SqlProcedureExecutor(connectionString);
				var serializer = new Serializer();
				var events = new EventRepository(executor);

				var unitOfWork = new UnitOfWork(connection);
				var clientBalanceRepository = new ClientBalanceRepository(executor);
				var calculationRepository = new CalculationRepository(executor);
				var clientRepository = new ClientRepository(unitOfWork);
				var adminRepository = new AdminRepository(unitOfWork);
				var excelClientCalculation = new ExcelClientCalculation(calculationRepository,
					clientBalanceRepository,
					clientRepository);
				var eventEmailRecipient = new EventEmailRecipient(executor);
				var clientExcelHelper = new ClientExcelHelper(clientRepository, excelClientCalculation);
				var localizedDataHelper = new BalanceLocalizedDataHelper(clientBalanceRepository, serializer, clientRepository);
				var recipientsFacade = new ClientEventRecipientsFacade(adminRepository, clientRepository, eventEmailRecipient);

				var messageBuilder = GetMessageBuilder(executor,
					serializer,
					clientExcelHelper,
					localizedDataHelper,
					recipientsFacade,
					unitOfWork);
				var emailingProcessor = GetDefaultEmailingProcessor(partitionId, executor, serializer, messageBuilder);

				var processors = new Dictionary<EventState, IEventProcessor>
				{
					{ EventState.Emailing, emailingProcessor }
				};

				new SequentialEventJob(events,
					partitionId,
					new Dictionary<EventType, IDictionary<EventState, IEventProcessor>>
					{
						{ EventType.BalanceDecreased, processors },
						{ EventType.BalanceIncreased, processors }
					}).Work();
			}
		}

		private static void RunClientJob(string connectionString, int partitionId)
		{
			using(var connection = new SqlConnection(connectionString))
			{
				var executor = new SqlProcedureExecutor(connectionString);
				var serializer = new Serializer();
				var events = new EventRepository(executor);

				var unitOfWork = new UnitOfWork(connection);
				var clientRepository = new ClientRepository(unitOfWork);
				var adminRepository = new AdminRepository(unitOfWork);
				var clientExcelHelper = new ClientExcelHelperStub();
				var localizedDataHelper = new CommonLocalizedDataHelper(serializer, clientRepository);
				var recipients = new EventEmailRecipient(executor);
				var recipientsFacade = new ClientEventRecipientsFacade(adminRepository, clientRepository, recipients);

				var messageBuilder = GetMessageBuilder(executor,
					serializer,
					clientExcelHelper,
					localizedDataHelper,
					recipientsFacade,
					unitOfWork);
				var emailingProcessor = GetDefaultEmailingProcessor(partitionId, executor, serializer, messageBuilder);

				var processors = new Dictionary<EventState, IEventProcessor>
				{
					{ EventState.Emailing, emailingProcessor }
				};

				new SequentialEventJob(events,
					partitionId,
					new Dictionary<EventType, IDictionary<EventState, IEventProcessor>>
					{
						{ EventType.ClientAdded, processors },
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

		internal static IMessageBuilder GetMessageBuilder(
			IDbConnection connection, string connectionString,
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
			var filesFasade = new ApplicationEventFilesFacade(serializer, new AwbFileRepository(unitOfWork), files, applications);
			var clientBalanceRepository = new ClientBalanceRepository(mainExecutor);
			var countries = new CountryRepository(mainExecutor);
			var cities = new CityRepository(mainExecutor);
			var textBulder = new Alicargo.Jobs.Application.Helpers.TextBuilder(
				serializer,
				awbs,
				countries,
				cities,
				states,
				files,
				clientBalanceRepository,
				new TextBuilder());
			var templates = new TemplateRepository(mainExecutor);
			var clientRepository = new ClientRepository(unitOfWork);
			var recipientsFacade = new ApplicationEventRecipientsFacade(
				awbs,
				applications,
				new AdminRepository(unitOfWork),
				new SenderRepository(passwordConverter, mainExecutor),
				clientRepository,
				new CarrierRepository(passwordConverter, mainExecutor),
				new ForwarderRepository(passwordConverter, mainExecutor),
				new BrokerRepository(unitOfWork),
				new EventEmailRecipient(mainExecutor));
			var templateRepositoryHelper = new TemplateRepositoryHelper(templates);
			var calculationRepository = new CalculationRepository(mainExecutor);
			var excelClientCalculation = new ExcelClientCalculation(calculationRepository,
				clientBalanceRepository,
				clientRepository);
			var clientExcelHelper = new ClientExcelHelper(clientRepository, excelClientCalculation);

			return new MessageBuilder(
				EmailsHelper.DefaultFrom,
				filesFasade,
				textBulder,
				recipientsFacade,
				templateRepositoryHelper,
				clientExcelHelper,
				serializer,
				applications);
		}
	}
}