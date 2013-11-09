//using System;
//using System.Data.SqlClient;
//using System.Linq;
//using Alicargo.Contracts.Enums;
//using Alicargo.Contracts.Exceptions;
//using Alicargo.DataAccess.BlackBox.Tests.Helpers;
//using Alicargo.DataAccess.BlackBox.Tests.Properties;
//using Alicargo.DataAccess.DbContext;
//using Alicargo.DataAccess.Repositories;
//using Alicargo.TestHelpers;
//using Dapper;
//using FluentAssertions;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Alicargo.DataAccess.BlackBox.Tests.Repositories
//{
//	[TestClass]
//	public class ApplicationEventRepositoryTests
//	{
//		private DbTestContext _context;
//		private ApplicationEventRepository _events;

//		[TestInitialize]
//		public void TestInitialize()
//		{
//			_context = new DbTestContext();
//			_events = new ApplicationEventRepository(new SqlProcedureExecutor(Settings.Default.MainConnectionString));
//		}

//		[TestCleanup]
//		public void TestCleanup()
//		{
//			_context.Cleanup();
//		}

//		[TestMethod, TestCategory("black-box")]
//		public void Test_AddDublicate()
//		{
//			_events.Add(TestConstants.TestApplicationId, ApplicationEventType.Created);
//			_events.Add(TestConstants.TestApplicationId, ApplicationEventType.Created);

//			using (var connection = new SqlConnection(Settings.Default.MainConnectionString))
//			{
//				var count = connection.Query<int>("select count(1) from [dbo].[ApplicationEvent] where [ApplicationId] = @AppId AND [EventType] = @Type",
//					new { AppId = TestConstants.TestApplicationId, Type = ApplicationEventType.Created }).First();

//				count.ShouldBeEquivalentTo(1);
//			}

//			_events.Add(TestConstants.TestApplicationId, ApplicationEventType.CPFileUploaded);

//			using (var connection = new SqlConnection(Settings.Default.MainConnectionString))
//			{
//				var count = connection.Query<int>("select count(1) from [dbo].[ApplicationEvent] where [ApplicationId] = @AppId",
//					new { AppId = TestConstants.TestApplicationId, Type = ApplicationEventType.Created }).First();

//				count.ShouldBeEquivalentTo(2);
//			}
//		}

//		[TestMethod, TestCategory("black-box")]
//		public void Test_GetNext()
//		{
//			_events.Add(TestConstants.TestApplicationId, ApplicationEventType.Created);

//			_events.GetNext(TODO, DateTimeOffset.UtcNow).Should().BeNull();

//			var data = _events.GetNext(TODO, DateTimeOffset.UtcNow.Subtract(TimeSpan.FromMinutes(1)));

//			data.ApplicationId.ShouldBeEquivalentTo(TestConstants.TestApplicationId);
//			data.EventType.ShouldBeEquivalentTo(ApplicationEventType.Created);
//		}

//		[TestMethod, TestCategory("black-box")]
//		public void Test_Touch()
//		{
//			_events.Add(TestConstants.TestApplicationId, ApplicationEventType.Created);

//			var data = _events.GetNext(TODO, DateTimeOffset.UtcNow.Subtract(TimeSpan.FromMinutes(1)));

//			var bytes = _events.Touch(data.Id, data.RowVersion);

//			data.RowVersion.SequenceEqual(bytes).Should().BeFalse();

//			_events.Touch(data.Id, bytes);
//		}

//		[TestMethod, TestCategory("black-box"), ExpectedException(typeof(EntityUpdateConflict))]
//		public void Test_TouchDouble()
//		{
//			_events.Add(TestConstants.TestApplicationId, ApplicationEventType.Created);

//			var data = _events.GetNext(TODO, DateTimeOffset.UtcNow.Subtract(TimeSpan.FromMinutes(1)));

//			_events.Touch(data.Id, data.RowVersion);
//			_events.Touch(data.Id, data.RowVersion);
//		}

//		[TestMethod, TestCategory("black-box")]
//		public void Test_Delete()
//		{
//			_events.Add(TestConstants.TestApplicationId, ApplicationEventType.Created);

//			var data = _events.GetNext(TODO, DateTimeOffset.UtcNow.Subtract(TimeSpan.FromMinutes(1)));

//			_events.Delete(data.Id, data.RowVersion);

//			data = _events.GetNext(TODO, DateTimeOffset.UtcNow.Subtract(TimeSpan.FromMinutes(1)));

//			data.Should().BeNull();
//		}

//		[TestMethod, TestCategory("black-box")]
//		public void Test_DeleteEmpty()
//		{
//			_events.Delete(-1, new byte[0]);
//		}
//	}
//}
