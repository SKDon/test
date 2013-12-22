using System.Configuration;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Core.Services.Abstract;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.User;

namespace Alicargo.Services.Email
{
	internal sealed class ClientManagerWithMailing : IClientManager
	{
		private static readonly string DefaultFrom = ConfigurationManager.AppSettings["DefaultFrom"];
		private readonly IMailSender _mailSender;
		private readonly IClientManager _manager;
		private readonly IMessageBuilder _messageBuilder;
		private readonly IAdminRepository _admins;

		public ClientManagerWithMailing(
			IAdminRepository admins,
			IClientManager manager, IMailSender mailSender, IMessageBuilder messageBuilder)
		{
			_admins = admins;
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
			var admins = _admins.GetAll().Select(x => x.Email).ToArray();

			_mailSender.Send(new EmailMessage(_messageBuilder.DefaultSubject, body, DefaultFrom, model.Emails) { CopyTo = admins });
		}
	}
}