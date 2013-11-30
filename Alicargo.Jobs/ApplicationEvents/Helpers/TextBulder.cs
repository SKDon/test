using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.Core.Helpers;
using Alicargo.Core.Services.Abstract;
using Alicargo.Jobs.ApplicationEvents.Abstract;
using Alicargo.Jobs.ApplicationEvents.Entities;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public sealed class TextBulder : ITextBulder
	{
		private static readonly PropertyInfo[] Properties =
			typeof(TextLocalizedData).GetProperties().Where(x => x.PropertyType == typeof(string)).ToArray();

		private readonly ILog _log;
		private readonly ISerializer _serializer;
		private readonly IStateRepository _states;

		public TextBulder(
			ISerializer serializer,
			IStateRepository states,
			ILog log)
		{
			_serializer = serializer;
			_states = states;
			_log = log;
		}

		public string GetText(string template, string language, ApplicationEventType type, ApplicationDetailsData application, byte[] bytes)
		{
			var data = GetTextLocalizedData(type, application, language, bytes);

			var builder = new StringBuilder(template);

			foreach (var property in Properties)
			{
				var name = property.Name;

				string match;
				string format;
				while (TextBulderHelper.GetMatch(builder.ToString(), name, out match, out format)) // todo: test two matchs
				{
					var value = (string)property.GetValue(data);

					var culture = CultureInfo.GetCultureInfo(language);

					var text = TextBulderHelper.GetText(culture, format, value);

					builder.Replace(match, text); // todo: test replace all
				}
			}

			return builder.ToString();
		}

		private TextLocalizedData GetTextLocalizedData(ApplicationEventType type,
			ApplicationDetailsData application, string language, byte[] bytes)
		{
			var culture = CultureInfo.GetCultureInfo(language);

			var localizedData = GetTextLocalizedData(application, language, culture);

			switch (type)
			{
				case ApplicationEventType.SetState:
					OnSetState(bytes, language, localizedData);
					break;

				case ApplicationEventType.SetTransitReference:
				case ApplicationEventType.Created:
				case ApplicationEventType.CPFileUploaded:
				case ApplicationEventType.InvoiceFileUploaded:
				case ApplicationEventType.PackingFileUploaded:
				case ApplicationEventType.SwiftFileUploaded:
				case ApplicationEventType.DeliveryBillFileUploaded:
				case ApplicationEventType.Torg12FileUploaded:
					break;

				case ApplicationEventType.SetDateOfCargoReceipt:
					OnSetDateOfCargoReceipt(bytes, culture, localizedData);
					break;					

				default:
					throw new ArgumentOutOfRangeException("type");
			}

			return localizedData;
		}

		private void OnSetDateOfCargoReceipt(byte[] bytes, CultureInfo culture, TextLocalizedData localizedData)
		{
			var dateOfCargoReceipt = _serializer.Deserialize<DateTimeOffset?>(bytes);

			localizedData.DateOfCargoReceipt = LocalizationHelper.GetDate(dateOfCargoReceipt, culture);
		}

		private TextLocalizedData GetTextLocalizedData(ApplicationDetailsData application, string language,
			CultureInfo culture)
		{
			var state = _states.Get(language, application.StateId).Select(x => x.Value).FirstOrDefault();

			return new TextLocalizedData
			{
				AddressLoad = application.AddressLoad,
				FactoryName = application.FactoryName,
				Id = application.Id.ToString(culture),
				Count = application.Count.HasValue ? application.Count.Value.ToString(culture) : null,
				MarkName = application.MarkName,
				Invoice = application.Invoice,
				CountryName = application.CountryName.First(x => x.Key == language).Value,
				CreationTimestamp = LocalizationHelper.GetDate(application.CreationTimestamp, culture),
				Value = LocalizationHelper.GetValueString(application.Value, (CurrencyType)application.CurrencyId, culture),
				Weight = application.Weight.HasValue ? application.Weight.Value.ToString(culture) : null,
				AirWaybill = application.AirWaybill,
				AirWaybillDateOfArrival = LocalizationHelper.GetDate(application.AirWaybillDateOfArrival, culture),
				AirWaybillDateOfDeparture = LocalizationHelper.GetDate(application.AirWaybillDateOfDeparture, culture),
				AirWaybillGTD = application.AirWaybillGTD,
				Characteristic = application.Characteristic,
				ClientNic = application.ClientNic,
				DateOfCargoReceipt = LocalizationHelper.GetDate(application.DateOfCargoReceipt, culture),
				DaysInWork = ApplicationHelper.GetDaysInWork(application.CreationTimestamp).ToString(culture),
				DeliveryBillFileName = application.DeliveryBillFileName,
				DeliveryType = LocalizationHelper.GetDeliveryType((DeliveryType)application.TransitDeliveryTypeId, culture),
				DisplayNumber = ApplicationHelper.GetDisplayNumber(application.Id, application.Count),
				FactoryContact = application.FactoryContact,
				FactoryEmail = application.FactoryEmail,
				FactoryPhone = application.FactoryPhone,
				InvoiceFileName = application.InvoiceFileName,
				LegalEntity = application.ClientLegalEntity,
				MethodOfDelivery = LocalizationHelper.GetMethodOfDelivery((MethodOfDelivery)application.MethodOfDeliveryId, culture),
				MethodOfTransit = LocalizationHelper.GetMethodOfTransit((MethodOfTransit)application.TransitMethodOfTransitId, culture),
				PackingFileName = application.PackingFileName,
				StateChangeTimestamp = LocalizationHelper.GetDate(application.StateChangeTimestamp, culture),
				StateName = state != null ? state.LocalizedName : null,
				SwiftFileName = application.SwiftFileName,
				TermsOfDelivery = application.TermsOfDelivery,
				Torg12FileName = application.Torg12FileName,
				TransitAddress = application.TransitAddress,
				TransitCarrierName = application.TransitCarrierName,
				TransitCity = application.TransitCity,
				TransitPhone = application.TransitPhone,
				TransitRecipientName = application.TransitRecipientName,
				TransitReference = application.TransitReference,
				TransitWarehouseWorkingTime = application.TransitWarehouseWorkingTime,
				Volume = application.Volume.ToString("N2", culture),
				WarehouseWorkingTime = application.WarehouseWorkingTime
			};
		}

		private void OnSetState(byte[] bytes, string language, TextLocalizedData templateData)
		{
			var data = _serializer.Deserialize<ApplicationSetStateEventData>(bytes);

			var state = _states.Get(language, data.StateId).Select(x => x.Value).FirstOrDefault();

			if (state != null)
			{
				templateData.StateName = state.LocalizedName;
				templateData.StateChangeTimestamp = LocalizationHelper.GetDate(data.Timestamp, CultureInfo.GetCultureInfo(language));
			}
		}
	}
}