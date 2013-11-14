using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.Core.Localization;
using Alicargo.Core.Services.Abstract;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	internal sealed class ApplicationListItemMapper : IApplicationListItemMapper
	{
		private readonly IApplicationRepository _applications;
		private readonly ICountryRepository _countryRepository;
		private readonly IIdentityService _identity;
		private readonly IStateConfig _stateConfig;
		private readonly IStateService _stateService;

		public ApplicationListItemMapper(IStateService stateService, IStateConfig stateConfig,
			IApplicationRepository applications,
			ICountryRepository countryRepository, IIdentityService identity)
		{
			_stateService = stateService;
			_stateConfig = stateConfig;
			_applications = applications;
			_countryRepository = countryRepository;
			_identity = identity;
		}

		public ApplicationListItem[] Map(ApplicationListItemData[] data)
		{
			var countries = _countryRepository.Get().ToDictionary(x => x.Id, x => x.Name[_identity.TwoLetterISOLanguageName]);
			var localizedStates = _stateService.GetLocalizedDictionary(data.Select(x => x.StateId).ToArray());
			var availableStates = _stateService.GetAvailableStatesToSet();
			var calculations = _applications.GetCalculations(data.Select(x => x.Id).ToArray());

			return data.Select(x => new ApplicationListItem
			{
				CountryName = x.CountryId.HasValue && countries.ContainsKey(x.CountryId.Value)
					? countries[x.CountryId.Value]
					: null,
				State = new ApplicationStateModel
				{
					StateId = x.StateId,
					StateName = localizedStates[x.StateId]
				},
				CanClose = x.StateId == _stateConfig.CargoOnTransitStateId,
				CanSetState = availableStates.Contains(x.StateId),
				AddressLoad = x.AddressLoad,
				Id = x.Id,
				PackingFileName = x.PackingFileName,
				FactoryName = x.FactoryName,
				Invoice = x.Invoice,
				InvoiceFileName = x.InvoiceFileName,
				MarkName = x.MarkName,
				SwiftFileName = x.SwiftFileName,
				Volume = x.Volume,
				Count = x.Count,
				AirWaybill = x.AirWaybill,
				CPFileName = x.CPFileName,
				Characteristic = x.Characteristic,
				ClientLegalEntity = x.ClientLegalEntity,
				ClientNic = x.ClientNic,
				CreationTimestamp = x.CreationTimestamp,
				DateInStock = x.DateInStock,
				DateOfCargoReceipt = x.DateOfCargoReceipt,
				DeliveryBillFileName = x.DeliveryBillFileName,
				FactoryContact = x.FactoryContact,
				FactoryEmail = x.FactoryEmail,
				FactoryPhone = x.FactoryPhone,
				StateChangeTimestamp = x.StateChangeTimestamp,
				StateId = x.StateId,
				TermsOfDelivery = x.TermsOfDelivery,
				Torg12FileName = x.Torg12FileName,
				TransitAddress = x.TransitAddress,
				TransitCarrierName = x.TransitCarrierName,
				TransitId = x.TransitId,
				TransitCity = x.TransitCity,
				TransitDeliveryTypeString = ((DeliveryType)x.TransitDeliveryTypeId).ToLocalString(),
				TransitMethodOfTransitString = ((MethodOfTransit)x.TransitMethodOfTransitId).ToLocalString(),
				TransitPhone = x.TransitPhone,
				TransitRecipientName = x.TransitRecipientName,
				TransitReference = x.TransitReference,
				TransitWarehouseWorkingTime = x.TransitWarehouseWorkingTime,
				WarehouseWorkingTime = x.WarehouseWorkingTime,
				Weight = x.Weight,
				MethodOfDeliveryId = x.MethodOfDeliveryId,
				Value = x.Value,
				CurrencyId = x.CurrencyId,
				AirWaybillId = x.AirWaybillId,
				PickupCost = x.PickupCost,
				FactureCost = x.FactureCost,
				ScotchCost = x.ScotchCost,
				SenderScotchCost = x.SenderScotchCost,
				TariffPerKg = x.TariffPerKg,
				TransitCost = x.TransitCost,
				ForwarderTransitCost = x.ForwarderTransitCost,
				CanSetTransitCost = !calculations.ContainsKey(x.Id)
			}).ToArray();
		}
	}
}