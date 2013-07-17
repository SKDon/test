using System;
using System.Linq;
using Alicargo.Core.Enums;
using Alicargo.Core.Exceptions;
using Alicargo.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Alicargo.Tests.Services
{
	[TestClass]
	public class StateServiceTests
	{
		private TestContext _context;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new TestContext();
		}

		[TestMethod]
		public void Test_HasPermissionToState_True()
		{
			const int stateId = 1;

			_context.StateRepository.Setup(x => x.GetAvailableRoles(stateId)).Returns(new[] { RoleType.Brocker, RoleType.Client });
			_context.IdentityService.Setup(x => x.IsInRole(RoleType.Brocker)).Returns(true);
			_context.IdentityService.Setup(x => x.IsInRole(RoleType.Client)).Returns(false);

			var stateService = _context.Create<StateService>();

			Assert.IsTrue(stateService.HasPermissionToSetState(stateId));

			_context.IdentityService.Verify(x => x.IsInRole(RoleType.Brocker), Times.Once());
			_context.StateRepository.Verify(x => x.GetAvailableRoles(stateId), Times.Once());
		}

		[TestMethod]
		public void Test_HasPermissionToState_False()
		{
			const int stateId = 1;

			_context.StateRepository.Setup(x => x.GetAvailableRoles(stateId)).Returns(new[] { RoleType.Brocker, RoleType.Client });
			_context.IdentityService.Setup(x => x.IsInRole(RoleType.Brocker)).Returns(false);
			_context.IdentityService.Setup(x => x.IsInRole(RoleType.Client)).Returns(false);

			var stateService = _context.Create<StateService>();

			Assert.IsFalse(stateService.HasPermissionToSetState(stateId));

			_context.IdentityService.Verify(x => x.IsInRole(RoleType.Brocker), Times.Once());
			_context.IdentityService.Verify(x => x.IsInRole(RoleType.Client), Times.Once());
			_context.StateRepository.Verify(x => x.GetAvailableRoles(stateId), Times.Once());
		}

		[TestMethod]
		public void Test_GetAvailableStates()
		{
			var roles = Enum.GetValues(typeof(RoleType)).Cast<RoleType>().Except(new[] { RoleType.Brocker }).ToArray();
			var states = _context.CreateMany<long>().ToArray();

			foreach (var roleType in roles)
			{
				var type = roleType;
				_context.IdentityService.Setup(x => x.IsInRole(type)).Returns(false);
			}

			foreach (var roleType in roles)
			{
				var type = roleType;
				_context.IdentityService.Setup(x => x.IsInRole(type)).Returns(true);
				_context.StateRepository.Setup(x => x.GetAvailableStates(type)).Returns(states);

				var stateService = _context.Create<StateService>();
				var availableStates = stateService.GetAvailableStates();

				_context.AreEquals(states, availableStates);
				_context.IdentityService.Verify(x => x.IsInRole(type));
				_context.StateRepository.Verify(x => x.GetAvailableStates(type));

				_context.IdentityService.Setup(x => x.IsInRole(type)).Returns(false);
				_context.StateRepository.Setup(x => x.GetAvailableStates(type)).Throws<InvalidOperationException>();
			}
		}

		[TestMethod, ExpectedException(typeof(InvalidLogicException))]
		public void Test_GetAvailableStates_Brocker()
		{
			var roles = Enum.GetValues(typeof(RoleType)).Cast<RoleType>().Except(new[] { RoleType.Brocker }).ToArray();
			foreach (var roleType in roles)
			{
				var type = roleType;
				_context.IdentityService.Setup(x => x.IsInRole(type)).Returns(false);
			}

			var stateService = _context.Create<StateService>();
			stateService.GetAvailableStates();
		}

		[TestMethod]
		public void Test_GetVisibleStates()
		{
			var roles = Enum.GetValues(typeof(RoleType)).Cast<RoleType>().Except(new[] { RoleType.Brocker }).ToArray();
			var states = _context.CreateMany<long>().ToArray();

			foreach (var roleType in roles)
			{
				var type = roleType;
				_context.IdentityService.Setup(x => x.IsInRole(type)).Returns(false);
			}

			foreach (var roleType in roles)
			{
				var type = roleType;
				_context.IdentityService.Setup(x => x.IsInRole(type)).Returns(true);
				_context.StateRepository.Setup(x => x.GetVisibleStates(type)).Returns(states);

				var stateService = _context.Create<StateService>();
				var availableStates = stateService.GetVisibleStates();

				_context.AreEquals(states, availableStates);
				_context.IdentityService.Verify(x => x.IsInRole(type));
				_context.StateRepository.Verify(x => x.GetVisibleStates(type));

				_context.IdentityService.Setup(x => x.IsInRole(type)).Returns(false);
				_context.StateRepository.Setup(x => x.GetVisibleStates(type)).Throws<InvalidOperationException>();
			}
		}

		[TestMethod, ExpectedException(typeof(InvalidLogicException))]
		public void Test_GetVisibleStates_Brocker()
		{
			var roles = Enum.GetValues(typeof(RoleType)).Cast<RoleType>().Except(new[] { RoleType.Brocker }).ToArray();
			foreach (var roleType in roles)
			{
				var type = roleType;
				_context.IdentityService.Setup(x => x.IsInRole(type)).Returns(false);
			}

			var stateService = _context.Create<StateService>();
			stateService.GetVisibleStates();
		}
	}
}
