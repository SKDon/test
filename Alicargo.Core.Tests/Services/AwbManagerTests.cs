using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Core.Contracts;
using Alicargo.Services;
using Alicargo.ViewModels;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

// ReSharper disable ImplicitlyCapturedClosure
namespace Alicargo.Core.Tests.Services
{
	[TestClass]
	public class AwbManagerTests
	{
		private TestHelpers.TestContext _context;
		private AwbManager _manager;

		private void VerifyUnitOfWork()
		{
			_context.UnitOfWork.Verify(x => x.SaveChanges());
			_context.Transaction.Verify(x => x.Complete());
			_context.UnitOfWork.Verify(x => x.StartTransaction());
		}

		private void SetupUnitOfWork()
		{
			_context.UnitOfWork.Setup(x => x.SaveChanges());
			_context.Transaction.Setup(x => x.Complete());
			_context.UnitOfWork.Setup(x => x.StartTransaction()).Returns(_context.Transaction.Object);
		}

		private void VerifySetState(long AirWaybillId, IEnumerable<long> ids, long stateId, ICollection<ApplicationData> applicationDatas)
		{
			_context.AirWaybillRepository.Verify(x => x.Get(AirWaybillId));
			_context.ApplicationRepository.Verify(x => x.GetByAirWaybill(AirWaybillId));
			_context.ApplicationManager.Verify(x => x.SetState(It.Is<long>(y => ids.Contains(y)), stateId),
				Times.Exactly(applicationDatas.Count));
			_context.AirWaybillRepository.Verify(x => x.SetState(AirWaybillId, stateId), Times.Once());
		}

		private void SetStateSetup(long AirWaybillId, AirWaybillData AirWaybillData, ApplicationData[] applicationDatas,
			long stateId, IEnumerable<long> ids)
		{
			_context.AirWaybillRepository.Setup(x => x.Get(AirWaybillId)).Returns(new[] { AirWaybillData });
			_context.ApplicationRepository.Setup(x => x.GetByAirWaybill(AirWaybillId)).Returns(applicationDatas);
			_context.AirWaybillRepository.Setup(x => x.SetState(AirWaybillId, stateId));
			_context.ApplicationManager.Setup(x => x.SetState(It.Is<long>(y => ids.Contains(y)), stateId));
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new TestHelpers.TestContext();

			_manager = _context.Create<AwbManager>();
		}

		[TestMethod]
		public void Test_SetState_ForApplicationsWithSameState()
		{
			var AirWaybillId = _context.Create<long>();
			var stateId = _context.Create<long>();
			var AirWaybillData = _context.Build<AirWaybillData>().With(x => x.Id, AirWaybillId).Create();
			var applicationDatas = _context.Build<ApplicationData>()
				.With(x => x.StateId, AirWaybillData.StateId).CreateMany().ToArray();
			var ids = applicationDatas.Select(x => x.Id).ToArray();

			SetStateSetup(AirWaybillId, AirWaybillData, applicationDatas, stateId, ids);

			SetupUnitOfWork();

			_manager.SetState(AirWaybillId, stateId);

			VerifySetState(AirWaybillId, ids, stateId, applicationDatas);

			VerifyUnitOfWork();
		}

		[TestMethod]
		public void Test_SetState_ForApplicationsWithOtherState()
		{
			var AirWaybillId = _context.Create<long>();
			var stateId = _context.Create<long>();
			var AirWaybillData = _context.Build<AirWaybillData>().With(x => x.Id, AirWaybillId).Create();
			var applicationDatas = _context.Build<ApplicationData>()
				.With(x => x.StateId, AirWaybillData.StateId + 1).CreateMany().ToArray();

			var ids = applicationDatas.Select(x => x.Id).ToArray();

			SetStateSetup(AirWaybillId, AirWaybillData, applicationDatas, stateId, ids);

			_context.UnitOfWork.Setup(x => x.SaveChanges());
			_context.Transaction.Setup(x => x.Complete());
			_context.UnitOfWork.Setup(x => x.StartTransaction()).Returns(_context.Transaction.Object);

			_manager.SetState(AirWaybillId, stateId);

			_context.ApplicationRepository.Verify(x => x.GetByAirWaybill(AirWaybillId), Times.Once());
			_context.ApplicationManager.Verify(x => x.SetState(It.IsAny<long>(), stateId), Times.Never());
			_context.AirWaybillRepository.Verify(x => x.SetState(AirWaybillId, stateId), Times.Once());
			_context.StateConfig.Verify(x => x.CargoIsCustomsClearedStateId, Times.Never());
			_context.ApplicationUpdater.Verify(x => x.SetDateInStock(It.IsAny<long>(), It.IsAny<DateTimeOffset>()), Times.Never());
			_context.UnitOfWork.Verify(x => x.SaveChanges(), Times.Once());
			_context.Transaction.Verify(x => x.Complete());
			_context.UnitOfWork.Verify(x => x.StartTransaction());
			_context.ApplicationRepository.Verify(x => x.GetByAirWaybill(AirWaybillId));
		}

