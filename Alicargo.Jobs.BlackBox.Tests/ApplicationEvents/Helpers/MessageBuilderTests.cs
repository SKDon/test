using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Calculation;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Jobs.BlackBox.Tests.Properties;
using Alicargo.Jobs.Helpers.Abstract;
using Alicargo.TestHelpers;
using Alicargo.Utilities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.Jobs.BlackBox.Tests.ApplicationEvents.Helpers
{
	[TestClass]
	public class MessageBuilderTests
	{
		private IMessageBuilder _builder;
		private DbTestContext _context;
		private Fixture _fixture;
		private Serializer _serializer;

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
			_serializer = new Serializer();
			_builder = CompositionJobsHelper.GetApplicationMessageBuilder(_context.Connection,
				Settings.Default.MainConnectionString,
				Settings.Default.FilesConnectionString);
		}

		[TestMethod]
		public void Test_Get()
		{
			var calculation = _fixture.Create<CalculationData>();
			calculation.ClientId = TestConstants.TestClientId1;
			var bytes = _serializer.Serialize(new EventDataForEntity
			{
				Data = _serializer.Serialize(calculation),
				EntityId = TestConstants.TestApplicationId
			});

			var messages = _builder.Get(EventType.Calculate, new EventData { Data = bytes, State = EventState.Emailing });

			messages.Should().HaveCount(4);
		}
	}
}