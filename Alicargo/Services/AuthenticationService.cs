using System.Globalization;
using System.Linq;
using System.Web.Security;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Services
{
	public sealed class AuthenticationService : IAuthenticationService
	{
		private readonly IAuthenticationRepository _authentications;
		private readonly IPasswordConverter _passwordConverter;
		private readonly IIdentityService _identity;

		public AuthenticationService(IAuthenticationRepository authentications, IPasswordConverter passwordConverter, IIdentityService identity)
		{
			_authentications = authentications;
			_passwordConverter = passwordConverter;
			_identity = identity;
		}

		public bool Authenticate(SignIdModel user)
		{
			var data = _authentications.GetByLogin(user.Login);
			if (data == null) return false;

			var hash = _passwordConverter.GetPasswordHash(user.Password, data.PasswordSalt.ToArray());
			if (!hash.SequenceEqual(data.PasswordHash.ToArray())) return false;

			FormsAuthentication.SetAuthCookie(data.Id.ToString(CultureInfo.InvariantCulture), user.RememberMe);
			_identity.Id = data.Id;

			return true;
		}
	}
}