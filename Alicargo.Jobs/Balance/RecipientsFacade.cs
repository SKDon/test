using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.User;

namespace Alicargo.Jobs.Balance
{
	public sealed class RecipientsFacade : IRecipientsFacade
	{
		private readonly IAdminRepository _admins;
		private readonly IClientRepository _clients;
		private readonly IEventEmailRecipient _recipients;

		public RecipientsFacade(
			IAdminRepository admins,
			IClientRepository clients,
			IEventEmailRecipient recipients)
		{
			_admins = admins;
			_clients = clients;
			_recipients = recipients;
		}

		private IEnumerable<RecipientData> GetRecipients(IEnumerable<RoleType> roles, long clientId)
		{
			foreach (var role in roles)
			{
				switch (role)
				{
					case RoleType.Admin:
						var recipients =
							_admins.GetAll().Select(user => new RecipientData(user.Email, user.Language, RoleType.Admin));
						foreach (var recipient in recipients)
						{
							yield return recipient;
						}
						break;

					case RoleType.Sender:
						yield return null;
						break;

					case RoleType.Broker:
						yield return null;
						break;

					case RoleType.Forwarder:
						yield return null;
						break;

					case RoleType.Client:
						var client = _clients.Get(clientId);
						foreach (var email in client.Emails)
						{
							yield return new RecipientData(email, client.Language, role);
						}
						break;

					default:
						throw new InvalidOperationException("Unknown role " + role);
				}
			}
		}

		public RecipientData[] GetRecipients(EventType type, long clientId)
		{
			var roles = _recipients.GetRecipientRoles(type);

			return roles.Length == 0
				? null
				: GetRecipients(roles, clientId).ToArray();
		}
	}
}