using System.Linq;
using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories
{
	[TestClass]
	public class EventEmailRecipientTests
	{
		private DbTestContext _context;
		private Fixture _fixture;
		private EventEmailRecipient _recipients;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);
			_fixture = new Fixture();

			var executor = new SqlProcedureExecutor(Settings.Default.MainConnectionString);
			_recipients = new EventEmailRecipient(executor);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod]
		public void Test_Set_SetForEvent()
		{
			const EventType eventType = EventType.ApplicationCreated;

			var recipients = _fixture.CreateMany<RoleType>().ToArray();

			_recipients.Set(eventType, recipients);

			_recipients.GetRecipientRoles(eventType).ShouldAllBeEquivalentTo(recipients);
		}
	}
}