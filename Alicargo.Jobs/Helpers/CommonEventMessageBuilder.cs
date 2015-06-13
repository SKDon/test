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
		private readonly ICommonFilesFacade _files;
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
			ICommonFilesFacade files)
		{
			_defaultFrom = defaultFrom;
			_recipients = recipients;
			_serializer = serializer;
			_textBuilder = textBuilder;
			_localizedHelper = localizedHelper;
			_templates = templates;
			_files = files;
		}

		public EmailMessage[] Get(EventType type, EventData eventData)
		{
			var data = _serializer.Deserialize<EventDataForEntity>(eventData.Data);

			var templateId = _templates.GetTemplateId(type);
			if(!templateId.HasValue)
			{
				return null;
			}

			var recipients = _recipients.GetRecipients(type, data);
			if(recipients == null || recipients.Length == 0)
			{
				return null;
			}

			var languages = recipients.Select(x => x.Culture).Distinct().ToArray();

			var files = _files.GetFiles(type, data, languages);

			return GetEmailMessages(templateId.Value, recipients, data, files, eventData.UserId, languages).ToArray();
		}

		private IEnumerable<EmailMessage> GetEmailMessages(
			long templateId,
			IEnumerable<RecipientData> recipients,
			EventDataForEntity data,
			IReadOnlyDictionary<string, FileHolder[]> files,
			long? userId,
			IEnumerable<string> languages)
		{
			var localizations = GetLocalizationData(data, languages, templateId);

			var emailMessages = recipients.Select(
				x => GetEmailMessage(
					userId,
					x.Email,
					localizations[x.Culture],
					files != null ? files[x.Culture] : null));

			return emailMessages;
		}

		private EmailMessage GetEmailMessage(
			long? userId,
			string email,
			EmailTemplateLocalizationData localizationData,
			FileHolder[] files)
		{
			return new EmailMessage(localizationData.Subject, localizationData.Body, _defaultFrom, email, userId)
			{
				Files = files,
				IsBodyHtml = localizationData.IsBodyHtml
			};
		}

		private Dictionary<string, EmailTemplateLocalizationData> GetLocalizationData(
			EventDataForEntity eventData,
			IEnumerable<string> languages,
			long templateId)
		{
			return languages.ToDictionary(
				x => x,
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