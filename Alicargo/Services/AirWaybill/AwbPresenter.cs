using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Services;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.AirWaybill;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.AirWaybill
{
	internal sealed class AwbPresenter : IAwbPresenter
	{
		private readonly IAwbRepository _awbRepository;
		private readonly IBrokerRepository _brokerRepository;
		private readonly IStateService _stateService;

		public AwbPresenter(
			IAwbRepository awbRepository,
			IBrokerRepository brokerRepository,
			IStateService stateService)
		{
			_awbRepository = awbRepository;
			_brokerRepository = brokerRepository;
			_stateService = stateService;
		}

		public ListCollection<AirWaybillListItem> List(int take, int skip, long? brokerId)
		{
			var data = _awbRepository.GetRange(take, skip, brokerId);
			var ids = data.Select(x => x.Id).ToArray();

			var aggregates = _awbRepository.GetAggregate(ids)
										   .ToDictionary(x => x.AirWaybillId, x => x);

			var localizedStates = _stateService.GetLocalizedDictionary();

			var items = data.Select(x => new AirWaybillListItem
				{
					Id = x.Id,
					PackingFileName = x.PackingFileName,
					InvoiceFileName = x.InvoiceFileName,
					State = new ApplicationStateModel
						{
							StateName = localizedStates[x.StateId],
							StateId = x.StateId
						},
					AWBFileName = x.AWBFileName,
					ArrivalAirport = x.ArrivalAirport,
					Bill = x.Bill,
					CreationTimestampLocalString = x.CreationTimestamp.ToLocalShortDateString(),
					DateOfArrivalLocalString = x.DateOfArrival.ToLocalShortDateString(),
					DateOfDepartureLocalString = x.DateOfDeparture.ToLocalShortDateString(),
					StateChangeTimestampLocalString = x.StateChangeTimestamp.ToLocalShortDateString(),
					DepartureAirport = x.DepartureAirport,
					GTD = x.GTD,
					GTDAdditionalFileName = x.GTDAdditionalFileName,
					GTDFileName = x.GTDFileName,
					TotalCount = aggregates[x.Id].TotalCount,
					TotalWeight = aggregates[x.Id].TotalWeight,
					AdditionalCost = x.AdditionalCost,
					TotalCostOfSenderForWeight = x.TotalCostOfSenderForWeight,
					BrokerCost = x.BrokerCost,
					CustomCost = x.CustomCost,
					FlightCost = x.FlightCost,
				}).ToArray();

			var total = _awbRepository.Count(brokerId);

			return new ListCollection<AirWaybillListItem> { Data = items, Total = total };
		}

		public AirWaybillEditModel Get(long id)
		{
			var data = _awbRepository.Get(id).FirstOrDefault();

			if (data == null) throw new EntityNotFoundException("Refarence: " + id);

			return Map(data);
		}

		public SenderAwbModel GetSenderAwbModel(long id)
		{
			var data = _awbRepository.Get(id).First();

			return new SenderAwbModel
			{
				AWBFile = null,
				AWBFileName = data.AWBFileName,
				ArrivalAirport = data.ArrivalAirport,
				Bill = data.Bill,
				BrokerId = data.BrokerId,
				DateOfArrivalLocalString = data.DateOfArrival.ToLocalShortDateString(),
				DateOfDepartureLocalString = data.DateOfDeparture.ToLocalShortDateString(),
				DepartureAirport = data.DepartureAirport,
				PackingFile = null,
				PackingFileName = data.PackingFileName,
				FlightCost = data.FlightCost,
				TotalCostOfSenderForWeight = data.TotalCostOfSenderForWeight
			};
		}

		public static AirWaybillEditModel Map(AirWaybillData data)
		{
			return new AirWaybillEditModel
			{
				PackingFileName = data.PackingFileName,
				InvoiceFileName = data.InvoiceFileName,
				PackingFile = null,
				AWBFileName = data.AWBFileName,
				ArrivalAirport = data.ArrivalAirport,
				Bill = data.Bill,
				GTDAdditionalFileName = data.GTDAdditionalFileName,
				DateOfArrivalLocalString = data.DateOfArrival.ToLocalShortDateString(),
				DateOfDepartureLocalString = data.DateOfDeparture.ToLocalShortDateString(),
				DepartureAirport = data.DepartureAirport,
				GTD = data.GTD,
				GTDFileName = data.GTDFileName,
				InvoiceFile = null,
				AWBFile = null,
				BrokerId = data.BrokerId,
				GTDAdditionalFile = null,
				GTDFile = null,
				AdditionalCost = data.AdditionalCost,
				BrokerCost = data.BrokerCost,
				CustomCost = data.CustomCost,
				FlightCost = data.FlightCost,
				TotalCostOfSenderForWeight = data.TotalCostOfSenderForWeight
			};
		}

		public AirWaybillData GetData(long id)
		{
			return _awbRepository.Get(id).First();
		}

		public AirWaybillAggregate GetAggregate(long id)
		{
			return _awbRepository.GetAggregate(id).First();
		}

		public BrokerData GetBroker(long brokerId)
		{
			return _brokerRepository.Get(brokerId);
		}
	}
}