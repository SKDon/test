using System.Linq;
using Alicargo.Core.Contract;
using Alicargo.Core.Services;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.Email
{
    internal sealed class AwbManagerWithMailing : IAwbManager
    {
        private readonly IAwbPresenter _awbPresenter;
        private readonly IMailSender _mailSender;
        private readonly IMessageBuilder _messageBuilder;
	    private readonly IRecipients _recipients;
	    private readonly IAwbManager _manager;

        public AwbManagerWithMailing(
			IRecipients recipients,
            IAwbManager manager,
            IAwbPresenter awbPresenter,
            IMailSender mailSender,
            IMessageBuilder messageBuilder)
        {
	        _recipients = recipients;
	        _manager = manager;
            _awbPresenter = awbPresenter;
            _mailSender = mailSender;
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
                    new Recipient
                        {
                            Culture = broker.TwoLetterISOLanguageName,
                            Email = broker.Email
                        }
                }
				.Concat(_recipients.GetForwarderEmails())
                .ToArray();

            var aggregate = _awbPresenter.GetAggregate(awbId);

            foreach (var recipient in to)
            {
                var body = _messageBuilder.AwbCreate(model, recipient.Culture, aggregate.TotalWeight,
                                                     aggregate.TotalCount);
                _mailSender.Send(new Message(_messageBuilder.DefaultSubject, body, recipient.Email));
            }
        }
    }
}