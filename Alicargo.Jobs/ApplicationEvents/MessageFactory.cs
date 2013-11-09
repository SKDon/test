using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Helpers;
using Alicargo.Core.Models;
using Alicargo.Core.Services.Abstract;
using Alicargo.Jobs.Entities;
using Resources;

namespace Alicargo.Jobs.ApplicationEvents
{
	public sealed class MessageFactory : IMessageFactory
	{
		private readonly IClientRepository _clients;
		private readonly ILocalizationService _localization;
		private readonly IRecipients _recipients;
		private readonly ISerializer _serializer;

		public MessageFactory(
			ISerializer serializer,
			IRecipients recipients,
			IClientRepository clients,
			ILocalizationService localization)
		{
			_serializer = serializer;
			_recipients = recipients;
			_clients = clients;
			_localization = localization;
		}

		public EmailMessage[] Get(ApplicationEventType type, byte[] bytes)
		{
			switch (type)
			{
				case ApplicationEventType.Created:
					return GetOnCreated(bytes).ToArray();

				case ApplicationEventType.SetState:
					return GetOnSetState(bytes).ToArray();

				case ApplicationEventType.CPFileUploaded:
					return GetOnCPFileUploaded(bytes).ToArray();

				case ApplicationEventType.InvoiceFileUploaded:
					return GetOnInvoiceFileUploaded(bytes).ToArray();

				case ApplicationEventType.PackingFileUploaded:
					return GetOnPackingFileUploaded(bytes).ToArray();

				case ApplicationEventType.SwiftFileUploaded:
					return GetOnSwiftFileUploaded(bytes).ToArray();

				case ApplicationEventType.DeliveryBillFileUploaded:
					return GetOnDeliveryBillFileUploaded(bytes).ToArray();

				case ApplicationEventType.Torg12FileUploaded:
					return GetOnTorg12FileUploaded(bytes).ToArray();

				case ApplicationEventType.SetDateOfCargoReceipt:
					return GetOnSetDateOfCargoReceipt(bytes).ToArray();

				case ApplicationEventType.SetTransitReference:
					return GetOnSetTransitReference(bytes).ToArray();

				default:
					throw new ArgumentOutOfRangeException("type");
			}
		}

		public EmailMessage Get(EmailMessageData data)
		{
			return new EmailMessage(data.Subject, data.Body, EmailMessageData.Split(data.To))
			{
				CopyTo = EmailMessageData.Split(data.CopyTo),
				Files = _serializer.Deserialize<FileHolder[]>(data.Files),
				From = data.From,
				IsBodyHtml = data.IsBodyHtml
			};
		}

		private IEnumerable<EmailMessage> GetOnCPFileUploaded(byte[] bytes)
		{
			throw new NotImplementedException();
		}

		private IEnumerable<EmailMessage> GetOnInvoiceFileUploaded(byte[] bytes)
		{
			throw new NotImplementedException();
		}

		private IEnumerable<EmailMessage> GetOnPackingFileUploaded(byte[] bytes)
		{
			throw new NotImplementedException();
		}

		private IEnumerable<EmailMessage> GetOnSwiftFileUploaded(byte[] bytes)
		{
			throw new NotImplementedException();
		}

		private IEnumerable<EmailMessage> GetOnDeliveryBillFileUploaded(byte[] bytes)
		{
			throw new NotImplementedException();
		}

		private IEnumerable<EmailMessage> GetOnTorg12FileUploaded(byte[] bytes)
		{
			throw new NotImplementedException();
		}

		private IEnumerable<EmailMessage> GetOnSetDateOfCargoReceipt(byte[] bytes)
		{
			throw new NotImplementedException();
		}

		private IEnumerable<EmailMessage> GetOnSetTransitReference(byte[] bytes)
		{
			throw new NotImplementedException();
		}

		private IEnumerable<EmailMessage> GetOnSetState(byte[] bytes)
		{
			throw new NotImplementedException();
		}

		private IEnumerable<EmailMessage> GetOnCreated(byte[] bytes)
		{
			var data = _serializer.Deserialize<ApplicationCreatedEventData>(bytes);
			var client = _clients.Get(data.ClientId);
			var language = _clients.GetLanguage(data.ClientId);

			return _recipients.GetAdminEmails()
				.Concat(_recipients.GetSenderEmails())
				.Concat(new[]
				{
					new Recipient
					{
						Culture = language,
						Email = client.Email
					}
				})
				.Select(recipient =>
				{
					var displayNumber = ApplicationHelper.GetDisplayNumber(data.Id, data.Count);

					var body = string.Format(Mail.Application_Add,
						displayNumber,
						client.Nic,
						data.FactoryName,
						data.MarkName,
						_localization.GetDate(data.CreationTimestamp, recipient.Culture));

					var subject = GetApplicationSubject(displayNumber);

					return new EmailMessage(subject, body, recipient.Email);
				});
		}

		public string GetApplicationSubject(string displayNumber)
		{
			return string.Format(Mail.Application_Subject, displayNumber);
		}
	}
}