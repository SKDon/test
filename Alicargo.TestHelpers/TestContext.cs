using System.Collections.Generic;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.AirWaybill;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Dsl;

namespace Alicargo.TestHelpers
{
	public /*sealed*/ class TestContext
	{
		public Fixture Fixture { get; private set; }

		public Mock<IIdentityService> IdentityService { get; private set; }
		public Mock<IStateRepository> StateRepository { get; private set; }
		public Mock<IStateService> StateService { get; private set; }
		public Mock<IApplicationRepository> ApplicationRepository { get; private set; }
		public Mock<IApplicationUpdateRepository> ApplicationUpdater { get; private set; }
		public Mock<IApplicationManager> ApplicationManager { get; private set; }
		public Mock<IAWBRepository> AirWaybillRepository { get; private set; }
		public Mock<IStateConfig> StateConfig { get; private set; }
		public Mock<IUnitOfWork> UnitOfWork { get; private set; }
		public Mock<ITransaction> Transaction { get; private set; }

		public TestContext()
		{
			Fixture = new Fixture();
			Fixture.Customize(new AutoMoqCustomization());


			Fixture.Register(() =>
				Fixture.Build<AirWaybillEditModel>()
					.Without(x => x.DateOfDepartureLocalString)
					.Without(x => x.DateOfArrivalLocalString)
					.Create());

			StateRepository = Inject<IStateRepository>();
			IdentityService = Inject<IIdentityService>();
			StateService = Inject<IStateService>();
			ApplicationRepository = Inject<IApplicationRepository>();
			ApplicationUpdater = Inject<IApplicationUpdateRepository>();
			ApplicationManager = Inject<IApplicationManager>();
			AirWaybillRepository = Inject<IAWBRepository>();
			StateConfig = Inject<IStateConfig>();
			UnitOfWork = Inject<IUnitOfWork>();
			Transaction = Inject<ITransaction>();
			Transaction.Setup(x => x.Dispose());
		}

		private Mock<T> Inject<T>() where T : class
		{
			var mock = new Mock<T>(MockBehavior.Strict);
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
