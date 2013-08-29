using System;
using System.Collections.Generic;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.AirWaybill;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Dsl;

namespace Alicargo.TestHelpers
{
    public /*sealed*/ class MockContainer
    {
        private readonly MockBehavior _mockBehavior;

        public MockContainer(MockBehavior behavior = MockBehavior.Strict)
        {
            _mockBehavior = behavior;

            Fixture = new Fixture();
            Fixture.Customize(new AutoMoqCustomization());

            Fixture.Register(() =>
                             Fixture.Build<AirWaybillEditModel>()
                                    .With(x => x.DateOfDepartureLocalString, DateTimeOffset.UtcNow.ToString())
                                    .With(x => x.DateOfArrivalLocalString, DateTimeOffset.UtcNow.ToString())
                                    .Create());

            StateRepository = Inject<IStateRepository>();
            IdentityService = Inject<IIdentityService>();
            StateService = Inject<IStateService>();
            ApplicationRepository = Inject<IApplicationRepository>();
            ApplicationUpdater = Inject<IApplicationUpdateRepository>();
            ApplicationManager = Inject<IApplicationManager>();
            ApplicationPresenter = Inject<IApplicationPresenter>();
            AirWaybillRepository = Inject<IAwbRepository>();
            AuthenticationRepository = Inject<IAuthenticationRepository>();
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

            Transaction.Setup(x => x.Dispose());
        }

        public Fixture Fixture { get; private set; }

        public Mock<IApplicationRepository> ApplicationRepository { get; private set; }
        public Mock<IApplicationUpdateRepository> ApplicationUpdater { get; private set; }
        public Mock<IApplicationManager> ApplicationManager { get; private set; }
        public Mock<IApplicationAwbManager> ApplicationAwbManager { get; private set; }
        public Mock<IApplicationGrouper> ApplicationGrouper { get; private set; }
        public Mock<IApplicationListItemMapper> ApplicationListItemMapper { get; private set; }
        public Mock<IApplicationPresenter> ApplicationPresenter { get; private set; }
        public Mock<IAwbRepository> AirWaybillRepository { get; private set; }
        public Mock<IAuthenticationRepository> AuthenticationRepository { get; private set; }
        public Mock<IClientPermissions> ClientPermissions { get; private set; }
        public Mock<IClientRepository> ClientRepository { get; private set; }
        public Mock<ICountryRepository> CountryRepository { get; private set; }
        public Mock<IIdentityService> IdentityService { get; private set; }
        public Mock<IMailSender> MailSender { get; private set; }
        public Mock<IMessageBuilder> MessageBuilder { get; private set; }
        public Mock<IStateRepository> StateRepository { get; private set; }
        public Mock<IStateService> StateService { get; private set; }
        public Mock<IStateConfig> StateConfig { get; private set; }
        public Mock<ITransaction> Transaction { get; private set; }
        public Mock<IUnitOfWork> UnitOfWork { get; private set; }

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