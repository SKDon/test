using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Alicargo.Core.Calculation;
using Alicargo.Core.Contracts.Event;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Jobs.Application.Abstract;
using Alicargo.Utilities;

namespace Alicargo.Jobs.Application.Helpers
{
	internal sealed class TextBuilder : ITextBuilder
	{
		private readonly IAwbRepository _awbs;
		private readonly IClientBalanceRepository _balance;
		private readonly Jobs.Helpers.Abstract.ITextBuilder _bulder;
		private readonly ICityRepository _cities;
		private readonly ICountryRepository _countries;
		private readonly IApplicationFileRepository _files;
		private readonly ISerializer _serializer;
		private readonly IStateRepository _states;

		public TextBuilder(
			ISerializer serializer,
			IAwbRepository awbs,
			ICountryRepository countries,
			ICityRepository cities,
			IStateRepository states,
			IApplicationFileRepository files,
			IClientBalanceRepository balance,
			Jobs.Helpers.Abstract.ITextBuilder bulder)
		{
			_serializer = serializer;
			_awbs = awbs;
			_countries = countries;
			_cities = cities;
			_states = states;
			_files = files;
			_balance = balance;
			_bulder = bulder;
		}

		public string GetText(string template, string language, EventType type, ApplicationData application,
			byte[] bytes)
		{
			var data = GetTextLocalizedData(type, application, language, bytes);

			return _bulder.GetText(template, language, data);
		}

		private IDictionary<string, string> GetTextLocalizedData(EventType type,
			ApplicationData application, string language, byte[] bytes)
		{
			var culture = CultureInfo.GetCultureInfo(language);

			var localizedData = GetTextLocalizedData(application, language, culture);

			switch(type)
			{
				case EventType.ApplicationSetState:
					OnSetState(bytes, language, localizedData);
					break;

				case EventType.SetTransitReference:
				case EventType.ApplicationCreated:
				case EventType.SetSender:
				case EventType.SetForwarder:
				case EventType.SetCarrier:
					break;

				case EventType.Calculate:
				case EventType.CalculationCanceled:
					OnCalculation(bytes, culture, localizedData);
					break;

				case EventType.CPFileUploaded:
				case EventType.InvoiceFileUploaded:
				case EventType.PackingFileUploaded:
				case EventType.SwiftFileUploaded:
				case EventType.DeliveryBillFileUploaded:
				case EventType.Torg12FileUploaded:
					OnFileUpload(bytes, localizedData);
					break;

				case EventType.SetDateOfCargoReceipt:
					OnSetDateOfCargoReceipt(bytes, culture, localizedData);
					break;

				case EventType.SetAwb:
					OnSetAwb(bytes, culture, localizedData);
					break;

				default:
					throw new ArgumentOutOfRangeException("type");
			}

			return localizedData;
		}

