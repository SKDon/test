using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.BlackBox.Tests.Helpers;
using Alicargo.DataAccess.Repositories;
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
		private IApplicationFileRepository _files;
		private IStateRepository _stateRepository;
		private DbTestContext _context;
		private ApplicationUpdateRepository _applicationUpater;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext();

			_applications = new ApplicationRepository(_context.UnitOfWork);
			_files = new ApplicationFileRepository(_context.UnitOfWork);
			_stateRepository = new StateRepository(_context.UnitOfWork);
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
			var defaultState = _stateRepository.Get(TestConstants.DefaultStateId);
			var count = _applications.Count(new[] { defaultState.Id });

			CreateTestApplication();

			var newCount = _applications.Count(new[] { defaultState.Id });

			Assert.AreEqual(newCount, count + 1);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_ApplicationRepository_UpdateState()
		{
			var application = CreateTestApplication();
			var state = _stateRepository.GetAll().First(x => x.Id != application.StateId);

			_applicationUpater.SetState(application.Id, state.Id);
			_context.UnitOfWork.SaveChanges();

			var actual = _applications.Get(application.Id);

			Assert.AreEqual(state.Id, actual.StateId);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_ApplicationRepository_Update()
		{
			var old = CreateTestApplication();

			var newData = _context.Fixture.Create<ApplicationData>();
			newData.Id = old.Id;
			newData.StateId = old.StateId;
			newData.SenderId = TestConstants.TestSenderId;
			newData.ClientId = old.ClientId;
			newData.TransitId = old.TransitId;
			newData.AirWaybillId = old.AirWaybillId;
			newData.CreationTimestamp = old.CreationTimestamp;
			newData.StateChangeTimestamp = old.StateChangeTimestamp;

			var swiftFile = _context.RandomBytes();
			var invoiceFile = _context.RandomBytes();
			var cpFile = _context.RandomBytes();
			var deliveryBillFile = _context.RandomBytes();
			var torg12File = _context.RandomBytes();
			var packingFile = _context.RandomBytes();

			_applicationUpater.Update(newData, swiftFile, invoiceFile, cpFile, deliveryBillFile, torg12File, packingFile);
			_context.UnitOfWork.SaveChanges();

			var data = _applications.Get(old.Id);

			_files.GetInvoiceFile(data.Id).Data.ShouldBeEquivalentTo(invoiceFile);
			_files.GetSwiftFile(data.Id).Data.ShouldBeEquivalentTo(swiftFile);
			_files.GetCPFile(data.Id).Data.ShouldBeEquivalentTo(cpFile);
			_files.GetDeliveryBillFile(data.Id).Data.ShouldBeEquivalentTo(deliveryBillFile);
			_files.GetTorg12File(data.Id).Data.ShouldBeEquivalentTo(torg12File);
			_files.GetPackingFile(data.Id).Data.ShouldBeEquivalentTo(packingFile);
			data.ShouldBeEquivalentTo(newData);
		}

		private ApplicationData CreateTestApplication()
		{
			var application = _context.Fixture
				.Build<ApplicationData>()
				.Without(x => x.Id)
				.Without(x => x.SenderId)
				.With(x => x.ClientId, TestConstants.TestClientId1)
				.With(x => x.TransitId, TestConstants.TestTransitId)
				.With(x => x.StateId, TestConstants.DefaultStateId)
				.With(x => x.CountryId, TestConstants.TestCountryId)
				.With(x => x.AirWaybillId, null)
				.With(x => x.InvoiceFileName, null)
				.With(x => x.SwiftFileName, null)
				.With(x => x.Torg12FileName, null)
				.With(x => x.DeliveryBillFileName, null)
				.With(x => x.CPFileName, null)
				.Create();

			var id = _applicationUpater.Add(application, _context.RandomBytes(),
				_context.RandomBytes(), _context.RandomBytes(), _context.RandomBytes(),
				_context.RandomBytes(), _context.RandomBytes());
			_context.UnitOfWork.SaveChanges();

			application.Id = id();

			return application;
		}
	}
}
