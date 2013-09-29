using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.Services.Contract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;
using Alicargo.ViewModels.Helpers;

namespace Alicargo.Services.Email
{
	internal sealed class ApplicationManagerWithMailing : IApplicationManager
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

		public ApplicationAdminModel Get(long id)
		{
			return _manager.Get(id);
		}

		public void Update(long applicationId, ApplicationAdminModel model, CarrierSelectModel carrierModel,
						   TransitEditModel transitModel)
		{
			var oldData = _applicationPresenter.GetDetails(applicationId);

			_manager.Update(applicationId, model, carrierModel, transitModel);

			SendOnFileAdd(applicationId, oldData);
		}

		public long Add(ApplicationAdminModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel,
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

	    public void SetTransitCost(long id, decimal? transitCost)
	    {
		    _manager.SetTransitCost(id, transitCost);

		    // todo: 2. think what an email should be sent
	    }

	    public void SetTariffPerKg(long id, decimal? tariffPerKg)
	    {
		    _manager.SetTariffPerKg(id, tariffPerKg);

			// todo: 2. think what an email should be sent
	    }

	    public void SetWithdrawCostEdited(long id, decimal? withdrawCost)
	    {
		    _manager.SetWithdrawCostEdited(id, withdrawCost);

			// todo: 2. think what an email should be sent
	    }

	    public void SetFactureCostEdited(long id, decimal? factureCost)
	    {
		    _manager.SetFactureCostEdited(id, factureCost);

			// todo: 2. think what an email should be sent
	    }

	    public void SetScotchCostEdited(long id, decimal? scotchCost)
	    {
		    _manager.SetScotchCostEdited(id, scotchCost);

			// todo: 2. think what an email should be sent
	    }

		public void SetSenderRate(long id, decimal? senderRate)
		{
			_manager.SetSenderRate(id, senderRate);
		}

		public void SetTransitReference(long id, string transitReference)
		{
			_manager.SetTransitReference(id, transitReference);

			SendOnSetState(id);
		}

		private void SendOnFileAdd(long id, ApplicationDetailsModel old)
		{
			var details = _applicationPresenter.GetDetails(id);
			var subject = _messageBuilder.GetApplicationSubject(ApplicationModelHelper.GetDisplayNumber(details.Id, details.Count));

			if (old.InvoiceFileName == null && details.InvoiceFileName != null)
			{
				var body = _messageBuilder.ApplicationInvoiceFileAdded(details);
				var to = _messageBuilder.GetSenderEmails();
				var file = _applicationRepository.GetInvoiceFile(details.Id);

				_mailSender.Send(new Message(subject, body, details.ClientEmail) { Files = new[] { file } });
				_mailSender.Send(new Message(subject, body, to.Select(x => x.Email).ToArray()) { Files = new[] { file } });
			}

			if (old.SwiftFileName == null && details.SwiftFileName != null)
			{
				var body = _messageBuilder.ApplicationSwiftFileAdded(details);
				var to = _messageBuilder.GetSenderEmails();
				var file = _applicationRepository.GetSwiftFile(details.Id);

				_mailSender.Send(new Message(subject, body, details.ClientEmail) { Files = new[] { file } });
				_mailSender.Send(new Message(subject, body, to.Select(x => x.Email).ToArray()) { Files = new[] { file } });
			}

			if (old.PackingFileName == null && details.PackingFileName != null)
			{
				var body = _messageBuilder.ApplicationPackingFileAdded(details);
				var file = _applicationRepository.GetPackingFile(details.Id);
				_mailSender.Send(new Message(subject, body, details.ClientEmail) { Files = new[] { file } });

				var to = _messageBuilder.GetSenderEmails().Select(x => x.Email).ToArray();
				_mailSender.Send(new Message(subject, body, to) { Files = new[] { file } });
			}

			if (old.DeliveryBillFileName == null && details.DeliveryBillFileName != null)
			{
				var body = _messageBuilder.ApplicationDeliveryBillFileAdded(details);
				var file = _applicationRepository.GetDeliveryBillFile(details.Id);

				_mailSender.Send(new Message(subject, body, details.ClientEmail) { Files = new[] { file } });
			}

			if (old.Torg12FileName == null && details.Torg12FileName != null)
			{
				var body = _messageBuilder.ApplicationTorg12FileAdded(details, details.ClientLegalEntity);
				var file = _applicationRepository.GetTorg12File(details.Id);
				var admins = _messageBuilder.GetAdminEmails().Select(x => x.Email).ToArray();

				_mailSender.Send(new Message(subject, body, details.ClientEmail) { Files = new[] { file }, CopyTo = admins });
			}

			if (old.CPFileName == null && details.CPFileName != null)
			{
				var body = _messageBuilder.ApplicationCPFileAdded(details, details.ClientLegalEntity);
				var file = _applicationRepository.GetCPFile(details.Id);
				var admins = _messageBuilder.GetAdminEmails().Select(x => x.Email).ToArray();

				_mailSender.Send(new Message(subject, body, details.ClientEmail) { Files = new[] { file }, CopyTo = admins });
			}
		}

		private void SendOnAdd(long id)
		{
			var model = _applicationPresenter.GetDetails(id);
			var subject = _messageBuilder.GetApplicationSubject(ApplicationModelHelper.GetDisplayNumber(model.Id, model.Count));
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

		private void SendOnSetState(long appId)
		{
			var details = _applicationPresenter.GetDetails(appId);
			var stateId = details.StateId;

			var subject = _messageBuilder.GetApplicationSubject(ApplicationModelHelper.GetDisplayNumber(details.Id, details.Count));

			if (stateId == _stateConfig.CargoReceivedStateId)
			{
				var to = _messageBuilder.GetAdminEmails().Concat(_messageBuilder.GetForwarderEmails()).ToArray();
				foreach (var recipient in to)
				{
					var body = _messageBuilder.ApplicationSetState(details, recipient.Culture);
					_mailSender.Send(new Message(subject, body, recipient.Email));
				}
			}
			else
			{
				var files = GeAllFiles(details.AirWaybillId, details.Id);
				var clientData = _authenticationRepository.GetById(details.ClientUserId);
				var body = _messageBuilder.ApplicationSetState(details, clientData.TwoLetterISOLanguageName);
				_mailSender.Send(new Message(subject, body, details.ClientEmail) { Files = files });

				if (stateId == _stateConfig.CargoAtCustomsStateId || stateId == _stateConfig.CargoIsCustomsClearedStateId)
				{
					var to = _messageBuilder.GetAdminEmails().Concat(_messageBuilder.GetForwarderEmails()).ToArray();
					foreach (var recipient in to)
					{
						body = _messageBuilder.ApplicationSetState(details, recipient.Culture);
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
			var subject = _messageBuilder.GetApplicationSubject(ApplicationModelHelper.GetDisplayNumber(model.Id, model.Count));
			var clientData = _authenticationRepository.GetById(model.ClientUserId);
			var body = _messageBuilder.ApplicationSetDateOfCargoReceipt(model, clientData.TwoLetterISOLanguageName);
			var message = new Message(subject, body, model.ClientEmail);

			_mailSender.Send(message);
		}
	}
}