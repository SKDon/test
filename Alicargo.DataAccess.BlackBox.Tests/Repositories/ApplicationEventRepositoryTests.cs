using System.Data.SqlClient;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Core.Services;
using Alicargo.DataAccess.BlackBox.Tests.Helpers;
using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories.Application;
using Alicargo.Jobs.ApplicationEvents.Entities;
using Alicargo.TestHelpers;
using Dapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories
{
	[TestClass]
	public class ApplicationEventRepositoryTests
	{
		private DbTestContext _context;
		private ApplicationEventRepository _events;
		private Serializer _serializer;
		private Fixture _fixture;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext();
			_serializer = new Serializer();
			_fixture = new Fixture();

			_events = new ApplicationEventRepository(new SqlProcedureExecutor(Settings.Default.MainConnectionString));
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_AddDublicate()
		{
			var data = _fixture.CreateMany<byte>().ToArray();
			_events.Add(TestConstants.TestApplicationId, ApplicationEventType.Created, data);
			_events.Add(TestConstants.TestApplicationId, ApplicationEventType.Created, data);

			using (var connection = new SqlConnection(Settings.Default.MainConnectionString))
			{
				var count = connection.Query<int>("select count(1) from [dbo].[ApplicationEvent] where [ApplicationId] = @AppId AND [EventTypeId] = @Type",
					new { AppId = TestConstants.TestApplicationId, Type = ApplicationEventType.Created }).First();

				count.ShouldBeEquivalentTo(2);
			}

			_events.Add(TestConstants.TestApplicationId, ApplicationEventType.CPFileUploaded, _serializer.Serialize(_fixture.Create<FileHolder>()));

			using (var connection = new SqlConnection(Settings.Default.MainConnectionString))
			{
				var count = connection.Query<int>("select count(1) from [dbo].[ApplicationEvent] where [ApplicationId] = @AppId",
					new { AppId = TestConstants.TestApplicationId, Type = ApplicationEventType.Created }).First();

				count.ShouldBeEquivalentTo(3);
			}
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_GetNext()
		{
			var eventData = _serializer.Serialize(_fixture.Create<ApplicationSetStateEventData>());

			_events.Add(TestConstants.TestApplicationId, ApplicationEventType.Created, eventData);

			_events.GetNext(ApplicationEventState.EmailPrepared, 0, 1).Should().BeNull();

			var data = _events.GetNext(ApplicationEventState.New, 0, 1);

			data.ApplicationId.ShouldBeEquivalentTo(TestConstants.TestApplicationId);
			data.EventType.ShouldBeEquivalentTo(ApplicationEventType.Created);
			data.Data.ShouldBeEquivalentTo(eventData);
			data.Id.Should().BeGreaterThan(0);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_SetState()
		{
			var eventData = _fixture.CreateMany<byte>().ToArray();

			_events.Add(TestConstants.TestApplicationId, ApplicationEventType.Created, eventData);

			var data = _events.GetNext(ApplicationEventState.New, 0, 1);

			_events.SetState(data.Id, ApplicationEventState.EmailPrepared);

			var next = _events.GetNext(ApplicationEventState.EmailPrepared, 0, 1);

			next.ShouldBeEquivalentTo(data);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Delete()
		{
			var eventData = _fixture.CreateMany<byte>().ToArray();

			_events.Add(TestConstants.TestApplicationId, ApplicationEventType.Created, eventData);

			var data = _events.GetNext(ApplicationEventState.New, 0, 1);

			_events.Delete(data.Id);

			data = _events.GetNext(ApplicationEventState.New, 0, 1);

			data.Should().BeNull();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_DeleteEmpty()
		{
			_events.Delete(-1);
		}
	}
}
