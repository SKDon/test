using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Alicargo.Core.Common;
using Alicargo.Core.Email;
using Alicargo.Core.Excel.Client;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using Alicargo.DataAccess.Repositories.Application;
using Alicargo.DataAccess.Repositories.User;
using Alicargo.Jobs.Application;
using Alicargo.Jobs.Application.Helpers;
using Alicargo.Jobs.Awb;
using Alicargo.Jobs.Bill;
using Alicargo.Jobs.Bill.Helpers;
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

namespace Alicargo.Jobs
{
	internal static class CompositionJobsHelper
	{
		public const int PartitionCount = 2;
		public const int PartitionIdForOtherMails = PartitionCount;
		private static readonly TimeSpan PausePeriod = TimeSpan.Parse(ConfigurationManager.AppSettings["JobPausePeriod"]);

		private static readonly TimeSpan CourseSourceRetryPolicyPeriod =
			TimeSpan.Parse(ConfigurationManager.AppSettings["CourseSourceRetryPolicyPeriod"]);

		private static readonly ushort CourseSourceAttempts =
			ushort.Parse(ConfigurationManager.AppSettings["CourseSourceAttempts"]);

		private static readonly ILog JobsLogger = new Log4NetWrapper(LogManager.GetLogger("JobsLogger"));

		private static readonly Holder<DateTimeOffset> PreviousRunEuroCourseJobRubTime =
			new Holder<DateTimeOffset>(DateTimeProvider.Now);

		public static void BindJobs(IKernel kernel, string mainConnectionString, string filesConnectionString)
		{
			for(var i = 0; i < PartitionCount; i++)
			{
				var partitionId = i;
				var connectionString = mainConnectionString;

				BindDefaultJobRunner(
					kernel,
					() => RunBalaceJob(connectionString, filesConnectionString, partitionId),
					"BalaceJob_" + partitionId);

				BindDefaultJobRunner(
					kernel,
					() => RunApplicationEventsJob(connectionString, filesConnectionString, partitionId),
					"ApplicationMailCreatorJob_" + partitionId);

				BindDefaultJobRunner(
					kernel,
					() => RunMailSenderJob(connectionString, partitionId),
					"MailSenderJob_" + partitionId);

				BindDefaultJobRunner(
					kernel,
					() => RunClientJob(connectionString, filesConnectionString, partitionId),
					"ClientJob_" + partitionId);

				BindDefaultJobRunner(
					kernel,
					() => RunAwbJob(connectionString, filesConnectionString, partitionId),
					"AwbJob_" + partitionId);
			}

			BindDefaultJobRunner(
				kernel,
				() => RunMailSenderJob(mainConnectionString, PartitionIdForOtherMails),
				"MailSenderJob_ForOtherMails");

			BindDefaultJobRunner(kernel, () => RunEuroCourseJob(mainConnectionString), "EuroCourseJob");
		}

		private static void BindDefaultJobRunner(
			IBindingRoot kernel,
			Action action,
			string jobName,
			TimeSpan pausePeriod)
		{
			kernel.Bind<IRunner>()
				.ToMethod(context => new DefaultRunner(action, jobName, JobsLogger, pausePeriod))
				.InSingletonScope()
				.Named(jobName);
		}

		private static void BindDefaultJobRunner(
			IBindingRoot kernel,
			Action action,
			string jobName)
		{
			BindDefaultJobRunner(kernel, action, jobName, PausePeriod);
		}

		private static IMessageBuilder GetCommonMessageBuilder(
			IDbConnection connection,
			string mainConnectionString,
			string filesConnectionString,
			ILocalizedDataHelper localizedDataHelper,
			IRecipientsFacade recipientsFacade)
		{
			var executor = new SqlProcedureExecutor(mainConnectionString);
			var templateRepository = new TemplateRepository(executor);
			var textBuilder = new TextBuilder();
			var templateRepositoryWrapper = new TemplateRepositoryHelper(templateRepository);
			var filesFacade = GetFilesFacade(connection, mainConnectionString, filesConnectionString);

			return new CommonEventMessageBuilder(
				EmailsHelper.DefaultFrom,
				recipientsFacade,
				new Serializer(),
				textBuilder,
				localizedDataHelper,
				templateRepositoryWrapper,
				filesFacade);
		}

		private static DefaultEmailingProcessor GetDefaultEmailingProcessor(
			int partitionId,
			ISqlProcedureExecutor executor,
			IMessageBuilder messageBuilder)
		{
			var emails = new EmailMessageRepository(executor);
			var mailSender = new DbMailSender(partitionId, emails, new Serializer());

			return new DefaultEmailingProcessor(mailSender, messageBuilder);
		}

