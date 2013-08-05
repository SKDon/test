using System.Collections.Generic;
using System.Linq;
using Alicargo.Core.Enums;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	// todo: test
	// todo: adhere SRP
	public sealed class ApplicationPresenter : IApplicationPresenter
	{
		private readonly IApplicationRepository _applicationRepository;
		private readonly IApplicationHelper _applicationHelper;
		private readonly IStateService _stateService;
		private readonly IIdentityService _identity;
		private readonly IStateRepository _stateRepository;

		public ApplicationPresenter(
			IApplicationRepository applicationRepository,
			IApplicationHelper applicationHelper,
			IIdentityService identity,
			IStateService stateService,
			IStateRepository stateRepository)
		{
			_applicationRepository = applicationRepository;
			_applicationHelper = applicationHelper;
			_identity = identity;
			_stateService = stateService;
			_stateRepository = stateRepository;
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
	}
}