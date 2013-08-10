using System;
using System.Linq;
using Alicargo.Core.Contracts;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Microsoft.Ajax.Utilities;

namespace Alicargo.Services
{
	public sealed class AwbManager : IAwbManager
	{
		private readonly IAirWaybillRepository _AirWaybillRepository;
		private readonly IApplicationRepository _applicationRepository;
		private readonly IApplicationUpdateRepository _applicationUpdater;
		private readonly IApplicationManager _applicationManager;
		private readonly IStateConfig _stateConfig;

		private readonly IUnitOfWork _unitOfWork;

		public AwbManager(
			IAirWaybillRepository AirWaybillRepository,
			IApplicationRepository applicationRepository,
			IUnitOfWork unitOfWork,
			IApplicationManager applicationManager,
			IStateConfig stateConfig, 
			IApplicationUpdateRepository applicationUpdater)
		{
			_AirWaybillRepository = AirWaybillRepository;
			_applicationRepository = applicationRepository;
			_unitOfWork = unitOfWork;
			_applicationManager = applicationManager;
			_stateConfig = stateConfig;
			_applicationUpdater = applicationUpdater;
		}

		public void Create(long applicationId, AirWaybillModel model)
		{
			model.CreationTimestamp = DateTimeOffset.UtcNow;
			model.StateChangeTimestamp = DateTimeOffset.UtcNow;

			using (var ts = _unitOfWork.StartTransaction())
			{
				model.StateId = _stateConfig.CargoIsFlewStateId;
				var id = _AirWaybillRepository.Add(model, model.GTDFile, model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile, model.AWBFile);
				_unitOfWork.SaveChanges();
				model.Id = id();

				SetAwb(applicationId, model.Id);

				ts.Complete();
			}
		}

		// todo: refactor with decorator
		public void SetAwb(long applicationId, long? awbId)
		{
			if (awbId.HasValue)
			{
				var aggregate = _AirWaybillRepository.GetAggregate(awbId.Value).First();

				using (var ts = _unitOfWork.StartTransaction())
				{
					// SetAirWaybill must be first
					_applicationUpdater.SetAirWaybill(applicationId, awbId.Value);

					_applicationManager.SetState(applicationId, aggregate.StateId);

					_unitOfWork.SaveChanges();

					ts.Complete();
				}
			}
			else
			{
				_applicationUpdater.SetAirWaybill(applicationId, null);

				_unitOfWork.SaveChanges();
			}
		}

		public void Update(AirWaybillModel model)
		{
			using (var ts = _unitOfWork.StartTransaction())
			{
				var old = _AirWaybillRepository.Get(model.Id).First();

				// Update must be before SetState
				_AirWaybillRepository.Update(model, model.GTDFile, model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile, model.AWBFile);

				if (IsReadyForCargoAtCustomsStateId(model))
				{
					if (!IsReadyForCargoAtCustomsStateId(old) && old.StateId != _stateConfig.CargoIsCustomsClearedStateId)
						SetState(model.Id, _stateConfig.CargoAtCustomsStateId);
				}

				_unitOfWork.SaveChanges();

				ts.Complete();
			}
		}

		private static bool IsReadyForCargoAtCustomsStateId(IAirWaybillData model)
		{
			return !model.GTD.IsNullOrWhiteSpace();
		}

		// todo: test
		public void Delete(long id)
		{
			using (var ts = _unitOfWork.StartTransaction())
			{
				var applicationDatas = _applicationRepository.GetByAirWaybill(id);

				foreach (var app in applicationDatas)
				{
					_applicationUpdater.SetAirWaybill(app.Id, null);
				}

				_AirWaybillRepository.Delete(id);

				_unitOfWork.SaveChanges();

				ts.Complete();
			}
		}

		public void SetState(long AirWaybillId, long stateId)
		{
			var AirWaybill = _AirWaybillRepository.Get(AirWaybillId).First();

			var applications = _applicationRepository.GetByAirWaybill(AirWaybillId);

			using (var ts = _unitOfWork.StartTransaction())
			{
				_AirWaybillRepository.SetState(AirWaybillId, stateId);

				foreach (var application in applications)
				{
					if (AirWaybill.StateId == application.StateId)
					{
						_applicationManager.SetState(application.Id, stateId);
					}
				}

				_unitOfWork.SaveChanges();

				ts.Complete();
			}
		}
	}
}