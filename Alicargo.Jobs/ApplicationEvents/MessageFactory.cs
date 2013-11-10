using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.Core.Helpers;
using Alicargo.Core.Models;
using Alicargo.Core.Resources;
using Alicargo.Core.Services.Abstract;
using Alicargo.Jobs.Entities;

namespace Alicargo.Jobs.ApplicationEvents
{
	public sealed class MessageFactory : IMessageFactory
	{
		private readonly IApplicationRepository _applications;
		private readonly IAwbRepository _awbs;
		private readonly IClientRepository _clients;
		private readonly ILocalizationService _localization;
		private readonly string _defaultFrom;
		private readonly IRecipients _recipients;
		private readonly IStateConfig _stateConfig;
		private readonly ISerializer _serializer;

		public MessageFactory(
			ISerializer serializer,
			IRecipients recipients,
			IStateConfig stateConfig,
			IApplicationRepository applications,
			IAwbRepository awbs,
			IClientRepository clients,
			ILocalizationService localization,
			string defaultFrom)
		{
			_serializer = serializer;
			_recipients = recipients;
			_stateConfig = stateConfig;
			_applications = applications;
			_awbs = awbs;
			_clients = clients;
			_localization = localization;
			_defaultFrom = defaultFrom;
		}

		public EmailMessage[] Get(long applicationId, ApplicationEventType type, byte[] bytes)
		{
			switch (type)
			{
				case ApplicationEventType.Created:
					return GetOnCreated(applicationId, bytes).ToArray();

				case ApplicationEventType.SetState:
					return GetOnSetState(applicationId, bytes).ToArray();

				case ApplicationEventType.CPFileUploaded:
					return GetOnCPFileUploaded(applicationId, bytes).ToArray();

				case ApplicationEventType.InvoiceFileUploaded:
					return GetOnInvoiceFileUploaded(applicationId, bytes).ToArray();

				case ApplicationEventType.PackingFileUploaded:
					return GetOnPackingFileUploaded(applicationId, bytes).ToArray();

				case ApplicationEventType.SwiftFileUploaded:
					return GetOnSwiftFileUploaded(applicationId, bytes).ToArray();

				case ApplicationEventType.DeliveryBillFileUploaded:
					return GetOnDeliveryBillFileUploaded(applicationId, bytes).ToArray();

				case ApplicationEventType.Torg12FileUploaded:
					return GetOnTorg12FileUploaded(applicationId, bytes).ToArray();

				case ApplicationEventType.SetDateOfCargoReceipt:
					return GetOnSetDateOfCargoReceipt(applicationId, bytes).ToArray();

				case ApplicationEventType.SetTransitReference:
					return GetOnSetTransitReference(applicationId).ToArray();

				default:
					throw new ArgumentOutOfRangeException("type");
			}
		}

		public EmailMessage Get(EmailMessageData data)
		{
			return new EmailMessage(data.Subject, data.Body, data.From, EmailMessageData.Split(data.To))
			{
				CopyTo = EmailMessageData.Split(data.CopyTo),
				Files = _serializer.Deserialize<FileHolder[]>(data.Files),
				IsBodyHtml = data.IsBodyHtml
			};
		}

		private IEnumerable<EmailMessage> GetOnCPFileUploaded(long applicationId, byte[] bytes)
		{
			ClientData client;
			string displayNumber;
			string subject;
			var data = GetData(applicationId, bytes, out client, out displayNumber, out subject);

			var body = string.Format(Mail.Application_CPFileAdded, client.LegalEntity, displayNumber);

			var admins = _recipients.GetAdminEmails().Select(x => x.Email).ToArray();

			yield return new EmailMessage(subject, body, _defaultFrom, client.Email) { Files = new[] { data.File }, CopyTo = admins };
		}

		private IEnumerable<EmailMessage> GetOnInvoiceFileUploaded(long applicationId, byte[] bytes)
		{
			ClientData client;
			string displayNumber;
			string subject;
			var data = GetData(applicationId, bytes, out client, out displayNumber, out subject);

			var body = string.Format(Mail.Application_InvoiceFileAdded,
				displayNumber, data.FactoryName, data.MarkName,
				data.Invoice, data.File.Name);

			var senders = _recipients.GetSenderEmails();

			yield return new EmailMessage(subject, body, _defaultFrom, client.Email) { Files = new[] { data.File } };

			yield return new EmailMessage(subject, body, _defaultFrom, senders.Select(x => x.Email).ToArray()) { Files = new[] { data.File } };
		}

