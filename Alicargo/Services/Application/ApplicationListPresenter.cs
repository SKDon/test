using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
    internal sealed class ApplicationListPresenter : IApplicationListPresenter
    {
	    private readonly IApplicationListItemMapper _itemMapper;
	    private readonly IApplicationGrouper _grouper;
	    private readonly IApplicationRepository _applications;
	    private readonly IStateService _stateService;

        public ApplicationListPresenter(
            IApplicationRepository applications,
            IApplicationListItemMapper itemMapper,
            IStateService stateService,
            IApplicationGrouper grouper)
        {
            _applications = applications;
            _itemMapper = itemMapper;
            _stateService = stateService;
            _grouper = grouper;
        }

		public ApplicationListCollection List(int? take = null, int skip = 0, Order[] groups = null, long? clientId = null, long? senderId = null)
        {
            var stateIds = _stateService.GetVisibleStates();

            var orders = PrepareOrders(groups);

			var data = _applications.List(take, skip, stateIds, orders, clientId, senderId);

            var applications = _itemMapper.Map(data);

            return groups == null || groups.Length == 0
                ? new ApplicationListCollection
                {
                    Data = applications,
					Total = _applications.Count(stateIds, clientId),
                }
                : new ApplicationListCollection
                {
					Total = _applications.Count(stateIds, clientId),
                    Groups = _grouper.Group(applications, groups.Select(x => x.OrderType).ToArray()),
                };
        }

        private static Order[] PrepareOrders(IEnumerable<Order> orders)
        {
            var byId = new[] { new Order { Desc = true, OrderType = OrderType.Id } };

            return orders == null ? byId : orders.Concat(byId).ToArray();
        }
    }
}