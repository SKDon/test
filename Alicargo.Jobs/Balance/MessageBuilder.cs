using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Core.Calculation;
using Alicargo.Core.Helpers;
using Alicargo.Jobs.Balance.Entities;
using Alicargo.Jobs.Helpers.Abstract;

namespace Alicargo.Jobs.Balance
{
	internal sealed class MessageBuilder : IMessageBuilder
	{
		private readonly IClientCalculationPresenter _calculationPresenter;
		private readonly IClientRepository _clients;
		private readonly string _defaultFrom;
		private readonly IExcelClientCalculation _excel;
		private readonly IRecipientsFacade _recipients;
		private readonly ISerializer _serializer;
		private readonly ITemplateRepositoryHelper _templates;
		private readonly ITextBuilder _textBuilder;

		public MessageBuilder(
			string defaultFrom,
			IRecipientsFacade recipients,
			IClientRepository clients,
			IClientCalculationPresenter calculationPresenter,
			IExcelClientCalculation excel,
			ISerializer serializer,
			ITextBuilder textBuilder,
			ITemplateRepositoryHelper templates)
		{
			_defaultFrom = defaultFrom;
			_recipients = recipients;
			_clients = clients;
			_calculationPresenter = calculationPresenter;
			_excel = excel;
			_serializer = serializer;
			_textBuilder = textBuilder;
			_templates = templates;
		}

		public EmailMessage[] Get(EventType type, EventData eventData)
		{
			var eventDataForEntity = _serializer.Deserialize<EventDataForEntity>(eventData.Data);
			var clientId = eventDataForEntity.EntityId;

			var templateId = _templates.GetTemplateId(type);
			if (!templateId.HasValue)
			{
				return null;
			}

			var recipients = _recipients.GetRecipients(type, clientId);
			var languages = recipients.Select(x => x.Culture).Distinct().ToArray();

			var files = GetFiles(clientId, languages);

			var localizations = GetLocalizationData(eventDataForEntity, languages, templateId.Value, clientId);

			return recipients.Select(x => GetEmailMessage(x.Email, localizations[x.Culture], files[x.Culture])).ToArray();
		}

		private Dictionary<string, EmailTemplateLocalizationData> GetLocalizationData(EventDataForEntity eventData,
			string[] languages,
			long templateId, long clientId)
		{
			var clientData = _clients.Get(clientId);
			var paymentEventData = _serializer.Deserialize<PaymentEventData>(eventData.Data);

			var localizations = languages.ToDictionary(x => x,
				language =>
				{
					var template = _templates.GetLocalization(templateId, language);

					var localizedData = GetLocalizedData(language, paymentEventData, clientData);

					return new EmailTemplateLocalizationData
					{
						IsBodyHtml = template.IsBodyHtml,
						Subject = _textBuilder.GetText(template.Subject, language, localizedData),
						Body = _textBuilder.GetText(template.Body, language, localizedData)
					};
				});

			return localizations;
		}

		private static IDictionary<string, string> GetLocalizedData(string language, PaymentEventData paymentEventData,
			ClientData clientData)
		{
			var culture = CultureInfo.GetCultureInfo(language);

			return new Dictionary<string, string>
			{
				{ "AbsMoney", paymentEventData.AbsMoney.ToString("N2") },
				{ "Comment", paymentEventData.Comment },
				{ "ClientNic", clientData.Nic },
				{ "LegalEntity", clientData.LegalEntity },
				{ "Timestamp", LocalizationHelper.GetDate(paymentEventData.Timestamp, culture) }
			};
		}

		private Dictionary<string, FileHolder> GetFiles(long clientId, IEnumerable<string> languages)
		{
			var list = _calculationPresenter.List(clientId, int.MaxValue, 0);
			var files = languages
				.ToDictionary(
					x => x,
					language =>
					{
						using(var stream = _excel.Get(list.Groups, language))
						{
							return new FileHolder
							{
								Data = stream.ToArray(),
								Name = "calculation.xlsx"
							};
						}
					});

			return files;
		}

		private EmailMessage GetEmailMessage(string email, EmailTemplateLocalizationData localizationData, FileHolder file)
		{
			return new EmailMessage(localizationData.Subject, localizationData.Body, _defaultFrom, email)
			{
				Files = new[] { file },
				IsBodyHtml = localizationData.IsBodyHtml
			};
		}
	}
}