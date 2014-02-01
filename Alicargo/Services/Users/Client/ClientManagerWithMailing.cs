using System.Linq;
using Alicargo.Core.Contracts.Email;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.User;

namespace Alicargo.Services.Users.Client
{
	internal sealed class ClientManagerWithMailing : IClientManager
	{
		private readonly IMailSender _mailSender;
		private readonly IClientManager _manager;
		private readonly IMessageBuilder _messageBuilder;
		private readonly IAdminRepository _admins;

		public ClientManagerWithMailing(
			IAdminRepository admins,
			IClientManager manager,
			IMailSender mailSender,
			IMessageBuilder messageBuilder)
		{
			_admins = admins;
			_manager = manager;
			_mailSender = mailSender;
			_messageBuilder = messageBuilder;
		}

		public void Update(long clientId, ClientModel model, TransitEditModel transit,AuthenticationModel authentication)
		{
			_manager.Update(clientId, model, transit, authentication);
		}

		public long Add(ClientModel client, TransitEditModel transit,AuthenticationModel authentication)
		{
			var id = _manager.Add(client, transit, authentication);

			EmailOnAdd(client, authentication);

			return id;
		}

		// todo: use ClientManagerWithEvent
		private void EmailOnAdd(ClientModel model, AuthenticationModel authenticationModel)
		{
			var body = _messageBuilder.ClientAdd(model, authenticationModel);
			var admins = _admins.GetAll().Select(x => x.Email).ToArray();

			_mailSender.Send(new EmailMessage(_messageBuilder.DefaultSubject, body, EmailsHelper.DefaultFrom, model.Emails) { CopyTo = admins });
		}
	}
}