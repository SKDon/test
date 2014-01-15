using System.Linq;
using Alicargo.Core.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Services.Abstract;
using Alicargo.Utilities.Localization;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	internal sealed class ApplicationListItemMapper : IApplicationListItemMapper
	{
		private readonly IApplicationRepository _applications;
		private readonly IApplicationFileRepository _files;
		private readonly ICountryRepository _countryRepository;
		private readonly IIdentityService _identity;
		private readonly IStateConfig _stateConfig;
		private readonly IStateFilter _stateFilter;
		private readonly IStateRepository _states;

		public ApplicationListItemMapper(
			IStateFilter stateFilter,
			IStateRepository states,
			IStateConfig stateConfig,
			IApplicationRepository applications,
			IApplicationFileRepository files,
			ICountryRepository countryRepository,
			IIdentityService identity)
		{
			_stateFilter = stateFilter;
			_states = states;
			_stateConfig = stateConfig;
			_applications = applications;
			_files = files;
			_countryRepository = countryRepository;
			_identity = identity;
		}

		public ApplicationListItem[] Map(ApplicationListItemData[] data)
		{
			var appIds = data.Select(x => x.Id).ToArray();
			var stateIds = data.Select(x => x.StateId).ToArray();

			var countries = _countryRepository.Get().ToDictionary(x => x.Id, x => x.Name[_identity.Language]);
			var states = _states.Get(_identity.Language, stateIds);
			var stateAvailability = _stateFilter.GetStateAvailabilityToSet();
			var calculations = _applications.GetCalculations(appIds);
			var cps = _files.GetInfo(appIds, ApplicationFileType.CP);
			var deliveryBills = _files.GetInfo(appIds, ApplicationFileType.DeliveryBill);
			var invoices = _files.GetInfo(appIds, ApplicationFileType.Invoice);
			var packings = _files.GetInfo(appIds, ApplicationFileType.Packing);
			var swifts = _files.GetInfo(appIds, ApplicationFileType.Swift);
			var torg12 = _files.GetInfo(appIds, ApplicationFileType.Torg12);

			return data.Select(x => new ApplicationListItem
			{
				CountryName = x.CountryId.HasValue && countries.ContainsKey(x.CountryId.Value)
					? countries[x.CountryId.Value]
					: null,
				State = new ApplicationStateModel
				{
					StateId = x.StateId,
					StateName = states[x.StateId].LocalizedName
				},
				CanClose = x.StateId == _stateConfig.CargoOnTransitStateId,
				CanSetState = stateAvailability.Contains(x.StateId),
				AddressLoad = x.AddressLoad,
				Id = x.Id,
				FactoryName = x.FactoryName,
				Invoice = x.Invoice,
				MarkName = x.MarkName,
				Volume = x.Volume,
				Count = x.Count,
				AirWaybill = x.AirWaybill,
				Characteristic = x.Characteristic,
				ClientLegalEntity = x.ClientLegalEntity,
				ClientNic = x.ClientNic,
				CreationTimestamp = x.CreationTimestamp,
				DateInStock = x.DateInStock,
				DateOfCargoReceipt = x.DateOfCargoReceipt,
				FactoryContact = x.FactoryContact,
				FactoryEmail = x.FactoryEmail,
				FactoryPhone = x.FactoryPhone,
				StateChangeTimestamp = x.StateChangeTimestamp,
				StateId = x.StateId,
				TermsOfDelivery = x.TermsOfDelivery,
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
				CanSetTransitCost = !calculations.ContainsKey(x.Id),
				CPFiles = cps.ContainsKey(x.Id) ? cps[x.Id] : null,
				DeliveryBillFiles = deliveryBills.ContainsKey(x.Id) ? deliveryBills[x.Id] : null,
				InvoiceFiles = invoices.ContainsKey(x.Id) ? invoices[x.Id] : null,
				PackingFiles = packings.ContainsKey(x.Id) ? packings[x.Id] : null,
				SwiftFiles = swifts.ContainsKey(x.Id) ? swifts[x.Id] : null,
				Torg12Files = torg12.ContainsKey(x.Id) ? torg12[x.Id] : null
			}).ToArray();
		}
	}
}