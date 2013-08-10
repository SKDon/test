using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Core.Contracts;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.Services.Contract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Email
{
	// todo: test!!!
	public sealed class ApplicationManagerWithMailing : IApplicationManager
	{
		private readonly IMailSender _mailSender;
		private readonly IMessageBuilder _messageBuilder;
		private readonly IStateConfig _stateConfig;
		private readonly IApplicationPresenter _applicationPresenter;
		private readonly IAuthenticationRepository _authenticationRepository;
		private readonly IAirWaybillRepository _airWaybillRepository;
		private readonly IApplicationRepository _applicationRepository;
		private readonly IApplicationManager _manager;

		public ApplicationManagerWithMailing(
			IMailSender mailSender,
			IMessageBuilder messageBuilder,
			IStateConfig stateConfig,
			IApplicationPresenter applicationPresenter,
			IAuthenticationRepository authenticationRepository,
			IAirWaybillRepository airWaybillRepository,
			IApplicationRepository applicationRepository,
			IApplicationManager manager)
		{
			_mailSender = mailSender;
			_messageBuilder = messageBuilder;
			_stateConfig = stateConfig;
			_applicationPresenter = applicationPresenter;
			_authenticationRepository = authenticationRepository;
			_airWaybillRepository = airWaybillRepository;
			_applicationRepository = applicationRepository;
			_manager = manager;
		}

		public ApplicationEditModel Get(long id)
		{
			return _manager.Get(id);
		}

		public void Update(ApplicationEditModel model, CarrierSelectModel carrierSelectModel)
		{
			var oldData = _applicationPresenter.Get(model.Id);

			_manager.Update(model, carrierSelectModel);

			SendOnFileAdd(model.Id, oldData);
		}

		private void SendOnFileAdd(long id, ApplicationDetailsModel oldData)
		{
			var model = _applicationPresenter.Get(id);

			var subject = string.Format(_messageBuilder.ApplicationSubject, ApplicationModelHelper.GetDisplayNumber(model.Id, model.Count));

			if (oldData.InvoiceFileName == null && model.InvoiceFileName != null)
			{
				var body = _messageBuilder.ApplicationInvoiceFileAdded(model);
				var to = _messageBuilder.GetSenderEmails();
				var file = _applicationRepository.GetInvoiceFile(model.Id);
				_mailSender.Send(new Message(subject, body, model.ClientEmail) { Files = new[] { file } });
				_mailSender.Send(new Message(subject, body, to.Select(x => x.Email).ToArray()) { Files = new[] { file } });
			}

			if (oldData.SwiftFileName == null && model.SwiftFileName != null)
			{
				var body = _messageBuilder.ApplicationSwiftFileAdded(model);
				var to = _messageBuilder.GetSenderEmails();
				var file = _applicationRepository.GetSwiftFile(model.Id);
				_mailSender.Send(new Message(subject, body, model.ClientEmail) { Files = new[] { file } });
				_mailSender.Send(new Message(subject, body, to.Select(x => x.Email).ToArray()) { Files = new[] { file } });
			}

			if (oldData.PackingFileName == null && model.PackingFileName != null)
			{
				var body = _messageBuilder.ApplicationPackingFileAdded(model);
				var file = _applicationRepository.GetPackingFile(model.Id);
				_mailSender.Send(new Message(subject, body, model.ClientEmail) { Files = new[] { file } });

				var to = _messageBuilder.GetSenderEmails().Select(x => x.Email).ToArray();
				_mailSender.Send(new Message(subject, body, to) { Files = new[] { file } });
			}

			if (oldData.DeliveryBillFileName == null && model.DeliveryBillFileName != null)
			{
				var body = _messageBuilder.ApplicationDeliveryBillFileAdded(model);
				var file = _applicationRepository.GetDeliveryBillFile(model.Id);
				_mailSender.Send(new Message(subject, body, model.ClientEmail) { Files = new[] { file } });
			}

			if (oldData.Torg12FileName == null && model.Torg12FileName != null)
			{
				var body = _messageBuilder.ApplicationTorg12FileAdded(model);
				var file = _applicationRepository.GetTorg12File(model.Id);
				_mailSender.Send(new Message(subject, body, model.ClientEmail) { Files = new[] { file } });
			}

			if (oldData.CPFileName == null && model.CPFileName != null)
			{
				var body = _messageBuilder.ApplicationCPFileAdded(model);
				var file = _applicationRepository.GetCPFile(model.Id);
				_mailSender.Send(new Message(subject, body, model.ClientEmail) { Files = new[] { file } });
			}
		}

		public void Add(ApplicationEditModel model, CarrierSelectModel carrierSelectModel)
		{
			_manager.Add(model, carrierSelectModel);

			SendOnAdd(model.Id);
		}

		private void SendOnAdd(long id)
		{
			var model = _applicationPresenter.Get(id);
			var subject = string.Format(_messageBuilder.ApplicationSubject, ApplicationModelHelper.GetDisplayNumber(model.Id, model.Count));
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

		public void Delete(long id)
		{
			_manager.Delete(id);
		}

		public void SetState(long id, long stateId)
		{
			_manager.SetState(id, stateId);

			SendOnSetState(id);
		}

		private void SendOnSetState(long id)
		{
			var model = _applicationPresenter.Get(id);
			var stateId = model.StateId;

			var subject = string.Format(_messageBuilder.ApplicationSubject, ApplicationModelHelper.GetDisplayNumber(model.Id, model.Count));

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
				_mailSender.Send(new Message(subject, body, model.ClientEmail) { Files = files });

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

			if (airWaybillId.HasValue)
			{
				var gtdFile = _airWaybillRepository.GetGTDFile(airWaybillId.Value);
				var gtdAdditionalFile = _airWaybillRepository.GTDAdditionalFile(airWaybillId.Value);

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

		private void SendOnSetDateOfCargoReceipt(long id)
		{
			var model = _applicationPresenter.Get(id);
			var subject = string.Format(_messageBuilder.ApplicationSubject, ApplicationModelHelper.GetDisplayNumber(model.Id, model.Count));
			var clientData = _authenticationRepository.GetById(model.ClientUserId);
			var body = _messageBuilder.ApplicationSetDateOfCargoReceipt(model, clientData.TwoLetterISOLanguageName);
			var message = new Message(subject, body, model.ClientEmail);

			_mailSender.Send(message);
		}
	}
}