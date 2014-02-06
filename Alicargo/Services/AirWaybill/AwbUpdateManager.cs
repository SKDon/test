using System;
using System.Globalization;
using System.Linq;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.AirWaybill
{
	internal sealed class AwbUpdateManager : IAwbUpdateManager
	{
		private readonly IAwbRepository _awbRepository;
		private readonly IStateConfig _stateConfig;
		private readonly IUnitOfWork _unitOfWork;

		public AwbUpdateManager(
			IAwbRepository awbRepository,
			IUnitOfWork unitOfWork,
			IStateConfig stateConfig)
		{
			_awbRepository = awbRepository;
			_unitOfWork = unitOfWork;
			_stateConfig = stateConfig;
		}

		public void Update(long id, AwbAdminModel model)
		{
			var data = _awbRepository.Get(id).First();

			Map(model, data);

			_awbRepository.Update(data, model.GTDFile, model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile,
								  model.AWBFile);

			_unitOfWork.SaveChanges();
		}

		public void Update(long id, AwbBrokerModel model)
		{
			var data = _awbRepository.Get(id).First();

			if (data.StateId == _stateConfig.CargoIsCustomsClearedStateId)
			{
				throw new UnexpectedStateException(
					data.StateId,
					"Can't update an AWB while it has the state "
					+ _stateConfig.CargoIsCustomsClearedStateId.ToString(CultureInfo.InvariantCulture));
			}

			Map(model, data);

			_awbRepository.Update(data, model.GTDFile, model.GTDAdditionalFile,
								  model.PackingFile, model.InvoiceFile, null);

			_unitOfWork.SaveChanges();
		}

		public void Update(long id, AwbSenderModel model)
		{
			var data = _awbRepository.Get(id).First();

			Map(model, data);

			_awbRepository.Update(data, null, null, model.PackingFile, null, model.AWBFile);

			_unitOfWork.SaveChanges();
		}

		public void SetAdditionalCost(long awbId, decimal? additionalCost)
		{
			_awbRepository.SetAdditionalCost(awbId, additionalCost);

			_unitOfWork.SaveChanges();
		}

		public static void Map(AwbAdminModel @from, AirWaybillData to)
		{
			to.PackingFileName = @from.PackingFileName;
			to.InvoiceFileName = @from.InvoiceFileName;
			to.AWBFileName = @from.AWBFileName;
			to.ArrivalAirport = @from.ArrivalAirport;
			to.Bill = @from.Bill;
			to.DepartureAirport = @from.DepartureAirport;
			to.GTD = @from.GTD;
			to.GTDFileName = @from.GTDFileName;
			to.BrokerId = @from.BrokerId;
			to.GTDAdditionalFileName = @from.GTDAdditionalFileName;
			to.DateOfArrival = DateTimeOffset.Parse(@from.DateOfArrivalLocalString);
			to.DateOfDeparture = DateTimeOffset.Parse(@from.DateOfDepartureLocalString);
			to.AdditionalCost = @from.AdditionalCost;
			to.BrokerCost = @from.BrokerCost;
			to.CustomCost = @from.CustomCost;
			to.FlightCost = @from.FlightCost;
			to.TotalCostOfSenderForWeight = from.TotalCostOfSenderForWeight;
		}

		private static void Map(AwbSenderModel @from, AirWaybillData to)
		{
			to.Bill = @from.Bill;
			to.ArrivalAirport = @from.ArrivalAirport;
			to.DepartureAirport = @from.DepartureAirport;
			to.DateOfArrival = DateTimeOffset.Parse(@from.DateOfArrivalLocalString);
			to.DateOfDeparture = DateTimeOffset.Parse(@from.DateOfDepartureLocalString);
			to.BrokerId = @from.BrokerId;
			to.PackingFileName = @from.PackingFileName;
			to.AWBFileName = @from.AWBFileName;
			to.FlightCost = @from.FlightCost;
			to.TotalCostOfSenderForWeight = from.TotalCostOfSenderForWeight;
		}

		public static void Map(AwbBrokerModel @from, AirWaybillData to)
		{
			to.PackingFileName = @from.PackingFileName;
			to.InvoiceFileName = @from.InvoiceFileName;
			to.GTD = @from.GTD;
			to.GTDFileName = @from.GTDFileName;
			to.GTDAdditionalFileName = @from.GTDAdditionalFileName;
			to.BrokerCost = @from.BrokerCost;
			to.CustomCost = @from.CustomCost;
		}
	}
}