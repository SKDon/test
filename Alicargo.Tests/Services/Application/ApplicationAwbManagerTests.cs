using System;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.Services.AirWaybill;
using Alicargo.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Alicargo.Tests.Services.Application
{
	[TestClass]
	public class ApplicationAwbManagerTests
	{
		private MockContainer _context;
		private ApplicationAwbManager _manager;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new MockContainer();

			_manager = _context.Create<ApplicationAwbManager>();
		}

		[TestMethod]
		public void Test_SetAirWaybill_Null()
		{
			var applicationId = _context.Create<long>();
			var stateId = _context.Create<long>();

			_context.ApplicationEditor.Setup(x => x.SetAirWaybill(applicationId, null));
			_context.ApplicationManager.Setup(x => x.SetState(applicationId, stateId));
			_context.StateConfig.Setup(x => x.CargoInStockStateId).Returns(stateId);

			_manager.SetAwb(applicationId, null);

			_context.ApplicationEditor.Verify(x => x.SetAirWaybill(applicationId, null), Times.Once());
			_context.ApplicationManager.Verify(x => x.SetState(applicationId, stateId), Times.Once);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Test_SetAirWaybill_NullAggregate()
		{
			var id = _context.Create<long>();

			_context.AirWaybillRepository.Setup(x => x.GetAggregate(new[] { id }, null, null, null, null)).Returns(new AirWaybillAggregate[0]);

			_manager.SetAwb(It.IsAny<long>(), id);
		}

		[TestMethod]
		public void Test_SetAirWaybill()
		{
			var airWaybillId = _context.Create<long>();
			var applicationId = _context.Create<long>();
			var aggregate = _context.Create<AirWaybillAggregate>();

			// ReSharper disable ImplicitlyCapturedClosure
			_context.AirWaybillRepository.Setup(x => x.GetAggregate(new[] { airWaybillId }, null, null, null, null)).Returns(new[] { aggregate });
			_context.ApplicationEditor.Setup(x => x.SetAirWaybill(applicationId, airWaybillId));
			_context.ApplicationManager.Setup(x => x.SetState(applicationId, aggregate.StateId));

			_manager.SetAwb(applicationId, airWaybillId);

			_context.AirWaybillRepository.Verify(x => x.GetAggregate(new[] { airWaybillId }, null, null, null, null), Times.Once());
			_context.ApplicationEditor.Verify(x => x.SetAirWaybill(applicationId, airWaybillId), Times.Once());
			_context.ApplicationManager.Verify(x => x.SetState(applicationId, aggregate.StateId), Times.Once());
			// ReSharper restore ImplicitlyCapturedClosure
		}
	}
}
