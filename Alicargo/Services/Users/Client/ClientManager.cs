using System;
using System.Transactions;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Calculation.Admin;
using Alicargo.ViewModels.User;

namespace Alicargo.Services.Users.Client
{
	internal sealed class ClientManager : IClientManager
	{
		private readonly IClientPermissions _permissions;
		private readonly IClientRepository _clients;
		private readonly IClientBalanceRepository _balance;
		private readonly ITransitService _transits;
		private readonly IUserRepository _users;
		private readonly IUnitOfWork _unitOfWork;

		public ClientManager(
			IClientRepository clients,
			IClientBalanceRepository balance,
			IClientPermissions permissions,
			ITransitService transits,
			IUserRepository users,
			IUnitOfWork unitOfWork)
		{
			_clients = clients;
			_balance = balance;
			_permissions = permissions;
			_transits = transits;
			_users = users;
			_unitOfWork = unitOfWork;
		}

		public void Update(long clientId, ClientModel model, CarrierSelectModel carrier,
			TransitEditModel transit,
			AuthenticationModel authentication)
		{
			var data = _clients.Get(clientId);

			if (!_permissions.HaveAccessToClient(data))
				throw new AccessForbiddenException();

			_transits.Update(data.TransitId, transit, carrier);

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

			if (!string.IsNullOrWhiteSpace(authentication.NewPassword))
			{
				var userId = _clients.GetUserId(clientId);
				_users.SetPassword(userId, authentication.NewPassword);
			}
		}

		public long Add(ClientModel model, CarrierSelectModel carrier, TransitEditModel transit,
			AuthenticationModel authentication)
		{
			var transitId = _transits.AddTransit(transit, carrier);

			_unitOfWork.SaveChanges();

			var data = new ClientData
			{
				ClientId = 0,
				BIC = model.BIC,
				Phone = model.Phone,
				Emails = EmailsHelper.SplitAndTrimEmails(model.Emails),
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
				RS = model.RS,
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

		public void AddToBalance(long clientId, PaymentModel model, DateTimeOffset timestamp)
		{
			var balance = _balance.GetBalance(clientId);

			balance += model.Money;

			using (var scope = new TransactionScope())
			{
				_balance.SetBalance(clientId, balance);

				_balance.AddToHistory(clientId, balance, model.Money, model.Comment);

				scope.Complete();
			}
		}
	}
}