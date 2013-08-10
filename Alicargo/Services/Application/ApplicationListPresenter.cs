using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Helpers;
using Alicargo.Core.Enums;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	public sealed class ApplicationListPresenter : IApplicationListPresenter
	{
		private readonly IApplicationRepository _applicationRepository;
		private readonly IApplicationGrouper _applicationGrouper;
		private readonly ICountryRepository _countryRepository;
		private readonly IIdentityService _identity;
		private readonly IAirWaybillRepository _AirWaybillRepository;
		private readonly IStateService _stateService;
		private readonly IStateConfig _stateConfig;

		public ApplicationListPresenter(IApplicationRepository applicationRepository,
			IStateService stateService, IAirWaybillRepository AirWaybillRepository, 
			IIdentityService identity, IApplicationGrouper applicationGrouper, 
			ICountryRepository countryRepository, IStateConfig stateConfig)
		{
			_applicationRepository = applicationRepository;
			_stateService = stateService;
			_identity = identity;
			_applicationGrouper = applicationGrouper;
			_countryRepository = countryRepository;
			_AirWaybillRepository = AirWaybillRepository;
			_stateConfig = stateConfig;
		}

		public ApplicationListCollection List(int take, int skip, Order[] groups)
		{
			var stateIds = _stateService.GetVisibleStates();

			var isClient = _identity.IsInRole(RoleType.Client);

			var orders = PrepareOrders(groups);

			var data = _applicationRepository.List(take, skip, stateIds, orders, isClient ? _identity.Id : null);

			var applications = GetListItems(data);

			if (groups == null || groups.Length == 0)
			{
				return new ApplicationListCollection
				{
					Data = applications,
					Total = _applicationRepository.Count(stateIds, isClient ? _identity.Id : null),
				};
			}

			return GetGroupedApplicationListCollection(groups, stateIds, isClient, applications);
		}

		private ApplicationListItem[] GetListItems(IEnumerable<ApplicationListItemData> data)
		{
			var countries = _countryRepository.Get().ToDictionary(x => x.Id, x => x.Name[_identity.TwoLetterISOLanguageName]);
			var localizedStates = _stateService.GetLocalizedDictionary();
			var availableStates = _stateService.GetAvailableStatesToSet();

			var applications = data.Select(x => new ApplicationListItem
			{
				Data = x,
				CountryName = x.CountryId.HasValue ? countries[x.CountryId.Value] : null,
				State = new ApplicationStateModel
				{
					StateId = x.StateId,
					StateName = localizedStates[x.StateId]
				},
				CanClose = x.StateId == _stateConfig.CargoOnTransitStateId, // todo: test 1.
				CanSetState = availableStates.Contains(x.StateId) // todo: test 1.
			}).ToArray();
			return applications;
		}

		private ApplicationListCollection GetGroupedApplicationListCollection(Order[] groups, 
			IEnumerable<long> stateIds, bool isClient, ApplicationListItem[] applications)
		{
			var ids = applications.Select(x => x.Data.AirWaybillId ?? 0).ToArray();
			var AirWaybills = _AirWaybillRepository.Get(ids).ToDictionary(x => x.Id, x => x);

			return new ApplicationListCollection
			{
				Total = _applicationRepository.Count(stateIds, isClient ? _identity.Id : null),
				Groups = _applicationGrouper.Group(applications, groups, AirWaybills),
			};
		}

		private static Order[] PrepareOrders(IEnumerable<Order> orders)
		{
			var byId = new[] { new Order { Desc = true, OrderType = OrderType.Id } };

			return orders == null ? byId : orders.Concat(byId).ToArray();
		}
	}
}