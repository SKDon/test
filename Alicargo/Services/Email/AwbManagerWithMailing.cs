using System.Configuration;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Core.Services.Abstract;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.Email
{
	internal sealed class AwbManagerWithMailing : IAwbManager
	{
		private readonly IAwbPresenter _awbPresenter;
		private readonly IForwarderRepository _forwarders;
		private readonly IMailSender _mailSender;
		private readonly IAwbManager _manager;
		private readonly IMessageBuilder _messageBuilder;

		public AwbManagerWithMailing(
			IAwbManager manager,
			IAwbPresenter awbPresenter,
			IMailSender mailSender,
			IForwarderRepository forwarders,
			IMessageBuilder messageBuilder)
		{
			_manager = manager;
			_awbPresenter = awbPresenter;
			_mailSender = mailSender;
			_forwarders = forwarders;
			_messageBuilder = messageBuilder;
		}

		public long Create(long? applicationId, AwbAdminModel model)
		{
			var awbId = _manager.Create(applicationId, model);

			SendOnCreate(awbId);

			return awbId;
		}

		public long Create(long? applicationId, AwbSenderModel model)
		{
			var id = _manager.Create(applicationId, model);

			SendOnCreate(id);

			return id;
		}

		public void Delete(long awbId)
		{
			_manager.Delete(awbId);
		}

		private void SendOnCreate(long awbId)
		{
			var model = _awbPresenter.GetData(awbId);
			var broker = _awbPresenter.GetBroker(model.BrokerId);

			var to = new[]
			{
				new RecipientData(broker.Email, broker.Language, RoleType.Broker)
			}
				.Concat(_forwarders.GetAll().Select(x => new RecipientData(x.Email, x.Language, RoleType.Forwarder)))
				.ToArray();

			var aggregate = _awbPresenter.GetAggregate(awbId);

			var from = ConfigurationManager.AppSettings["DefaultFrom"];

			foreach (var recipient in to)
			{
				var body = _messageBuilder.AwbCreate(model, recipient.Culture, aggregate.TotalWeight,
					aggregate.TotalCount);
				_mailSender.Send(new EmailMessage(_messageBuilder.DefaultSubject, body, from, recipient.Email));
			}
		}
	}
}