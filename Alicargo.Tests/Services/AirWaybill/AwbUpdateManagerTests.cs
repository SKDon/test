using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Exceptions;
using Alicargo.Services.AirWaybill;
using Alicargo.ViewModels;
using Alicargo.ViewModels.AirWaybill;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

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
            var model = _context.Create<AirWaybillData>();
            model.StateId = cargoIsCustomsClearedStateId;

            _context.StateConfig.SetupGet(x => x.CargoIsCustomsClearedStateId).Returns(cargoIsCustomsClearedStateId);
            _context.AirWaybillRepository.Setup(x => x.Get(id)).Returns(new[] { model });

            _manager.Update(id, It.IsAny<BrockerAWBModel>());
        }
    }
}