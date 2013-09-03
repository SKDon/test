using System;
using System.Globalization;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
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

		public void Update(long id, AirWaybillEditModel model)
		{
			var data = _awbRepository.Get(id).First();

			Map(model, data);

			// todo: 3. use update file methods
			_awbRepository.Update(data, model.GTDFile, model.GTDAdditionalFile, model.PackingFile, model.InvoiceFile,
								  model.AWBFile);

			_unitOfWork.SaveChanges();
		}

		public void Update(long id, BrockerAwbModel model)
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

			// todo: 3. use update file methods
			_awbRepository.Update(data, model.GTDFile, model.GTDAdditionalFile,
								  model.PackingFile, model.InvoiceFile, null);

			_unitOfWork.SaveChanges();
		}

		public void Update(long id, SenderAwbModel model)
		{
			var data = _awbRepository.Get(id).First();

			Map(model, data);

			// todo: 3. use update file methods
			_awbRepository.Update(data, null, null, model.PackingFile, null, model.AWBFile);

			_unitOfWork.SaveChanges();
		}

		public static void Map(AirWaybillEditModel @from, AirWaybillData to)
		{
			to.PackingFileName = @from.PackingFileName;
			to.InvoiceFileName = @from.InvoiceFileName;
			to.AWBFileName = @from.AWBFileName;
			to.ArrivalAirport = @from.ArrivalAirport;
			to.Bill = @from.Bill;
			to.DepartureAirport = @from.DepartureAirport;
			to.GTD = @from.GTD;
			to.GTDFileName = @from.GTDFileName;
			to.BrockerId = @from.BrockerId;
			to.GTDAdditionalFileName = @from.GTDAdditionalFileName;
			to.DateOfArrival = DateTimeOffset.Parse(@from.DateOfArrivalLocalString);
			to.DateOfDeparture = DateTimeOffset.Parse(@from.DateOfDepartureLocalString);
			to.AdditionalСost = @from.AdditionalСost;
			to.BrokerСost = @from.BrokerСost;
			to.CustomСost = @from.CustomСost;
			to.FlightСost = @from.FlightСost;
			to.ForwarderСost = @from.ForwarderСost;
		}

		private static void Map(SenderAwbModel @from, AirWaybillData to)
		{
			to.Bill = @from.Bill;
			to.ArrivalAirport = @from.ArrivalAirport;
			to.DepartureAirport = @from.DepartureAirport;
			to.DateOfArrival = DateTimeOffset.Parse(@from.DateOfArrivalLocalString);
			to.DateOfDeparture = DateTimeOffset.Parse(@from.DateOfDepartureLocalString);
			to.BrockerId = @from.BrockerId;
			to.PackingFileName = @from.PackingFileName;
			to.AWBFileName = @from.AWBFileName;
			to.FlightСost = @from.FlightСost;
		}

		public static void Map(BrockerAwbModel @from, AirWaybillData to)
		{
			to.PackingFileName = @from.PackingFileName;
			to.InvoiceFileName = @from.InvoiceFileName;
			to.GTD = @from.GTD;
			to.GTDFileName = @from.GTDFileName;
			to.GTDAdditionalFileName = @from.GTDAdditionalFileName;
		}
	}
}