using System.Collections.Generic;
using System.Linq;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	internal sealed class ApplicationListPresenter : IApplicationListPresenter
	{
		private readonly IApplicationRepository _applications;
		private readonly IApplicationGrouper _grouper;
		private readonly IApplicationListItemMapper _mapper;
		private readonly IStateFilter _stateFilter;
		private readonly IStateConfig _stateConfig;

		public ApplicationListPresenter(
			IApplicationRepository applications,
			IApplicationListItemMapper mapper,
			IStateFilter stateFilter,
			IStateConfig stateConfig,
			IApplicationGrouper grouper)
		{
			_applications = applications;
			_mapper = mapper;
			_stateFilter = stateFilter;
			_stateConfig = stateConfig;
			_grouper = grouper;
		}

		public ApplicationListCollection List(string language, int? take = null, int skip = 0, Order[] groups = null, long? clientId = null, long? senderId = null, bool? isForwarder = null)
		{
			long total;
			var data = GetList(take, skip, groups, clientId, senderId, isForwarder, out total);

			var applications = _mapper.Map(data, language);

			if (groups == null || groups.Length == 0)
				return new ApplicationListCollection
				{
					Data = applications,
					Total = total,
				};

			return GetGroupedResult(groups, applications, total);
		}

		private ApplicationListCollection GetGroupedResult(IEnumerable<Order> groups, ApplicationListItem[] applications, long total)
		{
			var applicationGroups = _grouper.Group(applications, groups.Select(x => x.OrderType).ToArray());

			OrderBottomGroupByClient(applicationGroups);

			return new ApplicationListCollection
			{
				Total = total,
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

		private ApplicationListItemData[] GetList(int? take, int skip, IEnumerable<Order> groups, long? clientId, long? senderId, bool? isForwarder, out long total)
		{
			var stateIds = _stateFilter.GetStateVisibility();

			var orders = GetOrders(groups);

			var cargoReceivedStateId = isForwarder.HasValue && isForwarder.Value
				? _stateConfig.CargoReceivedStateId
				: (long?)null;

			total = _applications.Count(stateIds, clientId, null, null, cargoReceivedStateId, _stateConfig.CargoReceivedDaysToShow);

			return _applications.List(stateIds, orders, take, skip, clientId, senderId, null,
				cargoReceivedStateId, _stateConfig.CargoReceivedDaysToShow);
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