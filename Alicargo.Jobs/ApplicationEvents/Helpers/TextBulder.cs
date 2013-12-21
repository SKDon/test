using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.Application;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.Core.Helpers;
using Alicargo.Jobs.ApplicationEvents.Abstract;
using Alicargo.Jobs.ApplicationEvents.Entities;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public sealed class TextBulder : ITextBulder
	{
		private static readonly PropertyInfo[] Properties =
			typeof(TextLocalizedData).GetProperties().Where(x => x.PropertyType == typeof(string)).ToArray();

		private readonly ISerializer _serializer;
		private readonly IStateRepository _states;
		private readonly IApplicationFileRepository _files;

		public TextBulder(ISerializer serializer, IStateRepository states, IApplicationFileRepository files)
		{
			_serializer = serializer;
			_states = states;
			_files = files;
		}

		public string GetText(string template, string language, ApplicationEventType type, ApplicationDetailsData application, byte[] bytes)
		{
			var data = GetTextLocalizedData(type, application, language, bytes);
			var culture = CultureInfo.GetCultureInfo(language);
			var builder = new StringBuilder(template);

			foreach (var property in Properties)
			{
				var name = property.Name;

				string match;
				string format;
				while (TextBulderHelper.GetMatch(builder.ToString(), name, out match, out format))
				{
					var value = (string)property.GetValue(data);

					var text = TextBulderHelper.GetText(culture, format, value);

					if (string.IsNullOrEmpty(text))
					{
						builder.Replace(match + Environment.NewLine, string.Empty);
					}

					builder.Replace(match, text);
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
					break;

				case ApplicationEventType.CPFileUploaded:
				case ApplicationEventType.InvoiceFileUploaded:
				case ApplicationEventType.PackingFileUploaded:
				case ApplicationEventType.SwiftFileUploaded:
				case ApplicationEventType.DeliveryBillFileUploaded:
				case ApplicationEventType.Torg12FileUploaded:
					OnFileUpload(bytes, localizedData);
					break;

				case ApplicationEventType.SetDateOfCargoReceipt:
					OnSetDateOfCargoReceipt(bytes, culture, localizedData);
					break;

				default:
					throw new ArgumentOutOfRangeException("type");
			}

			return localizedData;
		}

		private void OnFileUpload(byte[] bytes, TextLocalizedData localizedData)
		{
			var holder = _serializer.Deserialize<FileHolder>(bytes);

			localizedData.UploadedFile = holder.Name;
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
			var countryName = application.CountryName.First(x => x.Key == language).Value;
			var value = LocalizationHelper.GetValueString(application.Value, (CurrencyType)application.CurrencyId, culture);

			var deliveryBill = _files.GetNames(application.Id, ApplicationFileType.DeliveryBill).Select(x => x.Value).ToArray();
			var invoice = _files.GetNames(application.Id, ApplicationFileType.Invoice).Select(x => x.Value).ToArray();
			var packing = _files.GetNames(application.Id, ApplicationFileType.Packing).Select(x => x.Value).ToArray();
			var swift = _files.GetNames(application.Id, ApplicationFileType.Swift).Select(x => x.Value).ToArray();
			var torg12 = _files.GetNames(application.Id, ApplicationFileType.Torg12).Select(x => x.Value).ToArray();

			return new TextLocalizedData
			{
				AddressLoad = application.AddressLoad,
				FactoryName = application.FactoryName,
				Id = application.Id.ToString(culture),
				Count = application.Count.HasValue ? application.Count.Value.ToString(culture) : null,
				MarkName = application.MarkName,
				Invoice = application.Invoice,
				CountryName = countryName,
				CreationTimestamp = LocalizationHelper.GetDate(application.CreationTimestamp, culture),
				Value = value,
				Weight = application.Weight.HasValue ? application.Weight.Value.ToString(culture) : null,
				AirWaybill = application.AirWaybill,
				AirWaybillDateOfArrival = LocalizationHelper.GetDate(application.AirWaybillDateOfArrival, culture),
				AirWaybillDateOfDeparture = LocalizationHelper.GetDate(application.AirWaybillDateOfDeparture, culture),
				AirWaybillGTD = application.AirWaybillGTD,
				Characteristic = application.Characteristic,
				ClientNic = application.ClientNic,
				DateOfCargoReceipt = LocalizationHelper.GetDate(application.DateOfCargoReceipt, culture),
				DaysInWork = ApplicationHelper.GetDaysInWork(application.CreationTimestamp).ToString(culture),
				DeliveryBillFiles = string.Join(", ", deliveryBill),
				DeliveryType = LocalizationHelper.GetDeliveryType((DeliveryType)application.TransitDeliveryTypeId, culture),
				DisplayNumber = ApplicationHelper.GetDisplayNumber(application.Id, application.Count),
				FactoryContact = application.FactoryContact,
				FactoryEmail = application.FactoryEmail,
				FactoryPhone = application.FactoryPhone,
				InvoiceFiles = string.Join(", ", invoice),
				LegalEntity = application.ClientLegalEntity,
				MethodOfDelivery = LocalizationHelper.GetMethodOfDelivery((MethodOfDelivery)application.MethodOfDeliveryId, culture),
				MethodOfTransit = LocalizationHelper.GetMethodOfTransit((MethodOfTransit)application.TransitMethodOfTransitId, culture),
				PackingFiles = string.Join(", ", packing),
				StateChangeTimestamp = LocalizationHelper.GetDate(application.StateChangeTimestamp, culture),
				StateName = state != null ? state.LocalizedName : null,
				SwiftFiles = string.Join(", ", swift),
				TermsOfDelivery = application.TermsOfDelivery,
				Torg12Files = string.Join(", ", torg12),
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