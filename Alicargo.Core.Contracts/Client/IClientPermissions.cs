using Alicargo.DataAccess.Contracts.Contracts.User;

namespace Alicargo.Core.Contracts.Client
{
    public interface IClientPermissions
    {
        bool HaveAccessToClient(ClientData data);
    }
}