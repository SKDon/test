using System.Linq;
using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.Contracts.Contracts.State;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories
{
	[TestClass]
	public class StateRepositoryTests
	{
		private const long DefaultStateId = 1;

		private DbTestContext _context;
		private Fixture _fixture;
		private IStateRepository _states;

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

			_states = new StateRepository(new SqlProcedureExecutor(Settings.Default.MainConnectionString));
		}

		[TestMethod]
		public void Test_StateRepository_Delete()
		{
			var data = _fixture.Create<StateEditData>();
			data.Language = TwoLetterISOLanguageName.English;

			var id = _states.Add(data);

			var actual = _states.Get(TwoLetterISOLanguageName.English, id).Single().Value;

			actual.ShouldBeEquivalentTo(data);

			_states.Delete(id);

			_states.Get(TwoLetterISOLanguageName.English, id).Should().BeEmpty();
		}

		[TestMethod]
		public void Test_StateRepository_Get()
		{
			var states = _states.Get(TwoLetterISOLanguageName.Italian, 1, 2, 3);

			states.Count.ShouldBeEquivalentTo(3);
		}

		[TestMethod]
		public void Test_StateRepository_GetAll()
		{
			var states = _states.Get(TwoLetterISOLanguageName.Italian);

			var all = _states.Get(TwoLetterISOLanguageName.English);

			Assert.AreEqual(all.Count, states.Count);
		}

		[TestMethod]
		public void Test_StateRepository_GetDefaultState()
		{
			var it = _states.Get(TwoLetterISOLanguageName.Italian, DefaultStateId).First().Value;
			var en = _states.Get(TwoLetterISOLanguageName.English, DefaultStateId).First().Value;
			var ru = _states.Get(TwoLetterISOLanguageName.Russian, DefaultStateId).First().Value;

			Assert.AreEqual("Nuovo", it.LocalizedName);
			Assert.AreEqual("New order", en.LocalizedName);
			Assert.AreEqual("Новая заявка", ru.LocalizedName);
			Assert.AreEqual(10, it.Position);
			Assert.AreEqual(10, en.Position);
			Assert.AreEqual(10, ru.Position);
			Assert.IsTrue(ru.Name == it.Name);
			Assert.IsTrue(ru.Name == en.Name);
		}

		[TestMethod]
		public void Test_StateRepository_Upate()
		{
			var data = _fixture.Create<StateEditData>();
			data.Language = TwoLetterISOLanguageName.English;

			var id = _states.Add(data);

			var actual = _states.Get(TwoLetterISOLanguageName.English, id).Single().Value;

			actual.ShouldBeEquivalentTo(data);

			var newData = _fixture.Create<StateEditData>();
			newData.Language = TwoLetterISOLanguageName.English;

			_states.Update(id, newData);

			actual = _states.Get(TwoLetterISOLanguageName.English, id).Single().Value;

			actual.ShouldBeEquivalentTo(newData);
		}

		[TestMethod]
		public void Test_StateRepository_UpdateOtheLang()
		{
			var id = _states.Add(_fixture.Build<StateEditData>().With(x => x.Language, TwoLetterISOLanguageName.English).Create());

			var itData = _fixture.Build<StateEditData>().With(x => x.Language, TwoLetterISOLanguageName.Italian).Create();

			_states.Update(id, itData);

			var itActual = _states.Get(TwoLetterISOLanguageName.Italian, id).Single().Value;

			itActual.ShouldBeEquivalentTo(itData);

			var enActual = _states.Get(TwoLetterISOLanguageName.English, id).Single().Value;

			enActual.ShouldBeEquivalentTo(itActual, options => options.Excluding(x => x.LocalizedName));
		}
	}
}