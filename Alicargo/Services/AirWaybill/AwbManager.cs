using System;
using System.Globalization;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.AirWaybill;
using Microsoft.Ajax.Utilities;

namespace Alicargo.Services.AirWaybill
{
    internal sealed class AwbManager : IAwbManager
	{
		private readonly IAwbRepository _awbRepository;
		private readonly IApplicationManager _applicationManager;
		private readonly IApplicationRepository _applicationRepository;
		private readonly IApplicationUpdateRepository _applicationUpdater;
		private readonly IStateConfig _stateConfig;
		private readonly IUnitOfWork _unitOfWork;

		public AwbManager(
			IAwbRepository awbRepository,
			IApplicationRepository applicationRepository,
			IUnitOfWork unitOfWork,
			IApplicationManager applicationManager,
			IStateConfig stateConfig,
			IApplicationUpdateRepository applicationUpdater)
		{
			_awbRepository = awbRepository;
			_applicationRepository = applicationRepository;
			_unitOfWork = unitOfWork;
			_applicationManager = applicationManager;
			_stateConfig = stateConfig;
			_applicationUpdater = applicationUpdater;
		}

		public long Create(long applicationId, AirWaybillEditModel model)
		{
			using (var ts = _unitOfWork.StartTransaction())
			{
                // todo: 2. mapper
				var data = new AirWaybillData
				{
					StateId = _stateConfig.CargoIsFlewStateId,
					CreationTimestamp = DateTimeOffset.UtcNow,
					StateChangeTimestamp = DateTimeOffset.UtcNow,
					Id = 0,
					PackingFileName = model.PackingFileName,
					InvoiceFileName = model.InvoiceFileName,
					AWBFileName = model.AWBFileName,
					ArrivalAirport = model.ArrivalAirport,
					Bill = model.Bill,
					DepartureAirport = model.DepartureAirport,
					GTD = model.GTD,
					GTDFileName = model.GTDFileName,
					BrockerId = model.BrockerId,
					GTDAdditionalFileName = model.GTDAdditionalFileName,
					DateOfArrival = DateTimeOffset.Parse(model.DateOfArrivalLocalString),
					DateOfDeparture = DateTimeOffset.Parse(model.DateOfDepartureLocalString)
				};

				var id = _awbRepository.Add(data, model.GTDFile, model.GTDAdditionalFile, model.PackingFile,
												   model.InvoiceFile, model.AWBFile);

				_unitOfWork.SaveChanges();

				SetAwb(applicationId, id());

				ts.Complete();

				return id();
			}
		}

		public void SetAwb(long applicationId, long? awbId)
		{
            // todo: 2. test
			if (awbId.HasValue)
			{
				var aggregate = _awbRepository.GetAggregate(awbId.Value).First();

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

		public void Update(long id, AirWaybillEditModel model)
		{
			using (var ts = _unitOfWork.StartTransaction())
			{
				var data = _awbRepository.Get(id).First();

                // // todo: 2. mapper
				data.PackingFileName = model.PackingFileName;
				data.InvoiceFileName = model.InvoiceFileName;
				data.AWBFileName = model.AWBFileName;
				data.ArrivalAirport = model.ArrivalAirport;
				data.Bill = model.Bill;
				data.DepartureAirport = model.DepartureAirport;
				data.GTD = model.GTD;
				data.GTDFileName = model.GTDFileName;
				data.BrockerId = model.BrockerId;
				data.GTDAdditionalFileName = model.GTDAdditionalFileName;
				data.DateOfArrival = DateTimeOffset.Parse(model.DateOfArrivalLocalString);
				data.DateOfDeparture = DateTimeOffset.Parse(model.DateOfDepartureLocalString);

                // todo: 1. test
				// Update must be before SetState
                // todo: 2. use update file methods
				_awbRepository.Update(data, model.GTDFile, model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile,
											 model.AWBFile);

                // todo: 1. test
				if (IsReadyForCargoAtCustomsStateId(data))
				{
					if (!IsReadyForCargoAtCustomsStateId(data) && data.StateId != _stateConfig.CargoIsCustomsClearedStateId)
						SetStateImpl(id, _stateConfig.CargoAtCustomsStateId, data.StateId);
				}

				_unitOfWork.SaveChanges();

				ts.Complete();
			}
		}

		public void Update(long id, BrockerAWBModel model)
		{
			using (var ts = _unitOfWork.StartTransaction())
			{
				var data = _awbRepository.Get(id).First();

                // todo: 1. test
				if (data.StateId == _stateConfig.CargoIsCustomsClearedStateId)
				{
					throw new UnexpectedStateException(data.StateId,
													   "Can't update an AWB while it has the state "
													   + _stateConfig.CargoIsCustomsClearedStateId.ToString(CultureInfo.InvariantCulture));
				}

                // todo: 2. mapper
				data.PackingFileName = model.PackingFileName;
				data.InvoiceFileName = model.InvoiceFileName;
				data.GTD = model.GTD;
				data.GTDFileName = model.GTDFileName;
				data.GTDAdditionalFileName = model.GTDAdditionalFileName;

                // todo: 1. test
                // todo: 2. use update file methods
				// Update must be before SetState
				_awbRepository.Update(data, model.GTDFile, model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile,
											 null);

                // todo: 1. test
				if (IsReadyForCargoAtCustomsStateId(data))
				{
					if (!IsReadyForCargoAtCustomsStateId(data) && data.StateId != _stateConfig.CargoIsCustomsClearedStateId)
						SetStateImpl(id, _stateConfig.CargoAtCustomsStateId, data.StateId);
				}

				_unitOfWork.SaveChanges();

				ts.Complete();
			}
		}

		public void Delete(long id)
		{
			using (var ts = _unitOfWork.StartTransaction())
			{
				var applicationDatas = _applicationRepository.GetByAirWaybill(id);

                // todo: 1. test sets
				foreach (var app in applicationDatas)
				{
					_applicationUpdater.SetAirWaybill(app.Id, null);
				}

				_awbRepository.Delete(id);

				_unitOfWork.SaveChanges();

				ts.Complete();
			}
		}

		public void SetState(long airWaybillId, long stateId)
		{
			var data = _awbRepository.Get(airWaybillId).First();

			SetStateImpl(airWaybillId, stateId, data.StateId);
		}

		private static bool IsReadyForCargoAtCustomsStateId(AirWaybillData model)
		{
			return !model.GTD.IsNullOrWhiteSpace();
		}

		private void SetStateImpl(long airWaybillId, long newStateId, long currentStateId)
		{
			var applications = _applicationRepository.GetByAirWaybill(airWaybillId);

			using (var ts = _unitOfWork.StartTransaction())
			{
				_awbRepository.SetState(airWaybillId, newStateId);

                // todo: 1. test sets
				foreach (var application in applications)
				{
					if (currentStateId == application.StateId)
					{
						_applicationManager.SetState(application.Id, newStateId);
					}
				}

				_unitOfWork.SaveChanges();

				ts.Complete();
			}
		}
	}
}