using System.Collections.Generic;
using System.Linq;
using Alicargo.Core.Enums;
using Alicargo.Core.Helpers;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Services.Application
{
	public sealed class ApplicationListPresenter : IApplicationListPresenter
	{
		private readonly IApplicationRepository _applicationRepository;
		private readonly IApplicationHelper _applicationHelper;
		private readonly IStateService _stateService;
		private readonly IIdentityService _identity;
		private readonly IApplicationGrouper _applicationGrouper;

		public ApplicationListPresenter(IApplicationRepository applicationRepository,
			IApplicationHelper applicationHelper, IStateService stateService,
			IIdentityService identity, IApplicationGrouper applicationGrouper)
		{
			_applicationRepository = applicationRepository;
			_applicationHelper = applicationHelper;
			_stateService = stateService;
			_identity = identity;
			_applicationGrouper = applicationGrouper;
		}

		public ApplicationListCollection List(int take, int skip, Order[] groups)
		{
			var stateIds = _stateService.GetVisibleStates();

			var isClient = _identity.IsInRole(RoleType.Client);

			var orders = PrepareOrders(groups);

			var data = _applicationRepository.Get(take, skip, stateIds, orders,
				isClient ? _identity.Id : null);

			var applications = data.Select(x => new ApplicationModel(x)).ToArray();

			_applicationHelper.SetAdditionalData(applications);

			if (groups == null || groups.Length == 0)
			{
				return new ApplicationListCollection
				{
					Data = applications,
					Total = _applicationRepository.Count(stateIds, isClient ? _identity.Id : null),
				};
			}

			return new ApplicationListCollection
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