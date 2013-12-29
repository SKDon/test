using System.Linq;
using Alicargo.Contracts.Contracts.Application;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.BlackBox.Tests.Helpers;
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
		private IApplicationRepository _applications;
		private IStateRepository _stateRepository;
		private DbTestContext _context;
		private ApplicationUpdateRepository _applicationUpater;
		private Fixture _fixture;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext();
			_fixture = new Fixture();

			_applications = new ApplicationRepository(_context.UnitOfWork);
			_stateRepository = new StateRepository(new SqlProcedureExecutor(_context.Connection.ConnectionString));
			_applicationUpater = new ApplicationUpdateRepository(_context.UnitOfWork);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_ApplicationRepository_Add_Get()
		{
			var expected = CreateTestApplication();

			var actual = _applications.Get(expected.Id);

			Assert.IsNotNull(actual);

			expected.ShouldBeEquivalentTo(actual);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_ApplicationRepository_Add_GetDetails()
		{
			var expected = CreateTestApplication();

			var actual = _applications.GetDetails(expected.Id);

			Assert.IsNotNull(actual);

			expected.ShouldBeEquivalentTo(actual, options => options.ExcludingMissingProperties());
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_ApplicationRepository_Count()
		{
			var defaultState = _stateRepository.Get(TwoLetterISOLanguageName.Italian, TestConstants.DefaultStateId).First();
			var count = _applications.Count(new[] { defaultState.Key });

			CreateTestApplication();

			var newCount = _applications.Count(new[] { defaultState.Key });

			Assert.AreEqual(newCount, count + 1);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_ApplicationRepository_UpdateState()
		{
			var application = CreateTestApplication();
			var state = _stateRepository.Get(TwoLetterISOLanguageName.Italian).First(x => x.Key != application.StateId);

			_applicationUpater.SetState(application.Id, state.Key);
			_context.UnitOfWork.SaveChanges();

			var actual = _applications.Get(application.Id);

			Assert.AreEqual(state.Key, actual.StateId);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_ApplicationRepository_Update()
		{
			var old = CreateTestApplication();

			var newData = _fixture.Create<ApplicationData>();
			newData.Id = old.Id;
			newData.StateId = old.StateId;
			newData.SenderId = TestConstants.TestSenderId;
			newData.ClientId = old.ClientId;
			newData.TransitId = old.TransitId;
			newData.AirWaybillId = old.AirWaybillId;
			newData.CreationTimestamp = old.CreationTimestamp;
			newData.StateChangeTimestamp = old.StateChangeTimestamp;

			_applicationUpater.Update(newData);
			_context.UnitOfWork.SaveChanges();

			var data = _applications.Get(old.Id);

			data.ShouldBeEquivalentTo(newData);
		}

		private ApplicationData CreateTestApplication()
		{
			var application = _fixture
				.Build<ApplicationData>()
				.Without(x => x.Id)
				.With(x => x.SenderId, TestConstants.TestSenderId)
				.With(x => x.ClientId, TestConstants.TestClientId1)
				.With(x => x.TransitId, TestConstants.TestTransitId)
				.With(x => x.StateId, TestConstants.DefaultStateId)
				.With(x => x.CountryId, TestConstants.TestCountryId)
				.With(x => x.AirWaybillId, null)
				.Create();

			var id = _applicationUpater.Add(application);
			_context.UnitOfWork.SaveChanges();

			application.Id = id();

			return application;
		}
	}
}