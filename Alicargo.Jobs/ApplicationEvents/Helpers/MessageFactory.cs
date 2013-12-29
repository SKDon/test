using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.Application;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Jobs.ApplicationEvents.Abstract;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public sealed class MessageFactory : IMessageFactory
	{
		private readonly IApplicationRepository _applications;
		private readonly string _defaultFrom;
		private readonly IFilesFasade _files;
		private readonly IRecipientsFacade _recipients;
		private readonly ITemplatesFacade _templates;
		private readonly ITextBulder _textBulder;

		public MessageFactory(
			string defaultFrom,
			IFilesFasade files,
			ITextBulder textBulder,
			IRecipientsFacade recipients,
			ITemplatesFacade templates,
			IApplicationRepository applications)
		{
			_defaultFrom = defaultFrom;
			_files = files;
			_textBulder = textBulder;
			_recipients = recipients;
			_templates = templates;
			_applications = applications;
		}

		public EmailMessage[] Get(long applicationId, EventType type, byte[] data)
		{
			var application = _applications.GetDetails(applicationId);
			if (application == null)
			{
				throw new InvalidOperationException("Can't find application by id " + applicationId);
			}

			var templateId = _templates.GetTemplateId(type, data);
			if (!templateId.HasValue)
			{
				return null;
			}

			var recipients = _recipients.GetRecipients(application, type, data);
			if (recipients == null || recipients.Length == 0)
			{
				return null;
			}

			var files = _files.GetFiles(applicationId, application.AirWaybillId, type, data);

			return GetEmailMessages(templateId.Value, recipients, application, data, type, files).ToArray();
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
			var subject = _textBulder.GetText(localization.Subject, culture, type, application, data);
			var body = _textBulder.GetText(localization.Body, culture, type, application, data);

			return new EmailMessage(subject, body, _defaultFrom, email)
			{
				IsBodyHtml = localization.IsBodyHtml,
				Files = files
			};
		}
	}
}