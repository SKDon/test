using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.Services.Contract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Email
{
    // todo: 1.5. create the mailing engine
    // todo: 2. send a message constraction
	public sealed class ApplicationManagerWithMailing : IApplicationManager
	{
		private readonly IApplicationPresenter _applicationPresenter;
		private readonly IApplicationRepository _applicationRepository;
		private readonly IAuthenticationRepository _authenticationRepository;
		private readonly IAwbRepository _awbRepository;
		private readonly IMailSender _mailSender;
		private readonly IApplicationManager _manager;
		private readonly IMessageBuilder _messageBuilder;
		private readonly IStateConfig _stateConfig;

		public ApplicationManagerWithMailing(
			IMailSender mailSender,
			IMessageBuilder messageBuilder,
			IStateConfig stateConfig,
			IApplicationPresenter applicationPresenter,
			IAuthenticationRepository authenticationRepository,
			IAwbRepository awbRepository,
			IApplicationRepository applicationRepository,
			IApplicationManager manager)
		{
			_mailSender = mailSender;
			_messageBuilder = messageBuilder;
			_stateConfig = stateConfig;
			_applicationPresenter = applicationPresenter;
			_authenticationRepository = authenticationRepository;
			_awbRepository = awbRepository;
			_applicationRepository = applicationRepository;
			_manager = manager;
		}

		public ApplicationEditModel Get(long id)
		{
			return _manager.Get(id);
		}

		public void Update(long applicationId, ApplicationEditModel model, CarrierSelectModel carrierModel,
						   TransitEditModel transitModel)
		{
			var oldData = _applicationPresenter.GetDetails(applicationId);

			_manager.Update(applicationId, model, carrierModel, transitModel);

			SendOnFileAdd(applicationId, oldData);
		}

		public long Add(ApplicationEditModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel,
						long clientId)
		{
			var id = _manager.Add(model, carrierModel, transitModel, clientId);

			SendOnAdd(id);

			return id;
		}

		public void Delete(long id)
		{
			_manager.Delete(id);
		}

		public void SetState(long id, long stateId)
		{
			_manager.SetState(id, stateId);

			SendOnSetState(id);
		}

		public void SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt)
		{
			_manager.SetDateOfCargoReceipt(id, dateOfCargoReceipt);

			SendOnSetDateOfCargoReceipt(id);
		}

		public void SetTransitReference(long id, string transitReference)
		{
			_manager.SetTransitReference(id, transitReference);

			SendOnSetState(id);
		}

		private void SendOnFileAdd(long id, ApplicationDetailsModel old)
		{
			var details = _applicationPresenter.GetDetails(id);

			var subject = string.Format(_messageBuilder.ApplicationSubject,
										ApplicationModelHelper.GetDisplayNumber(details.Id, details.Count));

			if (old.InvoiceFileName == null && details.InvoiceFileName != null)
			{
				var body = _messageBuilder.ApplicationInvoiceFileAdded(details);
				var to = _messageBuilder.GetSenderEmails();
				var file = _applicationRepository.GetInvoiceFile(details.Id);

				_mailSender.Send(new Message(subject, body, details.ClientEmail) {Files = new[] {file}});
				_mailSender.Send(new Message(subject, body, to.Select(x => x.Email).ToArray()) {Files = new[] {file}});
			}

			if (old.SwiftFileName == null && details.SwiftFileName != null)
			{
				var body = _messageBuilder.ApplicationSwiftFileAdded(details);
				var to = _messageBuilder.GetSenderEmails();
				var file = _applicationRepository.GetSwiftFile(details.Id);

				_mailSender.Send(new Message(subject, body, details.ClientEmail) {Files = new[] {file}});
				_mailSender.Send(new Message(subject, body, to.Select(x => x.Email).ToArray()) {Files = new[] {file}});
			}

			if (old.PackingFileName == null && details.PackingFileName != null)
			{
				var body = _messageBuilder.ApplicationPackingFileAdded(details);
				var file = _applicationRepository.GetPackingFile(details.Id);
				_mailSender.Send(new Message(subject, body, details.ClientEmail) {Files = new[] {file}});

				var to = _messageBuilder.GetSenderEmails().Select(x => x.Email).ToArray();
				_mailSender.Send(new Message(subject, body, to) {Files = new[] {file}});
			}

			if (old.DeliveryBillFileName == null && details.DeliveryBillFileName != null)
			{
				var body = _messageBuilder.ApplicationDeliveryBillFileAdded(details);
				var file = _applicationRepository.GetDeliveryBillFile(details.Id);

				_mailSender.Send(new Message(subject, body, details.ClientEmail) {Files = new[] {file}});
			}

			if (old.Torg12FileName == null && details.Torg12FileName != null)
			{
				var body = _messageBuilder.ApplicationTorg12FileAdded(details);
				var file = _applicationRepository.GetTorg12File(details.Id);

				_mailSender.Send(new Message(subject, body, details.ClientEmail) {Files = new[] {file}});
			}

			if (old.CPFileName == null && details.CPFileName != null)
			{
				var body = _messageBuilder.ApplicationCPFileAdded(details);
				var file = _applicationRepository.GetCPFile(details.Id);

				_mailSender.Send(new Message(subject, body, details.ClientEmail) {Files = new[] {file}});
			}
		}

		private void SendOnAdd(long id)
		{
			var model = _applicationPresenter.GetDetails(id);
			var subject = string.Format(_messageBuilder.ApplicationSubject,
										ApplicationModelHelper.GetDisplayNumber(model.Id, model.Count));
			var clientData = _authenticationRepository.GetById(model.ClientUserId);

			var to = _messageBuilder.GetAdminEmails()
									.Concat(_messageBuilder.GetSenderEmails())
									.Concat(new[]
									{
										new Recipient
										{
											Culture = clientData.TwoLetterISOLanguageName,
											Email = model.ClientEmail
										}
									})
									.ToArray();

			foreach (var recipient in to)
			{
				var body = _messageBuilder.ApplicationAdd(model, recipient.Culture);

				_mailSender.Send(new Message(subject, body, recipient.Email));
			}
		}

		private void SendOnSetState(long id)
		{
			var model = _applicationPresenter.GetDetails(id);
			var stateId = model.StateId;

			var subject = string.Format(_messageBuilder.ApplicationSubject,
										ApplicationModelHelper.GetDisplayNumber(model.Id, model.Count));

            // todo: 1. test state dependency
			if (stateId == _stateConfig.CargoReceivedStateId)
			{
				var to = _messageBuilder.GetAdminEmails().Concat(_messageBuilder.GetForwarderEmails()).ToArray();
				foreach (var recipient in to)
				{
					var body = _messageBuilder.ApplicationSetState(model, recipient.Culture);
					_mailSender.Send(new Message(subject, body, recipient.Email));
				}
			}
			else
			{
				var files = GeAllFiles(model.AirWaybillId, model.Id);
				var clientData = _authenticationRepository.GetById(model.ClientUserId);
				var body = _messageBuilder.ApplicationSetState(model, clientData.TwoLetterISOLanguageName);
				_mailSender.Send(new Message(subject, body, model.ClientEmail) {Files = files});

				if (stateId == _stateConfig.CargoAtCustomsStateId || stateId == _stateConfig.CargoIsCustomsClearedStateId)
				{
					var to = _messageBuilder.GetAdminEmails().Concat(_messageBuilder.GetForwarderEmails()).ToArray();
					foreach (var recipient in to)
					{
						body = _messageBuilder.ApplicationSetState(model, recipient.Culture);
						_mailSender.Send(new Message(subject, body, recipient.Email));
					}
				}
			}
		}

		private FileHolder[] GeAllFiles(long? airWaybillId, long id)
		{
			var files = new List<FileHolder>(6);

			var invoiceFile = _applicationRepository.GetInvoiceFile(id);
			var deliveryBillFile = _applicationRepository.GetDeliveryBillFile(id);
			var cpFile = _applicationRepository.GetCPFile(id);
			var packingFile = _applicationRepository.GetPackingFile(id);
			var swiftFile = _applicationRepository.GetSwiftFile(id);
			var torg12File = _applicationRepository.GetTorg12File(id);

            // todo: 1. test
			if (airWaybillId.HasValue)
			{
				var gtdFile = _awbRepository.GetGTDFile(airWaybillId.Value);
				var gtdAdditionalFile = _awbRepository.GTDAdditionalFile(airWaybillId.Value);

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

		private void SendOnSetDateOfCargoReceipt(long id)
		{
			var model = _applicationPresenter.GetDetails(id);
			var subject = string.Format(_messageBuilder.ApplicationSubject,
										ApplicationModelHelper.GetDisplayNumber(model.Id, model.Count));
			var clientData = _authenticationRepository.GetById(model.ClientUserId);
			var body = _messageBuilder.ApplicationSetDateOfCargoReceipt(model, clientData.TwoLetterISOLanguageName);
			var message = new Message(subject, body, model.ClientEmail);

			_mailSender.Send(message);
		}
	}
}