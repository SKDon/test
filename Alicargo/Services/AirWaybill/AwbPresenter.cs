using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.AirWaybill;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.AirWaybill
{
	public sealed class AwbPresenter : IAwbPresenter
	{
		private readonly IAwbRepository _awbRepository;
		private readonly IBrockerRepository _brockerRepository;
		private readonly IStateService _stateService;
		private readonly IIdentityService _identityService;

		public AwbPresenter(
			IAwbRepository awbRepository,
			IBrockerRepository brockerRepository,
			IStateService stateService,
			IIdentityService identityService)
		{
			_awbRepository = awbRepository;
			_brockerRepository = brockerRepository;
			_stateService = stateService;
			_identityService = identityService;
		}

        // todo: 1. test
		public ListCollection<AirWaybillListItem> List(int take, int skip)
		{
			long? brockerId = null;
			if (_identityService.IsInRole(RoleType.Brocker) && _identityService.Id.HasValue)
			{
				var brocker = _brockerRepository.GetByUserId(_identityService.Id.Value);
				brockerId = brocker.Id;
			}

			var data = _awbRepository.GetRange(skip, take, brockerId);
			var ids = data.Select(x => x.Id).ToArray();

			var aggregates = _awbRepository.GetAggregate(ids)
												  .ToDictionary(x => x.AirWaybillId, x => x);

			var localizedStates = _stateService.GetLocalizedDictionary();

            // todo: 2. mapper
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
				CreationTimestampLocalString = x.CreationTimestamp.LocalDateTime.ToShortDateString(),
				DateOfArrivalLocalString = x.DateOfArrival.LocalDateTime.ToShortDateString(),
				DateOfDepartureLocalString = x.DateOfDeparture.LocalDateTime.ToShortDateString(),
				StateChangeTimestampLocalString = x.StateChangeTimestamp.LocalDateTime.ToShortDateString(),
				DepartureAirport = x.DepartureAirport,
				GTD = x.GTD,
				GTDAdditionalFileName = x.GTDAdditionalFileName,
				GTDFileName = x.GTDFileName,
				TotalCount = aggregates[x.Id].TotalCount,
				TotalWeight = aggregates[x.Id].TotalWeight
			}).ToArray();

			var total = _awbRepository.Count(brockerId);

			return new ListCollection<AirWaybillListItem> { Data = items, Total = total };
		}

		public AirWaybillEditModel Get(long id)
		{
			var data = _awbRepository.Get(id).FirstOrDefault();

			if (data == null) throw new EntityNotFoundException("Refarence: " + id);

			return AirWaybillEditModel.GetModel(data);
		}

		public AirWaybillData GetData(long id)
		{
			return _awbRepository.Get(id).First();
		}

		public AirWaybillAggregate GetAggregate(long id)
		{
			return _awbRepository.GetAggregate(id).First();
		}

		public BrockerData GetBrocker(long brockerId)
		{
			return _brockerRepository.Get(brockerId);
		}
	}
}
