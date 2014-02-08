using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.Services.AirWaybill;
using Alicargo.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Alicargo.Tests.Services.AirWaybill
{
    [TestClass]
    public class AwbStateManagerTests
    {
        private MockContainer _context;
        private AwbStateManager _stateManager;
        private long _airWaybillId;
        private long _stateId;

        [TestInitialize]
        public void TestInitialize()
        {
            _context = new MockContainer();

            _stateManager = _context.Create<AwbStateManager>();
            _airWaybillId = _context.Create<long>();
            _stateId = _context.Create<long>();
        }

        [TestMethod]
        public void Test_SetState()
        {
            var data = _context.Create<AirWaybillData>();
            var applicationData = _context.CreateMany<ApplicationData>().ToArray();
            applicationData[0].StateId = data.StateId;

            _context.AirWaybillRepository.Setup(x => x.Get(_airWaybillId)).Returns(new[] { data });
            _context.AirWaybillRepository.Setup(x => x.SetState(_airWaybillId, _stateId));
            _context.ApplicationRepository.Setup(x => x.GetByAirWaybill(_airWaybillId)).Returns(applicationData);
            _context.ApplicationManager.Setup(x => x.SetState(applicationData[0].Id, _stateId));

            _stateManager.SetState(_airWaybillId, _stateId);

            _context.AirWaybillRepository.Verify(x => x.Get(_airWaybillId), Times.Once());
            _context.AirWaybillRepository.Verify(x => x.SetState(_airWaybillId, _stateId), Times.Once());
            _context.ApplicationRepository.Verify(x => x.GetByAirWaybill(_airWaybillId), Times.Once());
            _context.ApplicationManager.Verify(x => x.SetState(applicationData[0].Id, _stateId), Times.Once());
        }
    }
}