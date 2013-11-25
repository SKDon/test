using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;

namespace Alicargo.Jobs.ApplicationEvents
{
	public sealed class MessageFactoryEx : IMessageFactory
	{
		private readonly string _defaultFrom;
		private readonly IEmailTemplateRepository _templates;
		private readonly IApplicationRepository _applications;

		public MessageFactoryEx(
			string defaultFrom,
			IEmailTemplateRepository templates,
			IApplicationRepository applications)
		{
			_defaultFrom = defaultFrom;
			_templates = templates;
			_applications = applications;
		}

		public EmailMessage[] Get(long applicationId, ApplicationEventType type, byte[] data)
		{
			var template = _templates.GetByEventType(type);
			if (template == null || !template.EnableEmailSend)
			{
				return null;
			}

			var recipients = GetRecipients(type, applicationId);
			if (recipients == null || recipients.Length == 0)
			{
				return null;
			}

			return GetEmailMessages(applicationId, data, recipients, template).ToArray();
		}

		private IEnumerable<EmailMessage> GetEmailMessages(long applicationId, byte[] data,
			IEnumerable<RecipientData> recipients, ApplicationEventTemplateData template)
		{
			foreach (var recipient in recipients)
			{
				var localization = _templates.GetLocalization(template.EmailTemplateId, recipient.Culture);

				yield return GetEmailMessage(localization, applicationId, data, recipient.Email);
			}
		}

		private EmailMessage GetEmailMessage(EmailTemplateLocalizationData localization, long applicationId, byte[] data, string email)
		{
			var subject = ProcessText(localization.Subject, null);
			var body = ProcessText(localization.Body, null);

			return new EmailMessage(subject, body, _defaultFrom, email)
			{
				IsBodyHtml = localization.IsBodyHtml,
				Files = null // todo
			};
		}

		private string ProcessText(string text, ApplicationData data)
		{
			return text;
		}

		private RecipientData[] GetRecipients(ApplicationEventType type, long applicationId)
		{
			var recipientRoles = _templates.GetRecipientRoles(type);

			var application = _applications.Get(applicationId);

			return null; // todo
		}
	}
}