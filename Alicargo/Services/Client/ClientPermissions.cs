using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Services.Abstract;

namespace Alicargo.Services.Client
{
    internal sealed class ClientPermissions : IClientPermissions
    {
        private readonly IIdentityService _identity;

        public ClientPermissions(IIdentityService identity)
        {
            _identity = identity;
        }

        // todo: 1. Test
        public bool HaveAccessToClient(ClientData data)
        {
            if (_identity.IsInRole(RoleType.Admin) || _identity.IsInRole(RoleType.Sender)) return true;

            return data.UserId == _identity.Id;
        }
    }
}