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
	public class CountryRepositoryTests
	{
		private CountryRepository _country;
		private DbTestContext _context;
		private Fixture _fixture;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);
			_fixture = new Fixture();

			_country = new CountryRepository(new SqlProcedureExecutor(Settings.Default.MainConnectionString));
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Add()
		{
			var position = _fixture.Create<int>();
			var russianName = _fixture.Create<string>();
			var englishName = _fixture.Create<string>();

			var id = _country.Add(englishName, russianName, position);

			var russian = _country.All(TwoLetterISOLanguageName.Russian).First(x => x.Id == id);

			var english = _country.All(TwoLetterISOLanguageName.English).First(x => x.Id == id);

			russian.ShouldBeEquivalentTo(english, options => options.Excluding(x => x.Name));
			russian.Name.ShouldBeEquivalentTo(russianName);
			english.Name.ShouldBeEquivalentTo(englishName);
		}
	}
}
