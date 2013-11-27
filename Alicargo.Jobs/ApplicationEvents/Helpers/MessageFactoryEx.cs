using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public sealed class MessageFactoryEx : IMessageFactory
	{
		private readonly string _defaultFrom;		
		private readonly TemplateFacade _templates;

		public MessageFactoryEx(
			string defaultFrom,
			IEmailTemplateRepository templates,
			ISerializer serializer,
			IApplicationRepository applications)
		{
			_defaultFrom = defaultFrom;

			_templates = new TemplateFacade(serializer, templates, applications);
		}

		public EmailMessage[] Get(long applicationId, ApplicationEventType type, byte[] data)
		{
			var templateId = _templates.GetTemplateId(type, data);
			if (!templateId.HasValue)
			{
				return null;
			}

			var recipients = _templates.GetRecipients(type, applicationId);
			if (recipients == null || recipients.Length == 0)
			{
				return null;
			}

			return GetEmailMessages(applicationId, data, recipients, templateId.Value).ToArray();
		}

		

		private IEnumerable<EmailMessage> GetEmailMessages(long applicationId, byte[] data,
			IEnumerable<RecipientData> recipients, long templateId)
		{
			foreach (var recipient in recipients)
			{
				var localization = _templates.GetLocalization(recipient, templateId);

				if (localization == null || (string.IsNullOrWhiteSpace(localization.Body) && string.IsNullOrWhiteSpace(localization.Subject)))
				{
					continue;
				}

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

		
	}
}