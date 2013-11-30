using System.Configuration;
using Alicargo.Contracts.Contracts;
using Alicargo.Core.Helpers;
using Alicargo.Core.Services.Abstract;
using Alicargo.Services.Abstract;

namespace Alicargo.Services.Email
{
	internal sealed class ApplicationAwbManagerWithMailing : IApplicationAwbManager
	{
		private readonly IApplicationPresenter _applicationPresenter;
		private readonly IAwbPresenter _awbPresenter;
		private readonly IMailSender _mailSender;
		private readonly IApplicationAwbManager _manager;
		private readonly IMessageBuilder _messageBuilder;
		private readonly IRecipients _recipients;

		public ApplicationAwbManagerWithMailing(
			IApplicationAwbManager manager,
			IAwbPresenter awbPresenter,
			IRecipients recipients,
			IApplicationPresenter applicationPresenter,
			IMailSender mailSender,
			IMessageBuilder messageBuilder)
		{
			_manager = manager;
			_awbPresenter = awbPresenter;
			_recipients = recipients;
			_applicationPresenter = applicationPresenter;
			_mailSender = mailSender;
			_messageBuilder = messageBuilder;
		}

		public void SetAwb(long applicationId, long? awbId)
		{
			_manager.SetAwb(applicationId, awbId);

			if (!awbId.HasValue) return;

			var model = _awbPresenter.GetData(awbId.Value);
			var applicationModel = _applicationPresenter.GetDetails(applicationId);

			var aggregate = _awbPresenter.GetAggregate(awbId.Value);

			var from = ConfigurationManager.AppSettings["DefaultFrom"]; // todo: 2. hack

			var to = _recipients.GetForwarderEmails();
			foreach (var recipient in to)
			{
				var body = _messageBuilder.AwbSet(model,
					ApplicationHelper.GetDisplayNumber(applicationModel.Id,
						applicationModel.Count),
					recipient.Culture, aggregate.TotalWeight, aggregate.TotalCount);
				_mailSender.Send(new EmailMessage(_messageBuilder.DefaultSubject, body, from, recipient.Email));
			}
		}
	}
}