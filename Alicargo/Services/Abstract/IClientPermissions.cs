using Alicargo.Contracts.Contracts;

namespace Alicargo.Services.Abstract
{
    public interface IClientPermissions
    {
        bool HaveAccessToClient(ClientData data);
    }
}