		private IEnumerable<EmailMessage> GetOnPackingFileUploaded(long applicationId, byte[] bytes)
		{
			ClientData client;
			string displayNumber;
			string subject;
			var data = GetData(applicationId, bytes, out client, out displayNumber, out subject);

			var body = string.Format(Mail.Application_PackingFileAdded,
				displayNumber, data.FactoryName, data.MarkName, data.Invoice);

			var senders = _recipients.GetSenderEmails().Select(x => x.Email).ToArray();

			yield return new EmailMessage(subject, body, _defaultFrom, client.Email) { Files = new[] { data.File } };

			yield return new EmailMessage(subject, body, _defaultFrom, senders) { Files = new[] { data.File } };
		}

		private IEnumerable<EmailMessage> GetOnSwiftFileUploaded(long applicationId, byte[] bytes)
		{
			ClientData client;
			string displayNumber;
			string subject;
			var data = GetData(applicationId, bytes, out client, out displayNumber, out subject);

			var body = string.Format(Mail.Application_SwiftFileAdded, displayNumber,
				data.FactoryName, data.MarkName, data.Invoice);

			var senders = _recipients.GetSenderEmails().Select(x => x.Email).ToArray();

			yield return new EmailMessage(subject, body, _defaultFrom, client.Email) { Files = new[] { data.File } };

			yield return new EmailMessage(subject, body, _defaultFrom, senders) { Files = new[] { data.File } };
		}

		private IEnumerable<EmailMessage> GetOnDeliveryBillFileUploaded(long applicationId, byte[] bytes)
		{
			ClientData client;
			string displayNumber;
			string subject;
			var data = GetData(applicationId, bytes, out client, out displayNumber, out subject);

			var body = string.Format(Mail.Application_DeliveryBillFileAdded, displayNumber, data.FactoryName, data.MarkName, data.Invoice);

			yield return new EmailMessage(subject, body, _defaultFrom, client.Email) { Files = new[] { data.File } };
		}

		private IEnumerable<EmailMessage> GetOnTorg12FileUploaded(long applicationId, byte[] bytes)
		{
			ClientData client;
			string displayNumber;
			string subject;
			var data = GetData(applicationId, bytes, out client, out displayNumber, out subject);

			var body = string.Format(Mail.Application_Torg12FileAdded, client.LegalEntity, displayNumber);

			var admins = _recipients.GetAdminEmails().Select(x => x.Email).ToArray();

			yield return new EmailMessage(subject, body, _defaultFrom, client.Email) { Files = new[] { data.File }, CopyTo = admins };
		}

		private ApplicationFileUploadedEventData GetData(
			long applicationId, byte[] bytes, out ClientData client,
			out string displayNumber, out string subject)
		{
			var data = _serializer.Deserialize<ApplicationFileUploadedEventData>(bytes);

			var clientId = _applications.GetClientId(applicationId);

			client = _clients.Get(clientId);

			displayNumber = ApplicationHelper.GetDisplayNumber(applicationId, data.Count);

			subject = GetApplicationSubject(displayNumber);

			return data;
		}

		private IEnumerable<EmailMessage> GetOnSetDateOfCargoReceipt(long applicationId, byte[] bytes)
		{
			var dateOfCargoReceipt = _serializer.Deserialize<DateTimeOffset?>(bytes);

			var application = _applications.Get(applicationId);

			var subject = GetApplicationSubject(ApplicationHelper.GetDisplayNumber(application.Id, application.Count));

			var language = _clients.GetLanguage(application.ClientId);

			var body = string.Format(Mail.Application_SetDateOfCargoReceipt, _localization.GetDate(dateOfCargoReceipt, language));

			var client = _clients.Get(application.ClientId);

			yield return new EmailMessage(subject, body, _defaultFrom, client.Email);
		}

		private IEnumerable<EmailMessage> GetOnSetTransitReference(long applicationId)
		{
			return SendOnSetState(_applications.GetDetails(applicationId));
		}

		private IEnumerable<EmailMessage> GetOnSetState(long applicationId, byte[] bytes)
		{
			var state = _serializer.Deserialize<ApplicationSetStateEventData>(bytes);

			var details = _applications.GetDetails(applicationId);

			details.StateId = state.StateId;

			details.StateChangeTimestamp = state.Timestamp;

			return SendOnSetState(details);
		}

