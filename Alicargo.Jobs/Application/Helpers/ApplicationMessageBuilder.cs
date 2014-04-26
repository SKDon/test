using System.Collections.Generic;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Jobs.Helpers.Abstract;
using Alicargo.Utilities;
using ITextBuilder = Alicargo.Jobs.Application.Abstract.ITextBuilder;

namespace Alicargo.Jobs.Application.Helpers
{
	internal sealed class ApplicationMessageBuilder : IMessageBuilder
	{
		private readonly string _defaultFrom;
		private readonly ICommonFilesFacade _files;
		private readonly IRecipientsFacade _recipients;
		private readonly ISerializer _serializer;
		private readonly ITemplateRepositoryHelper _templates;
		private readonly ITextBuilder _textBuilder;
		private readonly IApplicationRepository _applications;

		public ApplicationMessageBuilder(
			string defaultFrom,
			ICommonFilesFacade files,
			ITextBuilder textBuilder,
			IRecipientsFacade recipients,
			ITemplateRepositoryHelper templates,
			ISerializer serializer, 
			IApplicationRepository applications)
		{
			_defaultFrom = defaultFrom;
			_files = files;
			_textBuilder = textBuilder;
			_recipients = recipients;
			_templates = templates;
			_serializer = serializer;
			_applications = applications;
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

			return GetEmailMessages(templateId.Value, recipients, data, type, files).ToArray();
		}

		private IEnumerable<EmailMessage> GetEmailMessages(long templateId, RecipientData[] recipients,
			EventDataForEntity data, EventType type, IReadOnlyDictionary<string, FileHolder[]> files)
		{
			var application = _applications.Get(data.EntityId);

			foreach(var recipient in recipients)
			{
				var localization = _templates.GetLocalization(templateId, recipient.Culture);

				if(localization == null)
				{
					continue;
				}

				var filesToSend = GetFiles(type, recipient, files);

				yield return GetEmailMessage(recipient.Email, recipient.Culture,
					localization, application, data.Data, type, filesToSend);
			}
		}

		private static FileHolder[] GetFiles(EventType type, RecipientData recipient, IReadOnlyDictionary<string, FileHolder[]> files)
		{
			if(type == EventType.ApplicationSetState && recipient.Role != RoleType.Client)
			{
				return null;
			}

			return files == null ? null : files[recipient.Culture];
		}

		private EmailMessage GetEmailMessage(string email, string culture, EmailTemplateLocalizationData localization,
			ApplicationData application, byte[] data, EventType type, FileHolder[] files)
		{
			var subject = _textBuilder.GetText(localization.Subject, culture, type, application, data);
			var body = _textBuilder.GetText(localization.Body, culture, type, application, data);

			return new EmailMessage(subject, body, _defaultFrom, email)
			{
				IsBodyHtml = localization.IsBodyHtml,
				Files = files
			};
		}
	}
}