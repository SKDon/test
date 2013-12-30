using System.Data.SqlClient;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Core.Services;
using Alicargo.DataAccess.BlackBox.Tests.Helpers;
using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
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
		private Serializer _serializer;
		private Fixture _fixture;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext();
			_serializer = new Serializer();
			_fixture = new Fixture();

			_events = new EventRepository(new SqlProcedureExecutor(Settings.Default.MainConnectionString));
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
			var partitionId =_fixture.Create<int>();

			_events.Add(partitionId, EventType.ApplicationCreated, EventState.Emailing, data);
			_events.Add(partitionId, EventType.ApplicationCreated, EventState.Emailing, data);

			using (var connection = new SqlConnection(Settings.Default.MainConnectionString))
			{
				var count = connection.Query<int>("select count(1) from [dbo].[Event] where [PartitionId] = @partitionId AND [EventTypeId] = @Type",
					new { partitionId, Type = EventType.ApplicationCreated }).First();

				count.ShouldBeEquivalentTo(2);
			}

			_events.Add(partitionId, EventType.CPFileUploaded, EventState.Emailing, _serializer.Serialize(_fixture.Create<FileHolder>()));

			using (var connection = new SqlConnection(Settings.Default.MainConnectionString))
			{
				var count = connection.Query<int>("select count(1) from [dbo].[Event] where [PartitionId] = @partitionId",
					new { partitionId, Type = EventType.ApplicationCreated }).First();

				count.ShouldBeEquivalentTo(3);
			}
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_GetNext()
		{
			var eventData = _fixture.CreateMany<byte>().ToArray();
			var partitionId = _fixture.Create<int>();

			_events.Add(partitionId, EventType.ApplicationCreated, EventState.Emailing, eventData);

			_events.GetNext(EventState.StateHistorySaving, partitionId).Should().BeNull();

			var data = _events.GetNext(EventState.Emailing, partitionId);

			data.Type.ShouldBeEquivalentTo(EventType.ApplicationCreated);
			data.Data.ShouldBeEquivalentTo(eventData);
			data.Id.Should().BeGreaterThan(0);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_SetState()
		{
			var eventData = _fixture.CreateMany<byte>().ToArray();
			var partitionId =_fixture.Create<int>();

			_events.Add(partitionId, EventType.ApplicationCreated, EventState.Emailing, eventData);

			var data = _events.GetNext(EventState.Emailing, partitionId);

			_events.SetState(data.Id, EventState.StateHistorySaving);

			var next = _events.GetNext(EventState.StateHistorySaving, partitionId);

			next.ShouldBeEquivalentTo(data);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Delete()
		{
			var eventData = _fixture.CreateMany<byte>().ToArray();
			var partitionId =_fixture.Create<int>();

			_events.Add(partitionId, EventType.ApplicationCreated, EventState.Emailing, eventData);

			var data = _events.GetNext(EventState.Emailing, partitionId);

			_events.Delete(data.Id);

			data = _events.GetNext(EventState.Emailing, partitionId);

			data.Should().BeNull();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_DeleteEmpty()
		{
			_events.Delete(-1);
		}
	}
}