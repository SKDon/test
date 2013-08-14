using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.Services.Contract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.AirWaybill;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Email
{
	// todo: test
	public sealed class AwbManagerWithMailing : IAwbManager
	{
		private readonly IAirWaybillRepository _awbRepository;
		private readonly IApplicationPresenter _applicationPresenter;
		private readonly IBrockerRepository _brockerRepository;
		private readonly IMailSender _mailSender;
		private readonly IAwbManager _manager;
		private readonly IMessageBuilder _messageBuilder;

		public AwbManagerWithMailing(
			IAwbManager manager,
			IApplicationPresenter applicationPresenter,
			IAirWaybillRepository awbRepository,
			IMailSender mailSender,
			IMessageBuilder messageBuilder,
			IBrockerRepository brockerRepository)
		{
			_manager = manager;
			_applicationPresenter = applicationPresenter;
			_awbRepository = awbRepository;
			_mailSender = mailSender;
			_messageBuilder = messageBuilder;
			_brockerRepository = brockerRepository;
		}

		public long Create(long applicationId, AirWaybillEditModel model)
		{
			var id = _manager.Create(applicationId, model);

			SendOnCreate(id);

			return id;
		}

		public void SetAwb(long applicationId, long? awbId)
		{
			_manager.SetAwb(applicationId, awbId);

			if (awbId.HasValue)
			{
				var model = _awbRepository.Get(awbId.Value).First();
				var applicationModel = _applicationPresenter.Get(applicationId);

				var aggregate = _awbRepository.GetAggregate(awbId.Value).First();

				var to = _messageBuilder.GetForwarderEmails();
				foreach (var recipient in to)
				{
					var body = _messageBuilder.AwbSet(model, ApplicationModelHelper.GetDisplayNumber(applicationModel.Id, applicationModel.Count),
													  recipient.Culture, aggregate.TotalWeight, aggregate.TotalCount);
					_mailSender.Send(new Message(_messageBuilder.DefaultSubject, body, recipient.Email));
				}
			}
		}

		public void Update(long id, AirWaybillEditModel model)
		{
			var old = _awbRepository.Get(id).First();

			_manager.Update(id, model);

			SendOnFileAdd(id, old);
		}

		public void Update(long id, BrockerAWBModel model)
		{
			var old = _awbRepository.Get(id).First();

			_manager.Update(id, model);

			SendOnFileAdd(id, old);
		}

		public void Delete(long id)
		{
			_manager.Delete(id);
		}

		public void SetState(long airWaybillId, long stateId)
		{
			_manager.SetState(airWaybillId, stateId);
		}

		private void SendOnCreate(long id)
		{
			var model = _awbRepository.Get(id).First();
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

			var aggregate = _awbRepository.GetAggregate(id).First();

			foreach (var recipient in to)
			{
				var body = _messageBuilder.AwbCreate(model, recipient.Culture, aggregate.TotalWeight, aggregate.TotalCount);
				_mailSender.Send(new Message(_messageBuilder.DefaultSubject, body, recipient.Email));
			}
		}

		private void SendOnFileAdd(long id, AirWaybillData oldData)
		{
			var model = _awbRepository.Get(id).First();

			var subject = _messageBuilder.DefaultSubject;
			var brocker = _brockerRepository.Get(model.BrockerId);

			if (oldData.InvoiceFileName == null && model.InvoiceFileName != null)
			{
				var body = _messageBuilder.AwbInvoiceFileAdded(model);
				var to = _messageBuilder.GetSenderEmails()
										.Concat(_messageBuilder.GetAdminEmails())
										.Select(x => x.Email)
										.ToArray();
				var file = _awbRepository.GetInvoiceFile(model.Id);
				_mailSender.Send(new Message(subject, body, to) {Files = new[] {file}});
			}

			if (oldData.AWBFileName == null && model.AWBFileName != null)
			{
				var body = _messageBuilder.AwbAWBFileAdded(model);
				var to = _messageBuilder.GetSenderEmails()
										.Concat(_messageBuilder.GetAdminEmails())
										.Select(x => x.Email)
										.Concat(new[] {brocker.Email})
										.ToArray();
				var file = _awbRepository.GetAWBFile(model.Id);

				_mailSender.Send(new Message(subject, body, to) {Files = new[] {file}});
			}

			if (oldData.PackingFileName == null && model.PackingFileName != null)
			{
				var body = _messageBuilder.AwbPackingFileAdded(model);
				var to = new[] {brocker.Email}.Concat(_messageBuilder.GetAdminEmails().Select(x => x.Email)).ToArray();
				var file = _awbRepository.GetPackingFile(model.Id);

				_mailSender.Send(new Message(subject, body, to) {Files = new[] {file}});
			}

			if (oldData.GTDFileName == null && model.GTDFileName != null)
			{
				var body = _messageBuilder.AwbGTDFileAdded(model);
				var file = _awbRepository.GetGTDFile(model.Id);
				foreach (var client in _awbRepository.GetClientEmails(model.Id))
				{
					_mailSender.Send(new Message(subject, body, client) {Files = new[] {file}});
				}
				_mailSender.Send(new Message(subject, body, _messageBuilder.GetAdminEmails().Select(x => x.Email).ToArray())
				{
					Files = new[] {file}
				});
			}

			if (oldData.GTDAdditionalFileName == null && model.GTDAdditionalFileName != null)
			{
				var body = _messageBuilder.AwbGTDAdditionalFileAdded(model);
				var file = _awbRepository.GTDAdditionalFile(model.Id);
				foreach (var client in _awbRepository.GetClientEmails(model.Id))
				{
					_mailSender.Send(new Message(subject, body, client) {Files = new[] {file}});
				}
				_mailSender.Send(new Message(subject, body, _messageBuilder.GetAdminEmails().Select(x => x.Email).ToArray())
				{
					Files = new[] {file}
				});
			}
		}
	}
}