using System;
using System.Globalization;
using System.Reflection;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.Core.Helpers;
using Alicargo.Core.Services;
using Alicargo.Core.Services.Abstract;
using Alicargo.Jobs.Entities;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public sealed class TextBulder : ITextBulder
	{
		private static readonly PropertyInfo[] Properties = typeof(TextLocalizedData).GetProperties();

		private readonly ISerializer _serializer;
		private readonly ICountryRepository _countrys;
		private readonly ILog _log;

		public TextBulder(
			ISerializer serializer,
			ICountryRepository countrys,
			ILog log)
		{
			_serializer = serializer;
			_countrys = countrys;
			_log = log;
		}

		public string GetText(string template, string language, ApplicationEventType type, ApplicationData application, string countryName, byte[] bytes)
		{
			var data = GetTemplateData(type, application, countryName, language, bytes);

			foreach (var property in Properties)
			{
				var name = property.Name;

				string match;
				string format;
				if (TextBulderHelper.GetMatch(template, name, _log, out match, out format))
				{
					var value = property.GetValue(data);

					var culture = CultureInfo.GetCultureInfo(language);

					var text = string.Format(culture, format ?? "{0}", value);

					template = template.Replace(match, text);
				}
			}

			return template;
		}

		private TextLocalizedData GetTemplateData(ApplicationEventType type, ApplicationData application, string countryName, string language, byte[] bytes)
		{
			var culture = CultureInfo.GetCultureInfo(language);


			var templateData = new TextLocalizedData
			{
				AddressLoad = application.AddressLoad,
				FactoryName = application.FactoryName,
				Id = application.Id.ToString(culture),
				Count = application.Count.HasValue ? application.Count.Value.ToString(culture) : null,
				MarkName = application.MarkName,
				Invoice = application.Invoice,
				CountryName = countryName,
				CreationTimestamp = LocalizationHelper.GetDate(application.CreationTimestamp, CultureInfo.GetCultureInfo(language)),
				Value = LocalizationHelper.GetValueString(application.Value, (CurrencyType)application.CurrencyId, CultureInfo.GetCultureInfo(language)),
				Weight = application.Weight.HasValue ? application.Weight.Value.ToString(culture) : null
			};

			switch (type)
			{
				case ApplicationEventType.Created:
					break;
				case ApplicationEventType.SetState:
					break;

				case ApplicationEventType.CPFileUploaded:
				case ApplicationEventType.InvoiceFileUploaded:
				case ApplicationEventType.PackingFileUploaded:
				case ApplicationEventType.SwiftFileUploaded:
				case ApplicationEventType.DeliveryBillFileUploaded:
				case ApplicationEventType.Torg12FileUploaded:
					var data = _serializer.Deserialize<ApplicationFileUploadedEventData>(bytes);
					templateData.Count = (data.Count ?? 0).ToString(culture);
					templateData.FactoryName = data.FactoryName;
					templateData.MarkName = data.MarkName;
					templateData.Invoice = data.Invoice;
					break;

				case ApplicationEventType.SetDateOfCargoReceipt:
					break;
				case ApplicationEventType.SetTransitReference:
					break;
				default:
					throw new ArgumentOutOfRangeException("type");
			}

			return templateData;
		}
	}
}