using System.Linq;
using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Repositories.Application;
using Alicargo.DataAccess.Repositories.User;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories
{
	[TestClass]
	public class AwbRepositoryTests
	{
		private IAwbRepository _awbs;
		private DbTestContext _context;
		private IAwbFileRepository _files;
		private Fixture _fixture;

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

			_awbs = new AwbRepository(_context.UnitOfWork);
			_files = new AwbFileRepository(_context.UnitOfWork);
		}

		[TestMethod]
		public void Test_AwbRepository_Count_GetRange()
		{
			var airWaybillDatas = _awbs.Get();
			var count = _awbs.Count();

			Assert.AreEqual(airWaybillDatas.Length, count);

			var range = _awbs.GetRange((int)count, 0);

			airWaybillDatas.ShouldBeEquivalentTo(range);
		}

		[TestMethod]
		public void Test_AwbRepository_Delete()
		{
			var data = CreateTestAirWaybill();

			_awbs.Delete(data.Id);

			_awbs.Get(data.Id).Count().ShouldBeEquivalentTo(0);
		}

		[TestMethod]
		public void Test_AwbRepository_GetAggregate()
		{
			var data11 = CreateApplicationData(TestConstants.TestClientId1);
			var data12 = CreateApplicationData(TestConstants.TestClientId1);
			var data21 = CreateApplicationData(TestConstants.TestClientId1);
			var data22 = CreateApplicationData(TestConstants.TestClientId1);

			var awbId1 = _awbs.Add(CreateAirWaybillData(), null, null, null, null, null, null);
			var awbId2 = _awbs.Add(CreateAirWaybillData(), null, null, null, null, null, null);

			var applications = new ApplicationEditor(_context.UnitOfWork);
			var app11 = applications.Add(data11);
			var app12 = applications.Add(data12);
			var app21 = applications.Add(data21);
			var app22 = applications.Add(data22);

			applications.SetAirWaybill(app11, awbId1);
			applications.SetAirWaybill(app12, awbId1);
			applications.SetAirWaybill(app21, awbId2);
			applications.SetAirWaybill(app22, awbId2);

			var aggregates = _awbs.GetAggregate(new[] { awbId1, awbId2 });

			aggregates.Count().ShouldBeEquivalentTo(2);

			var aggregate1 = aggregates.First(x => x.AirWaybillId == awbId1);
			aggregate1.TotalCount.ShouldBeEquivalentTo(data11.Count + data12.Count);
			aggregate1.TotalWeight.ShouldBeEquivalentTo(data11.Weight + data12.Weight);
			aggregate1.TotalVolume.ShouldBeEquivalentTo(data11.Volume + data12.Volume);
			aggregate1.TotalValue.ShouldBeEquivalentTo(data11.Value + data12.Value);

			var aggregate2 = aggregates.First(x => x.AirWaybillId == awbId2);
			aggregate2.TotalCount.ShouldBeEquivalentTo(data21.Count + data22.Count);
			aggregate2.TotalWeight.ShouldBeEquivalentTo(data21.Weight + data22.Weight);
			aggregate2.TotalVolume.ShouldBeEquivalentTo(data21.Volume + data22.Volume);
			aggregate2.TotalValue.ShouldBeEquivalentTo(data21.Value + data22.Value);
		}

		[TestMethod]
		public void Test_AwbRepository_GetAll_Add_Get()
		{
			var oldData = _awbs.Get();

			var data = CreateTestAirWaybill();

			var newData = _awbs.Get();

			Assert.AreEqual(oldData.Length + 1, newData.Length);

			var airWaybill = _awbs.Get(data.Id).First();

			data.ShouldBeEquivalentTo(airWaybill);
		}

		[TestMethod]
		public void Test_AwbRepository_GetClientEmails()
		{
			var data1 = CreateApplicationData(TestConstants.TestClientId1);
			var data2 = CreateApplicationData(TestConstants.TestClientId2);

			var id = _awbs.Add(CreateAirWaybillData(), null, null, null, null, null, null);

			var applications = new ApplicationEditor(_context.UnitOfWork);
			var a1 = applications.Add(data1);
			var a2 = applications.Add(data2);

			applications.SetAirWaybill(a1, id);
			applications.SetAirWaybill(a2, id);

			var emails = _awbs.GetClientEmails(id).Select(x => x.Email).ToArray();

			var clientRepository = new ClientRepository(_context.UnitOfWork);

			var client1 = clientRepository.Get(TestConstants.TestClientId1);
			var client2 = clientRepository.Get(TestConstants.TestClientId2);
			var clients = new[] { client1, client2 };

			emails.ShouldAllBeEquivalentTo(clients.SelectMany(x => x.Emails).ToArray());
		}

		[TestMethod]
		public void Test_AwbRepository_SetState()
		{
			var data = CreateTestAirWaybill();

			_awbs.SetState(data.Id, TestConstants.CargoIsFlewStateId);

			var actual = _awbs.Get(data.Id).First();

			actual.StateId.ShouldBeEquivalentTo(TestConstants.CargoIsFlewStateId);
			actual.StateChangeTimestamp.Should().NotBe(data.StateChangeTimestamp);
		}

		[TestMethod]
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
			var drawFile = _context.RandomBytes();
			_awbs.Update(newData, gtdFile, additionalFile, packingFile, invoiceFile, awbFile, drawFile);

			var actual = _awbs.Get(newData.Id).First();
			actual.ShouldBeEquivalentTo(newData);

			_files.GetGTDFile(newData.Id).Data.ShouldBeEquivalentTo(gtdFile);
			_files.GTDAdditionalFile(newData.Id).Data.ShouldBeEquivalentTo(additionalFile);
			_files.GetPackingFile(newData.Id).Data.ShouldBeEquivalentTo(packingFile);
			_files.GetInvoiceFile(newData.Id).Data.ShouldBeEquivalentTo(invoiceFile);
			_files.GetAWBFile(newData.Id).Data.ShouldBeEquivalentTo(awbFile);
			_files.GetDrawFile(newData.Id).Data.ShouldBeEquivalentTo(drawFile);
		}

		private AirWaybillData CreateAirWaybillData()
		{
			return _fixture
				.Build<AirWaybillData>()
				.With(x => x.StateId, TestConstants.DefaultStateId)
				.With(x => x.BrokerId, TestConstants.TestBrokerId)
				.Create();
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
				.With(x => x.CurrencyId, (int)CurrencyType.Dollar)
				.With(x => x.ForwarderId, TestConstants.TestForwarderId1)
				.Create();
		}

		private AirWaybillData CreateTestAirWaybill()
		{
			var data = CreateAirWaybillData();

			var id = _awbs.Add(data, _context.RandomBytes(), _context.RandomBytes(),
				_context.RandomBytes(), _context.RandomBytes(),
				_context.RandomBytes(), _context.RandomBytes());

			data.Id = id;

			return data;
		}
	}
}