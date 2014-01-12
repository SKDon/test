using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Jobs.Balance.Entities;
using Alicargo.Jobs.Helpers.Abstract;
using Alicargo.Utilities;

namespace Alicargo.Jobs.Balance
{
	internal sealed class MessageBuilder : IMessageBuilder
	{
		private readonly IClientBalanceRepository _balance;		
		private readonly IClientRepository _clients;
		private readonly string _defaultFrom;
		private readonly IRecipientsFacade _recipients;
		private readonly ISerializer _serializer;
		private readonly ITemplateRepositoryHelper _templates;
		private readonly ITextBuilder _textBuilder;
		private readonly IClientExcelHelper _excel;

		public MessageBuilder(
			string defaultFrom,
			IRecipientsFacade recipients,
			IClientBalanceRepository balance,
			IClientRepository clients,
			ISerializer serializer,
			ITextBuilder textBuilder,
			IClientExcelHelper excel,
			ITemplateRepositoryHelper templates)
		{
			_defaultFrom = defaultFrom;
			_recipients = recipients;
			_balance = balance;
			_clients = clients;
			_serializer = serializer;
			_textBuilder = textBuilder;
			_excel = excel;
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

			var files = _excel.GetExcels(clientId, languages);

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

		private IDictionary<string, string> GetLocalizedData(string language, PaymentEventData paymentEventData,
			ClientData clientData)
		{
			var culture = CultureInfo.GetCultureInfo(language);
			var balance = _balance.GetBalance(clientData.ClientId);

			return new Dictionary<string, string>
			{
				{ "ClientBalance", balance.ToString("N2") },
				{ "Money", paymentEventData.Money.ToString("N2") },
				{ "Comment", paymentEventData.Comment },
				{ "ClientNic", clientData.Nic },
				{ "LegalEntity", clientData.LegalEntity },
				{ "Timestamp", LocalizationHelper.GetDate(paymentEventData.Timestamp, culture) }
			};
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