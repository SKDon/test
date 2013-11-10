using Alicargo.Contracts.Contracts;
using Alicargo.Core.Services.Abstract;
using Alicargo.Services.Abstract;
using Alicargo.Services.AirWaybill;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

namespace Alicargo.Tests.Services.AirWaybill
{
    [TestClass]
    public class AwbGtdHelperTests
    {
        private AirWaybillData _data;
        private Fixture _fixture;
        private AwbGtdHelper _helper;
        private Mock<IStateConfig> _stateConfig;
        private Mock<IAwbStateManager> _stateManager;

        [TestInitialize]
        public void TestInitialize()
        {
            _stateManager = new Mock<IAwbStateManager>(MockBehavior.Strict);
            _stateConfig = new Mock<IStateConfig>(MockBehavior.Strict);
            _helper = new AwbGtdHelper(_stateManager.Object, _stateConfig.Object);
            _fixture = new Fixture();
            _data = _fixture.Create<AirWaybillData>();
        }

        [TestMethod]
        public void Test_Not_IsReadyForCargoAtCustomsStateId()
        {
            _helper.ProcessGtd(_data, "");

            _stateManager.Verify(x => x.SetState(_data.Id, It.IsAny<long>()), Times.Never());
        }

        [TestMethod]
        public void Test_CargoIsCustomsClearedStateId()
        {
            var newGtd = _fixture.Create<string>();
            _stateConfig.SetupGet(x => x.CargoIsCustomsClearedStateId).Returns(_data.StateId);

            _helper.ProcessGtd(_data, newGtd);

            _stateManager.Verify(x => x.SetState(_data.Id, It.IsAny<long>()), Times.Never());
        }

        [TestMethod]
        public void Test_SetState_CargoAtCustomsStateId()
        {
            var newGtd = _fixture.Create<string>();
            var cargoIsCustomsClearedStateId = _data.StateId - 1;
            var cargoAtCustomsStateId = _data.StateId - 2;
            _data.GTD = null;

            _stateConfig.SetupGet(x => x.CargoIsCustomsClearedStateId).Returns(cargoIsCustomsClearedStateId);
            _stateConfig.SetupGet(x => x.CargoAtCustomsStateId).Returns(cargoAtCustomsStateId);
            _stateManager.Setup(x => x.SetState(_data.Id, cargoAtCustomsStateId));

            _helper.ProcessGtd(_data, newGtd);

            _stateManager.Verify(x => x.SetState(_data.Id, cargoAtCustomsStateId), Times.Once());
        }
    }
}