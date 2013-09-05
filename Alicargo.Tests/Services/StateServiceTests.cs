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
        public void Test_HasPermissionToState_True()
        {
            const int stateId = 1;

            _context.StateRepository.Setup(x => x.GetAvailableRoles(stateId))
                    .Returns(new[] {RoleType.Broker, RoleType.Client});
            _context.IdentityService.Setup(x => x.IsInRole(RoleType.Broker)).Returns(true);
            _context.IdentityService.Setup(x => x.IsInRole(RoleType.Client)).Returns(false);

            Assert.IsTrue(_stateService.HasPermissionToSetState(stateId));

            _context.IdentityService.Verify(x => x.IsInRole(RoleType.Broker), Times.Once());
            _context.StateRepository.Verify(x => x.GetAvailableRoles(stateId), Times.Once());
        }

        [TestMethod]
        public void Test_HasPermissionToState_False()
        {
            const int stateId = 1;

            _context.StateRepository.Setup(x => x.GetAvailableRoles(stateId))
                    .Returns(new[] {RoleType.Broker, RoleType.Client});
            _context.IdentityService.Setup(x => x.IsInRole(RoleType.Broker)).Returns(false);
            _context.IdentityService.Setup(x => x.IsInRole(RoleType.Client)).Returns(false);

            Assert.IsFalse(_stateService.HasPermissionToSetState(stateId));

            _context.IdentityService.Verify(x => x.IsInRole(RoleType.Broker), Times.Once());
            _context.IdentityService.Verify(x => x.IsInRole(RoleType.Client), Times.Once());
            _context.StateRepository.Verify(x => x.GetAvailableRoles(stateId), Times.Once());
        }

        [TestMethod]
        public void Test_GetAvailableStatesToSet()
        {
            var roles = Enum.GetValues(typeof (RoleType)).Cast<RoleType>().ToArray();
            var states = _context.CreateMany<long>(6).ToArray();
            _context.StateConfig.Setup(x => x.AwbStates).Returns(states.Take(3).ToArray());

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

                var availableStates = _stateService.GetAvailableStatesToSet();
                if (roleType == RoleType.Client || roleType == RoleType.Sender)
                {
                    states.Skip(3).ShouldBeEquivalentTo(availableStates);
                }
                else
                {
                    states.ShouldBeEquivalentTo(availableStates);
                }

                _context.IdentityService.Verify(x => x.IsInRole(type));
                _context.StateRepository.Verify(x => x.GetAvailableStates(type));

                _context.IdentityService.Setup(x => x.IsInRole(type)).Returns(false);
                _context.StateRepository.Setup(x => x.GetAvailableStates(type)).Throws<InvalidOperationException>();
            }
        }

        [Ignore] // todo: 3. this test should work because a broker can't set a state
        [TestMethod, ExpectedException(typeof (InvalidLogicException))]
        public void Test_GetAvailableStatesToSet_Broker()
        {
            var roles = Enum.GetValues(typeof (RoleType)).Cast<RoleType>().Except(new[] {RoleType.Broker}).ToArray();
            foreach (var roleType in roles)
            {
                var type = roleType;
                _context.IdentityService.Setup(x => x.IsInRole(type)).Returns(false);
            }

            _stateService.GetAvailableStatesToSet();
        }

        [TestMethod]
        public void Test_GetVisibleStates()
        {
            var roles = Enum.GetValues(typeof (RoleType)).Cast<RoleType>().ToArray();
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

                var availableStates = _stateService.GetVisibleStates();
                states.ShouldBeEquivalentTo(availableStates);

                _context.IdentityService.Verify(x => x.IsInRole(type));
                _context.StateRepository.Verify(x => x.GetVisibleStates(type));

                _context.IdentityService.Setup(x => x.IsInRole(type)).Returns(false);
                _context.StateRepository.Setup(x => x.GetVisibleStates(type)).Throws<InvalidOperationException>();
            }
        }

        [Ignore] // 3. todo: this test should work because a broker can't view applications
        [TestMethod, ExpectedException(typeof (InvalidLogicException))]
        public void Test_GetVisibleStates_Broker()
        {
            var roles = Enum.GetValues(typeof (RoleType)).Cast<RoleType>().Except(new[] {RoleType.Broker}).ToArray();
            foreach (var roleType in roles)
            {
                var type = roleType;
                _context.IdentityService.Setup(x => x.IsInRole(type)).Returns(false);
            }

            _stateService.GetVisibleStates();
        }


        [TestMethod]
        public void Test_ApplyBusinessLogicToStates_Weigth()
        {
            var applicationData = _context.Create<ApplicationData>();
            applicationData.Weigth = null;
            applicationData.AirWaybillId = null;
            var availableStates = new[] {_context.Create<long>()};

            _context.StateConfig.Setup(x => x.CargoIsFlewStateId).Returns(It.IsAny<long>());
            _context.StateConfig.Setup(x => x.CargoAtCustomsStateId).Returns(It.IsAny<long>());
            _context.StateConfig.Setup(x => x.CargoInStockStateId).Returns(availableStates[0]);

            var stateModels = _stateService.ApplyBusinessLogicToStates(applicationData, availableStates);

            stateModels.Should().BeEmpty();
        }

        [TestMethod]
        public void Test_ApplyBusinessLogicToStates_Count()
        {
            var applicationData = _context.Create<ApplicationData>();
            applicationData.Count = null;
            applicationData.AirWaybillId = null;
            var availableStates = _context.CreateMany<long>(1).ToArray();

            _context.StateConfig.Setup(x => x.CargoIsFlewStateId).Returns(It.IsAny<long>());
            _context.StateConfig.Setup(x => x.CargoAtCustomsStateId).Returns(It.IsAny<long>());
            _context.StateConfig.Setup(x => x.CargoInStockStateId).Returns(availableStates[0]);

            var stateModels = _stateService.ApplyBusinessLogicToStates(applicationData, availableStates);

            stateModels.Should().BeEmpty();
        }

        [TestMethod]
        public void Test_ApplyBusinessLogicToStates_AirWaybillIdNull()
        {
            var applicationData = _context.Create<ApplicationData>();
            applicationData.AirWaybillId = null;
            var availableStates = _context.CreateMany<long>(2).ToArray();

            _context.StateConfig.Setup(x => x.CargoIsFlewStateId).Returns(availableStates[0]);
            _context.StateConfig.Setup(x => x.CargoAtCustomsStateId).Returns(availableStates[1]);

            var stateModels = _stateService.ApplyBusinessLogicToStates(applicationData, availableStates);

            stateModels.Should().BeEmpty();
        }

        [TestMethod]
        public void Test_ApplyBusinessLogicToStates_AirWaybillIdNotNull()
        {
            var applicationData = _context.Create<ApplicationData>();
            var airWaybillData = _context.CreateMany<AirWaybillData>().ToArray();
            airWaybillData[0].GTD = null;
            var availableStates = _context.CreateMany<long>(1).ToArray();

            _context.AirWaybillRepository.Setup(x => x.Get(applicationData.AirWaybillId.Value)).Returns(airWaybillData);
            _context.StateConfig.Setup(x => x.CargoAtCustomsStateId).Returns(availableStates[0]);

            var stateModels = _stateService.ApplyBusinessLogicToStates(applicationData, availableStates);

            stateModels.Should().BeEmpty();
        }
    }
}