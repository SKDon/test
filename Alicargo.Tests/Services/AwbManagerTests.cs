using System;
using System.Collections.Generic;
using Alicargo.ViewModels;
using Ploeh.AutoFixture;
using System.Linq;
using Alicargo.Core.Contracts;
using Alicargo.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

// ReSharper disable ImplicitlyCapturedClosure
namespace Alicargo.Tests.Services
{
	[TestClass]
	public class AwbManagerTests
	{
		private TestContext _context;
		private AwbManager _manager;

		private void VerifyUnitOfWork()
		{
			_context.UnitOfWork.Verify(x => x.SaveChanges());
			_context.Transaction.Verify(x => x.Complete());
			_context.UnitOfWork.Verify(x => x.GetTransactionScope());
		}

		private void SetupUnitOfWork()
		{
			_context.UnitOfWork.Setup(x => x.SaveChanges());
			_context.Transaction.Setup(x => x.Complete());
			_context.UnitOfWork.Setup(x => x.GetTransactionScope()).Returns(_context.Transaction.Object);
		}

		private void VerifySetState(long referenceId, IEnumerable<long> ids, long stateId, ICollection<ApplicationData> applicationDatas)
		{
			_context.ApplicationRepository.Verify(x => x.GetByReference(referenceId), Times.Once());
			_context.ApplicationManager.Verify(x => x.SetState(It.Is<long>(y => ids.Contains(y)), stateId),
				Times.Exactly(applicationDatas.Count));
			_context.ReferenceRepository.Verify(x => x.SetState(referenceId, stateId), Times.Once());
			_context.StateConfig.Verify(x => x.CargoIsCustomsClearedStateId, Times.Exactly(applicationDatas.Count));
			_context.ApplicationRepository.Verify(
				x => x.SetDateInStock(It.Is<long>(y => ids.Contains(y)), It.IsAny<DateTimeOffset>()),
				Times.Exactly(applicationDatas.Count));
			_context.ApplicationRepository.Verify(x => x.GetByReference(referenceId));
		}

		private void SetStateSetup(long referenceId, ReferenceData referenceData, ApplicationData[] applicationDatas,
			long stateId, IEnumerable<long> ids)
		{
			_context.ReferenceRepository.Setup(x => x.Get(referenceId)).Returns(new[] { referenceData });
			_context.ApplicationRepository.Setup(x => x.GetByReference(referenceId)).Returns(applicationDatas);
			_context.ReferenceRepository.Setup(x => x.SetState(referenceId, stateId));
			_context.StateConfig.Setup(x => x.CargoIsCustomsClearedStateId).Returns(stateId);
			_context.ApplicationRepository.Setup(
				x => x.SetDateInStock(It.Is<long>(y => ids.Contains(y)), It.IsAny<DateTimeOffset>()));
			_context.ApplicationManager.Setup(x => x.SetState(It.Is<long>(y => ids.Contains(y)), stateId));
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new TestContext();

			_manager = _context.Create<AwbManager>();
		}

		[TestMethod]
		public void Test_SetState_ForApplicationsWithSameState()
		{
			var referenceId = _context.Create<long>();
			var stateId = _context.Create<long>();
			var referenceData = _context.Build<ReferenceData>().With(x => x.Id, referenceId).Create();
			var applicationDatas = _context.Build<ApplicationData>()
				.With(x => x.StateId, referenceData.StateId).CreateMany().ToArray();
			var ids = applicationDatas.Select(x => x.Id).ToArray();

			SetStateSetup(referenceId, referenceData, applicationDatas, stateId, ids);

			SetupUnitOfWork();

			_manager.SetState(referenceId, stateId);

			VerifySetState(referenceId, ids, stateId, applicationDatas);

			VerifyUnitOfWork();
		}

