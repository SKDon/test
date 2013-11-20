using System.Linq;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.BlackBox.Tests.Helpers;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories
{
	[TestClass]
	public class StateRepositoryTests
	{
		private const long DefaultStateId = 1;

		private IStateRepository _states;
		private DbTestContext _context;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext();

			_states = new StateRepository(new SqlProcedureExecutor(_context.Connection.ConnectionString));
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_StateRepository_GetAll()
		{
			var states = _states.Get();

			var all = _states.All();

			Assert.AreNotEqual(0, all.Length);

			Assert.AreEqual(all.Length, states.Count);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_StateRepository_Get()
		{
			var states = _states.Get(1, 2, 3);

			states.Count.ShouldBeEquivalentTo(3);

			foreach (var item in states)
			{
				item.Value.Localization.Should().Contain(x => x.Key == TwoLetterISOLanguageName.English);
				item.Value.Localization.Should().Contain(x => x.Key == TwoLetterISOLanguageName.Italian);
				item.Value.Localization.Should().Contain(x => x.Key == TwoLetterISOLanguageName.Russian);
				item.Value.Localization.Count.ShouldBeEquivalentTo(3);
			}
		}


		[TestMethod, TestCategory("black-box")]
		public void Test_StateRepository_GetDefaultState()
		{
			var state = _states.Get(DefaultStateId).First().Value;

			Assert.AreEqual("Nuovo", state.Localization[TwoLetterISOLanguageName.Italian]);
			Assert.AreEqual("New order", state.Localization[TwoLetterISOLanguageName.English]);
			Assert.AreEqual("Новая заявка", state.Localization[TwoLetterISOLanguageName.Russian]);
			Assert.AreEqual(10, state.Position);
		}
	}
}