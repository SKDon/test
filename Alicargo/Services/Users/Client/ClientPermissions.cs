using System;
using System.Diagnostics;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Services.Abstract;

namespace Alicargo.Services.Users.Client
{
	internal sealed class ClientPermissions : IClientPermissions
	{
		private readonly IIdentityService _identity;
		private readonly IClientRepository _clients;

		public ClientPermissions(
			IIdentityService identity,
			IClientRepository clients)
		{
			_identity = identity;
			_clients = clients;
		}

		public bool HaveAccessToClient(ClientData data)
		{
			if (_identity.IsInRole(RoleType.Admin) || _identity.IsInRole(RoleType.Sender)) return true;

			if (data == null) throw new ArgumentNullException("data");

			Debug.Assert(_identity.Id != null);
			var client = _clients.GetByUserId(_identity.Id.Value);

			return client != null && client.ClientId == data.ClientId;
		}
	}
}