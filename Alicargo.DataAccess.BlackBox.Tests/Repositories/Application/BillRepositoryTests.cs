using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories.Application;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories.Application
{
	[TestClass]
	public class BillRepositoryTests
	{
		private DbTestContext _context;
		private Fixture _fixture;
		private BillRepository _repository;

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

			_repository = new BillRepository(new SqlProcedureExecutor(Settings.Default.MainConnectionString));
		}

		[TestMethod]
		public void Test_Get()
		{
			var data = _fixture.Create<BillData>();

			_repository.AddOrReplace(TestConstants.TestApplicationId, data);

			var actual = _repository.Get(TestConstants.TestApplicationId);

			actual.ShouldBeEquivalentTo(data);
		}

		[TestMethod]
		public void Test_Update()
		{
			var data = _fixture.Create<BillData>();

			_repository.AddOrReplace(TestConstants.TestApplicationId, _fixture.Create<BillData>());
			_repository.AddOrReplace(TestConstants.TestApplicationId, data);

			var actual = _repository.Get(TestConstants.TestApplicationId);

			actual.ShouldBeEquivalentTo(data);
		}
	}
}