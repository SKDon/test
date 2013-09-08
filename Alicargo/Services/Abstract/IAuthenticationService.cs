using Alicargo.ViewModels.User;

namespace Alicargo.Services.Abstract
{
    public interface IAuthenticationService
    {
        bool Authenticate(SignIdModel user);
    }
}