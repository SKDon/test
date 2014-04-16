using System;
using System.Linq;
using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using Alicargo.DataAccess.Repositories.Application;
using Alicargo.TestHelpers;
using Alicargo.Utilities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories
{
	[TestClass]
	public class ApplicationRepositoryTests
	{
		private ApplicationRepository _applications;
		private DbTestContext _context;
		private ApplicationEditor _editor;
		private Fixture _fixture;
		private StateRepository _stateRepository;
		private Mock<DateTimeProvider.IDateTimeProvider> _dateTimeProvider;

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);
			_fixture = new Fixture();

			_applications = new ApplicationRepository(_context.Connection);
			var executor = new SqlProcedureExecutor(Settings.Default.MainConnectionString);
			_stateRepository = new StateRepository(executor);
			_editor = new ApplicationEditor(_context.Connection, executor, TestConstants.DefaultStateId);

			_dateTimeProvider = new Mock<DateTimeProvider.IDateTimeProvider>(MockBehavior.Strict);			

			DateTimeProvider.SetProvider(_dateTimeProvider.Object);
		}

		[TestMethod]
		public void Test_ApplicationRepository_Add_Get()
		{
			var now = _fixture.Create<DateTimeOffset>();
			_dateTimeProvider.SetupProperty(x => x.Now).SetupGet(provider => now);

			long id;
			var expected = CreateTestApplication(out id);
			
			var actual = _applications.Get(id);

			Assert.IsNotNull(actual);
			actual.StateId.ShouldBeEquivalentTo(TestConstants.DefaultStateId);
			actual.DisplayNumber.Should().NotBe(0);
			actual.ShouldBeEquivalentTo(expected);
			actual.CreationTimestamp.ShouldBeEquivalentTo(now);
			actual.StateChangeTimestamp.ShouldBeEquivalentTo(now);
			actual.GetAdjustedFactureCost().ShouldBeEquivalentTo(expected.FactureCostEdited);
			actual.GetAdjustedFactureCostEx().ShouldBeEquivalentTo(expected.FactureCostExEdited);
			actual.GetAdjustedPickupCost().ShouldBeEquivalentTo(expected.PickupCostEdited);
			actual.GetAdjustedTransitCost().ShouldBeEquivalentTo(expected.TransitCostEdited);
		}

		[TestMethod]
		public void Test_ApplicationRepository_Count()
		{
			var defaultState = _stateRepository.Get(TwoLetterISOLanguageName.Italian, TestConstants.DefaultStateId).First();
			var count = _applications.Count(new[] { defaultState.Key });

			long id;
			CreateTestApplication(out id);

			var newCount = _applications.Count(new[] { defaultState.Key });

			Assert.AreEqual(newCount, count + 1);
		}

		[TestMethod]
		public void Test_ApplicationRepository_Update()
		{
			var now = _fixture.Create<DateTimeOffset>();
			_dateTimeProvider.SetupProperty(x => x.Now).SetupGet(provider => now);

			long id;
			var editData = CreateTestApplication(out id);

			var expectation = _fixture.Create<ApplicationEditData>();
			expectation.SenderId = TestConstants.TestSenderId;
			expectation.CountryId = TestConstants.TestCountryId;
			expectation.ClientId = editData.ClientId;
			expectation.TransitId = editData.TransitId;
			expectation.AirWaybillId = editData.AirWaybillId;
			expectation.ForwarderId = TestConstants.TestForwarderId2;

			_editor.Update(id, expectation);

			var actual = _applications.Get(id);

			actual.StateId.ShouldBeEquivalentTo(TestConstants.DefaultStateId);
			actual.DisplayNumber.Should().NotBe(0);
			actual.ShouldBeEquivalentTo(expectation);
			actual.CreationTimestamp.ShouldBeEquivalentTo(now);
			actual.StateChangeTimestamp.ShouldBeEquivalentTo(now);
			actual.GetAdjustedFactureCost().ShouldBeEquivalentTo(expectation.FactureCostEdited);
			actual.GetAdjustedFactureCostEx().ShouldBeEquivalentTo(expectation.FactureCostExEdited);
			actual.GetAdjustedPickupCost().ShouldBeEquivalentTo(expectation.PickupCostEdited);
			actual.GetAdjustedTransitCost().ShouldBeEquivalentTo(expectation.TransitCostEdited);
		}

		[TestMethod]
		public void Test_ApplicationRepository_UpdateState()
		{
			long id;
			CreateTestApplication(out id);

			Assert.AreEqual(TestConstants.DefaultStateId, _applications.Get(id).StateId);

			_editor.SetState(id, TestConstants.CargoIsFlewStateId);

			Assert.AreEqual(TestConstants.CargoIsFlewStateId, _applications.Get(id).StateId);
		}

		private ApplicationEditData CreateTestApplication(out long id)
		{
			var application = _fixture
				.Build<ApplicationEditData>()
				.With(x => x.SenderId, TestConstants.TestSenderId)
				.With(x => x.ClientId, TestConstants.TestClientId1)
				.With(x => x.TransitId, TestConstants.TestTransitId)
				.With(x => x.CountryId, TestConstants.TestCountryId)
				.With(x => x.AirWaybillId, null)
				.With(x => x.ForwarderId, TestConstants.TestForwarderId1)
				.Create();

			id = _editor.Add(application);

			return application;
		}
	}
}