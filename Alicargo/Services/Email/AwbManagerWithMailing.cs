using System.Linq;
using Alicargo.Core.Contracts;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Services.Abstract;

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

		public long Create(long? applicationId, AirWaybillData data, byte[] gtdFile,
			byte[] gtdAdditionalFile, byte[] packingFile, byte[] invoiceFile, byte[] awbFile)
		{
			var awbId = _manager.Create(applicationId, data,
				gtdFile, gtdAdditionalFile, packingFile, invoiceFile, awbFile);

			SendOnCreate(awbId);

			return awbId;
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

			foreach(var recipient in to)
			{
				var body = _messageBuilder.AwbCreate(model, recipient.Culture, aggregate.TotalWeight,
					aggregate.TotalCount);
				_mailSender.Send(new EmailMessage(_messageBuilder.DefaultSubject, body, EmailsHelper.DefaultFrom, recipient.Email));
			}
		}
	}
}