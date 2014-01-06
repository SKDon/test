using System.Linq;
using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.Repositories.User;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories.User
{
	[TestClass]
	public class Tests
	{
		private BrokerRepository _repository;
		private DbTestContext _context;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);

			_repository = new BrokerRepository(_context.UnitOfWork);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_BrokerRepository_Get()
		{
			var all = _repository.GetAll();

			var broker = _repository.Get(TestConstants.TestBrokerId);

			var data = all.First(x => x.Id == TestConstants.TestBrokerId);

			data.ShouldBeEquivalentTo(broker);

			var byUserId = _repository.GetByUserId(broker.UserId);

			data.ShouldBeEquivalentTo(byUserId);
		}
	}
}