using System.Transactions;
using Alicargo.Core.Contracts.Client;
using Alicargo.Core.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.User;

namespace Alicargo.Services.Users.Client
{
	internal sealed class ClientManager : IClientManager
	{
		private readonly IClientRepository _clients;
		private readonly IClientPermissions _permissions;
		private readonly ITransitService _transits;
		private readonly IUserRepository _users;

		public ClientManager(
			IClientRepository clients,
			IClientPermissions permissions,
			ITransitService transits,
			IUserRepository users)
		{
			_clients = clients;
			_permissions = permissions;
			_transits = transits;
			_users = users;
		}

		public void Update(long clientId, ClientModel model, TransitEditModel transit)
		{
			var data = _clients.Get(clientId);
			var userId = data.UserId;
			var transitId = data.TransitId;

			if(!_permissions.HaveAccessToClient(data))
			{
				throw new AccessForbiddenException();
			}

			var clientEditData = GetData(model);

			Update(clientId, clientEditData, transitId, transit, userId, model.Authentication);
		}

		public long Add(ClientModel client, TransitEditModel transit)
		{
			var data = GetData(client);

			using(var ts = new TransactionScope())
			{
				var transitId = _transits.Add(transit, null);

				var userId = _users.Add(client.Authentication.Login,
					client.Authentication.NewPassword,
					TwoLetterISOLanguageName.English);

				var clientId = _clients.Add(data, userId, transitId);

				ts.Complete();

				return clientId;
			}
		}

		private void Update(
			long clientId, ClientEditData clientEditData,
			long transitId, TransitEditModel transit,
			long userId, AuthenticationModel authentication)
		{
			using(var ts = new TransactionScope())
			{
				_transits.Update(transitId, transit, null, null);

				_clients.Update(clientId, clientEditData);

				_users.SetLogin(userId, authentication.Login);

				if(!string.IsNullOrWhiteSpace(authentication.NewPassword))
				{
					_users.SetPassword(userId, authentication.NewPassword);
				}

				ts.Complete();
			}
		}

		private static ClientEditData GetData(ClientModel model)
		{
			return new ClientEditData
			{
				BIC = model.BIC,
				Phone = model.Phone,
				Emails = model.Emails,
				LegalEntity = model.LegalEntity,
				Bank = model.Bank,
				Contacts = model.Contacts,
				INN = model.INN,
				KPP = model.KPP,
				KS = model.KS,
				LegalAddress = model.LegalAddress,
				MailingAddress = model.MailingAddress,
				Nic = model.Nic,
				OGRN = model.OGRN,
				RS = model.RS
			};
		}
	}
}