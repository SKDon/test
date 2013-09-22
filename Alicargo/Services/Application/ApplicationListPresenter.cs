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
        private readonly IApplicationRepository _applicationRepository;
        private readonly IApplicationListItemMapper _itemMapper;
        private readonly IApplicationGrouper _applicationGrouper;
        private readonly IStateService _stateService;

        public ApplicationListPresenter(
            IApplicationRepository applicationRepository,
            IApplicationListItemMapper itemMapper,
            IStateService stateService,
            IApplicationGrouper applicationGrouper)
        {
            _applicationRepository = applicationRepository;
            _itemMapper = itemMapper;
            _stateService = stateService;
            _applicationGrouper = applicationGrouper;
        }

		public ApplicationListCollection List(int? take = null, int skip = 0, Order[] groups = null, long? clientId = null)
        {
            var stateIds = _stateService.GetVisibleStates();

            var orders = PrepareOrders(groups);

			var data = _applicationRepository.List(take, skip, stateIds, orders, clientId);

            var applications = _itemMapper.Map(data);

            return groups == null || groups.Length == 0
                ? new ApplicationListCollection
                {
                    Data = applications,
					Total = _applicationRepository.Count(stateIds, clientId),
                }
                : new ApplicationListCollection
                {
					Total = _applicationRepository.Count(stateIds, clientId),
                    Groups = _applicationGrouper.Group(applications, groups.Select(x => x.OrderType).ToArray()),
                };
        }

        private static Order[] PrepareOrders(IEnumerable<Order> orders)
        {
            var byId = new[] { new Order { Desc = true, OrderType = OrderType.Id } };

            return orders == null ? byId : orders.Concat(byId).ToArray();
        }
    }
}