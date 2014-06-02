using System.Globalization;
using System.Linq;
using Alicargo.Core.Contracts.State;
using Alicargo.Core.Helpers;
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
		private readonly ICityRepository _cities;
		private readonly ICountryRepository _countries;
		private readonly IApplicationFileRepository _files;
		private readonly IStateConfig _stateConfig;
		private readonly IStateFilter _stateFilter;
		private readonly IStateRepository _states;

		public ApplicationListItemMapper(
			IStateFilter stateFilter,
			IStateRepository states,
			ICityRepository cities,
			IStateConfig stateConfig,
			IApplicationRepository applications,
			IApplicationFileRepository files,
			ICountryRepository countries)
		{
			_stateFilter = stateFilter;
			_states = states;
			_cities = cities;
			_stateConfig = stateConfig;
			_applications = applications;
			_files = files;
			_countries = countries;
		}

		public ApplicationListItem[] Map(ApplicationData[] data, string language)
		{
			var appIds = data.Select(x => x.Id).ToArray();
			var stateIds = data.Select(x => x.StateId).ToArray();

			var countries = _countries.All(language).ToDictionary(x => x.Id, x => x.Name);
			var states = _states.Get(language, stateIds);
			var stateAvailability = _stateFilter.GetStateAvailabilityToSet();
			var calculations = _applications.GetCalculations(appIds);
			var cps = _files.GetInfo(appIds, ApplicationFileType.CP);
			var deliveryBills = _files.GetInfo(appIds, ApplicationFileType.DeliveryBill);
			var invoices = _files.GetInfo(appIds, ApplicationFileType.Invoice);
			var packings = _files.GetInfo(appIds, ApplicationFileType.Packing);
			var otherFiles = _files.GetInfo(appIds, ApplicationFileType.Other);
			var swifts = _files.GetInfo(appIds, ApplicationFileType.Swift);
			var torg12 = _files.GetInfo(appIds, ApplicationFileType.Torg12);
			var cities = _cities.All(language).ToDictionary(x => x.Id, x => x.Name);

			return data.Select(x => new ApplicationListItem
			{
				CountryName = countries[x.CountryId],
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
				CarrierName = x.CarrierName,
				CarrierContact = x.CarrierContact,
				CarrierAddress = x.CarrierAddress,
				CarrierPhone = x.CarrierPhone,
				TransitId = x.TransitId,
				TransitCity = cities[x.TransitCityId],
				TransitDeliveryTypeString = x.TransitDeliveryType.ToLocalString(),
				TransitMethodOfTransitString = x.TransitMethodOfTransit.ToLocalString(),
				TransitPhone = x.TransitPhone,
				TransitRecipientName = x.TransitRecipientName,
				TransitReference = x.TransitReference,
				TransitWarehouseWorkingTime = x.TransitWarehouseWorkingTime,
				WarehouseWorkingTime = x.WarehouseWorkingTime,
				Weight = x.Weight,
				MethodOfDeliveryId = (int)x.MethodOfDelivery,
				Value = x.Value,
				CurrencyId = (int)x.CurrencyId,
				AirWaybillId = x.AirWaybillId,
				PickupCost = x.GetAdjustedPickupCost(),
				FactureCost = x.GetAdjustedFactureCost(),
				FactureCostEx = x.GetAdjustedFactureCostEx(),
				ScotchCost = x.GetAdjustedScotchCost(),
				SenderName = x.SenderName,
				ForwarderName = x.ForwarderName,
				SenderScotchCost = x.SenderScotchCost,
				TariffPerKg = x.TariffPerKg,
				TransitCost = x.GetAdjustedTransitCost(),
				ForwarderTransitCost = x.TransitCost,
				CanSetTransitCost = !calculations.ContainsKey(x.Id),
				CPFiles = cps.ContainsKey(x.Id) ? cps[x.Id].ToArray() : null,
				DeliveryBillFiles = deliveryBills.ContainsKey(x.Id) ? deliveryBills[x.Id].ToArray() : null,
				InvoiceFiles = invoices.ContainsKey(x.Id) ? invoices[x.Id].ToArray() : null,
				PackingFiles = packings.ContainsKey(x.Id) ? packings[x.Id].ToArray() : null,
				OtherFiles = otherFiles.ContainsKey(x.Id) ? otherFiles[x.Id].ToArray() : null,
				SwiftFiles = swifts.ContainsKey(x.Id) ? swifts[x.Id].ToArray() : null,
				Torg12Files = torg12.ContainsKey(x.Id) ? torg12[x.Id].ToArray() : null,
				DisplayNumber = x.GetApplicationDisplay(),
				DaysInWork = x.GetDaysInWork(),
				CreationTimestampLocalString = LocalizationHelper.GetDate(x.CreationTimestamp, CultureProvider.GetCultureInfo()),
				StateChangeTimestampLocalString =
					LocalizationHelper.GetDate(x.StateChangeTimestamp, CultureProvider.GetCultureInfo()),
				DateOfCargoReceiptLocalString =
					x.DateOfCargoReceipt.HasValue
						? LocalizationHelper.GetDate(x.DateOfCargoReceipt.Value, CultureProvider.GetCultureInfo())
						: null,
				DateInStockLocalString =
					x.DateInStock.HasValue ? LocalizationHelper.GetDate(x.DateInStock.Value, CultureProvider.GetCultureInfo()) : null,
				MethodOfDeliveryLocalString = x.MethodOfDelivery.ToLocalString(),
				ValueString = GetValueString(x.Value, x.CurrencyId)
			}).ToArray();
		}

		private static string GetValueString(decimal value, CurrencyType currencyId)
		{
			var currency = currencyId;

			var culture = CultureProvider.Get();

			return LocalizationHelper.GetValueString(value, currency, CultureInfo.GetCultureInfo(culture));
		}
	}
}