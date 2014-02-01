using Alicargo.Core.Contracts.Client;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.User;

namespace Alicargo.Services.Users.Client
{
	internal sealed class ClientManager : IClientManager
	{
		private readonly IClientPermissions _permissions;
		private readonly IClientRepository _clients;
		private readonly ITransitService _transits;
		private readonly IUserRepository _users;
		private readonly IUnitOfWork _unitOfWork;

		public ClientManager(
			IClientRepository clients,
			IClientPermissions permissions,
			ITransitService transits,
			IUserRepository users,
			IUnitOfWork unitOfWork)
		{
			_clients = clients;
			_permissions = permissions;
			_transits = transits;
			_users = users;
			_unitOfWork = unitOfWork;
		}

		public void Update(long clientId, ClientModel model, TransitEditModel transit, AuthenticationModel authentication)
		{
			var data = _clients.Get(clientId);

			if(!_permissions.HaveAccessToClient(data))
				throw new AccessForbiddenException();

			_transits.Update(data.TransitId, transit, null);

			data.BIC = model.BIC;
			data.Phone = model.Phone;
			data.Emails = EmailsHelper.SplitAndTrimEmails(model.Emails);
			data.LegalEntity = model.LegalEntity;
			data.Bank = model.Bank;
			data.Contacts = model.Contacts;
			data.INN = model.INN;
			data.KPP = model.KPP;
			data.KS = model.KS;
			data.LegalAddress = model.LegalAddress;
			data.MailingAddress = model.MailingAddress;
			data.Nic = model.Nic;
			data.OGRN = model.OGRN;
			data.RS = model.RS;
			data.Login = authentication.Login;

			_clients.Update(data);

			_unitOfWork.SaveChanges();

			if(!string.IsNullOrWhiteSpace(authentication.NewPassword))
			{
				var userId = _clients.GetUserId(clientId);
				_users.SetPassword(userId, authentication.NewPassword);
			}
		}

		public long Add(ClientModel client, TransitEditModel transit, AuthenticationModel authentication)
		{
			var transitId = _transits.Add(transit, null);

			_unitOfWork.SaveChanges();

			var data = new ClientData
			{
				ClientId = 0,
				BIC = client.BIC,
				Phone = client.Phone,
				Emails = EmailsHelper.SplitAndTrimEmails(client.Emails),
				LegalEntity = client.LegalEntity,
				Bank = client.Bank,
				Contacts = client.Contacts,
				INN = client.INN,
				KPP = client.KPP,
				KS = client.KS,
				LegalAddress = client.LegalAddress,
				MailingAddress = client.MailingAddress,
				Nic = client.Nic,
				OGRN = client.OGRN,
				RS = client.RS,
				TransitId = transitId,
				Language = TwoLetterISOLanguageName.English,
				Login = authentication.Login
			};

			var id = _clients.Add(data);

			_unitOfWork.SaveChanges();

			var clientId = id();

			var userId = _clients.GetUserId(clientId);
			_users.SetPassword(userId, authentication.NewPassword);

			return clientId;
		}
	}
}