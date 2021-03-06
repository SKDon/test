﻿using System;
using System.Linq;
using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Contracts.Awb;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories.Application;
using Alicargo.DataAccess.Repositories.User;
using Alicargo.TestHelpers;
using Alicargo.Utilities;
using EmitMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories.Application
{
	[TestClass]
	public class AwbRepositoryTests
	{
		private ApplicationEditor _applicationEditor;
		private IAwbRepository _awbs;
		private DbTestContext _context;
		private ISqlProcedureExecutor _executor;
		private Fixture _fixture;
		private DateTimeOffset _now;

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
			_now = _fixture.Create<DateTimeOffset>();
			_executor = new SqlProcedureExecutor(Settings.Default.MainConnectionString);
			DateTimeProvider.SetProvider(new DateTimeProviderStub(_now));
			_awbs = new AwbRepository(_context.Connection);
			_applicationEditor = new ApplicationEditor(_context.Connection, _executor, TestConstants.DefaultStateId);
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

			var awbId1 = _awbs.Add(CreateAirWaybillData(), TestConstants.DefaultStateId, TestConstants.TestAdminUserId);
			var awbId2 = _awbs.Add(CreateAirWaybillData(), TestConstants.DefaultStateId, TestConstants.TestAdminUserId);

			var applications = _applicationEditor;
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

			var airWaybill = _awbs.Get(data.Id).Single();

			data.ShouldBeEquivalentTo(airWaybill);
		}

		[TestMethod]
		public void Test_AwbRepository_GetClientEmails()
		{
			var data1 = CreateApplicationData(TestConstants.TestClientId1);
			var data2 = CreateApplicationData(TestConstants.TestClientId2);

			var id = _awbs.Add(CreateAirWaybillData(), TestConstants.DefaultStateId, TestConstants.TestAdminUserId);

			var a1 = _applicationEditor.Add(data1);
			var a2 = _applicationEditor.Add(data2);

			_applicationEditor.SetAirWaybill(a1, id);
			_applicationEditor.SetAirWaybill(a2, id);

			var emails = _awbs.GetClientEmails(id).Select(x => x.Email).ToArray();

			var clientRepository = new ClientRepository(_executor);

			var client1 = clientRepository.Get(TestConstants.TestClientId1);
			var client2 = clientRepository.Get(TestConstants.TestClientId2);
			var clients = new[] { client1, client2 };

			emails.ShouldAllBeEquivalentTo(clients.Select(x => x.Emails).ToArray());
		}

		[TestMethod]
		public void Test_AwbRepository_SetState()
		{
			var data = CreateTestAirWaybill();

			_awbs.SetState(data.Id, TestConstants.CargoIsFlewStateId);

			var actual = _awbs.Get(data.Id).First();

			actual.StateId.ShouldBeEquivalentTo(TestConstants.CargoIsFlewStateId);
		}

		[TestMethod]
		public void Test_AwbRepository_Update()
		{
			var data = CreateTestAirWaybill();

			var newData = CreateAirWaybillData();
			newData.BrokerId = data.BrokerId;

			_awbs.Update(data.Id, newData);

			var actual = _awbs.Get(data.Id).First();
			actual.ShouldBeEquivalentTo(newData, options => options.ExcludingMissingProperties());
			actual.CreationTimestamp.ShouldBeEquivalentTo(_now);
			actual.StateChangeTimestamp.ShouldBeEquivalentTo(_now);
			actual.StateId = data.StateId;
		}

		[TestMethod]
		public void Test_SetActive()
		{
			var expected = CreateTestAirWaybill();
			var isActive = _fixture.Create<bool>();

			_awbs.SetActive(expected.Id, isActive);

			_awbs.Get(expected.Id).Single().IsActive.ShouldBeEquivalentTo(isActive);
		}

		private AirWaybillEditData CreateAirWaybillData()
		{
			return _fixture
				.Build<AirWaybillEditData>()
				.With(x => x.BrokerId, TestConstants.TestBrokerId)
				.With(x => x.SenderUserId, TestConstants.TestSenderUserId)
				.Create();
		}

		private ApplicationEditData CreateApplicationData(long clientId)
		{
			return _fixture
				.Build<ApplicationEditData>()
				.With(x => x.SenderId, TestConstants.TestSenderId)
				.With(x => x.ClientId, clientId)
				.With(x => x.AirWaybillId, null)
				.With(x => x.CountryId, TestConstants.TestCountryId)
				.With(x => x.TransitId, TestConstants.TestTransitId)
				.With(x => x.CurrencyId, CurrencyType.Dollar)
				.With(x => x.ForwarderId, TestConstants.TestForwarderId1)
				.Create();
		}

		private AirWaybillData CreateTestAirWaybill()
		{
			var data = CreateAirWaybillData();

			var id = _awbs.Add(data, TestConstants.DefaultStateId, TestConstants.TestAdminUserId);

			var result = ObjectMapperManager.DefaultInstance
				.GetMapper<AirWaybillEditData, AirWaybillData>()
				.Map(data);

			result.Id = id;
			result.CreationTimestamp = _now;
			result.StateChangeTimestamp = _now;
			result.StateId = TestConstants.DefaultStateId;
			result.IsActive = true;
			result.CreatorUserId = TestConstants.TestAdminUserId;

			return result;
		}
	}
}