﻿using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public sealed class MessageFactoryEx : IMessageFactory
	{
		private readonly IApplicationRepository _applications;
		private readonly string _defaultFrom;
		private readonly IFilesFasade _files;
		private readonly IRecipientsFacade _recipients;
		private readonly ITemplatesFacade _templates;
		private readonly ITextBulder _textBulder;

		public MessageFactoryEx(
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

			return GetEmailMessages(templateId.Value, recipients, application, data, type, files).ToArray();
		}

		private IEnumerable<EmailMessage> GetEmailMessages(long templateId, IEnumerable<Recipient> recipients,
			ApplicationData application, byte[] data, ApplicationEventType type, FileHolder[] files)
		{
			foreach (var recipient in recipients)
			{
				var localization = _templates.GetLocalization(templateId, recipient.Culture);

				if (localization == null)
				{
					continue;
				}

				if (type == ApplicationEventType.SetState && recipient.Role != RoleType.Client)
				{
					files = null;
				}

				yield return GetEmailMessage(recipient.Email, localization, application, data, type, files);
			}
		}

		private EmailMessage GetEmailMessage(string email, EmailTemplateLocalizationData localization,
			ApplicationData application, byte[] data, ApplicationEventType type, FileHolder[] files)
		{
			var subject = _textBulder.GetText(localization.Subject, type, application, data);
			var body = _textBulder.GetText(localization.Body, type, application, data);

			return new EmailMessage(subject, body, _defaultFrom, email)
			{
				IsBodyHtml = localization.IsBodyHtml,
				Files = files
			};
		}
	}
}