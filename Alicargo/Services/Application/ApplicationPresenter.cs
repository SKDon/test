using System.Collections.Generic;
using System.Linq;
using Alicargo.Core.Enums;
using Alicargo.Core.Helpers;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Services.Application
{
	// todo: test
	// todo: adhere SRP
	public sealed class ApplicationPresenter : IApplicationPresenter
	{
		private readonly IIdentityService _identity;
		private readonly IApplicationRepository _applicationRepository;
		private readonly IApplicationHelper _applicationHelper;
		private readonly IClientRepository _clientRepository;
		private readonly IStateService _stateService;
		private readonly IStateConfig _stateConfig;
		private readonly IApplicationGrouper _applicationGrouper;
		private readonly IStateRepository _stateRepository;
		private readonly IReferenceRepository _referenceRepository;

		public ApplicationPresenter(
			IApplicationRepository applicationRepository,
			IApplicationHelper applicationHelper,
			IIdentityService identity,
			IStateService stateService,
			IApplicationGrouper applicationGrouper,
			IStateConfig stateConfig,
			IStateRepository stateRepository,
			IClientRepository clientRepository,
			IReferenceRepository referenceRepository)
		{
			_applicationRepository = applicationRepository;
			_applicationHelper = applicationHelper;
			_identity = identity;
			_stateService = stateService;
			_applicationGrouper = applicationGrouper;
			_stateConfig = stateConfig;
			_stateRepository = stateRepository;
			_clientRepository = clientRepository;
			_referenceRepository = referenceRepository;
		}

		public ApplicationIndexModel GetApplicationIndexModel()
		{
			var clients = _clientRepository.GetAll()
				.OrderBy(x => x.Nic)
				.ToDictionary(x => x.Id, x => x.Nic);

			var model = new ApplicationIndexModel
			{
				Clients = clients,
				References = _referenceRepository.GetAll()
					.Where(x => x.StateId == _stateConfig.CargoIsFlewStateId)
					.OrderBy(x => x.Bill)
					.ToDictionary(x => x.Id, x => x.Bill)
			};
			return model;
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

		public ApplicationModel Get(long id)
		{
			var data = _applicationRepository.Get(id);

			var application = new ApplicationModel(data);

			_applicationHelper.SetAdditionalData(application);

			return application;
		}

		public ApplicationStateModel[] GetAvailableStates(long id)
		{
			var applicationData = _applicationRepository.Get(id);

			var states = _stateService.GetAvailableStatesToSet();

			if (_identity.IsInRole(RoleType.Admin)) return ToApplicationStateModel(states);

			states = _stateService.ApplyBusinessLogicToStates(applicationData, states);

			var currentState = _stateRepository.Get(applicationData.StateId);

			states = _stateService.FilterByPosition(states, currentState.Position);

			return ToApplicationStateModel(states);
		}

		private ApplicationStateModel[] ToApplicationStateModel(IEnumerable<long> ids)
		{
			return _stateService.GetLocalizedDictionary(ids)
				.Select(x => new ApplicationStateModel
				{
					StateId = x.Key,
					StateName = x.Value
				})
				.ToArray();
		}

		private static Order[] PrepareOrders(IEnumerable<Order> orders)
		{
			var byId = new[] { new Order { Desc = true, OrderType = OrderType.Id } };

			return orders == null ? byId : orders.Concat(byId).ToArray();
		}
	}
}