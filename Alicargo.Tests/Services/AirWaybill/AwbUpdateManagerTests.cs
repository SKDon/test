using System;
using Alicargo.Core.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Awb;
using Alicargo.Services.AirWaybill;
using Alicargo.TestHelpers;
using Alicargo.ViewModels.AirWaybill;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Alicargo.Tests.Services.AirWaybill
{
    [TestClass]
    public class AwbUpdateManagerTests
    {
        private MockContainer _context;
        private AwbUpdateManager _manager;

        [TestInitialize]
        public void TestInitialize()
        {
            _context = new MockContainer();

            _manager = _context.Create<AwbUpdateManager>();
        }

        [TestMethod, ExpectedException(typeof(UnexpectedStateException))]
        public void Test_Update_BrokerAWBModel_CargoIsCustomsClearedStateId()
        {
            var id = _context.Create<long>();
            var cargoIsCustomsClearedStateId = _context.Create<long>();
            var data = _context.Create<AirWaybillData>();
            data.StateId = cargoIsCustomsClearedStateId;

            _context.StateConfig.SetupGet(x => x.CargoIsCustomsClearedStateId).Returns(cargoIsCustomsClearedStateId);
            _context.AwbRepository.Setup(x => x.Get(id)).Returns(new[] { data });

            _manager.Update(id, It.IsAny<AwbBrokerModel>());
        }

        [TestMethod]
        public void Test_AwbUpdateManager_Map_BrokerAWBModel()
        {
            var model = _context.Create<AwbAdminModel>();
            var data = new AirWaybillData();

            AwbMapper.Map(model, data);

            model.ShouldBeEquivalentTo(data, options => options.ExcludingMissingProperties());
            data.DateOfArrival.ShouldBeEquivalentTo(DateTimeOffset.Parse(model.DateOfArrivalLocalString));
            data.DateOfDeparture.ShouldBeEquivalentTo(DateTimeOffset.Parse(model.DateOfDepartureLocalString));
        }

        [TestMethod]
        public void Test_AwbUpdateManager_Map_AirWaybillEditModel()
        {
            var model = _context.Create<AwbBrokerModel>();
            var data = new AirWaybillData();

            AwbMapper.Map(model, data);

            model.ShouldBeEquivalentTo(data, options => options.ExcludingMissingProperties());
        }
    }
}