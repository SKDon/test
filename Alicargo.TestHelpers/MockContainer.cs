using System;
using System.Collections.Generic;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.Application;
using Alicargo.Contracts.Contracts.State;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Core.Services.Abstract;
using Alicargo.Jobs.ApplicationEvents.Abstract;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.AirWaybill;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Dsl;

namespace Alicargo.TestHelpers
{
	public sealed class MockContainer
	{
		private readonly MockBehavior _mockBehavior;

		public MockContainer(MockBehavior behavior = MockBehavior.Strict)
		{
			_mockBehavior = behavior;

			Fixture = new Fixture();
			Fixture.Customize(new AutoMoqCustomization());

			Fixture.Register(() => Fixture.Build<RecipientData>()
				.With(x => x.Culture, TwoLetterISOLanguageName.English)
				.Create());

			Fixture.Register(() => Fixture.Build<ApplicationDetailsData>()
				.With(x => x.CurrencyId, 1)
				.With(x => x.CountryName, new[]
				{
					new KeyValuePair<string, string>(TwoLetterISOLanguageName.English, Fixture.Create<string>()),
					new KeyValuePair<string, string>(TwoLetterISOLanguageName.Russian, Fixture.Create<string>()),
					new KeyValuePair<string, string>(TwoLetterISOLanguageName.Italian, Fixture.Create<string>())
				})
				.Create());

			Fixture.Register(() => Fixture.Build<AwbAdminModel>()
				.With(x => x.DateOfDepartureLocalString, Fixture.Create<DateTimeOffset>().ToString())
				.With(x => x.DateOfArrivalLocalString, Fixture.Create<DateTimeOffset>().ToString())
				.Create());

			Fixture.Register(() => Fixture.Build<AwbSenderModel>()
				.With(x => x.DateOfDepartureLocalString, Fixture.Create<DateTimeOffset>().ToString())
				.With(x => x.DateOfArrivalLocalString, Fixture.Create<DateTimeOffset>().ToString())
				.Create());

			Fixture.Register(() => new StateData
			{
				Name = Fixture.Create<string>(), 
				Position = Fixture.Create<int>(),
				LocalizedName = Fixture.Create<string>()
			});

			Serializer = Inject<ISerializer>();
			StateRepository = Inject<IStateRepository>();
			IdentityService = Inject<IIdentityService>();
			StateService = Inject<IStateFilter>();
			ApplicationRepository = Inject<IApplicationRepository>();
			ApplicationUpdater = Inject<IApplicationUpdateRepository>();
			ApplicationManager = Inject<IApplicationManager>();
			ApplicationPresenter = Inject<IApplicationPresenter>();
			AirWaybillRepository = Inject<IAwbRepository>();
			StateConfig = Inject<IStateConfig>();
			UnitOfWork = Inject<IUnitOfWork>();
			Transaction = Inject<ITransaction>();
			ApplicationListItemMapper = Inject<IApplicationListItemMapper>();
			CountryRepository = Inject<ICountryRepository>();
			ApplicationGrouper = Inject<IApplicationGrouper>();
			ApplicationAwbManager = Inject<IApplicationAwbManager>();
			ClientPermissions = Inject<IClientPermissions>();
			ClientRepository = Inject<IClientRepository>();
			MailSender = Inject<IMailSender>();
			MessageBuilder = Inject<IMessageBuilder>();
			ApplicationFileRepository = Inject<IApplicationFileRepository>();
			StateSettingsRepository = Inject<IStateSettingsRepository>();
			EmailTemplateRepository = Inject<IEmailTemplateRepository>();
			AdminRepository = Inject<IAdminRepository>();
			TemplatesFacade = Inject<ITemplatesFacade>();
			RecipientsFacade = Inject<IRecipientsFacade>();
			FilesFasade = Inject<IFilesFacade>();
			TextBulder = Inject<ITextBulder>();

			Transaction.Setup(x => x.Dispose());
		}

		public Fixture Fixture { get; private set; }

		public Mock<IAdminRepository> AdminRepository { get; private set; }
		public Mock<IApplicationRepository> ApplicationRepository { get; private set; }
		public Mock<IApplicationFileRepository> ApplicationFileRepository { get; private set; }
		public Mock<ISerializer> Serializer { get; private set; }
		public Mock<IApplicationUpdateRepository> ApplicationUpdater { get; private set; }
		public Mock<IApplicationManager> ApplicationManager { get; private set; }
		public Mock<IApplicationAwbManager> ApplicationAwbManager { get; private set; }
		public Mock<IApplicationGrouper> ApplicationGrouper { get; private set; }
		public Mock<IApplicationListItemMapper> ApplicationListItemMapper { get; private set; }
		public Mock<IApplicationPresenter> ApplicationPresenter { get; private set; }
		public Mock<IAwbRepository> AirWaybillRepository { get; private set; }
		public Mock<IClientPermissions> ClientPermissions { get; private set; }
		public Mock<IClientRepository> ClientRepository { get; private set; }
		public Mock<ICountryRepository> CountryRepository { get; private set; }
		public Mock<IIdentityService> IdentityService { get; private set; }
		public Mock<IMailSender> MailSender { get; private set; }
		public Mock<IMessageBuilder> MessageBuilder { get; private set; }
		public Mock<IStateRepository> StateRepository { get; private set; }
		public Mock<IStateSettingsRepository> StateSettingsRepository { get; private set; }
		public Mock<IStateFilter> StateService { get; private set; }
		public Mock<IStateConfig> StateConfig { get; private set; }
		public Mock<ITransaction> Transaction { get; private set; }
		public Mock<IUnitOfWork> UnitOfWork { get; private set; }
		public Mock<IEmailTemplateRepository> EmailTemplateRepository { get; private set; }
		public Mock<ITemplatesFacade> TemplatesFacade { get; private set; }
		public Mock<IRecipientsFacade> RecipientsFacade { get; private set; }
		public Mock<IFilesFacade> FilesFasade { get; private set; }
		public Mock<ITextBulder> TextBulder { get; private set; }

		private Mock<T> Inject<T>() where T : class
		{
			var mock = new Mock<T>(_mockBehavior);
			Fixture.Inject(mock);
			return mock;
		}

		public T Create<T>()
		{
			return Fixture.Create<T>();
		}

		public ICustomizationComposer<T> Build<T>()
		{
			return Fixture.Build<T>();
		}

		public IEnumerable<T> CreateMany<T>(int count = 3)
		{
			return Fixture.CreateMany<T>(count);
		}
	}
}