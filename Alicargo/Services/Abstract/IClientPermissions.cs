using Alicargo.DataAccess.Contracts.Contracts.User;

namespace Alicargo.Services.Abstract
{
    public interface IClientPermissions
    {
        bool HaveAccessToClient(ClientData data);
    }
}