		private IDictionary<string, string> GetTextLocalizedData(ApplicationData application, string language,
			CultureInfo culture)
		{
			var state = _states.Get(language, application.StateId).Select(x => x.Value).FirstOrDefault();
			var countryName = _countries.All(language).First(x => x.Id == application.CountryId).Name;
			var cityName = _cities.All(language).First(x => x.Id == application.TransitCityId).Name;
			var value = LocalizationHelper.GetValueString(application.Value, (CurrencyType)application.CurrencyId, culture);
			var deliveryBill = _files.GetNames(application.Id, ApplicationFileType.DeliveryBill).Select(x => x.Value).ToArray();
			var invoice = _files.GetNames(application.Id, ApplicationFileType.Invoice).Select(x => x.Value).ToArray();
			var packing = _files.GetNames(application.Id, ApplicationFileType.Packing).Select(x => x.Value).ToArray();
			var swift = _files.GetNames(application.Id, ApplicationFileType.Swift).Select(x => x.Value).ToArray();
			var torg12 = _files.GetNames(application.Id, ApplicationFileType.Torg12).Select(x => x.Value).ToArray();

			var localizedData = new Dictionary<string, string>();
			Add(localizedData, "AddressLoad", application.AddressLoad);
			Add(localizedData, "FactoryName", application.FactoryName);
			Add(localizedData, "Id", application.Id.ToString(culture));
			Add(localizedData, "Count", application.Count.HasValue ? application.Count.Value.ToString(culture) : null);
			Add(localizedData, "MarkName", application.MarkName);
			Add(localizedData, "Invoice", application.Invoice);
			Add(localizedData, "CountryName", countryName);
			Add(localizedData, "CreationTimestamp", LocalizationHelper.GetDate(application.CreationTimestamp, culture));
			Add(localizedData, "Value", value);
			Add(localizedData, "Weight", application.Weight.HasValue ? application.Weight.Value.ToString(culture) : null);
			Add(localizedData, "AirWaybill", application.AirWaybill);
			Add(localizedData, "AirWaybillDateOfArrival",
				LocalizationHelper.GetDate(application.AirWaybillDateOfArrival, culture));
			Add(localizedData, "AirWaybillDateOfDeparture",
				LocalizationHelper.GetDate(application.AirWaybillDateOfDeparture, culture));
			Add(localizedData, "AirWaybillGTD", application.AirWaybillGTD);
			Add(localizedData, "Characteristic", application.Characteristic);
			Add(localizedData, "ClientNic", application.ClientNic);
			Add(localizedData, "DateOfCargoReceipt", LocalizationHelper.GetDate(application.DateOfCargoReceipt, culture));
			Add(localizedData, "DaysInWork", application.GetDaysInWork().ToString(culture));
			Add(localizedData, "DeliveryBillFiles", string.Join(", ", deliveryBill));
			Add(localizedData, "DeliveryType", LocalizationHelper.GetDeliveryType(application.TransitDeliveryType, culture));
			Add(localizedData, "DisplayNumber", application.GetApplicationDisplay());
			Add(localizedData, "FactoryContact", application.FactoryContact);
			Add(localizedData, "FactoryEmail", application.FactoryEmail);
			Add(localizedData, "FactoryPhone", application.FactoryPhone);
			Add(localizedData, "InvoiceFiles", string.Join(", ", invoice));
			Add(localizedData, "LegalEntity", application.ClientLegalEntity);
			Add(localizedData, "MethodOfDelivery", LocalizationHelper.GetMethodOfDelivery(application.MethodOfDelivery, culture));
			Add(localizedData, "MethodOfTransit",
				LocalizationHelper.GetMethodOfTransit(application.TransitMethodOfTransit, culture));
			Add(localizedData, "PackingFiles", string.Join(", ", packing));
			Add(localizedData, "StateChangeTimestamp", LocalizationHelper.GetDate(application.StateChangeTimestamp, culture));
			Add(localizedData, "StateName", state != null ? state.LocalizedName : null);
			Add(localizedData, "SwiftFiles", string.Join(", ", swift));
			Add(localizedData, "TermsOfDelivery", application.TermsOfDelivery);
			Add(localizedData, "Torg12Files", string.Join(", ", torg12));
			Add(localizedData, "TransitAddress", application.TransitAddress);
			Add(localizedData, "CarrierName", application.CarrierName);
			Add(localizedData, "CarrierContact", application.CarrierContact);
			Add(localizedData, "CarrierAddress", application.CarrierAddress);
			Add(localizedData, "CarrierPhone", application.CarrierPhone);
			Add(localizedData, "CarrierEmail", application.CarrierEmail);
			Add(localizedData, "TransitCity", cityName);
			Add(localizedData, "TransitPhone", application.TransitPhone);
			Add(localizedData, "TransitRecipientName", application.TransitRecipientName);
			Add(localizedData, "TransitReference", application.TransitReference);
			Add(localizedData, "TransitWarehouseWorkingTime", application.TransitWarehouseWorkingTime);
			Add(localizedData, "Volume", application.Volume.ToString("N2", culture));
			Add(localizedData, "WarehouseWorkingTime", application.WarehouseWorkingTime);
			Add(localizedData, "SenderName", application.SenderName);
			Add(localizedData, "SenderContact", application.SenderContact);
			Add(localizedData, "SenderPhone", application.SenderPhone);
			Add(localizedData, "SenderAddress", application.SenderAddress);
			Add(localizedData, "SenderEmail", application.SenderEmail);
			Add(localizedData, "ForwarderName", application.ForwarderName);

			return localizedData;
		}

