using System.Collections.Generic;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Jobs.Helpers.Abstract;
using Alicargo.Utilities;

namespace Alicargo.Jobs.Client
{
	internal sealed class ClientEventMessageBuilder : IMessageBuilder
	{
		private readonly string _defaultFrom;
		private readonly ISerializer _serializer;
		private readonly IClientRepository _clients;
		private readonly IClientExcelHelper _excel;
		private readonly ILocalizedDataHelper _localizedHelper;
		private readonly IClientEventRecipientsFacade _recipients;
		private readonly ITemplateRepositoryHelper _templates;
		private readonly ITextBuilder _textBuilder;

		public ClientEventMessageBuilder(
			string defaultFrom,
			IClientEventRecipientsFacade recipients,
			IClientRepository clients,
			ISerializer serializer,
			ITextBuilder textBuilder,
			IClientExcelHelper excel,
			ILocalizedDataHelper localizedHelper,
			ITemplateRepositoryHelper templates)
		{
			_defaultFrom = defaultFrom;
			_recipients = recipients;
			_clients = clients;
			_serializer = serializer;
			_textBuilder = textBuilder;
			_excel = excel;
			_localizedHelper = localizedHelper;
			_templates = templates;
		}

		public EmailMessage[] Get(EventType type, EventData eventData)
		{
			var eventDataForEntity = _serializer.Deserialize<EventDataForEntity>(eventData.Data);
			var clientId = eventDataForEntity.EntityId;

			var templateId = _templates.GetTemplateId(type);
			if(!templateId.HasValue)
			{
				return null;
			}

			var recipients = _recipients.GetRecipients(type, clientId);
			var languages = recipients.Select(x => x.Culture).Distinct().ToArray();

			var files = _excel.GetExcels(clientId, languages);

			var localizations = GetLocalizationData(eventDataForEntity, languages, templateId.Value, clientId);

			return recipients.Select(x =>
				GetEmailMessage(x.Email, localizations[x.Culture], files != null ? files[x.Culture] : null)).ToArray();
		}

		private EmailMessage GetEmailMessage(string email, EmailTemplateLocalizationData localizationData, FileHolder file)
		{
			return new EmailMessage(localizationData.Subject, localizationData.Body, _defaultFrom, email)
			{
				Files = file != null ? new[] { file } : null,
				IsBodyHtml = localizationData.IsBodyHtml
			};
		}

		private Dictionary<string, EmailTemplateLocalizationData> GetLocalizationData(EventDataForEntity eventData,
			string[] languages, long templateId, long clientId)
		{
			var clientData = _clients.Get(clientId);

			return languages.ToDictionary(x => x,
				language =>
				{
					var template = _templates.GetLocalization(templateId, language);

					var localizedData = _localizedHelper.Get(language, eventData.Data, clientData);

					return new EmailTemplateLocalizationData
					{
						IsBodyHtml = template.IsBodyHtml,
						Subject = _textBuilder.GetText(template.Subject, language, localizedData),
						Body = _textBuilder.GetText(template.Body, language, localizedData)
					};
				});
		}
	}
}