		[TestMethod]
		public void Test_SetState_ForApplicationsWithOtherState()
		{
			var referenceId = _context.Create<long>();
			var stateId = _context.Create<long>();
			var referenceData = _context.Build<ReferenceData>().With(x => x.Id, referenceId).Create();
			var applicationDatas = _context.Build<ApplicationData>()
				.With(x => x.StateId, referenceData.StateId + 1).CreateMany().ToArray();

			var ids = applicationDatas.Select(x => x.Id).ToArray();

			SetStateSetup(referenceId, referenceData, applicationDatas, stateId, ids);

			_context.UnitOfWork.Setup(x => x.SaveChanges());
			_context.Transaction.Setup(x => x.Complete());
			_context.UnitOfWork.Setup(x => x.GetTransactionScope()).Returns(_context.Transaction.Object);

			_manager.SetState(referenceId, stateId);

			_context.ApplicationRepository.Verify(x => x.GetByReference(referenceId), Times.Once());
			_context.ApplicationManager.Verify(x => x.SetState(It.IsAny<long>(), stateId), Times.Never());
			_context.ReferenceRepository.Verify(x => x.SetState(referenceId, stateId), Times.Once());
			_context.StateConfig.Verify(x => x.CargoIsCustomsClearedStateId, Times.Never());
			_context.ApplicationRepository.Verify(x => x.SetDateInStock(It.IsAny<long>(), It.IsAny<DateTimeOffset>()), Times.Never());
			_context.UnitOfWork.Verify(x => x.SaveChanges(), Times.Once());
			_context.Transaction.Verify(x => x.Complete());
			_context.UnitOfWork.Verify(x => x.GetTransactionScope());
			_context.ApplicationRepository.Verify(x => x.GetByReference(referenceId));
		}

