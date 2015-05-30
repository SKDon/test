using System.Data.SqlClient;
using System.Linq;
using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using Alicargo.TestHelpers;
using Alicargo.Utilities;
using Dapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories
{
	[TestClass]
	public class EventRepositoryTests
	{
		private DbTestContext _context;
		private EventRepository _events;
		private Fixture _fixture;
		private Serializer _serializer;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);
			_serializer = new Serializer();
			_fixture = new Fixture();

			_events = new EventRepository(new SqlProcedureExecutor(Settings.Default.MainConnectionString));
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod]
		public void Test_AddDublicate()
		{
			var data = _fixture.CreateMany<byte>().ToArray();
			var partitionId = _fixture.Create<int>();

			_events.Add(partitionId, TestConstants.TestAdminUserId, EventType.ApplicationCreated, EventState.Emailing, data);
			_events.Add(partitionId, TestConstants.TestAdminUserId, EventType.ApplicationCreated, EventState.Emailing, data);

			using(var connection = new SqlConnection(Settings.Default.MainConnectionString))
			{
				var count =
					connection.Query<int>(
						"select count(1) from [dbo].[Event] where [PartitionId] = @partitionId AND [EventTypeId] = @Type",
						new {partitionId, Type = EventType.ApplicationCreated}).First();

				count.ShouldBeEquivalentTo(2);
			}

			_events.Add(
				partitionId,
				TestConstants.TestAdminUserId,
				EventType.CPFileUploaded,
				EventState.Emailing,
				_serializer.Serialize(_fixture.Create<FileHolder>()));

			using(var connection = new SqlConnection(Settings.Default.MainConnectionString))
			{
				var count = connection.Query<int>(
					"select count(1) from [dbo].[Event] where [PartitionId] = @partitionId",
					new {partitionId, Type = EventType.ApplicationCreated}).First();

				count.ShouldBeEquivalentTo(3);
			}
		}

		[TestMethod]
		public void Test_GetNext()
		{
			var eventData = _fixture.CreateMany<byte>().ToArray();
			var partitionId = _fixture.Create<int>();

			_events.Add(partitionId, TestConstants.TestAdminUserId, EventType.ApplicationCreated, EventState.Emailing, eventData);

			_events.GetNext(EventType.BalanceDecreased, partitionId).Should().BeNull();

			var data = _events.GetNext(EventType.ApplicationCreated, partitionId);

			data.State.ShouldBeEquivalentTo(EventState.Emailing);
			data.Data.ShouldBeEquivalentTo(eventData);
			data.Id.Should().BeGreaterThan(0);
			data.UserId.ShouldBeEquivalentTo(TestConstants.TestAdminUserId);
		}

		[TestMethod]
		public void Test_GetNextFaulted()
		{
			var eventType = _fixture.Create<EventType>();
			var partitionId = _fixture.Create<int>();

			_events.Add(
				partitionId,
				TestConstants.TestAdminUserId,
				eventType,
				EventState.Failed,
				_fixture.CreateMany<byte>().ToArray());

			_events.GetNext(eventType, partitionId).Should().BeNull();
		}

		[TestMethod]
		public void Test_SetState()
		{
			var eventData = _fixture.CreateMany<byte>().ToArray();
			var partitionId = _fixture.Create<int>();
			_events.Add(partitionId, TestConstants.TestAdminUserId, EventType.ApplicationCreated, EventState.Emailing, eventData);
			var data = _events.GetNext(EventType.ApplicationCreated, partitionId);

			_events.SetState(data.Id, EventState.StateHistorySaving);

			var next = _events.GetNext(EventType.ApplicationCreated, partitionId);

			next.State.ShouldBeEquivalentTo(EventState.StateHistorySaving);
		}

		[TestMethod]
		public void Test_Delete()
		{
			var eventData = _fixture.CreateMany<byte>().ToArray();
			var partitionId = _fixture.Create<int>();
			var type = _fixture.Create<EventType>();
			_events.Add(partitionId, TestConstants.TestAdminUserId, type, EventState.Emailing, eventData);
			var data = _events.GetNext(type, partitionId);

			_events.Delete(data.Id);

			data = _events.GetNext(type, partitionId);
			data.Should().BeNull();
		}

		[TestMethod]
		public void Test_DeleteEmpty()
		{
			_events.Delete(-1);
		}
	}
}