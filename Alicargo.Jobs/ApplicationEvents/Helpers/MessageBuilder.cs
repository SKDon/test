using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Jobs.ApplicationEvents.Abstract;
using Alicargo.Jobs.Helpers.Abstract;
using Alicargo.Utilities;
using ITextBuilder = Alicargo.Jobs.ApplicationEvents.Abstract.ITextBuilder;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	internal sealed class MessageBuilder : IMessageBuilder
	{
		private readonly IApplicationRepository _applications;
		private readonly string _defaultFrom;
		private readonly IClientExcelHelper _excel;
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
			IClientExcelHelper excel,
			ISerializer serializer)
		{
			_defaultFrom = defaultFrom;
			_files = files;
			_textBuilder = textBuilder;
			_recipients = recipients;
			_templates = templates;
			_applications = applications;
			_excel = excel;
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
			if(application == null)
			{
				throw new InvalidOperationException("Can't find application by id " + applicationId);
			}

			var templateId = _templates.GetTemplateId(type, applicationEventData);
			if(!templateId.HasValue)
			{
				return null;
			}

			var recipients = _recipients.GetRecipients(application, type, applicationEventData);
			if(recipients == null || recipients.Length == 0)
			{
				return null;
			}

			var files = _files.GetFiles(applicationId, application.AirWaybillId, type, applicationEventData);

			return GetEmailMessages(templateId.Value, recipients, application, applicationEventData, type, files).ToArray();
		}

		private IEnumerable<EmailMessage> GetEmailMessages(long templateId, RecipientData[] recipients,
			ApplicationDetailsData application, byte[] data, EventType type, FileHolder[] files)
		{
			IReadOnlyDictionary<string, FileHolder> excels = null;
			if(type == EventType.Calculate || type == EventType.CalculationCanceled)
			{
				var languages = recipients.Select(x => x.Culture).Distinct().ToArray();
				excels = _excel.GetExcels(application.ClientId, languages);
			}

			foreach(var recipient in recipients)
			{
				var localization = _templates.GetLocalization(templateId, recipient.Culture);

				if(localization == null)
				{
					continue;
				}

				var filesToSend = GetFiles(type, files, recipient, excels);

				yield return GetEmailMessage(recipient.Email, recipient.Culture,
					localization, application, data, type, filesToSend);
			}
		}

		private static FileHolder[] GetFiles(EventType type, FileHolder[] files,
			RecipientData recipient, IReadOnlyDictionary<string, FileHolder> excels)
		{
			if(type == EventType.ApplicationSetState && recipient.Role != RoleType.Client)
			{
				files = null;
			}

			var filesToSend = new List<FileHolder>();
			if(files != null)
			{
				filesToSend.AddRange(files);
			}

			if(excels != null)
			{
				var excel = excels[recipient.Culture];
				filesToSend.Add(excel);
			}

			return filesToSend.Count != 0 ? filesToSend.ToArray() : null;
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