		private static CommonFilesFacade GetFilesFacade(
			IDbConnection connection,
			string connectionString,
			string filesConnectionString)
		{
			var mainExecutor = new SqlProcedureExecutor(connectionString);
			var filesExecutor = new SqlProcedureExecutor(filesConnectionString);
			var applicationFiles = new ApplicationFileRepository(filesExecutor);
			var clientRepository = new ClientRepository(mainExecutor);
			var calculationRepository = new CalculationRepository(mainExecutor);
			var clientBalanceRepository = new ClientBalanceRepository(mainExecutor);
			var applications = new ApplicationRepository(connection);
			var excelClientCalculation = new ExcelClientCalculation(
				calculationRepository,
				clientBalanceRepository,
				clientRepository);
			var awbFiles = new AwbFileRepository(filesExecutor);
			var clientExcelHelper = new ClientExcelHelper(clientRepository, excelClientCalculation);
			var serializer = new Serializer();

			return new CommonFilesFacade(awbFiles, clientExcelHelper, serializer, applicationFiles, applications);
		}

		private static void RunApplicationEventsJob(
			string connectionString,
			string filesConnectionString,
			int partitionId)
		{
			using(var connection = new SqlConnection(connectionString))
			{
				var executor = new SqlProcedureExecutor(connectionString);
				var events = new EventRepository(executor);

				var messageBuilder = GetApplicationMessageBuilder(connection, connectionString, filesConnectionString);
				var mailCreatorProcessor = GetDefaultEmailingProcessor(partitionId, executor, messageBuilder);

				var processors = new Dictionary<EventState, IEventProcessor>
				{
					{ EventState.Emailing, mailCreatorProcessor }
				};

				var processorsForApplicationSetState = new Dictionary<EventState, IEventProcessor>
				{
					{ EventState.Emailing, mailCreatorProcessor },
					{ EventState.StateHistorySaving, new ApplicationStateHistoryProcessor() }
				};

				var dictionary = EventHelper.ApplicationEventTypes
					.Except(new[] { EventType.ApplicationSetState })
					.ToDictionary(x => x, x => (IDictionary<EventState, IEventProcessor>)processors);
				dictionary.Add(EventType.ApplicationSetState, processorsForApplicationSetState);

				new SequentialEventJob(
					events,
					partitionId,
					dictionary).Work();
			}
		}

		private static void RunAwbJob(string mainConnectionString, string filesConnectionString, int partitionId)
		{
			using(var connection = new SqlConnection(mainConnectionString))
			{
				var executor = new SqlProcedureExecutor(mainConnectionString);
				var events = new EventRepository(executor);
				var adminRepository = new AdminRepository(connection);
				var brokerRepository = new BrokerRepository(connection);
				var awbs = new AwbRepository(connection);
				var converter = new PasswordConverter();
				var senders = new SenderRepository(converter, executor);
				var localizedDataHelper = new AwbEventLocalizedDataHelper(awbs, senders);
				var eventEmailRecipient = new EventEmailRecipient(executor);
				var managerRepository = new ManagerRepository(connection);
				var recipientsFacade = new AwbEventRecipientsFacade(
					adminRepository,
					managerRepository,
					brokerRepository,
					awbs,
					eventEmailRecipient);

				var messageBuilder = GetCommonMessageBuilder(
					connection,
					mainConnectionString,
					filesConnectionString,
					localizedDataHelper,
					recipientsFacade);
				var emailingProcessor = GetDefaultEmailingProcessor(partitionId, executor, messageBuilder);

				var processors = (IDictionary<EventState, IEventProcessor>)new Dictionary<EventState, IEventProcessor>
				{
					{ EventState.Emailing, emailingProcessor }
				};

				new SequentialEventJob(
					events,
					partitionId,
					EventHelper.AwbEventTypes.ToDictionary(x => x, x => processors)).Work();
			}
		}

		private static void RunBalaceJob(string connectionString, string filesConnectionString, int partitionId)
		{
			using(var connection = new SqlConnection(connectionString))
			{
				var executor = new SqlProcedureExecutor(connectionString);
				var serializer = new Serializer();
				var events = new EventRepository(executor);
				var clientBalanceRepository = new ClientBalanceRepository(executor);
				var clientRepository = new ClientRepository(executor);
				var adminRepository = new AdminRepository(connection);
				var eventEmailRecipient = new EventEmailRecipient(executor);
				var localizedDataHelper = new BalanceLocalizedDataHelper(clientBalanceRepository, serializer, clientRepository);
				var managerRepository = new ManagerRepository(connection);
				var recipientsFacade = new ClientEventRecipientsFacade(
					adminRepository,
					managerRepository,
					clientRepository,
					eventEmailRecipient);

				var messageBuilder = GetCommonMessageBuilder(
					connection,
					connectionString,
					filesConnectionString,
					localizedDataHelper,
					recipientsFacade);
				var emailingProcessor = GetDefaultEmailingProcessor(partitionId, executor, messageBuilder);

				var processors = new Dictionary<EventState, IEventProcessor>
				{
					{ EventState.Emailing, emailingProcessor }
				};

				new SequentialEventJob(
					events,
					partitionId,
					new Dictionary<EventType, IDictionary<EventState, IEventProcessor>>
					{
						{ EventType.BalanceDecreased, processors },
						{ EventType.BalanceIncreased, processors }
					}).Work();
			}
		}