		[TestMethod]
		public void Test_SetReference_Null()
		{
			var applicationId = _context.Create<long>();

			_context.ApplicationRepository.Setup(x => x.SetReference(applicationId, null));
			_context.UnitOfWork.Setup(x => x.SaveChanges());

			_manager.SetAwb(applicationId, null);

			_context.ApplicationRepository.Verify(x => x.SetReference(applicationId, null), Times.Once());
			_context.UnitOfWork.Verify(x => x.SaveChanges(), Times.Once());
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Test_SetReference_NullAggregate()
		{
			var id = _context.Create<long>();

			_context.ReferenceRepository.Setup(x => x.GetAggregate(id)).Returns(new ReferenceAggregate[0]);

			_manager.SetAwb(It.IsAny<long>(), id);
		}

		[TestMethod]
		public void Test_SetReference()
		{
			var referenceId = _context.Create<long>();
			var applicationId = _context.Create<long>();
			var aggregate = _context.Create<ReferenceAggregate>();

			_context.Transaction.Setup(x => x.Complete());
			_context.UnitOfWork.Setup(x => x.GetTransactionScope()).Returns(_context.Transaction.Object);
			_context.ReferenceRepository.Setup(x => x.GetAggregate(referenceId)).Returns(new[] { aggregate });
			_context.ApplicationRepository.Setup(x => x.SetReference(applicationId, referenceId));
			_context.ApplicationManager.Setup(x => x.SetState(applicationId, aggregate.StateId));
			_context.UnitOfWork.Setup(x => x.SaveChanges());

			_manager.SetAwb(applicationId, referenceId);

			_context.ReferenceRepository.Verify(x => x.GetAggregate(referenceId), Times.Once());
			_context.ApplicationRepository.Verify(x => x.SetReference(applicationId, referenceId), Times.Once());
			_context.ApplicationManager.Verify(x => x.SetState(applicationId, aggregate.StateId), Times.Once());
			_context.UnitOfWork.Verify(x => x.SaveChanges(), Times.Once());
			_context.Transaction.Verify(x => x.Complete());
			_context.UnitOfWork.Verify(x => x.GetTransactionScope());
		}

		[TestMethod]
		public void Test_Create()
		{
			var referenceId = _context.Create<long>();
			var applicationId = _context.Create<long>();
			var cargoIsFlewStateId = _context.Create<long>();
			var model = _context.Create<ReferenceModel>();
			var aggregate = _context.Build<ReferenceAggregate>().With(x => x.StateId, cargoIsFlewStateId).Create();

			_context.Transaction.Setup(x => x.Complete());
			_context.UnitOfWork.Setup(x => x.GetTransactionScope()).Returns(_context.Transaction.Object);
			_context.StateConfig.Setup(x => x.CargoIsFlewStateId).Returns(cargoIsFlewStateId);
			_context.ReferenceRepository.Setup(x => x.GetAggregate(referenceId)).Returns(new[] { aggregate });
			_context.ApplicationRepository.Setup(x => x.SetReference(applicationId, referenceId));
			_context.ApplicationManager.Setup(x => x.SetState(applicationId, cargoIsFlewStateId));
			_context.UnitOfWork.Setup(x => x.SaveChanges());
			_context.ReferenceRepository.Setup(
				x => x.Add(model, model.GTDFile, model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile, model.AWBFile))
				.Returns(() => referenceId);

			_manager.Create(applicationId, model);

			Assert.AreEqual(cargoIsFlewStateId, model.StateId);
			_context.ReferenceRepository.Verify(x => x.GetAggregate(referenceId), Times.Once());
			_context.ApplicationRepository.Verify(x => x.SetReference(applicationId, referenceId), Times.Once());
			_context.ApplicationManager.Verify(x => x.SetState(applicationId, cargoIsFlewStateId), Times.Once());
			_context.UnitOfWork.Verify(x => x.SaveChanges());
			_context.ReferenceRepository.Verify(
				x => x.Add(model, model.GTDFile, model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile, model.AWBFile),
				Times.Once());
			_context.StateConfig.Verify(x => x.CargoIsFlewStateId, Times.Once());
			_context.Transaction.Verify(x => x.Complete());
			_context.UnitOfWork.Verify(x => x.GetTransactionScope());
		}

		[TestMethod]
		public void Test_UpdateWithSetCargoAtCustomsState()
		{
			var cargoAtCustomsStateId = _context.Create<long>();
			var referenceId = _context.Create<long>();
			var model = _context.Build<ReferenceModel>()
				.With(x => x.Id, referenceId)
				.Without(x => x.DateOfDepartureLocalString)
				.Without(x => x.DateOfArrivalLocalString)
				.Create();
			var referenceData = _context.Build<ReferenceData>()
				.With(x => x.Id, referenceId)
				.With(x => x.GTD, null)
				.Create();
			var applicationDatas = _context.Build<ApplicationData>()
				.With(x => x.StateId, referenceData.StateId).CreateMany().ToArray();
			var ids = applicationDatas.Select(x => x.Id).ToArray();

			SetupUnitOfWork();
			SetStateSetup(referenceId, referenceData, applicationDatas, cargoAtCustomsStateId, ids);
			_context.StateConfig.Setup(x => x.CargoAtCustomsStateId).Returns(cargoAtCustomsStateId);
			_context.ReferenceRepository.Setup(x => x.Update(model, model.GTDFile,
				model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile, model.AWBFile));

			_manager.Update(model);

			_context.StateConfig.Verify(x => x.CargoAtCustomsStateId, Times.Once());
			_context.ReferenceRepository.Verify(x => x.Update(model, model.GTDFile,
				model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile, model.AWBFile), Times.Once());
			VerifySetState(referenceId, ids, cargoAtCustomsStateId, applicationDatas);
			VerifyUnitOfWork();
		}

		[TestMethod]
		public void Test_UpdateWithoutSetCargoAtCustomsState()
		{
			var referenceId = _context.Create<long>();
			var model = _context.Build<ReferenceModel>()
				.With(x => x.Id, referenceId)
				.Without(x => x.DateOfDepartureLocalString)
				.Without(x => x.DateOfArrivalLocalString)
				.Without(x => x.GTD)
				.Create();

			SetupUnitOfWork();
			_context.ReferenceRepository.Setup(x => x.Update(model, model.GTDFile,
				model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile, model.AWBFile));

			_manager.Update(model);

			_context.ReferenceRepository.Verify(x => x.Update(model, model.GTDFile,
				model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile, model.AWBFile), Times.Once());
			VerifyUnitOfWork();
		}

		[TestMethod]
		public void Test_UpdateNotSetCargoAtCustomsState()
		{
			var referenceId = _context.Create<long>();
			var model = _context.Build<ReferenceModel>()
				.With(x => x.Id, referenceId)
				.Without(x => x.DateOfDepartureLocalString)
				.Without(x => x.DateOfArrivalLocalString)
				.Create();

			SetupUnitOfWork();
			_context.ReferenceRepository.Setup(x => x.Get(referenceId)).Returns(new[] { (ReferenceData)model });
			_context.ReferenceRepository.Setup(x => x.Update(model, model.GTDFile,
				model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile, model.AWBFile));

			_manager.Update(model);

			_context.ReferenceRepository.Verify(x => x.Get(referenceId), Times.Once());
			_context.ReferenceRepository.Verify(x => x.Update(model, model.GTDFile,
				model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile, model.AWBFile), Times.Once());
			VerifyUnitOfWork();
		}
	}
}
// ReSharper restore ImplicitlyCapturedClosure