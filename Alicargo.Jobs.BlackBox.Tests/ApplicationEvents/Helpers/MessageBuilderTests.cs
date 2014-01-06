using Alicargo.App_Start.Jobs;
using Alicargo.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.Jobs.BlackBox.Tests.ApplicationEvents.Helpers
{
	[TestClass]
	public class MessageBuilderTests
	{
		private DbTestContext _context;
		private Fixture _fixture;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);
			_fixture = new Fixture();
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod]
		[TestCategory("black-box")]
		public void Test_Get()
		{
			//CompositionJobsHelper.GetMessageFactory()
		}
	}
}