		private void OnCalculation(byte[] bytes, CultureInfo culture, IDictionary<string, string> localizedData)
		{
			var calculation = _serializer.Deserialize<CalculationData>(bytes);
			var balance = _balance.GetBalance(calculation.ClientId);
			var insuranceCost = CalculationHelper.GetInsuranceCost(calculation.Value, calculation.InsuranceRate);
			var weightCost = calculation.TariffPerKg * (decimal)calculation.Weight;
			var total = CalculationDataHelper.GetMoney(calculation, calculation.InsuranceRate);

			Add(localizedData, "ClientBalance", balance.ToString("N2"));
			Add(localizedData, "CalculationTimestamp", LocalizationHelper.GetDate(calculation.CreationTimestamp, culture));
			Add(localizedData, "AirWaybillDisplay", calculation.AirWaybillDisplay);
			Add(localizedData, "ApplicationDisplay", calculation.ApplicationDisplay);
			Add(localizedData, "MarkName", calculation.MarkName);
			Add(localizedData, "FactoryName", calculation.FactoryName);
			Add(localizedData, "Weight", calculation.Weight.ToString("N2", culture));
			Add(localizedData, "TariffPerKg", calculation.TariffPerKg.ToString("N2", culture));
			Add(localizedData, "WeightCost", weightCost.ToString("N2", culture));
			Add(localizedData, "ScotchCost", calculation.ScotchCost.ToString("N2", culture));
			Add(localizedData, "InsuranceCost", insuranceCost.ToString("N2", culture));
			Add(localizedData, "FactureCost", (calculation.FactureCost + calculation.FactureCostEx).ToString("N2", culture));
			Add(localizedData, "FactureCostT1", calculation.FactureCost.ToString("N2", culture));
			Add(localizedData, "FactureCostEx", calculation.FactureCostEx.ToString("N2", culture));
			Add(localizedData, "TransitCost", calculation.TransitCost.ToString("N2", culture));
			Add(localizedData, "PickupCost", calculation.PickupCost.ToString("N2", culture));
			Add(localizedData, "TotalCost", total.ToString("N2", culture));
		}

		private void OnFileUpload(byte[] bytes, IDictionary<string, string> localizedData)
		{
			var holder = _serializer.Deserialize<FileHolder>(bytes);

			Add(localizedData, "UploadedFile", holder.Name);
		}

		private void OnSetAwb(byte[] bytes, CultureInfo culture, IDictionary<string, string> localizedData)
		{
			var awbId = _serializer.Deserialize<long>(bytes);
			var awb = _awbs.Get(awbId).Single();
			var aggregate = _awbs.GetAggregate(new[] { awb.Id }).Single();

			Add(localizedData, "DepartureAirport", awb.DepartureAirport);
			Add(localizedData, "DateOfDeparture", LocalizationHelper.GetDate(awb.DateOfDeparture, culture));
			Add(localizedData, "ArrivalAirport", awb.ArrivalAirport);
			Add(localizedData, "DateOfArrival", LocalizationHelper.GetDate(awb.DateOfArrival, culture));
			Add(localizedData, "TotalWeight", aggregate.TotalWeight.ToString("N2"));
			Add(localizedData, "TotalCount", aggregate.TotalCount.ToString("N2"));
			Add(localizedData, "TotalValue", aggregate.TotalValue.ToString("N2"));
			Add(localizedData, "TotalVolume", aggregate.TotalVolume.ToString("N2"));
		}

		private void OnSetDateOfCargoReceipt(byte[] bytes, CultureInfo culture, IDictionary<string, string> localizedData)
		{
			var dateOfCargoReceipt = _serializer.Deserialize<DateTimeOffset?>(bytes);

			Add(localizedData, "DateOfCargoReceipt", LocalizationHelper.GetDate(dateOfCargoReceipt, culture));
		}

		private void OnSetState(byte[] bytes, string language, IDictionary<string, string> templateData)
		{
			var data = _serializer.Deserialize<ApplicationSetStateEventData>(bytes);

			var state = _states.Get(language, data.StateId).Select(x => x.Value).FirstOrDefault();

			if(state != null)
			{
				Add(templateData, "StateName", state.LocalizedName);
				Add(templateData, "StateChangeTimestamp",
					LocalizationHelper.GetDate(data.Timestamp, CultureInfo.GetCultureInfo(language)));
			}
		}

		private static void Add(IDictionary<string, string> localizedData, string key, string value)
		{
			if(localizedData.ContainsKey(key))
			{
				localizedData[key] = value;
			}
			else
			{
				localizedData.Add(key, value);
			}
		}
	}
}