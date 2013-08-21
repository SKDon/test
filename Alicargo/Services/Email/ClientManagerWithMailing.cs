using System.Linq;
using Alicargo.Services.Abstract;
using Alicargo.Services.Contract;
using Alicargo.ViewModels;

namespace Alicargo.Services.Email
{
    internal sealed class ClientManagerWithMailing : IClientManager
	{
		private readonly IMailSender _mailSender;
		private readonly IClientManager _manager;
		private readonly IMessageBuilder _messageBuilder;

		public ClientManagerWithMailing(IClientManager manager, IMailSender mailSender, IMessageBuilder messageBuilder)
		{
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

		public bool HaveAccessToClient(long clientUserId)
		{
			return _manager.HaveAccessToClient(clientUserId);
		}

		private void EmailOnAdd(ClientModel model, AuthenticationModel authenticationModel)
		{
			var body = _messageBuilder.ClientAdd(model, authenticationModel);
			var admins = _messageBuilder.GetAdminEmails().Select(x => x.Email).ToArray();

			_mailSender.Send(new Message(_messageBuilder.DefaultSubject, body, model.Email) {CC = admins});
		}
	}
}