using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
    internal sealed class ApplicationListPresenter : IApplicationListPresenter
	{
		private readonly IApplicationRepository _applicationRepository;
	    private readonly IApplicationListItemMapper _itemMapper;
	    private readonly IApplicationGrouper _applicationGrouper;
		private readonly IIdentityService _identity;	
		private readonly IStateService _stateService;

		public ApplicationListPresenter(
			IApplicationRepository applicationRepository,
            IApplicationListItemMapper itemMapper,
			IStateService stateService, 
			IIdentityService identity,
			IApplicationGrouper applicationGrouper)
		{
			_applicationRepository = applicationRepository;
		    _itemMapper = itemMapper;
		    _stateService = stateService;
			_identity = identity;
			_applicationGrouper = applicationGrouper;
		}

        // todo: 1. test
		public ApplicationListCollection List(int take, int skip, Order[] groups)
		{
			var stateIds = _stateService.GetVisibleStates();

			var isClient = _identity.IsInRole(RoleType.Client);

			var orders = PrepareOrders(groups);

			var data = _applicationRepository.List(take, skip, stateIds, orders, isClient ? _identity.Id : null);

            var applications = _itemMapper.GetListItems(data);

			return groups == null || groups.Length == 0
				? new ApplicationListCollection
				{
					Data = applications,
					Total = _applicationRepository.Count(stateIds, isClient ? _identity.Id : null),
				}
				: new ApplicationListCollection
				{
					Total = _applicationRepository.Count(stateIds, isClient ? _identity.Id : null),
					Groups = _applicationGrouper.Group(applications, groups),
				};
		}

		private static Order[] PrepareOrders(IEnumerable<Order> orders)
		{
			var byId = new[] { new Order { Desc = true, OrderType = OrderType.Id } };

			return orders == null ? byId : orders.Concat(byId).ToArray();
		}
	}
}