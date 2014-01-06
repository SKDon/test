using System.Linq;
using Alicargo.Contracts.Contracts.State;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
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
	public class StateRepositoryTests
	{
		private const long DefaultStateId = 1;

		private IStateRepository _states;
		private DbTestContext _context;
		private Fixture _fixture;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);
			_fixture = new Fixture();

			_states = new StateRepository(new SqlProcedureExecutor(Settings.Default.MainConnectionString));
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_StateRepository_GetAll()
		{
			var states = _states.Get(TwoLetterISOLanguageName.Italian);

			var all = _states.All();

			Assert.AreNotEqual(0, all.Length);

			Assert.AreEqual(all.Length, states.Count);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_StateRepository_Get()
		{
			var states = _states.Get(TwoLetterISOLanguageName.Italian, 1, 2, 3);

			states.Count.ShouldBeEquivalentTo(3);
		}

		[TestMethod, TestCategory("black-box")]
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
		public void Test_StateRepository_UpdateOtheLang()
		{
			var id = _states.Add(TwoLetterISOLanguageName.English, _fixture.Create<StateData>());

			var itData = _fixture.Create<StateData>();

			_states.Update(id, TwoLetterISOLanguageName.Italian, itData);

			var itActual = _states.Get(TwoLetterISOLanguageName.Italian, id).Single().Value;

			itActual.ShouldBeEquivalentTo(itData);

			var enActual = _states.Get(TwoLetterISOLanguageName.English, id).Single().Value;

			enActual.ShouldBeEquivalentTo(itActual, options => options.Excluding(x => x.LocalizedName));
		}

		[TestMethod]
		public void Test_StateRepository_Upate()
		{
			var data = _fixture.Create<StateData>();

			var id = _states.Add(TwoLetterISOLanguageName.English, data);

			var actual = _states.Get(TwoLetterISOLanguageName.English, id).Single().Value;

			actual.ShouldBeEquivalentTo(data);

			var newData = _fixture.Create<StateData>();

			_states.Update(id, TwoLetterISOLanguageName.English, newData);

			actual = _states.Get(TwoLetterISOLanguageName.English, id).Single().Value;

			actual.ShouldBeEquivalentTo(newData);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_StateRepository_Delete()
		{
			var data = _fixture.Create<StateData>();

			var id = _states.Add(TwoLetterISOLanguageName.English, data);

			var actual = _states.Get(TwoLetterISOLanguageName.English, id).Single().Value;

			actual.ShouldBeEquivalentTo(data);

			_states.Delete(id);

			_states.Get(TwoLetterISOLanguageName.English, id).Should().BeEmpty();
		}
	}
}