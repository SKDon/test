using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.User;

namespace Alicargo.Jobs.Client
{
	public sealed class ClientEventClientEventRecipientsFacade : IClientEventRecipientsFacade
	{
		private readonly IAdminRepository _admins;
		private readonly IClientRepository _clients;
		private readonly IEventEmailRecipient _recipients;

		public ClientEventClientEventRecipientsFacade(
			IAdminRepository admins,
			IClientRepository clients,
			IEventEmailRecipient recipients)
		{
			_admins = admins;
			_clients = clients;
			_recipients = recipients;
		}

		public RecipientData[] GetRecipients(EventType type, long clientId)
		{
			var roles = _recipients.GetRecipientRoles(type);

			return roles.Length == 0
				? null
				: GetRecipients(roles, clientId).ToArray();
		}

		private IEnumerable<RecipientData> GetRecipients(IEnumerable<RoleType> roles, long clientId)
		{
			foreach (var role in roles)
			{
				switch (role)
				{
					case RoleType.Admin:
						var recipients = _admins.GetAll()
							.Select(user => new RecipientData(user.Email, user.Language, RoleType.Admin));
						foreach (var recipient in recipients)
						{
							yield return recipient;
						}
						break;

					case RoleType.Client:
						var client = _clients.Get(clientId);
						foreach (var email in client.Emails)
						{
							yield return new RecipientData(email, client.Language, role);
						}
						break;

					case RoleType.Sender:
					case RoleType.Broker:
					case RoleType.Forwarder:
					case RoleType.Carrier:
						throw new NotSupportedException("Only client and admin can be recipient of client event");

					default:
						throw new InvalidOperationException("Unknown role " + role);
				}
			}
		}
	}
}