using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.Application;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories.Application;
using Alicargo.Jobs.ApplicationEvents.Abstract;
using Alicargo.Jobs.Helpers.Abstract;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public sealed class MessageBuilder : IMessageBuilder
	{
		private readonly IApplicationRepository _applications;
		private readonly string _defaultFrom;
		private readonly IFilesFacade _files;
		private readonly IRecipientsFacade _recipients;
		private readonly ISerializer _serializer;
		private readonly IApplicationEventTemplates _templates;
		private readonly ITextBuilder _textBuilder;

		public MessageBuilder(
			string defaultFrom,
			IFilesFacade files,
			ITextBuilder textBuilder,
			IRecipientsFacade recipients,
			IApplicationEventTemplates templates,
			IApplicationRepository applications,
			ISerializer serializer)
		{
			_defaultFrom = defaultFrom;
			_files = files;
			_textBuilder = textBuilder;
			_recipients = recipients;
			_templates = templates;
			_applications = applications;
			_serializer = serializer;
		}

		public EmailMessage[] Get(EventType type, EventData eventData)
		{
			var data = _serializer.Deserialize<EventDataForEntity>(eventData.Data);

			return Get(type, data.EntityId, data.Data);
		}

		private EmailMessage[] Get(EventType type, long applicationId, byte[] applicationEventData)
		{
			var application = _applications.GetDetails(applicationId);
			if (application == null)
			{
				throw new InvalidOperationException("Can't find application by id " + applicationId);
			}

			var templateId = _templates.GetTemplateId(type, applicationEventData);
			if (!templateId.HasValue)
			{
				return null;
			}

			var recipients = _recipients.GetRecipients(application, type, applicationEventData);
			if (recipients == null || recipients.Length == 0)
			{
				return null;
			}

			var files = _files.GetFiles(applicationId, application.AirWaybillId, type, applicationEventData);

			return GetEmailMessages(templateId.Value, recipients, application, applicationEventData, type, files).ToArray();
		}

		private IEnumerable<EmailMessage> GetEmailMessages(long templateId, IEnumerable<RecipientData> recipients,
			ApplicationDetailsData application, byte[] data, EventType type, FileHolder[] files)
		{
			foreach (var recipient in recipients)
			{
				var localization = _templates.GetLocalization(templateId, recipient.Culture);

				if (localization == null)
				{
					continue;
				}

				if (type == EventType.ApplicationSetState && recipient.Role != RoleType.Client)
				{
					files = null;
				}

				yield return GetEmailMessage(recipient.Email, recipient.Culture, localization, application, data, type, files);
			}
		}

		private EmailMessage GetEmailMessage(string email, string culture, EmailTemplateLocalizationData localization,
			ApplicationDetailsData application, byte[] data, EventType type, FileHolder[] files)
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