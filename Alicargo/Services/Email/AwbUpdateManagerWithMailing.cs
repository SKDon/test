using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Contract;
using Alicargo.Core.Services.Abstract;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.Email
{
	internal sealed class AwbUpdateManagerWithMailing : IAwbUpdateManager
	{
		private readonly IAwbPresenter _awbPresenter;
		private readonly IAwbRepository _awbRepository;
		private readonly IMailSender _mailSender;
		private readonly IAwbUpdateManager _manager;
		private readonly IMessageBuilder _messageBuilder;
		private readonly IRecipients _recipients;

		public AwbUpdateManagerWithMailing(
			IAwbUpdateManager manager,
			IRecipients recipients,
			IAwbPresenter awbPresenter,
			IAwbRepository awbRepository,
			IMailSender mailSender,
			IMessageBuilder messageBuilder)
		{
			_manager = manager;
			_recipients = recipients;
			_awbPresenter = awbPresenter;
			_awbRepository = awbRepository;
			_mailSender = mailSender;
			_messageBuilder = messageBuilder;
		}

		public void Update(long id, AwbAdminModel model)
		{
			var old = _awbPresenter.GetData(id);

			_manager.Update(id, model);

			SendOnFileAdd(id, old);
		}

		public void Update(long id, AwbBrokerModel model)
		{
			var old = _awbPresenter.GetData(id);

			_manager.Update(id, model);

			SendOnFileAdd(id, old);
		}

		public void Update(long id, AwbSenderModel model)
		{
			var old = _awbPresenter.GetData(id);

			_manager.Update(id, model);

			SendOnFileAdd(id, old);
		}

		public void SetAdditionalCost(long awbId, decimal? additionalCost)
		{
			_manager.SetAdditionalCost(awbId, additionalCost);

			// todo: 2. email
		}

		private void SendOnFileAdd(long id, AirWaybillData oldData)
		{
			var model = _awbPresenter.GetData(id);

			var subject = _messageBuilder.DefaultSubject;
			var broker = _awbPresenter.GetBroker(model.BrokerId);

			if (oldData.InvoiceFileName == null && model.InvoiceFileName != null)
			{
				var body = _messageBuilder.AwbInvoiceFileAdded(model);
				var to = _recipients.GetSenderEmails()
									.Concat(_recipients.GetAdminEmails())
									.Select(x => x.Email)
									.ToArray();
				var file = _awbRepository.GetInvoiceFile(model.Id);
				_mailSender.Send(new Message(subject, body, to) { Files = new[] { file } });
			}

			if (oldData.AWBFileName == null && model.AWBFileName != null)
			{
				var body = _messageBuilder.AwbAWBFileAdded(model);
				var to = _recipients.GetSenderEmails()
									.Concat(_recipients.GetAdminEmails())
									.Select(x => x.Email)
									.Concat(new[] { broker.Email })
									.ToArray();
				var file = _awbRepository.GetAWBFile(model.Id);

				_mailSender.Send(new Message(subject, body, to) { Files = new[] { file } });
			}

			if (oldData.PackingFileName == null && model.PackingFileName != null)
			{
				var body = _messageBuilder.AwbPackingFileAdded(model);
				var to = new[] { broker.Email }.Concat(_recipients.GetAdminEmails().Select(x => x.Email)).ToArray();
				var file = _awbRepository.GetPackingFile(model.Id);

				_mailSender.Send(new Message(subject, body, to) { Files = new[] { file } });
			}

			if (oldData.GTDFileName == null && model.GTDFileName != null)
			{
				var body = _messageBuilder.AwbGTDFileAdded(model);
				var file = _awbRepository.GetGTDFile(model.Id);
				foreach (var client in _awbRepository.GetClientEmails(model.Id))
				{
					_mailSender.Send(new Message(subject, body, client) { Files = new[] { file } });
				}
				_mailSender.Send(new Message(subject, body,
											 _recipients.GetAdminEmails().Select(x => x.Email).ToArray())
				{
					Files = new[] { file }
				});
			}

			if (oldData.GTDAdditionalFileName == null && model.GTDAdditionalFileName != null)
			{
				var body = _messageBuilder.AwbGTDAdditionalFileAdded(model);
				var file = _awbRepository.GTDAdditionalFile(model.Id);
				foreach (var client in _awbRepository.GetClientEmails(model.Id))
				{
					_mailSender.Send(new Message(subject, body, client) { Files = new[] { file } });
				}
				_mailSender.Send(new Message(subject, body,
											 _recipients.GetAdminEmails().Select(x => x.Email).ToArray())
				{
					Files = new[] { file }
				});
			}
		}
	}
}