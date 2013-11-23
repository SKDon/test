using System.Linq;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.BlackBox.Tests.Helpers;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories
{
	[TestClass]
	public class StateSettingsRepositoryTests
	{
		private const long DefaultStateId = 1;
		private DbTestContext _context;

		private StateSettingsRepository _settings;
		private IStateRepository _states;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext();

			var executor = new SqlProcedureExecutor(_context.Connection.ConnectionString);
			_settings = new StateSettingsRepository(executor);
			_states = new StateRepository(executor);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_StateRepository_GetStateAvailability()
		{
			var availabilities = _settings.GetStateAvailabilities().Where(x => x.Role == RoleType.Admin).ToArray();

			var states = _states.Get(TwoLetterISOLanguageName.Italian).ToArray();

			Assert.AreEqual(states.Length, availabilities.Length);

			foreach (var state in states)
			{
				Assert.IsTrue(availabilities.Any(x => x.StateId == state.Key));
			}
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_StateRepository_GetStateVisibility()
		{
			var visibilities = _settings.GetStateVisibilities().Where(x => x.Role == RoleType.Admin).ToArray();

			var states = _states.Get(TwoLetterISOLanguageName.Italian);

			Assert.AreEqual(states.Count, visibilities.Length);

			foreach (var state in states)
			{
				Assert.IsTrue(visibilities.Any(x => x.StateId == state.Key));
			}
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_StateRepository_GetAvailableRoles()
		{
			var roles = _settings.GetStateAvailabilities()
				.Where(x => x.StateId == DefaultStateId)
				.Select(x => x.Role)
				.ToArray();

			Assert.AreEqual(3, roles.Length);
			Assert.IsTrue(roles.Contains(RoleType.Admin));
			Assert.IsTrue(roles.Contains(RoleType.Sender));
			Assert.IsTrue(roles.Contains(RoleType.Client));
		}
	}
}