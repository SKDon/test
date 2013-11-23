using System.Linq;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.BlackBox.Tests.Helpers;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories
{
	[TestClass]
	public class StateSettingsRepositoryTests
	{
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
		public void Test_StateRepository_SetStateAvailability()
		{
			const long stateId = 7;

			_settings.GetStateAvailabilities().Count(x => x.StateId == stateId).ShouldBeEquivalentTo(3);

			_settings.SetStateAvailabilities(stateId, new[] { RoleType.Broker, RoleType.Forwarder });

			var array = _settings.GetStateAvailabilities().Where(x => x.StateId == stateId).ToArray();

			array.Length.ShouldBeEquivalentTo(2);

			array[0].StateId.ShouldBeEquivalentTo(stateId);
			array.Should().Contain(x => x.Role == RoleType.Broker);
			array.Should().Contain(x => x.Role == RoleType.Forwarder);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_StateRepository_SetStateAvailability_Add()
		{
			const long stateId = 7;

			_settings.GetStateAvailabilities().Count(x => x.StateId == stateId).ShouldBeEquivalentTo(3);

			_settings.SetStateAvailabilities(stateId, new[] { RoleType.Broker, RoleType.Forwarder, RoleType.Admin, RoleType.Sender });

			var array = _settings.GetStateAvailabilities().Where(x => x.StateId == stateId).ToArray();

			array.Length.ShouldBeEquivalentTo(4);

			array[0].StateId.ShouldBeEquivalentTo(stateId);
			array.Should().Contain(x => x.Role == RoleType.Broker);
			array.Should().Contain(x => x.Role == RoleType.Forwarder);
			array.Should().Contain(x => x.Role == RoleType.Admin);
			array.Should().Contain(x => x.Role == RoleType.Sender);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_StateRepository_SetStateAvailability_None()
		{
			const long stateId = 7;

			_settings.GetStateAvailabilities().Count(x => x.StateId == stateId).ShouldBeEquivalentTo(3);

			_settings.SetStateAvailabilities(stateId, new RoleType[0]);

			var array = _settings.GetStateAvailabilities().Where(x => x.StateId == stateId).ToArray();

			array.Length.ShouldBeEquivalentTo(0);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_StateRepository_SetStateAvailability_One()
		{
			_settings.GetStateAvailabilities().Count(x => x.StateId == TestConstants.DefaultStateId).ShouldBeEquivalentTo(3);

			_settings.SetStateAvailabilities(TestConstants.DefaultStateId, new[] { RoleType.Broker });

			var array = _settings.GetStateAvailabilities().Where(x => x.StateId == TestConstants.DefaultStateId).ToArray();

			array.Length.ShouldBeEquivalentTo(1);

			array[0].StateId.ShouldBeEquivalentTo(TestConstants.DefaultStateId);
			array.Should().Contain(x => x.Role == RoleType.Broker);
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
				.Where(x => x.StateId == TestConstants.DefaultStateId)
				.Select(x => x.Role)
				.ToArray();

			Assert.AreEqual(3, roles.Length);
			Assert.IsTrue(roles.Contains(RoleType.Admin));
			Assert.IsTrue(roles.Contains(RoleType.Sender));
			Assert.IsTrue(roles.Contains(RoleType.Client));
		}
	}
}