		private static void RunClientJob(string mainConnectionString, string filesConnectionString, int partitionId)
		{
			using(var connection = new SqlConnection(mainConnectionString))
			{
				var executor = new SqlProcedureExecutor(mainConnectionString);
				var serializer = new Serializer();
				var events = new EventRepository(executor);
				var clientRepository = new ClientRepository(executor);
				var adminRepository = new AdminRepository(connection);
				var localizedDataHelper = new CommonLocalizedDataHelper(serializer, clientRepository);
				var recipients = new EventEmailRecipient(executor);
				var managerRepository = new ManagerRepository(connection);
				var recipientsFacade = new ClientEventRecipientsFacade(
					adminRepository,
					managerRepository,
					clientRepository,
					recipients);

				var messageBuilder = GetCommonMessageBuilder(
					connection,
					mainConnectionString,
					filesConnectionString,
					localizedDataHelper,
					recipientsFacade);
				var emailingProcessor = GetDefaultEmailingProcessor(partitionId, executor, messageBuilder);

				var processors = new Dictionary<EventState, IEventProcessor>
				{
					{ EventState.Emailing, emailingProcessor }
				};

				new SequentialEventJob(
					events,
					partitionId,
					new Dictionary<EventType, IDictionary<EventState, IEventProcessor>>
					{
						{ EventType.ClientAdded, processors },
					}).Work();
			}
		}

		private static void RunEuroCourseJob(string connectionString)
		{
			var serializer = new Serializer();
			var executor = new SqlProcedureExecutor(connectionString);
			var settings = new SettingRepository(executor, serializer);
			var httpClient = new HttpClient();
			var emailMessageRepository = new EmailMessageRepository(executor);
			var mailSender = new DbMailSender(PartitionIdForOtherMails, emailMessageRepository, serializer);
			var courseSource = new CourseSourceFailPolicy(
				new CourseSourceRetryPolicy(
					new CourseSource(httpClient),
					CourseSourceAttempts,
					JobsLogger,
					CourseSourceRetryPolicyPeriod),
				mailSender,
				EmailsHelper.DefaultFrom,
				EmailsHelper.SupportEmail);

			new EuroCourseJob(settings, courseSource, serializer, PreviousRunEuroCourseJobRubTime).Work();
		}

		private static void RunMailSenderJob(string connectionString, int partitionId)
		{
			var serializer = new Serializer();
			var executor = new SqlProcedureExecutor(connectionString);
			var messages = new EmailMessageRepository(executor);
			var mailConfiguration = new MailConfiguration();
			var sender = new MailSender(mailConfiguration);

			var job = new MailSenderJob(messages, partitionId, sender, serializer);

			job.Work();
		}

		internal static IMessageBuilder GetApplicationMessageBuilder(
			IDbConnection connection,
			string mainConnectionString,
			string filesConnectionString)
		{
			var serializer = new Serializer();
			var passwordConverter = new PasswordConverter();
			var mainExecutor = new SqlProcedureExecutor(mainConnectionString);
			var filesExecutor = new SqlProcedureExecutor(filesConnectionString);
			var states = new StateRepository(mainExecutor);
			var applications = new ApplicationRepository(connection);
			var awbs = new AwbRepository(connection);
			var applicationFiles = new ApplicationFileRepository(filesExecutor);
			var clientRepository = new ClientRepository(mainExecutor);
			var clientBalanceRepository = new ClientBalanceRepository(mainExecutor);
			var countries = new CountryRepository(mainExecutor);
			var cities = new CityRepository(mainExecutor);
			var textBulder = new Application.Helpers.TextBuilder(
				serializer,
				awbs,
				countries,
				cities,
				states,
				applicationFiles,
				clientBalanceRepository,
				new TextBuilder());
			var templates = new TemplateRepository(mainExecutor);
			var recipientsFacade = new ApplicationEventRecipientsFacade(
				awbs,
				applications,
				new AdminRepository(connection),
				new ManagerRepository(connection),
				new SenderRepository(passwordConverter, mainExecutor),
				clientRepository,
				new CarrierRepository(passwordConverter, mainExecutor),
				new ForwarderRepository(passwordConverter, mainExecutor),
				new BrokerRepository(connection),
				new EventEmailRecipient(mainExecutor));
			var templateRepositoryHelper = new TemplateRepositoryHelper(templates);

			var filesFacade = GetFilesFacade(connection, mainConnectionString, filesConnectionString);

			return new ApplicationMessageBuilder(
				EmailsHelper.DefaultFrom,
				filesFacade,
				textBulder,
				recipientsFacade,
				templateRepositoryHelper,
				serializer,
				applications);
		}
	}
}