using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Jobs.Helpers.Abstract;

namespace Alicargo.Jobs.Client
{
	public sealed class ClientEventRecipientsFacade : IRecipientsFacade
	{
		private readonly IAdminRepository _admins;
		private readonly IManagerRepository _managers;
		private readonly IClientRepository _clients;
		private readonly IEventEmailRecipient _recipients;

		public ClientEventRecipientsFacade(
			IAdminRepository admins,
			IManagerRepository managers,
			IClientRepository clients,
			IEventEmailRecipient recipients)
		{
			_admins = admins;
			_managers = managers;
			_clients = clients;
			_recipients = recipients;
		}

		public RecipientData[] GetRecipients(EventType type, EventDataForEntity data)
		{
			var roles = _recipients.GetRecipientRoles(type);

			return roles.Length == 0
				? null
				: GetRecipients(roles, data.EntityId).ToArray();
		}

		private IEnumerable<RecipientData> GetRecipients(IEnumerable<RoleType> roles, long clientId)
		{
			foreach(var role in roles)
			{
				switch(role)
				{
					case RoleType.Admin:
						{
							var recipients = _admins.GetAll()
								.Select(user => new RecipientData(user.Email, user.Language, RoleType.Admin));
							foreach(var recipient in recipients)
							{
								yield return recipient;
							}
							break;
						}

					case RoleType.Manager:
						{
							var recipients = _managers.GetAll()
								.Select(user => new RecipientData(user.Email, user.Language, RoleType.Manager));
							foreach(var recipient in recipients)
							{
								yield return recipient;
							}
							break;
						}

					case RoleType.Client:
						var client = _clients.Get(clientId);
						foreach(var email in client.Emails)
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