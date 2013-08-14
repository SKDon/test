using System.Linq;
using Alicargo.Core.Enums;
using Alicargo.Core.Exceptions;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.AirWaybill;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.AirWaybill
{
	public sealed class AwbPresenter : IAwbPresenter
	{
		private readonly IAirWaybillRepository _airWaybillRepository;
		private readonly IBrockerRepository _brockerRepository;
		private readonly IStateService _stateService;
		private readonly IIdentityService _identityService;

		public AwbPresenter(
			IAirWaybillRepository airWaybillRepository,
			IBrockerRepository brockerRepository,
			IStateService stateService,
			IIdentityService identityService)
		{
			_airWaybillRepository = airWaybillRepository;
			_brockerRepository = brockerRepository;
			_stateService = stateService;
			_identityService = identityService;
		}

		public ListCollection<AirWaybillListItem> List(int take, int skip)
		{
			long? brockerId = null;
			if (_identityService.IsInRole(RoleType.Brocker) && _identityService.Id.HasValue)
			{
				var brocker = _brockerRepository.GetByUserId(_identityService.Id.Value);
				brockerId = brocker.Id;
			}

			var data = _airWaybillRepository.GetRange(skip, take, brockerId);
			var ids = data.Select(x => x.Id).ToArray();

			var aggregates = _airWaybillRepository.GetAggregate(ids)
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

			var total = _airWaybillRepository.Count(brockerId);

			return new ListCollection<AirWaybillListItem> { Data = items, Total = total };
		}

		public AirWaybillEditModel Get(long id)
		{
			var data = _airWaybillRepository.Get(id).FirstOrDefault();

			if (data == null) throw new EntityNotFoundException("Refarence: " + id);

			var model = new AirWaybillEditModel
			{
				PackingFileName = data.PackingFileName,
				InvoiceFileName = data.InvoiceFileName,
				PackingFile = null,
				AWBFileName = data.AWBFileName,
				ArrivalAirport = data.ArrivalAirport,
				Bill = data.Bill,
				GTDAdditionalFileName = data.GTDAdditionalFileName,
				DateOfArrivalLocalString = data.DateOfArrival.LocalDateTime.ToShortDateString(),
				DateOfDepartureLocalString = data.DateOfDeparture.LocalDateTime.ToShortDateString(),
				DepartureAirport = data.DepartureAirport,
				GTD = data.GTD,
				GTDFileName = data.GTDFileName,
				InvoiceFile = null,
				AWBFile = null,
				BrockerId = data.BrockerId,
				GTDAdditionalFile = null,
				GTDFile = null
			};

			return model;
		}
	}
}
