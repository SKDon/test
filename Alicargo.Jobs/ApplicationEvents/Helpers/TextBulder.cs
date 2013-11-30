using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Core.Localization;
using Alicargo.Core.Services.Abstract;
using Alicargo.Jobs.Entities;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public sealed class TextBulder : ITextBulder
	{
		private static readonly PropertyInfo[] Properties = typeof(TextTemplateData).GetProperties();

		private readonly ISerializer _serializer;
		private readonly ILog _log;
		private readonly ILocalizationService _localization;

		public TextBulder(
			ISerializer serializer,
			ILog log,
			ILocalizationService localization)
		{
			_serializer = serializer;
			_log = log;
			_localization = localization;
		}

		public string GetText(string template, string language, ApplicationEventType type, ApplicationData application,
			byte[] bytes)
		{
			var data = GetTemplateData(type, application, language, bytes);

			foreach (var property in Properties)
			{
				var name = property.Name;

				string match;
				string format;
				if (FormatHelper.GetFormat(template, name, _log, out match, out format))
				{
					var value = property.GetValue(data);

					var culture = CultureInfo.GetCultureInfo(language);

					var text = string.Format(culture, format, value);

					template = template.Replace(match, text);	
				}
			}

			return template;
		}

		private TextTemplateData GetTemplateData(ApplicationEventType type, ApplicationData application, string language, byte[] bytes)
		{
			var templateData = new TextTemplateData();

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
					templateData.Count = data.Count;
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