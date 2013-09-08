using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.AirWaybill
{
	internal sealed class AwbManager : IAwbManager
	{
		private readonly IApplicationAwbManager _applicationAwbManager;
		private readonly IApplicationRepository _applicationRepository;
		private readonly IApplicationUpdateRepository _applicationUpdater;
		private readonly IAwbRepository _awbRepository;
		private readonly IStateConfig _stateConfig;
		private readonly IUnitOfWork _unitOfWork;

		public AwbManager(
			IAwbRepository awbRepository,
			IApplicationAwbManager applicationAwbManager,
			IApplicationRepository applicationRepository,
			IUnitOfWork unitOfWork,
			IStateConfig stateConfig,
			IApplicationUpdateRepository applicationUpdater)
		{
			_awbRepository = awbRepository;
			_applicationAwbManager = applicationAwbManager;
			_applicationRepository = applicationRepository;
			_unitOfWork = unitOfWork;
			_stateConfig = stateConfig;
			_applicationUpdater = applicationUpdater;
		}

		public long Create(long applicationId, AirWaybillEditModel model)
		{
			if (model.GTD != null)
			{
				throw new InvalidLogicException("GTD data should be defined by update");
			}

			var data = Map(model, _stateConfig.CargoIsFlewStateId);

			var id = _awbRepository.Add(data, model.GTDFile, model.GTDAdditionalFile, model.PackingFile,
										model.InvoiceFile, model.AWBFile);

			_unitOfWork.SaveChanges();

			_applicationAwbManager.SetAwb(applicationId, id());

			return id();
		}

		public long Create(long applicationId, SenderAwbModel model)
		{
			var data = Map(model, _stateConfig.CargoIsFlewStateId);

			var id = _awbRepository.Add(data, null, null, model.PackingFile, null, model.AWBFile);

			_unitOfWork.SaveChanges();

			_applicationAwbManager.SetAwb(applicationId, id());

			return id();
		}

		public void Delete(long awbId)
		{
			var applicationDatas = _applicationRepository.GetByAirWaybill(awbId);

			foreach (var app in applicationDatas)
			{
				_applicationUpdater.SetAirWaybill(app.Id, null);
			}

			_awbRepository.Delete(awbId);

			_unitOfWork.SaveChanges();
		}

		public static AirWaybillData Map(SenderAwbModel model, long cargoIsFlewStateId)
		{
			return new AirWaybillData
			{
				StateId = cargoIsFlewStateId,
				CreationTimestamp = DateTimeOffset.UtcNow,
				StateChangeTimestamp = DateTimeOffset.UtcNow,
				Id = 0,
				PackingFileName = model.PackingFileName,
				InvoiceFileName = null,
				AWBFileName = model.AWBFileName,
				ArrivalAirport = model.ArrivalAirport,
				Bill = model.Bill,
				DepartureAirport = model.DepartureAirport,
				BrokerId = model.BrokerId,
				DateOfArrival = DateTimeOffset.Parse(model.DateOfArrivalLocalString),
				DateOfDeparture = DateTimeOffset.Parse(model.DateOfDepartureLocalString),
				GTD = null,
				GTDAdditionalFileName = null,
				GTDFileName = null,
				AdditionalCost = null,
				BrokerCost = null,
				CustomCost = null,
				FlightCost = model.FlightCost
			};
		}

		public static AirWaybillData Map(AirWaybillEditModel model, long cargoIsFlewStateId)
		{
			return new AirWaybillData
			{
				StateId = cargoIsFlewStateId,
				CreationTimestamp = DateTimeOffset.UtcNow,
				StateChangeTimestamp = DateTimeOffset.UtcNow,
				Id = 0,
				PackingFileName = model.PackingFileName,
				InvoiceFileName = model.InvoiceFileName,
				AWBFileName = model.AWBFileName,
				ArrivalAirport = model.ArrivalAirport,
				Bill = model.Bill,
				DepartureAirport = model.DepartureAirport,
				BrokerId = model.BrokerId,
				DateOfArrival = DateTimeOffset.Parse(model.DateOfArrivalLocalString),
				DateOfDeparture = DateTimeOffset.Parse(model.DateOfDepartureLocalString),
				GTD = null,
				GTDAdditionalFileName = model.GTDAdditionalFileName,
				GTDFileName = model.GTDFileName,
				AdditionalCost = model.AdditionalCost,
				BrokerCost = model.BrokerCost,
				CustomCost = model.CustomCost,
				FlightCost = model.FlightCost
			};
		}
	}
}