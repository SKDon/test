using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Exceptions;
using Alicargo.Services.AirWaybill;
using Alicargo.ViewModels.AirWaybill;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Alicargo.Tests.Services.AirWaybill
{
    [TestClass]
    public class AwbUpdateManagerTests
    {
        private TestHelpers.MockContainer _context;
        private AwbUpdateManager _manager;

        [TestInitialize]
        public void TestInitialize()
        {
            _context = new TestHelpers.MockContainer();

            _manager = _context.Create<AwbUpdateManager>();
        }

        [TestMethod, ExpectedException(typeof(UnexpectedStateException))]
        public void Test_Update_BrockerAWBModel_CargoIsCustomsClearedStateId()
        {
            var id = _context.Create<long>();
            var cargoIsCustomsClearedStateId = _context.Create<long>();
            var data = _context.Create<AirWaybillData>();
            data.StateId = cargoIsCustomsClearedStateId;

            _context.StateConfig.SetupGet(x => x.CargoIsCustomsClearedStateId).Returns(cargoIsCustomsClearedStateId);
            _context.AirWaybillRepository.Setup(x => x.Get(id)).Returns(new[] { data });

            _manager.Update(id, It.IsAny<BrockerAwbModel>());
        }

        [TestMethod]
        public void Test_AwbUpdateManager_Map_BrockerAWBModel()
        {
            var model = _context.Create<AirWaybillEditModel>();
            var data = new AirWaybillData();

            AwbUpdateManager.Map(model, data);

            model.ShouldBeEquivalentTo(data, options => options.ExcludingMissingProperties());
            data.DateOfArrival.ShouldBeEquivalentTo(DateTimeOffset.Parse(model.DateOfArrivalLocalString));
            data.DateOfDeparture.ShouldBeEquivalentTo(DateTimeOffset.Parse(model.DateOfDepartureLocalString));
        }

        [TestMethod]
        public void Test_AwbUpdateManager_Map_AirWaybillEditModel()
        {
            var model = _context.Create<BrockerAwbModel>();
            var data = new AirWaybillData();

            AwbUpdateManager.Map(model, data);

            model.ShouldBeEquivalentTo(data, options => options.ExcludingMissingProperties());
        }
    }
}