		[TestMethod]
		public void Test_SetAirWaybill_Null()
		{
			var applicationId = _context.Create<long>();

			_context.ApplicationUpdater.Setup(x => x.SetAirWaybill(applicationId, null));
			_context.UnitOfWork.Setup(x => x.SaveChanges());

			_manager.SetAwb(applicationId, null);

			_context.ApplicationUpdater.Verify(x => x.SetAirWaybill(applicationId, null), Times.Once());
			_context.UnitOfWork.Verify(x => x.SaveChanges(), Times.Once());
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Test_SetAirWaybill_NullAggregate()
		{
			var id = _context.Create<long>();

			_context.AirWaybillRepository.Setup(x => x.GetAggregate(id)).Returns(new AirWaybillAggregate[0]);

			_manager.SetAwb(It.IsAny<long>(), id);
		}

		[TestMethod]
		public void Test_SetAirWaybill()
		{
			var AirWaybillId = _context.Create<long>();
			var applicationId = _context.Create<long>();
			var aggregate = _context.Create<AirWaybillAggregate>();

			_context.Transaction.Setup(x => x.Complete());
			_context.UnitOfWork.Setup(x => x.StartTransaction()).Returns(_context.Transaction.Object);
			_context.AirWaybillRepository.Setup(x => x.GetAggregate(AirWaybillId)).Returns(new[] { aggregate });
			_context.ApplicationUpdater.Setup(x => x.SetAirWaybill(applicationId, AirWaybillId));
			_context.ApplicationManager.Setup(x => x.SetState(applicationId, aggregate.StateId));
			_context.UnitOfWork.Setup(x => x.SaveChanges());

			_manager.SetAwb(applicationId, AirWaybillId);

			_context.AirWaybillRepository.Verify(x => x.GetAggregate(AirWaybillId), Times.Once());
			_context.ApplicationUpdater.Verify(x => x.SetAirWaybill(applicationId, AirWaybillId), Times.Once());
			_context.ApplicationManager.Verify(x => x.SetState(applicationId, aggregate.StateId), Times.Once());
			_context.UnitOfWork.Verify(x => x.SaveChanges(), Times.Once());
			_context.Transaction.Verify(x => x.Complete());
			_context.UnitOfWork.Verify(x => x.StartTransaction());
		}

		[TestMethod]
		public void Test_Create()
		{
			var AirWaybillId = _context.Create<long>();
			var applicationId = _context.Create<long>();
			var cargoIsFlewStateId = _context.Create<long>();
			var model = _context.Create<AirWaybillModel>();
			var aggregate = _context.Build<AirWaybillAggregate>().With(x => x.StateId, cargoIsFlewStateId).Create();

			_context.Transaction.Setup(x => x.Complete());
			_context.UnitOfWork.Setup(x => x.StartTransaction()).Returns(_context.Transaction.Object);
			_context.StateConfig.Setup(x => x.CargoIsFlewStateId).Returns(cargoIsFlewStateId);
			_context.AirWaybillRepository.Setup(x => x.GetAggregate(AirWaybillId)).Returns(new[] { aggregate });
			_context.ApplicationUpdater.Setup(x => x.SetAirWaybill(applicationId, AirWaybillId));
			_context.ApplicationManager.Setup(x => x.SetState(applicationId, cargoIsFlewStateId));
			_context.UnitOfWork.Setup(x => x.SaveChanges());
			_context.AirWaybillRepository.Setup(
				x => x.Add(model, model.GTDFile, model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile, model.AWBFile))
				.Returns(() => AirWaybillId);

			_manager.Create(applicationId, model);

			Assert.AreEqual(cargoIsFlewStateId, model.StateId);
			_context.AirWaybillRepository.Verify(x => x.GetAggregate(AirWaybillId), Times.Once());
			_context.ApplicationUpdater.Verify(x => x.SetAirWaybill(applicationId, AirWaybillId), Times.Once());
			_context.ApplicationManager.Verify(x => x.SetState(applicationId, cargoIsFlewStateId), Times.Once());
			_context.UnitOfWork.Verify(x => x.SaveChanges());
			_context.AirWaybillRepository.Verify(
				x => x.Add(model, model.GTDFile, model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile, model.AWBFile),
				Times.Once());
			_context.StateConfig.Verify(x => x.CargoIsFlewStateId, Times.Once());
			_context.Transaction.Verify(x => x.Complete());
			_context.UnitOfWork.Verify(x => x.StartTransaction());
		}

		[TestMethod]
		public void Test_UpdateWithSetCargoAtCustomsState()
		{
			var cargoAtCustomsStateId = _context.Create<long>();
			var cargoIsCustomsClearedStateId = _context.Create<long>();
			cargoAtCustomsStateId.Should().NotBe(cargoIsCustomsClearedStateId);

			var AirWaybillId = _context.Create<long>();
			var model = _context.Build<AirWaybillModel>()
				.With(x => x.Id, AirWaybillId)
				.Without(x => x.DateOfDepartureLocalString)
				.Without(x => x.DateOfArrivalLocalString)
				.Create();
			var old = _context.Build<AirWaybillData>()
				.With(x => x.Id, AirWaybillId)
				.With(x => x.GTD, null)
				.Create();
			var applicationDatas = _context.Build<ApplicationData>()
				.With(x => x.StateId, old.StateId).CreateMany().ToArray();
			var ids = applicationDatas.Select(x => x.Id).ToArray();

			SetupUnitOfWork();
			SetStateSetup(AirWaybillId, old, applicationDatas, cargoAtCustomsStateId, ids);
			_context.StateConfig.Setup(x => x.CargoIsCustomsClearedStateId).Returns(cargoIsCustomsClearedStateId);
			_context.StateConfig.Setup(x => x.CargoAtCustomsStateId).Returns(cargoAtCustomsStateId);
			_context.AirWaybillRepository.Setup(x => x.Update(model, model.GTDFile,
				model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile, model.AWBFile));

			_manager.Update(model);

			_context.StateConfig.Verify(x => x.CargoAtCustomsStateId, Times.Once());
			_context.StateConfig.Verify(x => x.CargoIsCustomsClearedStateId, Times.Once());
			_context.AirWaybillRepository.Verify(x => x.Update(model, model.GTDFile,
				model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile, model.AWBFile), Times.Once());
			VerifySetState(AirWaybillId, ids, cargoAtCustomsStateId, applicationDatas);
			VerifyUnitOfWork();
		}

		[TestMethod]
		public void Test_UpdateWithoutSetCargoAtCustomsState()
		{
			var AirWaybillId = _context.Create<long>();
			var model = _context.Build<AirWaybillModel>()
				.With(x => x.Id, AirWaybillId)
				.Without(x => x.DateOfDepartureLocalString)
				.Without(x => x.DateOfArrivalLocalString)
				.Without(x => x.GTD)
				.Create();
			var old = _context.Build<AirWaybillData>()
				.With(x => x.Id, AirWaybillId)
				.With(x => x.GTD, null)
				.Create();

			SetupUnitOfWork();
			_context.AirWaybillRepository.Setup(x => x.Get(AirWaybillId)).Returns(new[] { old });
			_context.AirWaybillRepository.Setup(x => x.Update(model, model.GTDFile,
				model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile, model.AWBFile));

			_manager.Update(model);

			_context.AirWaybillRepository.Verify(x => x.Update(model, model.GTDFile,
				model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile, model.AWBFile), Times.Once());
			_context.AirWaybillRepository.Verify(x => x.SetState(AirWaybillId, It.IsAny<long>()), Times.Never());
			VerifyUnitOfWork();
		}

		[TestMethod]
		public void Test_UpdateWithAlreadryCargoIsCustomsClearedStateIdSet()
		{
			var AirWaybillId = _context.Create<long>();
			var model = _context.Build<AirWaybillModel>()
				.With(x => x.Id, AirWaybillId)
				.Without(x => x.DateOfDepartureLocalString)
				.Without(x => x.DateOfArrivalLocalString)
				.Create();
			var cargoIsCustomsClearedStateId = _context.Create<long>();
			var old = _context.Build<AirWaybillData>()
				.With(x => x.Id, AirWaybillId)
				.With(x => x.GTD, null)
				.With(x => x.StateId, cargoIsCustomsClearedStateId)
				.Create();

			SetupUnitOfWork();
			_context.AirWaybillRepository.Setup(x => x.Get(AirWaybillId)).Returns(new[] { old });
			_context.AirWaybillRepository.Setup(x => x.Update(model, model.GTDFile,
				model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile, model.AWBFile));
			_context.StateConfig.Setup(x => x.CargoIsCustomsClearedStateId).Returns(cargoIsCustomsClearedStateId);

			_manager.Update(model);

			_context.StateConfig.Verify(x => x.CargoIsCustomsClearedStateId, Times.Once());
			_context.AirWaybillRepository.Verify(x => x.Update(model, model.GTDFile,
				model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile, model.AWBFile), Times.Once());
			_context.AirWaybillRepository.Verify(x => x.SetState(AirWaybillId, It.IsAny<long>()), Times.Never());
			VerifyUnitOfWork();
		}

		[TestMethod]
		public void Test_UpdateNotSetCargoAtCustomsState()
		{
			var AirWaybillId = _context.Create<long>();
			var model = _context.Build<AirWaybillModel>()
				.With(x => x.Id, AirWaybillId)
				.Without(x => x.DateOfDepartureLocalString)
				.Without(x => x.DateOfArrivalLocalString)
				.Create();

			SetupUnitOfWork();
			_context.AirWaybillRepository.Setup(x => x.Get(AirWaybillId)).Returns(new[] { (AirWaybillData)model });
			_context.AirWaybillRepository.Setup(x => x.Update(model, model.GTDFile,
				model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile, model.AWBFile));

			_manager.Update(model);

			_context.AirWaybillRepository.Verify(x => x.Get(AirWaybillId), Times.Once());
			_context.AirWaybillRepository.Verify(x => x.Update(model, model.GTDFile,
				model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile, model.AWBFile), Times.Once());
			VerifyUnitOfWork();
		}
	}
}
// ReSharper restore ImplicitlyCapturedClosure