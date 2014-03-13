using System.Collections.Generic;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Jobs.Helpers.Abstract;
using Alicargo.Utilities;

namespace Alicargo.Jobs.Helpers
{
	internal sealed class CommonEventMessageBuilder : IMessageBuilder
	{
		private readonly string _defaultFrom;
		private readonly ICommonFilesFacade _filesFacade;
		private readonly ILocalizedDataHelper _localizedHelper;
		private readonly IRecipientsFacade _recipients;
		private readonly ISerializer _serializer;
		private readonly ITemplateRepositoryHelper _templates;
		private readonly ITextBuilder _textBuilder;

		public CommonEventMessageBuilder(
			string defaultFrom,
			IRecipientsFacade recipients,
			ISerializer serializer,
			ITextBuilder textBuilder,
			ILocalizedDataHelper localizedHelper,
			ITemplateRepositoryHelper templates,
			ICommonFilesFacade filesFacade)
		{
			_defaultFrom = defaultFrom;
			_recipients = recipients;
			_serializer = serializer;
			_textBuilder = textBuilder;
			_localizedHelper = localizedHelper;
			_templates = templates;
			_filesFacade = filesFacade;
		}

		public EmailMessage[] Get(EventType type, EventData eventData)
		{
			var eventDataForEntity = _serializer.Deserialize<EventDataForEntity>(eventData.Data);

			var templateId = _templates.GetTemplateId(type);
			if(!templateId.HasValue)
			{
				return null;
			}

			var recipients = _recipients.GetRecipients(type, eventDataForEntity);
			if(recipients == null || recipients.Length == 0)
			{
				return null;
			}

			var languages = recipients.Select(x => x.Culture).Distinct().ToArray();

			var files = _filesFacade.GetFiles(type, eventDataForEntity, languages);

			var localizations = GetLocalizationData(eventDataForEntity, languages, templateId.Value);

			return recipients.Select(x =>
				GetEmailMessage(x.Email, localizations[x.Culture], files != null ? files[x.Culture] : null)).ToArray();
		}

		private EmailMessage GetEmailMessage(string email, EmailTemplateLocalizationData localizationData, FileHolder[] files)
		{
			return new EmailMessage(localizationData.Subject, localizationData.Body, _defaultFrom, email)
			{
				Files = files,
				IsBodyHtml = localizationData.IsBodyHtml
			};
		}

		private Dictionary<string, EmailTemplateLocalizationData> GetLocalizationData(
			EventDataForEntity eventData,
			string[] languages, long templateId)
		{
			return languages.ToDictionary(x => x,
				language =>
				{
					var template = _templates.GetLocalization(templateId, language);

					var localizedData = _localizedHelper.Get(language, eventData);

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