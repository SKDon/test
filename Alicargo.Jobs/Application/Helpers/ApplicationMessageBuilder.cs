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
		private readonly IApplicationRepository _applications;
		private readonly string _defaultFrom;
		private readonly ICommonFilesFacade _files;
		private readonly IRecipientsFacade _recipients;
		private readonly ISerializer _serializer;
		private readonly ITemplateRepositoryHelper _templates;
		private readonly ITextBuilder _textBuilder;

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

			return GetEmailMessages(templateId.Value, recipients, data, files, eventData.UserId, type).ToArray();
		}

		private IEnumerable<EmailMessage> GetEmailMessages(
			long templateId,
			IEnumerable<RecipientData> recipients,
			EventDataForEntity data,
			IReadOnlyDictionary<string, FileHolder[]> files,
			long? userId,
			EventType type)
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

				yield return GetEmailMessage(
					recipient.Email,
					recipient.Culture,
					localization,
					application,
					data.Data,
					type,
					filesToSend,
					userId);
			}
		}

		private static FileHolder[] GetFiles(
			EventType type,
			RecipientData recipient,
			IReadOnlyDictionary<string, FileHolder[]> files)
		{
			if(type == EventType.ApplicationSetState && recipient.Role != RoleType.Client)
			{
				return null;
			}

			return files == null ? null : files[recipient.Culture];
		}

		private EmailMessage GetEmailMessage(
			string email,
			string culture,
			EmailTemplateLocalizationData localization,
			ApplicationData application,
			byte[] data,
			EventType type,
			FileHolder[] files,
			long? emailSenderUserId)
		{
			var subject = _textBuilder.GetText(localization.Subject, culture, type, application, data);
			var body = _textBuilder.GetText(localization.Body, culture, type, application, data);

			return new EmailMessage(subject, body, _defaultFrom, email, emailSenderUserId)
			{
				IsBodyHtml = localization.IsBodyHtml,
				Files = files
			};
		}
	}
}