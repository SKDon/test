using System.Collections.Generic;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Jobs.Application.Abstract;
using Alicargo.Jobs.Helpers.Abstract;
using Alicargo.Utilities;
using ITextBuilder = Alicargo.Jobs.Application.Abstract.ITextBuilder;

namespace Alicargo.Jobs.Application.Helpers
{
	internal sealed class MessageBuilder : IMessageBuilder
	{
		private readonly string _defaultFrom;
		private readonly IClientExcelHelper _excel;
		private readonly IFilesFacade _files;
		private readonly IRecipientsFacade _recipients;
		private readonly ISerializer _serializer;
		private readonly ITemplateRepositoryHelper _templates;
		private readonly ITextBuilder _textBuilder;
		private readonly IApplicationRepository _applications;

		public MessageBuilder(
			string defaultFrom,
			IFilesFacade files,
			ITextBuilder textBuilder,
			IRecipientsFacade recipients,
			ITemplateRepositoryHelper templates,
			IClientExcelHelper excel,
			ISerializer serializer, 
			IApplicationRepository applications)
		{
			_defaultFrom = defaultFrom;
			_files = files;
			_textBuilder = textBuilder;
			_recipients = recipients;
			_templates = templates;
			_excel = excel;
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

			var files = _files.GetFiles(type, data);

			return GetEmailMessages(templateId.Value, recipients, data, type, files).ToArray();
		}

		private IEnumerable<EmailMessage> GetEmailMessages(long templateId, RecipientData[] recipients,
			EventDataForEntity data, EventType type, FileHolder[] files)
		{
			var application = _applications.GetExtendedData(data.EntityId);

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
					localization, application, data.Data, type, filesToSend);
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
			ApplicationExtendedData application, byte[] data, EventType type, FileHolder[] files)
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