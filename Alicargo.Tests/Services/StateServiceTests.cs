using System;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Services;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Alicargo.Tests.Services
{
    [TestClass]
    public class StateServiceTests
    {
        private MockContainer _context;
        private StateService _stateService;

        [TestInitialize]
        public void TestInitialize()
        {
            _context = new MockContainer();
            _stateService = _context.Create<StateService>();
        }

        [TestMethod]
        public void Test_GetStateAvailabilityToSet()
        {
            var roles = Enum.GetValues(typeof (RoleType)).Cast<RoleType>().ToArray();
			var states = _context.CreateMany<StateRole>(6).ToArray();

            foreach (var roleType in roles)
            {
                var type = roleType;
                _context.IdentityService.Setup(x => x.IsInRole(type)).Returns(false);
            }

            foreach (var roleType in roles)
            {
                var type = roleType;
                _context.IdentityService.Setup(x => x.IsInRole(type)).Returns(true);
                _context.StateSettingsRepository.Setup(x => x.GetStateAvailabilities()).Returns(states);

                var stateAvailability = _stateService.GetStateAvailabilityToSet();
                if (roleType == RoleType.Client || roleType == RoleType.Sender)
                {
                    states.Skip(3).ShouldBeEquivalentTo(stateAvailability);
                }
                else
                {
                    states.ShouldBeEquivalentTo(stateAvailability);
                }

                _context.IdentityService.Verify(x => x.IsInRole(type));
				_context.StateSettingsRepository.Verify(x => x.GetStateAvailabilities());

                _context.IdentityService.Setup(x => x.IsInRole(type)).Returns(false);
				_context.StateSettingsRepository.Setup(x => x.GetStateAvailabilities()).Throws<InvalidOperationException>();
            }
        }

        [Ignore] // todo: 3. this test should work because a broker can't set a state
        [TestMethod, ExpectedException(typeof (InvalidLogicException))]
        public void Test_GetStateAvailabilityToSet_Broker()
        {
            var roles = Enum.GetValues(typeof (RoleType)).Cast<RoleType>().Except(new[] {RoleType.Broker}).ToArray();
            foreach (var roleType in roles)
            {
                var type = roleType;
                _context.IdentityService.Setup(x => x.IsInRole(type)).Returns(false);
            }

            _stateService.GetStateAvailabilityToSet();
        }

        [TestMethod]
        public void Test_GetStateVisibility()
        {
            var roles = Enum.GetValues(typeof (RoleType)).Cast<RoleType>().ToArray();
			var states = _context.CreateMany<StateRole>().ToArray();

            foreach (var roleType in roles)
            {
                var type = roleType;
                _context.IdentityService.Setup(x => x.IsInRole(type)).Returns(false);
            }

            foreach (var roleType in roles)
            {
                var type = roleType;
                _context.IdentityService.Setup(x => x.IsInRole(type)).Returns(true);
				_context.StateSettingsRepository.Setup(x => x.GetStateVisibilities()).Returns(states);

                var stateAvailability = _stateService.GetStateVisibility();
                states.ShouldBeEquivalentTo(stateAvailability);

                _context.IdentityService.Verify(x => x.IsInRole(type));
				_context.StateSettingsRepository.Verify(x => x.GetStateVisibilities());

                _context.IdentityService.Setup(x => x.IsInRole(type)).Returns(false);
				_context.StateSettingsRepository.Setup(x => x.GetStateVisibilities()).Throws<InvalidOperationException>();
            }
        }

        [Ignore] // 3. todo: this test should work because a broker can't view applications
        [TestMethod, ExpectedException(typeof (InvalidLogicException))]
        public void Test_GetStateVisibility_Broker()
        {
            var roles = Enum.GetValues(typeof (RoleType)).Cast<RoleType>().Except(new[] {RoleType.Broker}).ToArray();
            foreach (var roleType in roles)
            {
                var type = roleType;
                _context.IdentityService.Setup(x => x.IsInRole(type)).Returns(false);
            }

            _stateService.GetStateVisibility();
        }


        [TestMethod]
        public void Test_ApplyBusinessLogicToStates_Weight()
        {
            var applicationData = _context.Create<ApplicationData>();
            applicationData.Weight = null;
            applicationData.AirWaybillId = null;
            var stateAvailability = new[] {_context.Create<long>()};

            _context.StateConfig.Setup(x => x.CargoIsFlewStateId).Returns(It.IsAny<long>());
            _context.StateConfig.Setup(x => x.CargoAtCustomsStateId).Returns(It.IsAny<long>());
            _context.StateConfig.Setup(x => x.CargoInStockStateId).Returns(stateAvailability[0]);

            var stateModels = _stateService.FilterByBusinessLogic(applicationData, stateAvailability);

            stateModels.Should().BeEmpty();
        }

        [TestMethod]
        public void Test_ApplyBusinessLogicToStates_Count()
        {
            var applicationData = _context.Create<ApplicationData>();
            applicationData.Count = null;
            applicationData.AirWaybillId = null;
            var stateAvailability = _context.CreateMany<long>(1).ToArray();

            _context.StateConfig.Setup(x => x.CargoIsFlewStateId).Returns(It.IsAny<long>());
            _context.StateConfig.Setup(x => x.CargoAtCustomsStateId).Returns(It.IsAny<long>());
            _context.StateConfig.Setup(x => x.CargoInStockStateId).Returns(stateAvailability[0]);

            var stateModels = _stateService.FilterByBusinessLogic(applicationData, stateAvailability);

            stateModels.Should().BeEmpty();
        }

        [TestMethod]
        public void Test_ApplyBusinessLogicToStates_AirWaybillIdNull()
        {
            var applicationData = _context.Create<ApplicationData>();
            applicationData.AirWaybillId = null;
            var stateAvailability = _context.CreateMany<long>(2).ToArray();

            _context.StateConfig.Setup(x => x.CargoIsFlewStateId).Returns(stateAvailability[0]);
            _context.StateConfig.Setup(x => x.CargoAtCustomsStateId).Returns(stateAvailability[1]);

            var stateModels = _stateService.FilterByBusinessLogic(applicationData, stateAvailability);

            stateModels.Should().BeEmpty();
        }

        [TestMethod]
        public void Test_ApplyBusinessLogicToStates_AirWaybillIdNotNull()
        {
            var applicationData = _context.Create<ApplicationData>();
            var airWaybillData = _context.CreateMany<AirWaybillData>().ToArray();
            airWaybillData[0].GTD = null;
            var stateAvailability = _context.CreateMany<long>(1).ToArray();

            _context.AirWaybillRepository.Setup(x => x.Get(applicationData.AirWaybillId.Value)).Returns(airWaybillData);
            _context.StateConfig.Setup(x => x.CargoAtCustomsStateId).Returns(stateAvailability[0]);

            var stateModels = _stateService.FilterByBusinessLogic(applicationData, stateAvailability);

            stateModels.Should().BeEmpty();
        }
    }
}