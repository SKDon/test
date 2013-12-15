using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Services.Abstract;

namespace Alicargo.Services.Users.Client
{
	internal sealed class ClientPermissions : IClientPermissions
	{
		private readonly IIdentityService _identity;

		public ClientPermissions(IIdentityService identity)
		{
			_identity = identity;
		}

		public bool HaveAccessToClient(ClientData data)
		{
			if (_identity.IsInRole(RoleType.Admin) || _identity.IsInRole(RoleType.Sender)) return true;

			if (data == null) throw new ArgumentNullException("data");

			return data.UserId == _identity.Id;
		}
	}
}