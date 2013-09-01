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

		public static void Map(AirWaybillEditModel model, AirWaybillData data)
		{
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

		private static void Map(SenderAwbModel model, AirWaybillData data)
		{
			data.Bill = model.Bill;
			data.ArrivalAirport = model.ArrivalAirport;
			data.DepartureAirport = model.DepartureAirport;
			data.DateOfArrival = DateTimeOffset.Parse(model.DateOfArrivalLocalString);
			data.DateOfDeparture = DateTimeOffset.Parse(model.DateOfDepartureLocalString);
			data.BrockerId = model.BrockerId;
			data.PackingFileName = model.PackingFileName;
			data.AWBFileName = model.AWBFileName;
		}

		public static void Map(BrockerAwbModel model, AirWaybillData data)
		{
			data.PackingFileName = model.PackingFileName;
			data.InvoiceFileName = model.InvoiceFileName;
			data.GTD = model.GTD;
			data.GTDFileName = model.GTDFileName;
			data.GTDAdditionalFileName = model.GTDAdditionalFileName;
		}
	}
}