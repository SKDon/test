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
		private readonly IStateConfig _stateConfig;
		private readonly IStateFilter _stateFilter;

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

		public ApplicationListCollection List(
			string language, int? take = null, int skip = 0, Order[] groups = null,
			long? clientId = null, long? senderId = null, long? forwarderId = null, long? carrierId = null)
		{
			long total;
			var data = GetList(take, skip, groups, clientId, senderId, forwarderId, carrierId, out total);

			var applications = _mapper.Map(data, language);

			if(groups == null || groups.Length == 0)
				return new ApplicationListCollection
				{
					Data = applications,
					Total = total,
				};

			return GetGroupedResult(groups, applications, total, clientId, senderId, forwarderId, carrierId);
		}

		private ApplicationListCollection GetGroupedResult(
			IEnumerable<Order> groups, ApplicationListItem[] applications,
			long total, long? clientId = null, long? senderId = null, long? forwarderId = null, long? carrierId = null)
		{
			var orderTypes = groups.Select(x => x.OrderType).ToArray();

			var applicationGroups = _grouper.Group(applications, orderTypes, clientId, senderId, forwarderId, carrierId);

			OrderBottomGroupByClient(applicationGroups);

			return new ApplicationListCollection
			{
				Total = total,
				Groups = applicationGroups,
			};
		}

		private ApplicationData[] GetList(
			int? take, int skip, IEnumerable<Order> groups, long? clientId,
			long? senderId, long? forwarderId, long? carrierId, out long total)
		{
			var stateIds = _stateFilter.GetStateVisibility();

			var orders = GetOrders(groups);

			total = _applications.Count(stateIds,
				clientId,
				senderId,
				carrierId,
				forwarderId,
				_stateConfig.CargoReceivedStateId,
				_stateConfig.CargoReceivedDaysToShow);

			return _applications.List(stateIds,
				orders,
				take,
				skip,
				clientId,
				senderId,
				carrierId,
				forwarderId,
				_stateConfig.CargoReceivedStateId,
				_stateConfig.CargoReceivedDaysToShow);
		}

		private static Order[] GetOrders(IEnumerable<Order> orders)
		{
			if(orders != null) return orders.ToArray();

			return new[]
			{
				new Order
				{
					Desc = true,
					OrderType = OrderType.AirWaybill
				}
			};
		}

		private static void OrderBottomGroupByClient(IEnumerable<ApplicationGroup> applicationGroups)
		{
			foreach(var group in applicationGroups)
			{
				if(@group.hasSubgroups)
				{
					OrderBottomGroupByClient(@group.items.Cast<ApplicationGroup>());
				}
				else
				{
					var items = @group.items.Cast<ApplicationListItem>();

					if(!string.IsNullOrWhiteSpace(@group.value))
					{
						@group.items = items.OrderBy(x => x.ClientNic)
							.ThenByDescending(x => x.Id)
							.ToArray<object>();
					}
				}
			}
		}
	}
}