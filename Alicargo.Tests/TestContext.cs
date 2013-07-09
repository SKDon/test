using System.Collections.Generic;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Dsl;

namespace Alicargo.Tests
{
	class TestContext
	{
		public Fixture Fixture { get; private set; }
		private readonly CompareObjects _comparer;

		public Mock<IIdentityService> IdentityService { get; private set; }
		public Mock<IStateRepository> StateRepository { get; private set; }
		public Mock<IStateService> StateService { get; private set; }
		public Mock<IApplicationRepository> ApplicationRepository { get; private set; }
		public Mock<IApplicationManager> ApplicationManager { get; private set; }
		public Mock<IReferenceRepository> ReferenceRepository { get; private set; }
		public Mock<IStateConfig> StateConfig { get; private set; }
		public Mock<IUnitOfWork> UnitOfWork { get; private set; }
		public Mock<ITransaction> Transaction { get; private set; }

		public TestContext()
		{
			Fixture = new Fixture();
			Fixture.Customize(new AutoMoqCustomization());
			_comparer = new CompareObjects
			{
				MaxDifferences = 3,
				Caching = true,
				AutoClearCache = false
			};

			Fixture.Register(() =>
				Fixture.Build<ReferenceModel>()
					.Without(x => x.DateOfDepartureLocalString)
					.Without(x => x.DateOfArrivalLocalString)
					.Create());

			StateRepository = Inject<IStateRepository>();
			IdentityService = Inject<IIdentityService>();
			StateService = Inject<IStateService>();
			ApplicationRepository = Inject<IApplicationRepository>();
			ApplicationManager = Inject<IApplicationManager>();
			ReferenceRepository = Inject<IReferenceRepository>();
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

		public void AreEquals<T>(T one, T other)
		{
			if (!_comparer.Compare(one, other))
				Assert.Fail(_comparer.DifferencesString);
		}

		public void AreNotEquals<T>(T one, T other)
		{
			if (_comparer.Compare(one, other))
				Assert.Fail("Objects should be not equal");
		}
	}
}
