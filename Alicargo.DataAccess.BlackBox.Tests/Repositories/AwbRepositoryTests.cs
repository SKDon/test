using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.DataAccess.BlackBox.Tests.Helpers;
using Alicargo.DataAccess.Repositories;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories
{
	[TestClass]
	public class AwbRepositoryTests
	{
		private IAwbRepository _awbRepository;
		private DbTestContext _context;
		private Fixture _fixture;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext();
			_fixture = new Fixture();

			_awbRepository = new AwbRepository(_context.UnitOfWork);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_AwbRepository_GetAll_Add_Get()
		{
			var oldData = _awbRepository.Get();

			var data = CreateTestAirWaybill();

			var newData = _awbRepository.Get();

			Assert.AreEqual(oldData.Length + 1, newData.Length);

			var airWaybill = _awbRepository.Get(data.Id).First();

			data.ShouldBeEquivalentTo(airWaybill);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_AwbRepository_Count_GetRange()
		{
			var airWaybillDatas = _awbRepository.Get();
			var count = _awbRepository.Count();

			Assert.AreEqual(airWaybillDatas.Length, count);

			var range = _awbRepository.GetRange((int) count, 0);

			airWaybillDatas.ShouldBeEquivalentTo(range);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_AwbRepository_GetClientEmails()
		{
			var data1 = CreateApplicationData(TestConstants.TestClientId1);
			var data2 = CreateApplicationData(TestConstants.TestClientId2);

			var id = _awbRepository.Add(CreateAirWaybillData(), null, null, null, null, null);
			_context.UnitOfWork.SaveChanges();

			var applications = new ApplicationUpdateRepository(_context.UnitOfWork);
			var a1 = applications.Add(data1);
			var a2 = applications.Add(data2);
			_context.UnitOfWork.SaveChanges();

			applications.SetAirWaybill(a1(), id());
			applications.SetAirWaybill(a2(), id());
			_context.UnitOfWork.SaveChanges();

			var emails = _awbRepository.GetClientEmails(id());

			var clientRepository = new ClientRepository(_context.UnitOfWork);

			var client1 = clientRepository.Get(TestConstants.TestClientId1);
			var client2 = clientRepository.Get(TestConstants.TestClientId2);
			var clients = new[] {client1, client2};

			emails.ShouldBeEquivalentTo(clients.Select(x => x.Email).ToArray());
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_AwbRepository_Update()
		{
			var data = CreateTestAirWaybill();

			var newData = CreateAirWaybillData();
			newData.StateId = data.StateId;
			newData.Id = data.Id;
			newData.BrokerId = data.BrokerId;
			newData.CreationTimestamp = data.CreationTimestamp;
			newData.StateChangeTimestamp = data.StateChangeTimestamp;

			var gtdFile = _context.RandomBytes();
			var additionalFile = _context.RandomBytes();
			var packingFile = _context.RandomBytes();
			var invoiceFile = _context.RandomBytes();
			var awbFile = _context.RandomBytes();
			_awbRepository.Update(newData, gtdFile, additionalFile, packingFile, invoiceFile, awbFile);
			_context.UnitOfWork.SaveChanges();

			var actual = _awbRepository.Get(newData.Id).First();
			actual.ShouldBeEquivalentTo(newData);

			_awbRepository.GetGTDFile(newData.Id).Data.ShouldBeEquivalentTo(gtdFile);
			_awbRepository.GTDAdditionalFile(newData.Id).Data.ShouldBeEquivalentTo(additionalFile);
			_awbRepository.GetPackingFile(newData.Id).Data.ShouldBeEquivalentTo(packingFile);
			_awbRepository.GetInvoiceFile(newData.Id).Data.ShouldBeEquivalentTo(invoiceFile);
			_awbRepository.GetAWBFile(newData.Id).Data.ShouldBeEquivalentTo(awbFile);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_AwbRepository_SetState()
		{
			var data = CreateTestAirWaybill();

			_awbRepository.SetState(data.Id, TestConstants.CargoIsFlewStateId);
			_context.UnitOfWork.SaveChanges();

			var actual = _awbRepository.Get(data.Id).First();

			actual.StateId.ShouldBeEquivalentTo(TestConstants.CargoIsFlewStateId);
			actual.StateChangeTimestamp.Should().NotBe(data.StateChangeTimestamp);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_AwbRepository_Delete()
		{
			var data = CreateTestAirWaybill();

			_awbRepository.Delete(data.Id);
			_context.UnitOfWork.SaveChanges();

			_awbRepository.Get(data.Id).Count().ShouldBeEquivalentTo(0);
		}

		private AirWaybillData CreateTestAirWaybill()
		{
			var data = CreateAirWaybillData();

			var id = _awbRepository.Add(data, _context.RandomBytes(), _context.RandomBytes(),
				_context.RandomBytes(), _context.RandomBytes(),
				_context.RandomBytes());

			_context.UnitOfWork.SaveChanges();

			data.Id = id();

			return data;
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_AwbRepository_GetAggregate()
		{
			var data11 = CreateApplicationData(TestConstants.TestClientId1);
			var data12 = CreateApplicationData(TestConstants.TestClientId1);
			var data21 = CreateApplicationData(TestConstants.TestClientId1);
			var data22 = CreateApplicationData(TestConstants.TestClientId1);

			var awbId1 = _awbRepository.Add(CreateAirWaybillData(), null, null, null, null, null);
			var awbId2 = _awbRepository.Add(CreateAirWaybillData(), null, null, null, null, null);
			_context.UnitOfWork.SaveChanges();

			var applications = new ApplicationUpdateRepository(_context.UnitOfWork);
			var app11 = applications.Add(data11);
			var app12 = applications.Add(data12);
			var app21 = applications.Add(data21);
			var app22 = applications.Add(data22);
			_context.UnitOfWork.SaveChanges();

			applications.SetAirWaybill(app11(), awbId1());
			applications.SetAirWaybill(app12(), awbId1());
			applications.SetAirWaybill(app21(), awbId2());
			applications.SetAirWaybill(app22(), awbId2());
			_context.UnitOfWork.SaveChanges();

			var aggregates = _awbRepository.GetAggregate(awbId1(), awbId2());

			aggregates.Count().ShouldBeEquivalentTo(2);

			var aggregate1 = aggregates.First(x => x.AirWaybillId == awbId1());
			aggregate1.TotalCount.ShouldBeEquivalentTo(data11.Count + data12.Count);
			aggregate1.TotalWeight.ShouldBeEquivalentTo(data11.Weight + data12.Weight);
			aggregate1.TotalVolume.ShouldBeEquivalentTo(data11.Volume + data12.Volume);
			aggregate1.TotalValue.ShouldBeEquivalentTo(data11.Value + data12.Value);

			var aggregate2 = aggregates.First(x => x.AirWaybillId == awbId2());
			aggregate2.TotalCount.ShouldBeEquivalentTo(data21.Count + data22.Count);
			aggregate2.TotalWeight.ShouldBeEquivalentTo(data21.Weight + data22.Weight);
			aggregate2.TotalVolume.ShouldBeEquivalentTo(data21.Volume + data22.Volume);
			aggregate2.TotalValue.ShouldBeEquivalentTo(data21.Value + data22.Value);
		}

		private ApplicationData CreateApplicationData(long clientId)
		{
			return _fixture
				.Build<ApplicationData>()
				.With(x => x.SenderId, TestConstants.TestSenderId)
				.With(x => x.ClientId, clientId)
				.With(x => x.AirWaybillId, null)
				.With(x => x.CountryId, TestConstants.TestCountryId)
				.With(x => x.StateId, TestConstants.DefaultStateId)
				.With(x => x.TransitId, 1)
				.With(x => x.CurrencyId, (int) CurrencyType.Dollar)
				.Create();
		}

		private AirWaybillData CreateAirWaybillData()
		{
			return _fixture
				.Build<AirWaybillData>()
				.With(x => x.StateId, TestConstants.DefaultStateId)
				.With(x => x.BrokerId, TestConstants.TestBrokerId)
				.Create();
		}
	}
}