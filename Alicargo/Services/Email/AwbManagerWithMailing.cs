using System.Linq;
using Alicargo.Core.Contracts;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.Services.Contract;
using Alicargo.ViewModels;

namespace Alicargo.Services.Email
{
	// todo: test
	public sealed class AwbManagerWithMailing : IAwbManager
	{
		private readonly IAwbManager _manager;
		private readonly IAwbPresenter _awbPresenter;
		private readonly IApplicationPresenter _applicationPresenter;
		private readonly IReferenceRepository _referenceRepository;
		private readonly IMailSender _mailSender;
		private readonly IMessageBuilder _messageBuilder;
		private readonly IBrockerRepository _brockerRepository;

		public AwbManagerWithMailing(
			IAwbManager manager,
			IAwbPresenter awbPresenter,
			IApplicationPresenter applicationPresenter,
			IReferenceRepository referenceRepository,
			IMailSender mailSender,
			IMessageBuilder messageBuilder,
			IBrockerRepository brockerRepository)
		{
			_manager = manager;
			_awbPresenter = awbPresenter;
			_applicationPresenter = applicationPresenter;
			_referenceRepository = referenceRepository;
			_mailSender = mailSender;
			_messageBuilder = messageBuilder;
			_brockerRepository = brockerRepository;
		}

		public void Create(long applicationId, ReferenceModel model)
		{
			_manager.Create(applicationId, model);

			SendOnCreate(model.Id);
		}

		private void SendOnCreate(long id)
		{
			var model = _awbPresenter.Get(id);
			var brocker = _brockerRepository.Get(model.BrockerId);

			var to = new[]
			{
				new Recipient
				{
					Culture = brocker.TwoLetterISOLanguageName,
					Email = brocker.Email
				}
			}
				.Concat(_messageBuilder.GetForwarderEmails())
				.ToArray();

			foreach (var recipient in to)
			{
				var body = _messageBuilder.AwbCreate(model, recipient.Culture);
				_mailSender.Send(new Message(_messageBuilder.DefaultSubject, body, recipient.Email));
			}
		}

		public void SetAwb(long applicationId, long? awbId)
		{
			_manager.SetAwb(applicationId, awbId);

			if (awbId.HasValue)
			{
				var model = _awbPresenter.Get(awbId.Value);
				var applicationModel = _applicationPresenter.Get(applicationId);

				var to = _messageBuilder.GetForwarderEmails();
				foreach (var recipient in to)
				{
					var body = _messageBuilder.AwbSet(model, ApplicationListItem.GetDisplayNumber(applicationModel.Id, applicationModel.Count), recipient.Culture);
					_mailSender.Send(new Message(_messageBuilder.DefaultSubject, body, recipient.Email));
				}
			}
		}

		public void Update(ReferenceModel model)
		{
			var old = _awbPresenter.Get(model.Id);

			_manager.Update(model);

			SendOnFileAdd(model.Id, old);
		}

		private void SendOnFileAdd(long id, IReferenceData oldData)
		{
			var model = _awbPresenter.Get(id);

			var subject = _messageBuilder.DefaultSubject;
			var brocker = _brockerRepository.Get(model.BrockerId);

			if (oldData.InvoiceFileName == null && model.InvoiceFileName != null)
			{
				var body = _messageBuilder.AwbInvoiceFileAdded(model);
				var to = _messageBuilder.GetSenderEmails()
					.Concat(_messageBuilder.GetAdminEmails())
					.Select(x => x.Email)
					.ToArray();
				var file = _referenceRepository.GetInvoiceFile(model.Id);
				_mailSender.Send(new Message(subject, body, to) { Files = new[] { file } });
			}

			if (oldData.AWBFileName == null && model.AWBFileName != null)
			{
				var body = _messageBuilder.AwbAWBFileAdded(model);
				var to = _messageBuilder.GetSenderEmails()
					.Concat(_messageBuilder.GetAdminEmails())
					.Select(x => x.Email)
					.Concat(new[] { brocker.Email })
					.ToArray();
				var file = _referenceRepository.GetAWBFile(model.Id);

				_mailSender.Send(new Message(subject, body, to) { Files = new[] { file } });
			}

			if (oldData.PackingFileName == null && model.PackingFileName != null)
			{
				var body = _messageBuilder.AwbPackingFileAdded(model);
				var to = new[] { brocker.Email }.Concat(_messageBuilder.GetAdminEmails().Select(x => x.Email)).ToArray();
				var file = _referenceRepository.GetPackingFile(model.Id);

				_mailSender.Send(new Message(subject, body, to) { Files = new[] { file } });
			}

			if (oldData.GTDFileName == null && model.GTDFileName != null)
			{
				var body = _messageBuilder.AwbGTDFileAdded(model);
				var file = _referenceRepository.GetGTDFile(model.Id);
				foreach (var client in _referenceRepository.GetClientEmails(model.Id))
				{
					_mailSender.Send(new Message(subject, body, client) { Files = new[] { file } });
				}
				_mailSender.Send(new Message(subject, body, _messageBuilder.GetAdminEmails().Select(x => x.Email).ToArray())
				{
					Files = new[] { file }
				});
			}

			if (oldData.GTDAdditionalFileName == null && model.GTDAdditionalFileName != null)
			{
				var body = _messageBuilder.AwbGTDAdditionalFileAdded(model);
				var file = _referenceRepository.GTDAdditionalFile(model.Id);
				foreach (var client in _referenceRepository.GetClientEmails(model.Id))
				{
					_mailSender.Send(new Message(subject, body, client) { Files = new[] { file } });
				}
				_mailSender.Send(new Message(subject, body, _messageBuilder.GetAdminEmails().Select(x => x.Email).ToArray()) { Files = new[] { file } });
			}
		}

		public void Delete(long id)
		{
			_manager.Delete(id);
		}

		public void SetState(long referenceId, long stateId)
		{
			_manager.SetState(referenceId, stateId);
		}
	}
}