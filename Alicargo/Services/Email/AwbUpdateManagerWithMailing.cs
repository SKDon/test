using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Core.Services.Abstract;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.Email
{
	internal sealed class AwbUpdateManagerWithMailing : IAwbUpdateManager
	{		
		private readonly IAwbPresenter _awbPresenter;
		private readonly IAwbRepository _awbRepository;
		private readonly IAdminRepository _admins;
		private readonly ISenderRepository _senders;
		private readonly IMailSender _mailSender;
		private readonly IAwbUpdateManager _manager;
		private readonly IMessageBuilder _messageBuilder;

		public AwbUpdateManagerWithMailing(
			IAwbUpdateManager manager,
			IAwbPresenter awbPresenter,
			IAwbRepository awbRepository,
			IAdminRepository admins,
			ISenderRepository senders,
			IMailSender mailSender,
			IMessageBuilder messageBuilder)
		{
			_manager = manager;
			_awbPresenter = awbPresenter;
			_awbRepository = awbRepository;
			_admins = admins;
			_senders = senders;
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
		}

		private void SendOnFileAdd(long id, AirWaybillData oldData)
		{
			var model = _awbPresenter.GetData(id);

			var subject = _messageBuilder.DefaultSubject;
			var broker = _awbPresenter.GetBroker(model.BrokerId);

			if (oldData.InvoiceFileName == null && model.InvoiceFileName != null)
			{
				var body = _messageBuilder.AwbInvoiceFileAdded(model);
				var to = _senders.GetAll()
					.Concat(_admins.GetAll())
					.Select(x => x.Email)
					.ToArray();
				var file = _awbRepository.GetInvoiceFile(model.Id);
				_mailSender.Send(new EmailMessage(subject, body, EmailsHelper.DefaultFrom, to) { Files = new[] { file } });
			}

			if (oldData.AWBFileName == null && model.AWBFileName != null)
			{
				var body = _messageBuilder.AwbAWBFileAdded(model);
				var to = _senders.GetAll()
					.Concat(_admins.GetAll())
					.Select(x => x.Email)
					.Concat(new[] { broker.Email })
					.ToArray();
				var file = _awbRepository.GetAWBFile(model.Id);

				_mailSender.Send(new EmailMessage(subject, body, EmailsHelper.DefaultFrom, to) { Files = new[] { file } });
			}

			if (oldData.PackingFileName == null && model.PackingFileName != null)
			{
				var body = _messageBuilder.AwbPackingFileAdded(model);
				var to = new[] { broker.Email }.Concat(_admins.GetAll().Select(x => x.Email)).ToArray();
				var file = _awbRepository.GetPackingFile(model.Id);

				_mailSender.Send(new EmailMessage(subject, body, EmailsHelper.DefaultFrom, to) { Files = new[] { file } });
			}

			if (oldData.GTDFileName == null && model.GTDFileName != null)
			{
				var body = _messageBuilder.AwbGTDFileAdded(model);
				var file = _awbRepository.GetGTDFile(model.Id);
				foreach (var client in _awbRepository.GetClientEmails(model.Id))
				{
					_mailSender.Send(new EmailMessage(subject, body, EmailsHelper.DefaultFrom, client) { Files = new[] { file } });
				}
				_mailSender.Send(new EmailMessage(subject, body, EmailsHelper.DefaultFrom,
					_admins.GetAll().Select(x => x.Email).ToArray())
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
					_mailSender.Send(new EmailMessage(subject, body, EmailsHelper.DefaultFrom, client) { Files = new[] { file } });
				}
				_mailSender.Send(new EmailMessage(subject, body, EmailsHelper.DefaultFrom,
					_admins.GetAll().Select(x => x.Email).ToArray())
				{
					Files = new[] { file }
				});
			}
		}
	}
}