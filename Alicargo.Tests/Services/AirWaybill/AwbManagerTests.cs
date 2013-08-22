using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Services.AirWaybill;
using Alicargo.TestHelpers;
using Alicargo.ViewModels.AirWaybill;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Alicargo.Tests.Services.AirWaybill
{
    [TestClass]
    public class AwbManagerTests
    {
        private MockContainer _context;
        private AwbManager _manager;

        [TestInitialize]
        public void TestInitialize()
        {
            _context = new MockContainer();

            _manager = _context.Create<AwbManager>();
        }

        [TestMethod]
        public void Test_AwbManager_Create()
        {
            var airWaybillId = _context.Create<long>();
            var applicationId = _context.Create<long>();
            var cargoIsFlewStateId = _context.Create<long>();
            var model = _context.Create<AirWaybillEditModel>();
            model.GTD = null;
            model.GTDAdditionalFileName = null;
            model.GTDFileName = null;

            _context.StateConfig.Setup(x => x.CargoIsFlewStateId).Returns(cargoIsFlewStateId);
            _context.ApplicationAwbManager.Setup(x => x.SetAwb(applicationId, airWaybillId));
            _context.UnitOfWork.Setup(x => x.SaveChanges());
            _context.AirWaybillRepository.Setup(
                x =>
                x.Add(It.IsAny<AirWaybillData>(), model.GTDFile, model.GTDAdditionalFile, model.PackingFile,
                      model.InvoiceFile, model.AWBFile))
                    .Returns(() => airWaybillId);

            _manager.Create(applicationId, model);

            _context.ApplicationAwbManager.Verify(x => x.SetAwb(applicationId, airWaybillId), Times.Once());
            _context.UnitOfWork.Verify(x => x.SaveChanges());
            _context.AirWaybillRepository.Verify(
                x => x.Add(It.Is<AirWaybillData>(data => data.Id == 0
                                                         && data.StateId == cargoIsFlewStateId
                                                         && data.CreationTimestamp != null
                                                         && data.StateChangeTimestamp != null
                                                         && data.PackingFileName == model.PackingFileName
                                                         && data.InvoiceFileName == model.InvoiceFileName
                                                         && data.AWBFileName == model.AWBFileName
                                                         && data.ArrivalAirport == model.ArrivalAirport
                                                         && data.Bill == model.Bill
                                                         && data.DepartureAirport == model.DepartureAirport
                                                         && data.BrockerId == model.BrockerId
                                                         && data.DateOfArrival == DateTimeOffset.Parse(model.DateOfArrivalLocalString)
                                                         && data.DateOfDeparture == DateTimeOffset.Parse(model.DateOfDepartureLocalString)
                                                         && data.GTD == null
                                                         && data.GTDAdditionalFileName == null
                                                         && data.GTDFileName == null
                                                         ),
                           model.GTDFile, model.GTDAdditionalFile, model.PackingFile,
                           model.InvoiceFile, model.AWBFile),
                Times.Once());

            _context.StateConfig.Verify(x => x.CargoIsFlewStateId, Times.Once());
        }

        [TestMethod]
        public void Test_AwbManager_Map()
        {
            var cargoIsFlewStateId = _context.Create<long>();
            var model = _context.Create<AirWaybillEditModel>();
            var data = AwbManager.Map(model, cargoIsFlewStateId);

            model.ShouldBeEquivalentTo(data, options => options.ExcludingMissingProperties()
                .Excluding(x => x.GTD));
        }
    }
}