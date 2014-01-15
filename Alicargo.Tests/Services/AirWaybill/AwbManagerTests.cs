using System;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Exceptions;
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
            var model = _context.Create<AwbAdminModel>();
            model.GTD = null;

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
                                                         && data.BrokerId == model.BrokerId
                                                         &&
                                                         data.DateOfArrival ==
                                                         DateTimeOffset.Parse(model.DateOfArrivalLocalString)
                                                         &&
                                                         data.DateOfDeparture ==
                                                         DateTimeOffset.Parse(model.DateOfDepartureLocalString)
                                                         && data.GTD == null
                                                         && data.GTDAdditionalFileName == model.GTDAdditionalFileName
                                                         && data.GTDFileName == model.GTDFileName),
                           model.GTDFile, model.GTDAdditionalFile, model.PackingFile,
                           model.InvoiceFile, model.AWBFile),
                Times.Once());

            _context.StateConfig.Verify(x => x.CargoIsFlewStateId, Times.Once());
        }

        [TestMethod]
		public void Test_AwbManager_Map_AirWaybillEditModel()
        {
            var cargoIsFlewStateId = _context.Create<long>();
            var model = _context.Create<AwbAdminModel>();
            var data = AwbManager.Map(model, cargoIsFlewStateId);

            model.ShouldBeEquivalentTo(data, options => options.ExcludingMissingProperties()
                                                               .Excluding(x => x.GTD));

            data.DateOfArrival.ShouldBeEquivalentTo(DateTimeOffset.Parse(model.DateOfArrivalLocalString));
            data.DateOfDeparture.ShouldBeEquivalentTo(DateTimeOffset.Parse(model.DateOfDepartureLocalString));
            data.GTD.Should().BeNull();
        }

		[TestMethod]
		public void Test_AwbManager_Map_SenderAwbModel()
		{
			var cargoIsFlewStateId = _context.Create<long>();
			var model = _context.Create<AwbSenderModel>();
			var data = AwbManager.Map(model, cargoIsFlewStateId);

			model.ShouldBeEquivalentTo(data, options => options.ExcludingMissingProperties());

			data.DateOfArrival.ShouldBeEquivalentTo(DateTimeOffset.Parse(model.DateOfArrivalLocalString));
			data.DateOfDeparture.ShouldBeEquivalentTo(DateTimeOffset.Parse(model.DateOfDepartureLocalString));
			data.GTD.Should().BeNull();
		}

        [TestMethod]
        public void Test_AwbManager_Delete()
        {
            var id = _context.Create<long>();
            var applications = _context.CreateMany<ApplicationData>().ToArray();
            _context.ApplicationRepository.Setup(x => x.GetByAirWaybill(id)).Returns(applications);
            _context.ApplicationUpdater.Setup(x => x.SetAirWaybill(It.IsAny<long>(), null));
            _context.AirWaybillRepository.Setup(x => x.Delete(id));
            _context.UnitOfWork.Setup(x => x.SaveChanges());

            _manager.Delete(id);

            _context.ApplicationRepository.Verify(x => x.GetByAirWaybill(id), Times.Once());
            _context.ApplicationUpdater.Verify(
                x => x.SetAirWaybill(It.Is<long>(i => applications.Any(a => a.Id == i)), null),
                Times.Exactly(applications.Length));
            _context.AirWaybillRepository.Verify(x => x.Delete(id), Times.Once());
            _context.UnitOfWork.Verify(x => x.SaveChanges(), Times.Once());
        }

        [TestMethod, ExpectedException(typeof(InvalidLogicException))]
        public void Test_AwbManager_Create_WithGtd()
        {
            var model = _context.Create<AwbAdminModel>();
            _manager.Create(It.IsAny<long>(), model);
        }
    }
}