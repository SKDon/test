using Alicargo.Contracts.Contracts;
using Alicargo.Core.Services;
using Alicargo.DataAccess.BlackBox.Tests.Helpers;
using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories
{
	[TestClass]
	public class SenderRepositoryTests
	{
		private DbTestContext _context;
		private SenderRepository _repository;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext();

			_repository = new SenderRepository(_context.UnitOfWork, new PasswordConverter(), new SqlProcedureExecutor(Settings.Default.MainConnectionString));
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Add_Get()
		{
			var data = _context.Fixture.Create<SenderData>();

			var password = _context.Fixture.Create<string>();

			var id = _repository.Add(data, password);

			var actual = _repository.Get(id);

			actual.ShouldBeEquivalentTo(data);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_GetTraiffs()
		{
			var data1 = _context.Fixture.Create<SenderData>();
			var id1 = _repository.Add(data1, _context.Fixture.Create<string>());

			var data2 = _context.Fixture.Create<SenderData>();
			var id2 = _repository.Add(data2, _context.Fixture.Create<string>());

			var actual = _repository.GetTariffs(new[] { id1, id2 });

			actual[id1].ShouldBeEquivalentTo(data1.TariffOfTapePerBox);
			actual[id2].ShouldBeEquivalentTo(data2.TariffOfTapePerBox);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Update()
		{
			var data = _context.Fixture.Create<SenderData>();

			_repository.Update(TestConstants.TestSenderId, data);

			var actual = _repository.Get(TestConstants.TestSenderId);

			actual.ShouldBeEquivalentTo(data);
		}

		[TestMethod]
		public void Test_GetByUserId()
		{
			var id = _repository.GetByUserId(-1);

			Assert.IsNull(id);
		}
	}
}
