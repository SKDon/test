using System.Linq;
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
	public class ForwarderRepositoryTests
	{
		private DbTestContext _context;
		private Fixture _fixture;
		private ForwarderRepository _repository;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);
			_fixture = new Fixture();

			_repository = new ForwarderRepository(new PasswordConverter(),
				new SqlProcedureExecutor(Settings.Default.MainConnectionString));
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod]
		
		public void Test_Add_Get()
		{
			var data = _fixture.Create<ForwarderData>();
			data.Language = TwoLetterISOLanguageName.English;
			data.CityId = TestConstants.TestCityId2;

			var password = _fixture.Create<string>();

			var id = _repository.Add(data.Name, data.Login, password, data.Email, data.Language, data.CityId);

			var actual = _repository.Get(id);

			actual.ShouldBeEquivalentTo(data, options => options.Excluding(x => x.Id).Excluding(x => x.UserId));
			actual.Id.ShouldBeEquivalentTo(id);
			actual.UserId.Should().NotBe(0);
		}

		[TestMethod]
		
		public void Test_Update()
		{
			var original = _repository.GetAll().First();
			var data = _fixture.Create<ForwarderData>();
			data.CityId = TestConstants.TestCityId2;

			_repository.Update(original.Id, data.Name, data.Login, data.Email, data.CityId);

			var actual = _repository.Get(original.Id);

			actual.ShouldBeEquivalentTo(data,
				options => options.Excluding(x => x.Id).Excluding(x => x.UserId).Excluding(x => x.Language));
			actual.Id.ShouldBeEquivalentTo(original.Id);
			actual.UserId.ShouldBeEquivalentTo(original.UserId);
			actual.Language.ShouldBeEquivalentTo(original.Language);
		}
	}
}