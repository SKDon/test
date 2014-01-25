using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories.User;
using Alicargo.TestHelpers;
using Alicargo.Utilities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories.User
{
	[TestClass]
	public class SenderRepositoryTests
	{
		private DbTestContext _context;
		private SenderRepository _repository;
		private Fixture _fixture;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);
			_fixture = new Fixture();

			_repository = new SenderRepository(new PasswordConverter(), new SqlProcedureExecutor(Settings.Default.MainConnectionString));
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod]
		public void Test_Add_Get()
		{
			var data = _fixture.Create<SenderData>();
			data.Language = TwoLetterISOLanguageName.English;

			var password = _fixture.Create<string>();

			var id = _repository.Add(data, password);

			var actual = _repository.Get(id);

			actual.ShouldBeEquivalentTo(data);
		}

		[TestMethod]
		public void Test_GetTraiffs()
		{
			var data1 = _fixture.Create<SenderData>();
			var id1 = _repository.Add(data1, _fixture.Create<string>());

			var data2 = _fixture.Create<SenderData>();
			var id2 = _repository.Add(data2, _fixture.Create<string>());

			var actual = _repository.GetTariffs(new[] { id1, id2 });

			actual[id1].ShouldBeEquivalentTo(data1.TariffOfTapePerBox);
			actual[id2].ShouldBeEquivalentTo(data2.TariffOfTapePerBox);
		}

		[TestMethod]
		public void Test_Update()
		{
			var data = _fixture.Create<SenderData>();
			data.Language = TwoLetterISOLanguageName.English;

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
