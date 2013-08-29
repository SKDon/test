using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.Tests.Services.Application
{
    [TestClass]
    public class ApplicationAwbManagerTests
    {

        //		[TestMethod]
        //		public void Test_SetAirWaybill_Null()
        //		{
        //			var applicationId = _context.Create<long>();
        //
        //			_context.ApplicationUpdater.Setup(x => x.SetAirWaybill(applicationId, null));
        //			_context.UnitOfWork.Setup(x => x.SaveChanges());
        //
        //			_stateManager.SetAwb(applicationId, null);
        //
        //			_context.ApplicationUpdater.Verify(x => x.SetAirWaybill(applicationId, null), Times.Once());
        //			_context.UnitOfWork.Verify(x => x.SaveChanges(), Times.Once());
        //		}
        //
        //		[TestMethod]
        //		[ExpectedException(typeof(InvalidOperationException))]
        //		public void Test_SetAirWaybill_NullAggregate()
        //		{
        //			var id = _context.Create<long>();
        //
        //			_context.AirWaybillRepository.Setup(x => x.GetAggregate(id)).Returns(new AirWaybillAggregate[0]);
        //
        //			_stateManager.SetAwb(It.IsAny<long>(), id);
        //		}
        //
        //		[TestMethod]
        //		public void Test_SetAirWaybill()
        //		{
        //			var AirWaybillId = _context.Create<long>();
        //			var applicationId = _context.Create<long>();
        //			var aggregate = _context.Create<AirWaybillAggregate>();
        //
        //			_context.Transaction.Setup(x => x.Complete());
        //			_context.UnitOfWork.Setup(x => x.StartTransaction()).Returns(_context.Transaction.Object);
        //			_context.AirWaybillRepository.Setup(x => x.GetAggregate(AirWaybillId)).Returns(new[] { aggregate });
        //			_context.ApplicationUpdater.Setup(x => x.SetAirWaybill(applicationId, AirWaybillId));
        //			_context.ApplicationManager.Setup(x => x.SetState(applicationId, aggregate.StateId));
        //			_context.UnitOfWork.Setup(x => x.SaveChanges());
        //
        //			_stateManager.SetAwb(applicationId, AirWaybillId);
        //
        //			_context.AirWaybillRepository.Verify(x => x.GetAggregate(AirWaybillId), Times.Once());
        //			_context.ApplicationUpdater.Verify(x => x.SetAirWaybill(applicationId, AirWaybillId), Times.Once());
        //			_context.ApplicationManager.Verify(x => x.SetState(applicationId, aggregate.StateId), Times.Once());
        //			_context.UnitOfWork.Verify(x => x.SaveChanges(), Times.Once());
        //			_context.Transaction.Verify(x => x.Complete());
        //			_context.UnitOfWork.Verify(x => x.StartTransaction());
        //		}
    }
}