		public string ApplicationSetState(ApplicationDetailsData data, string culture)
		{
			return string.Format(Mail.Application_SetState,
								 ApplicationHelper.GetDisplayNumber(data.Id, data.Count),
								 _localization.GetDate(data.DateOfCargoReceipt, culture),
								 data.TransitCarrierName,
								 data.FactoryName,
								 data.FactoryEmail,
								 data.FactoryPhone,
								 data.FactoryContact,
								 ApplicationHelper.GetDaysInWork(data.CreationTimestamp),
								 data.Invoice,
								 _localization.GetDate(data.CreationTimestamp, culture),
								 _localization.GetDate(data.StateChangeTimestamp, culture),
								 data.MarkName,
								 data.Count,
								 data.Volume,
								 data.Weight,
								 data.Characteristic,
								 ApplicationHelper.GetValueString(data.Value, (CurrencyType)data.CurrencyId, culture),
								 data.AddressLoad,
								 data.CountryName.Where(x => x.Key == culture).Select(x => x.Value).First(),
								 data.WarehouseWorkingTime,
								 data.TermsOfDelivery,
								 _localization.GetMethodOfDelivery((MethodOfDelivery)data.MethodOfDeliveryId, culture),
								 data.TransitCity,
								 data.TransitAddress,
								 data.TransitRecipientName,
								 data.TransitPhone,
								 data.TransitWarehouseWorkingTime,
								 _localization.GetMethodOfTransit((MethodOfTransit)data.TransitMethodOfTransitId, culture),
								 _localization.GetDeliveryType((DeliveryType)data.TransitDeliveryTypeId, culture),
								 data.AirWaybill,
								 _localization.GetDate(data.AirWaybillDateOfDeparture, culture),
								 _localization.GetDate(data.AirWaybillDateOfArrival, culture),
								 data.AirWaybillGTD,
								 data.TransitReference,
								 _localization.GetStateName(data.StateId, culture));
		}

		private IEnumerable<EmailMessage> SendOnSetState(ApplicationDetailsData details)
		{
			var subject = GetApplicationSubject(ApplicationHelper.GetDisplayNumber(details.Id, details.Count));

			if (details.StateId == _stateConfig.CargoReceivedStateId)
			{
				var to = _recipients.GetAdminEmails().Concat(_recipients.GetForwarderEmails()).ToArray();

				foreach (var recipient in to)
				{
					var body = ApplicationSetState(details, recipient.Culture);

					yield return new EmailMessage(subject, body, _defaultFrom, recipient.Email);
				}
			}
			else
			{
				var files = GeAllFiles(details.AirWaybillId, details.Id);
				var culture = _clients.GetLanguage(details.ClientId);
				var body = ApplicationSetState(details, culture);

				yield return new EmailMessage(subject, body, _defaultFrom, details.ClientEmail) { Files = files };

				if (details.StateId == _stateConfig.CargoAtCustomsStateId || details.StateId == _stateConfig.CargoIsCustomsClearedStateId)
				{
					var to = _recipients.GetAdminEmails().Concat(_recipients.GetForwarderEmails()).ToArray();
					foreach (var recipient in to)
					{
						body = ApplicationSetState(details, recipient.Culture);

						yield return new EmailMessage(subject, body, _defaultFrom, recipient.Email);
					}
				}
			}
		}

		private FileHolder[] GeAllFiles(long? airWaybillId, long id)
		{
			var files = new List<FileHolder>(8);

			var invoiceFile = _applications.GetInvoiceFile(id);
			var deliveryBillFile = _applications.GetDeliveryBillFile(id);
			var cpFile = _applications.GetCPFile(id);
			var packingFile = _applications.GetPackingFile(id);
			var swiftFile = _applications.GetSwiftFile(id);
			var torg12File = _applications.GetTorg12File(id);

			if (airWaybillId.HasValue)
			{
				var gtdFile = _awbs.GetGTDFile(airWaybillId.Value);
				var gtdAdditionalFile = _awbs.GTDAdditionalFile(airWaybillId.Value);

				if (gtdFile != null) files.Add(gtdFile);
				if (gtdAdditionalFile != null) files.Add(gtdAdditionalFile);
			}

			if (invoiceFile != null) files.Add(invoiceFile);
			if (deliveryBillFile != null) files.Add(deliveryBillFile);
			if (cpFile != null) files.Add(cpFile);
			if (packingFile != null) files.Add(packingFile);
			if (swiftFile != null) files.Add(swiftFile);
			if (torg12File != null) files.Add(torg12File);

			return files.ToArray();
		}
		private IEnumerable<EmailMessage> GetOnCreated(long applicationId, byte[] bytes)
		{
			var data = _serializer.Deserialize<ApplicationCreatedEventData>(bytes);
			var client = _clients.Get(data.ClientId);
			var language = _clients.GetLanguage(data.ClientId);
			var displayNumber = ApplicationHelper.GetDisplayNumber(applicationId, data.Count);
			var subject = GetApplicationSubject(displayNumber);

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
					var body = string.Format(Mail.Application_Add,
						displayNumber, client.Nic, data.FactoryName, data.MarkName,
						_localization.GetDate(data.CreationTimestamp, recipient.Culture));

					return new EmailMessage(subject, body, _defaultFrom, recipient.Email);
				});
		}
		private static string GetApplicationSubject(string displayNumber)
		{
			return string.Format(Mail.Application_Subject, displayNumber);
		}
	}
}