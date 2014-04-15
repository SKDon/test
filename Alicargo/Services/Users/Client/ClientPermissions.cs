using System;
using System.Diagnostics;
using Alicargo.Core.Contracts.Client;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.User;

namespace Alicargo.Services.Users.Client
{
	internal sealed class ClientPermissions : IClientPermissions
	{
		private readonly IClientRepository _clients;
		private readonly IIdentityService _identity;

		public ClientPermissions(
			IIdentityService identity,
			IClientRepository clients)
		{
			_identity = identity;
			_clients = clients;
		}

		public bool HaveAccessToClient(ClientData data)
		{
			if(_identity.IsInRole(RoleType.Admin) || _identity.IsInRole(RoleType.Sender) || _identity.IsInRole(RoleType.Manager))
			{
				return true;
			}

			if(data == null)
			{
				throw new ArgumentNullException("data");
			}

			Debug.Assert(_identity.Id != null);
			var client = _clients.GetByUserId(_identity.Id.Value);

			return client != null && client.ClientId == data.ClientId;
		}
	}
}