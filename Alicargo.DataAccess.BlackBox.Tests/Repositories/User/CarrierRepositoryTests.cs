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
	public class CarrierRepositoryTests
	{
		private DbTestContext _context;
		private Fixture _fixture;
		private CarrierRepository _repository;

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

			_repository = new CarrierRepository(new PasswordConverter(),
				new SqlProcedureExecutor(Settings.Default.MainConnectionString));
		}

		[TestMethod]
		public void Test_Add_Get()
		{
			var data = _fixture.Create<CarrierData>();
			data.Language = TwoLetterISOLanguageName.English;

			var password = _fixture.Create<string>();

			var id = _repository.Add(data.Name, data.Login, password, data.Email, data.Language);

			var actual = _repository.Get(id);

			actual.ShouldBeEquivalentTo(data, options => options.Excluding(x => x.Id).Excluding(x => x.UserId));
			actual.Id.ShouldBeEquivalentTo(id);
			actual.UserId.Should().NotBe(0);
		}

		[TestMethod]
		public void Test_Get_And_Set_Countries()
		{
			var cities = new long[] { 1, 2, 3, 4 };

			_repository.SetCities(TestConstants.TestCarrierId1, cities);

			var actual = _repository.GetCities(TestConstants.TestCarrierId1);

			actual.ShouldAllBeEquivalentTo(cities);
		}

		[TestMethod]
		public void Test_Set_Empty_Countries()
		{
			var cities = new long[] { };

			_repository.SetCities(TestConstants.TestCarrierId1, cities);

			var actual = _repository.GetCities(TestConstants.TestCarrierId1);

			actual.Should().HaveCount(0);
		}

		[TestMethod]
		public void Test_Update()
		{
			var original = _repository.GetAll().First();
			var data = _fixture.Create<CarrierData>();

			_repository.Update(original.Id, data.Name, data.Login, data.Email);

			var actual = _repository.Get(original.Id);

			actual.ShouldBeEquivalentTo(data,
				options => options.Excluding(x => x.Id).Excluding(x => x.UserId).Excluding(x => x.Language));
			actual.Id.ShouldBeEquivalentTo(original.Id);
			actual.UserId.ShouldBeEquivalentTo(original.UserId);
			actual.Language.ShouldBeEquivalentTo(original.Language);
		}
	}
}
