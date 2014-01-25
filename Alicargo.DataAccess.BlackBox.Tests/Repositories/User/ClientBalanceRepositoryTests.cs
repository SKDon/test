using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories.User;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories.User
{
	[TestClass]
	public class ClientBalanceRepositoryTests
	{
		private ClientBalanceRepository _repository;
		private DbTestContext _context;
		private Fixture _fixture;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);
			_fixture = new Fixture();

			_repository = new ClientBalanceRepository(new SqlProcedureExecutor(Settings.Default.MainConnectionString));
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod]
		
		public void Test_SumBalance()
		{
			var first = _repository.SumBalance();
			var balance = _fixture.Create<decimal>();

			_repository.SetBalance(TestConstants.TestClientId1, balance);

			_repository.SumBalance().ShouldBeEquivalentTo(first + balance);
		}
	}
}
