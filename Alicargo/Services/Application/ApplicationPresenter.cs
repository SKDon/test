using System.Collections.Generic;
using System.Linq;
using Alicargo.Core.Enums;
using Alicargo.Core.Exceptions;
using Alicargo.Core.Helpers;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Microsoft.Ajax.Utilities;
using Resources;

namespace Alicargo.Services.Application
{
	// todo: test
	// todo: adhere SRP
	public sealed class ApplicationPresenter : IApplicationPresenter
	{
		private readonly IIdentityService _identity;
		private readonly IApplicationRepository _applicationRepository;
		private readonly IClientRepository _clientRepository;
		private readonly ICountryRepository _countryRepository;
		private readonly IStateService _stateService;
		private readonly ITransitService _transitService;
		private readonly IApplicationGrouper _applicationGrouper;
		private readonly IStateConfig _stateConfig;
		private readonly IStateRepository _stateRepository;
		private readonly IReferenceRepository _referenceRepository;

		public ApplicationPresenter(
			IApplicationRepository applicationRepository,
			IIdentityService identity,
			IStateService stateService,
			ITransitService transitService,
			IApplicationGrouper applicationGrouper,
			IStateConfig stateConfig,
			IStateRepository stateRepository,
			IClientRepository clientRepository,
			ICountryRepository countryRepository,
			IReferenceRepository referenceRepository)
		{
			_applicationRepository = applicationRepository;
			_identity = identity;
			_stateService = stateService;
			_transitService = transitService;
			_applicationGrouper = applicationGrouper;
			_stateConfig = stateConfig;
			_stateRepository = stateRepository;
			_clientRepository = clientRepository;
			_countryRepository = countryRepository;
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

			SetAdditionalData(applications);

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

			SetAdditionalData(application);

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

		#region private

		private static Order[] PrepareOrders(IEnumerable<Order> orders)
		{
			var byId = new[] { new Order { Desc = true, OrderType = OrderType.Id } };

			return orders == null ? byId : orders.Concat(byId).ToArray();
		}

		private void SetAdditionalData(params ApplicationModel[] applications)
		{
			SetClientData(applications);

			SetStateData(applications);

			SetTransitData(applications);

			SetReferenceData(applications);

			SetCountryData(applications);
		}

		private void SetCountryData(IEnumerable<ApplicationModel> applications)
		{
			var applicationWithCountry = applications.Where(x => x.CountryId.HasValue).ToArray();

			var countries = _countryRepository
				// ReSharper disable PossibleInvalidOperationException
				.Get(applicationWithCountry.Select(x => x.CountryId.Value).ToArray())
				// ReSharper restore PossibleInvalidOperationException
				.ToDictionary(x => x.Id, x => x.Name);

			foreach (var application in applicationWithCountry)
			{
				// ReSharper disable PossibleInvalidOperationException
				// ReSharper disable AssignNullToNotNullAttribute
				application.CountryName = countries[application.CountryId.Value][_identity.TwoLetterISOLanguageName];
				// ReSharper restore AssignNullToNotNullAttribute
				// ReSharper restore PossibleInvalidOperationException
			}
		}

		private void SetReferenceData(params ApplicationModel[] applications)
		{
			var applicationsWithReference = applications.Where(x => x.ReferenceId.HasValue).ToArray();

			if (applicationsWithReference.Length == 0) return;

			var ids = applicationsWithReference.Select(x => x.ReferenceId ?? 0).ToArray();

			var references = _referenceRepository.Get(ids).ToDictionary(x => x.Id, x => x);

			foreach (var application in applicationsWithReference)
			{
				if (!application.ReferenceId.HasValue || !references.ContainsKey(application.ReferenceId.Value))
					throw new InvalidLogicException();

				var referenceData = references[application.ReferenceId.Value];

				application.ReferenceBill = referenceData.Bill;
				application.ReferenceGTD = referenceData.GTD;
				application.ReferenceDateOfArrival = referenceData.DateOfArrival;
				application.ReferenceDateOfDeparture = referenceData.DateOfDeparture;
				application.AirWayBillDisplay = string.Format("{0} &plusmn; {1}_{2} &plusmn; {3}_{4}{5}", referenceData.Bill,
					referenceData.DepartureAirport, referenceData.DateOfDeparture.ToString("ddMMMyyyy").ToUpperInvariant(),
					referenceData.ArrivalAirport, referenceData.DateOfArrival.ToString("ddMMMyyyy").ToUpperInvariant(),
					referenceData.GTD.IsNullOrWhiteSpace() ? "" : string.Format(" &plusmn; {0}_{1}", Entities.GTD, referenceData.GTD));
			}
		}

		private void SetTransitData(params ApplicationModel[] applications)
		{
			var ids = applications.Select(x => x.TransitId).ToArray();
			var transits = _transitService.Get(ids).ToDictionary(x => x.Id, x => x);

			foreach (var application in applications)
			{
				application.Transit = transits[application.TransitId];
			}
		}

		private void SetClientData(params ApplicationModel[] applications)
		{
			var clientIds = applications.Select(x => x.ClientId).ToArray();
			var clients = _clientRepository.Get(clientIds).ToDictionary(x => x.Id, x => x);

			foreach (var application in applications)
			{
				var clientData = clients[application.ClientId];

				application.ClientUserId = clientData.UserId;
				application.LegalEntity = clientData.LegalEntity;
				application.ClientNic = clientData.Nic;
				application.ClientEmail = clientData.Email;
			}
		}

		private void SetStateData(params ApplicationModel[] applications)
		{
			var localizedStates = _stateService.GetLocalizedDictionary();

			var availableStates = _stateService.GetAvailableStatesToSet();

			var states = _stateService.GetDictionary();

			foreach (var application in applications)
			{
				var state = states[application.StateId];

				application.CanClose = state.Id == _stateConfig.CargoOnTransitStateId; // todo: test 1.

				// todo: test 1.
				application.CanSetState = availableStates.Contains(application.StateId);

				application.StateName = localizedStates[application.StateId];
			}
		}

		#endregion
	}
}