using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Core.Services.Abstract;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.User;

namespace Alicargo.Services.Email
{
	internal sealed class ClientManagerWithMailing : IClientManager
	{
		private readonly IMailSender _mailSender;
		private readonly IRecipients _recipients;
		private readonly IClientManager _manager;
		private readonly IMessageBuilder _messageBuilder;

		public ClientManagerWithMailing(
			IRecipients recipients,
			IClientManager manager, IMailSender mailSender, IMessageBuilder messageBuilder)
		{
			_recipients = recipients;
			_manager = manager;
			_mailSender = mailSender;
			_messageBuilder = messageBuilder;
		}

		public void Update(long clientId, ClientModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel,
						   AuthenticationModel authenticationModel)
		{
			_manager.Update(clientId, model, carrierModel, transitModel, authenticationModel);
		}

		public long Add(ClientModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel,
						AuthenticationModel authenticationModel)
		{
			var id = _manager.Add(model, carrierModel, transitModel, authenticationModel);

			EmailOnAdd(model, authenticationModel);

			return id;
		}

		private void EmailOnAdd(ClientModel model, AuthenticationModel authenticationModel)
		{
			var body = _messageBuilder.ClientAdd(model, authenticationModel);
			var admins = _recipients.GetAdminEmails().Select(x => x.Email).ToArray();

			_mailSender.Send(new EmailMessage(_messageBuilder.DefaultSubject, body, model.Email) { CopyTo = admins });
		}
	}
}