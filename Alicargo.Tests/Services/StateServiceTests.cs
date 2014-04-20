using System;
using System.Linq;
using Alicargo.Core.Contracts.Exceptions;
using Alicargo.Core.State;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Contracts.State;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

namespace Alicargo.Tests.Services
{
	[TestClass]
	public class StateServiceTests
	{
		private MockContainer _context;
		private StateFilter _stateFilter;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new MockContainer();
			_stateFilter = _context.Create<StateFilter>();
		}

		[TestMethod]
		public void Test_GetStateAvailabilityToSet()
		{
			var roles = Enum.GetValues(typeof(RoleType)).Cast<RoleType>().ToArray();

			foreach (var roleType in roles)
			{
				var type = roleType;
				_context.IdentityService.Setup(x => x.IsInRole(type)).Returns(false);
			}

			foreach (var roleType in roles)
			{
				var states = _context.Build<StateRole>().With(x => x.Role, roleType).CreateMany(6).ToArray();
				_context.StateConfig.Setup(x => x.AwbStates).Returns(states.Take(3).Select(x => x.StateId).ToArray());

				var type = roleType;
				_context.IdentityService.Setup(x => x.IsInRole(type)).Returns(true);
				_context.StateSettingsRepository.Setup(x => x.GetStateAvailabilities()).Returns(states);

				var stateAvailability = _stateFilter.GetStateAvailabilityToSet();
				if (roleType == RoleType.Client || roleType == RoleType.Sender)
				{
					states.Select(x => x.StateId).Skip(3).ShouldBeEquivalentTo(stateAvailability);
				}
				else
				{
					states.Select(x => x.StateId).ShouldBeEquivalentTo(stateAvailability);
				}

				_context.IdentityService.Verify(x => x.IsInRole(type));
				_context.StateSettingsRepository.Verify(x => x.GetStateAvailabilities());

				_context.IdentityService.Setup(x => x.IsInRole(type)).Returns(false);
				_context.StateSettingsRepository.Setup(x => x.GetStateAvailabilities()).Throws<InvalidOperationException>();
			}
		}

		[Ignore] // todo: 3. this test should work because a broker can't set a state
		[TestMethod, ExpectedException(typeof(InvalidLogicException))]
		public void Test_GetStateAvailabilityToSet_Broker()
		{
			var roles = Enum.GetValues(typeof(RoleType)).Cast<RoleType>().Except(new[] { RoleType.Broker }).ToArray();
			foreach (var roleType in roles)
			{
				var type = roleType;
				_context.IdentityService.Setup(x => x.IsInRole(type)).Returns(false);
			}

			_stateFilter.GetStateAvailabilityToSet();
		}

		[TestMethod]
		public void Test_GetStateVisibility()
		{
			var roles = Enum.GetValues(typeof(RoleType)).Cast<RoleType>().ToArray();

			foreach (var roleType in roles)
			{
				var type = roleType;
				_context.IdentityService.Setup(x => x.IsInRole(type)).Returns(false);
			}

			foreach (var roleType in roles)
			{
				var states = _context.Build<StateRole>().With(x => x.Role, roleType).CreateMany().ToArray();

				var type = roleType;
				_context.IdentityService.Setup(x => x.IsInRole(type)).Returns(true);
				_context.StateSettingsRepository.Setup(x => x.GetStateVisibilities()).Returns(states);

				var stateAvailability = _stateFilter.GetStateVisibility();
				states.Select(x=>x.StateId).ShouldBeEquivalentTo(stateAvailability);

				_context.IdentityService.Verify(x => x.IsInRole(type));
				_context.StateSettingsRepository.Verify(x => x.GetStateVisibilities());

				_context.IdentityService.Setup(x => x.IsInRole(type)).Returns(false);
				_context.StateSettingsRepository.Setup(x => x.GetStateVisibilities()).Throws<InvalidOperationException>();
			}
		}

		[Ignore] // 3. todo: this test should work because a broker can't view applications
		[TestMethod, ExpectedException(typeof(InvalidLogicException))]
		public void Test_GetStateVisibility_Broker()
		{
			var roles = Enum.GetValues(typeof(RoleType)).Cast<RoleType>().Except(new[] { RoleType.Broker }).ToArray();
			foreach (var roleType in roles)
			{
				var type = roleType;
				_context.IdentityService.Setup(x => x.IsInRole(type)).Returns(false);
			}

			_stateFilter.GetStateVisibility();
		}


		[TestMethod]
		public void Test_ApplyBusinessLogicToStates_Weight()
		{
			var applicationData = _context.Create<ApplicationEditData>();
			applicationData.Weight = null;
			applicationData.AirWaybillId = null;
			var stateAvailability = new[] { _context.Create<long>() };

			_context.StateConfig.Setup(x => x.CargoIsFlewStateId).Returns(It.IsAny<long>());
			_context.StateConfig.Setup(x => x.CargoAtCustomsStateId).Returns(It.IsAny<long>());
			_context.StateConfig.Setup(x => x.CargoInStockStateId).Returns(stateAvailability[0]);

			var stateModels = _stateFilter.FilterByBusinessLogic(applicationData, stateAvailability);

			stateModels.Should().BeEmpty();
		}

		[TestMethod]
		public void Test_ApplyBusinessLogicToStates_Count()
		{
			var applicationData = _context.Create<ApplicationEditData>();
			applicationData.Count = null;
			applicationData.AirWaybillId = null;
			var stateAvailability = _context.CreateMany<long>(1).ToArray();

			_context.StateConfig.Setup(x => x.CargoIsFlewStateId).Returns(It.IsAny<long>());
			_context.StateConfig.Setup(x => x.CargoAtCustomsStateId).Returns(It.IsAny<long>());
			_context.StateConfig.Setup(x => x.CargoInStockStateId).Returns(stateAvailability[0]);

			var stateModels = _stateFilter.FilterByBusinessLogic(applicationData, stateAvailability);

			stateModels.Should().BeEmpty();
		}

		[TestMethod]
		public void Test_ApplyBusinessLogicToStates_AirWaybillIdNull()
		{
			var applicationData = _context.Create<ApplicationEditData>();
			applicationData.AirWaybillId = null;
			var stateAvailability = _context.CreateMany<long>(2).ToArray();

			_context.StateConfig.Setup(x => x.CargoIsFlewStateId).Returns(stateAvailability[0]);
			_context.StateConfig.Setup(x => x.CargoAtCustomsStateId).Returns(stateAvailability[1]);

			var stateModels = _stateFilter.FilterByBusinessLogic(applicationData, stateAvailability);

			stateModels.Should().BeEmpty();
		}

		[TestMethod]
		public void Test_ApplyBusinessLogicToStates_AirWaybillIdNotNull()
		{
			var applicationData = _context.Create<ApplicationEditData>();
			var airWaybillData = _context.CreateMany<AirWaybillData>().ToArray();
			airWaybillData[0].GTD = null;
			var stateAvailability = _context.CreateMany<long>(1).ToArray();

			_context.AirWaybillRepository.Setup(x => x.Get(applicationData.AirWaybillId.Value)).Returns(airWaybillData);
			_context.StateConfig.Setup(x => x.CargoAtCustomsStateId).Returns(stateAvailability[0]);

			var stateModels = _stateFilter.FilterByBusinessLogic(applicationData, stateAvailability);

			stateModels.Should().BeEmpty();
		}
	}
}