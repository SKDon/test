using System.Globalization;
using System.Linq;
using System.Web.Security;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.User;

namespace Alicargo.Services
{
	internal sealed class AuthenticationService : IAuthenticationService
	{
		private readonly IAuthenticationRepository _authentications;
		private readonly IPasswordConverter _passwordConverter;
		private readonly IIdentityService _identity;

		public AuthenticationService(
			IAuthenticationRepository authentications,
			IPasswordConverter passwordConverter,
			IIdentityService identity)
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

			AuthenticateForce(data.Id, user.RememberMe);

			return true;
		}

		public void AuthenticateForce(long id, bool createPersistentCookie)
		{
			FormsAuthentication.SetAuthCookie(id.ToString(CultureInfo.InvariantCulture), createPersistentCookie);
			_identity.Id = id;
		}

		public void SignOut()
		{
			_identity.Id = null;
			FormsAuthentication.SignOut();
		}
	}
}