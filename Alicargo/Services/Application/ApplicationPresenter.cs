﻿using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	internal sealed class ApplicationPresenter : IApplicationPresenter
	{
		private readonly IApplicationRepository _applications;
		private readonly ICountryRepository _countryRepository;
		private readonly IIdentityService _identity;
		private readonly IStateRepository _stateRepository;
		private readonly IStateService _stateService;

		public ApplicationPresenter(
			IApplicationRepository applications,
			IIdentityService identity,
			ICountryRepository countryRepository,
			IStateService stateService,
			IStateRepository stateRepository)
		{
			_applications = applications;
			_identity = identity;
			_countryRepository = countryRepository;
			_stateService = stateService;
			_stateRepository = stateRepository;
		}

		public ApplicationAdminModel Get(long id)
		{
			var data = _applications.Get(id);

			var application = GetModel(data);

			return application;
		}

		public ApplicationDetailsModel GetDetails(long id)
		{
			var data = _applications.GetDetails(id);

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
				Weight = data.Weight,
				MethodOfDeliveryId = data.MethodOfDeliveryId,
				Value = data.Value,
				CurrencyId = data.CurrencyId,
				AirWaybillDateOfArrival = data.AirWaybillDateOfArrival,
				AirWaybillDateOfDeparture = data.AirWaybillDateOfDeparture,
				AirWaybillGTD = data.AirWaybillGTD,
				ClientEmail = data.ClientEmail,
				ClientUserId = data.ClientUserId,
				CountryName = data.CountryName.First(x => x.Key == _identity.TwoLetterISOLanguageName).Value,
				AirWaybillId = data.AirWaybillId
			};

			return application;
		}

		public ApplicationStateModel[] GetAvailableStates(long id)
		{
			var applicationData = _applications.Get(id);

			var states = _stateService.GetAvailableStatesToSet();

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

		private ApplicationStateModel[] ToApplicationStateModel(long[] ids)
		{
			return _stateService.GetLocalizedDictionary(ids)
								.Select(x => new ApplicationStateModel
								{
									StateId = x.Key,
									StateName = x.Value
								})
								.ToArray();
		}

		private static ApplicationAdminModel GetModel(ApplicationData data)
		{
			return new ApplicationAdminModel
			{
				AddressLoad = data.AddressLoad,
				Characteristic = data.Characteristic,
				Count = data.Count,
				CPFileName = data.CPFileName,
				Currency = new CurrencyModel
				{
					CurrencyId = data.CurrencyId,
					Value = data.Value
				},
				DeliveryBillFileName = data.DeliveryBillFileName,
				FactoryContact = data.FactoryContact,
				FactoryEmail = data.FactoryEmail,
				FactoryName = data.FactoryName,
				FactoryPhone = data.FactoryPhone,
				Weight = data.Weight,
				Invoice = data.Invoice,
				InvoiceFileName = data.InvoiceFileName,
				PackingFileName = data.PackingFileName,
				MarkName = data.MarkName,
				MethodOfDelivery = (MethodOfDelivery)data.MethodOfDeliveryId,
				SwiftFileName = data.SwiftFileName,
				TermsOfDelivery = data.TermsOfDelivery,
				Torg12FileName = data.Torg12FileName,
				CountryId = data.CountryId,
				Volume = data.Volume,
				WarehouseWorkingTime = data.WarehouseWorkingTime,
				FactureCost = data.FactureCost,
				InvoiceFile = null,
				PackingFile = null,
				CPFile = null,
				DeliveryBillFile = null,
				SwiftFile = null,
				Torg12File = null,
				TransitCost = data.TransitCost,
				PickupCost = data.PickupCost,
				TariffPerKg = data.TariffPerKg,
				ScotchCostEdited = data.ScotchCostEdited,
				FactureCostEdited = data.FactureCostEdited,
				TransitCostEdited = data.TransitCostEdited,
				PickupCostEdited = data.PickupCostEdited,
				SenderId = data.SenderId
			};
		}
	}
}