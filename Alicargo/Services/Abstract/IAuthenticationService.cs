namespace Alicargo.Services.Abstract
{
    public interface IAuthenticationService
    {
        bool Authenticate(ViewModels.SignIdModel user);
    }
}