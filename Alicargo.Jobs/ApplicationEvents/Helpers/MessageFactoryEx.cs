using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public sealed class MessageFactoryEx : IMessageFactory
	{
		private readonly string _defaultFrom;
		private readonly IFilesFasade _files;
		private readonly IRecipientsFacade _recipients;
		private readonly IApplicationRepository _applications;
		private readonly ITemplatesFacade _templates;

		public MessageFactoryEx(
			string defaultFrom,
			IFilesFasade files,
			IRecipientsFacade recipients,
			ITemplatesFacade templates, 
			IApplicationRepository applications)
		{
			_defaultFrom = defaultFrom;
			_files = files;
			_recipients = recipients;
			_templates = templates;
			_applications = applications;
		}

		public EmailMessage[] Get(long applicationId, ApplicationEventType type, byte[] data)
		{
			var application = _applications.Get(applicationId);
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

			return GetEmailMessages(applicationId, data, recipients, templateId.Value, files).ToArray();
		}

		private IEnumerable<EmailMessage> GetEmailMessages(long applicationId, byte[] data,
			IEnumerable<RecipientData> recipients, long templateId, FileHolder[] files)
		{
			foreach (var recipient in recipients)
			{
				var localization = _templates.GetLocalization(templateId, recipient.Culture);

				if (localization == null)
				{
					continue;
				}

				yield return GetEmailMessage(localization, applicationId, data, recipient.Email, files);
			}
		}

		private EmailMessage GetEmailMessage(EmailTemplateLocalizationData localization, long applicationId, byte[] data,
			string email, FileHolder[] files)
		{
			var subject = ProcessText(localization.Subject, null);
			var body = ProcessText(localization.Body, null);

			return new EmailMessage(subject, body, _defaultFrom, email)
			{
				IsBodyHtml = localization.IsBodyHtml,
				Files = files
			};
		}

		private string ProcessText(string text, ApplicationData data)
		{
			// todo
			return text;
		}
	}
}