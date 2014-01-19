using Alicargo.Core.Contracts;
using Alicargo.Core.Contracts.AirWaybill;
using Alicargo.Core.Contracts.Email;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Services.Abstract;

namespace Alicargo.Services.Email
{
	internal sealed class ApplicationAwbManagerWithMailing : IApplicationAwbManager
	{
		private readonly IApplicationRepository _applications;
		private readonly IAwbPresenter _awbPresenter;
		private readonly IForwarderRepository _forwarders;
		private readonly IMailSender _mailSender;
		private readonly IApplicationAwbManager _manager;
		private readonly IMessageBuilder _messageBuilder;

		public ApplicationAwbManagerWithMailing(
			IApplicationAwbManager manager,
			IApplicationRepository applications,
			IAwbPresenter awbPresenter,
			IMailSender mailSender,
			IMessageBuilder messageBuilder,
			IForwarderRepository forwarders)
		{
			_manager = manager;
			_applications = applications;
			_awbPresenter = awbPresenter;
			_mailSender = mailSender;
			_messageBuilder = messageBuilder;
			_forwarders = forwarders;
		}

		public void SetAwb(long applicationId, long? awbId)
		{
			_manager.SetAwb(applicationId, awbId);

			if (!awbId.HasValue) return;

			var model = _awbPresenter.GetData(awbId.Value);
			var applicationModel = _applications.Get(applicationId);

			var aggregate = _awbPresenter.GetAggregate(awbId.Value);

			var to = _forwarders.GetAll();

			foreach (var recipient in to)
			{
				var body = _messageBuilder.AwbSet(
					model,
					ApplicationHelper.GetDisplayNumber(applicationModel.Id, applicationModel.Count),
					recipient.Language,
					aggregate.TotalWeight,
					aggregate.TotalCount);
				_mailSender.Send(new EmailMessage(_messageBuilder.DefaultSubject, body, EmailsHelper.DefaultFrom, recipient.Email));
			}
		}
	}
}