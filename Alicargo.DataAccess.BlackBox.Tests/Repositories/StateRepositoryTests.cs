using System.Linq;
using Alicargo.Contracts.Contracts;
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
		public void Test_StateRepository_Crud()
		{
			//_states.Add("")
		}
	}
}