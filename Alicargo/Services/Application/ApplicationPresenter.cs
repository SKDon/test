using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	public sealed class ApplicationPresenter : IApplicationPresenter
	{
		private readonly IApplicationRepository _applicationRepository;
		private readonly ICountryRepository _countryRepository;
		private readonly IIdentityService _identity;
		private readonly IStateRepository _stateRepository;
		private readonly IStateService _stateService;

		public ApplicationPresenter(
			IApplicationRepository applicationRepository,
			IIdentityService identity,
			ICountryRepository countryRepository,
			IStateService stateService,
			IStateRepository stateRepository)
		{
			_applicationRepository = applicationRepository;
			_identity = identity;
			_countryRepository = countryRepository;
			_stateService = stateService;
			_stateRepository = stateRepository;
		}

		public ApplicationDetailsModel GetDetails(long id)
		{
			var data = _applicationRepository.GetDetails(id);

			var countries = _countryRepository.Get().ToDictionary(x => x.Id, x => x.Name[_identity.TwoLetterISOLanguageName]);

            // todo: 2. create mapper and test it
			var application = new ApplicationDetailsModel
			{
				AddressLoad = data.AddressLoad,
				Id = data.Id,
				PackingFileName = data.PackingFileName,
				FactoryName = data.FactoryName,
				Invoice = data.Invoice,
				InvoiceFileName = data.InvoiceFileName,
				MarkName = data.MarkName,
				SwiftFileName = data.SwiftFileName,
				Volume = data.Volume,
				Count = data.Count,
				AirWaybill = data.AirWaybill,
				CPFileName = data.CPFileName,
				Characteristic = data.Characteristic,
				ClientNic = data.ClientNic,
				ClientLegalEntity = data.ClientLegalEntity,
				CreationTimestamp = data.CreationTimestamp,
				DateOfCargoReceipt = data.DateOfCargoReceipt,
				DeliveryBillFileName = data.DeliveryBillFileName,
				FactoryContact = data.FactoryContact,
				FactoryEmail = data.FactoryEmail,
				FactoryPhone = data.FactoryPhone,
				StateChangeTimestamp = data.StateChangeTimestamp,
				StateId = data.StateId,
				TermsOfDelivery = data.TermsOfDelivery,
				Torg12FileName = data.Torg12FileName,
				TransitAddress = data.TransitAddress,
				TransitCarrierName = data.TransitCarrierName,
				TransitCity = data.TransitCity,
				TransitDeliveryTypeId = data.TransitDeliveryTypeId,
				TransitMethodOfTransitId = data.TransitMethodOfTransitId,
				TransitPhone = data.TransitPhone,
				TransitRecipientName = data.TransitRecipientName,
				TransitReference = data.TransitReference,
				TransitWarehouseWorkingTime = data.TransitWarehouseWorkingTime,
				WarehouseWorkingTime = data.WarehouseWorkingTime,
				Weigth = data.Weight,
				MethodOfDeliveryId = data.MethodOfDeliveryId,
				Value = data.Value,
				CurrencyId = data.CurrencyId,
				AirWaybillDateOfArrival = data.AirWaybillDateOfArrival,
				AirWaybillDateOfDeparture = data.AirWaybillDateOfDeparture,
				AirWaybillGTD = data.AirWaybillGTD,
				ClientEmail = data.ClientEmail,
				ClientUserId = data.ClientUserId,
				CountryName = data.CountryId.HasValue
					? countries[data.CountryId.Value]
					: null,
				AirWaybillId = data.AirWaybillId
			};

			return application;
		}

		public ApplicationStateModel[] GetAvailableStates(long id)
		{
			var applicationData = _applicationRepository.Get(id);

			var states = _stateService.GetAvailableStatesToSet();

            // todo: 2. test admin role
			if (_identity.IsInRole(RoleType.Admin)) return ToApplicationStateModel(states);

			states = _stateService.ApplyBusinessLogicToStates(applicationData, states);

			var currentState = _stateRepository.Get(applicationData.StateId);

			states = _stateService.FilterByPosition(states, currentState.Position);

			return ToApplicationStateModel(states);
		}

		public IDictionary<long, string> GetLocalizedCountries()
		{
			return _countryRepository.Get()
				.ToDictionary(x => x.Id, x => x.Name[_identity.TwoLetterISOLanguageName]);
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