using System.Data.SqlClient;
using System.Linq;
using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using Alicargo.DataAccess.Repositories.Application;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
			_stateRepository = new StateRepository(new SqlProcedureExecutor(Settings.Default.MainConnectionString));
			_editor = new ApplicationEditor(new SqlConnection(Settings.Default.MainConnectionString));
		}

		[TestMethod]
		public void Test_ApplicationRepository_Add_Get()
		{
			long id;
			var expected = CreateTestApplication(out id);

			var actual = _applications.Get(id);

			Assert.IsNotNull(actual);

			expected.ShouldBeEquivalentTo(actual);
		}

		[TestMethod]
		public void Test_ApplicationRepository_Add_GetDetails()
		{
			long id;
			var expected = CreateTestApplication(out id);

			var actual = _applications.Get(id);

			Assert.IsNotNull(actual);

			expected.ShouldBeEquivalentTo(actual,
				options => options.ExcludingMissingProperties()
					.Excluding(x => x.FactureCost)
					.Excluding(x => x.FactureCostEx)
					.Excluding(x => x.PickupCost)
					.Excluding(x => x.TransitCost));

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
			long id;
			var old = CreateTestApplication(out id);

			var newData = _fixture.Create<ApplicationEditData>();
			newData.StateId = old.StateId;
			newData.SenderId = TestConstants.TestSenderId;
			newData.CountryId = TestConstants.TestCountryId;
			newData.ClientId = old.ClientId;
			newData.TransitId = old.TransitId;
			newData.AirWaybillId = old.AirWaybillId;
			newData.ForwarderId = TestConstants.TestForwarderId2;

			_editor.Update(id, newData);

			var data = _applications.Get(id);

			data.ShouldBeEquivalentTo(newData);
		}

		[TestMethod]
		public void Test_ApplicationRepository_UpdateState()
		{
			long id;
			var application = CreateTestApplication(out id);
			var state = _stateRepository.Get(TwoLetterISOLanguageName.Italian).First(x => x.Key != application.StateId);

			_editor.SetState(id, state.Key);

			var actual = _applications.Get(id);

			Assert.AreEqual(state.Key, actual.StateId);
		}

		private ApplicationEditData CreateTestApplication(out long id)
		{
			var application = _fixture
				.Build<ApplicationEditData>()
				.With(x => x.SenderId, TestConstants.TestSenderId)
				.With(x => x.ClientId, TestConstants.TestClientId1)
				.With(x => x.TransitId, TestConstants.TestTransitId)
				.With(x => x.StateId, TestConstants.DefaultStateId)
				.With(x => x.CountryId, TestConstants.TestCountryId)
				.With(x => x.AirWaybillId, null)
				.With(x => x.ForwarderId, TestConstants.TestForwarderId1)
				.Create();

			id = _editor.Add(application);

			return application;
		}
	}
}