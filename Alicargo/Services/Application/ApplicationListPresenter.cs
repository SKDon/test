using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
    internal sealed class ApplicationListPresenter : IApplicationListPresenter
    {
        private readonly IApplicationRepository _applications;
        private readonly IApplicationGrouper _grouper;
        private readonly IApplicationListItemMapper _itemMapper;
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

        public ApplicationListCollection List(int? take = null, int skip = 0, Order[] groups = null, long? clientId = null,
            long? senderId = null)
        {
            var stateIds = _stateService.GetVisibleStates();

            var data = GetList(take, skip, groups, clientId, senderId, stateIds);

            var applications = _itemMapper.Map(data);

            if (groups == null || groups.Length == 0)
                return new ApplicationListCollection
                {
                    Data = applications,
                    Total = _applications.Count(stateIds, clientId),
                };

            return GetGroupedResult(groups, clientId, applications, stateIds);
        }

        private ApplicationListCollection GetGroupedResult(IEnumerable<Order> groups, long? clientId, ApplicationListItem[] applications,
            long[] stateIds)
        {
            var applicationGroups = _grouper.Group(applications, groups.Select(x => x.OrderType).ToArray());

            OrderBottomGroupByClient(applicationGroups);

            return new ApplicationListCollection
            {
                Total = _applications.Count(stateIds, clientId),
                Groups = applicationGroups,
            };
        }

        private static void OrderBottomGroupByClient(IEnumerable<ApplicationGroup> applicationGroups)
        {
            foreach (var group in applicationGroups)
            {
                if (@group.hasSubgroups)
                {
                    OrderBottomGroupByClient(@group.items.Cast<ApplicationGroup>());
                }
                else
                {
                    var items = @group.items.Cast<ApplicationListItem>();

                    @group.items = @group.value == ""
                        ? items.OrderByDescending(x => x.Id).ToArray<object>()
                        : items.OrderBy(x => x.ClientNic).ThenByDescending(x => x.Id).ToArray<object>();
                }
            }
        }

        private ApplicationListItemData[] GetList(int? take, int skip, IEnumerable<Order> groups, long? clientId,
            long? senderId, long[] stateIds)
        {
            var orders = GetOrders(groups);

            return _applications.List(stateIds, orders, take, skip, clientId, senderId);
        }

        private static Order[] GetOrders(IEnumerable<Order> orders)
        {
            if (orders != null) return orders.ToArray();

            return new[]
			{
				new Order
				{
					Desc = true,
					OrderType = OrderType.AirWaybill
				}
			};
        }
    }
}