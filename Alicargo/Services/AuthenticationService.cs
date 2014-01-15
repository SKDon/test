using System.Globalization;
using System.Linq;
using System.Web.Security;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Services.Abstract;
using Alicargo.Utilities;
using Alicargo.ViewModels.User;

namespace Alicargo.Services
{
	internal sealed class AuthenticationService : IAuthenticationService
	{
		private readonly IUserRepository _users;
		private readonly IPasswordConverter _passwordConverter;
		private readonly IIdentityService _identity;

		public AuthenticationService(
			IUserRepository users,
			IPasswordConverter passwordConverter,
			IIdentityService identity)
		{
			_users = users;
			_passwordConverter = passwordConverter;
			_identity = identity;
		}

		public bool Authenticate(SignIdModel user)
		{
			var data = _users.GetPasswordData(user.Login);
			if (data == null) return false;

			var hash = _passwordConverter.GetPasswordHash(user.Password, data.PasswordSalt.ToArray());
			if (!hash.SequenceEqual(data.PasswordHash.ToArray())) return false;

			AuthenticateForce(data.UserId, user.RememberMe);

			return true;
		}

		public void AuthenticateForce(long usreId, bool createPersistentCookie)
		{
			FormsAuthentication.SetAuthCookie(usreId.ToString(CultureInfo.InvariantCulture), createPersistentCookie);
			_identity.Id = usreId;
		}

		public void SignOut()
		{
			_identity.Id = null;
			FormsAuthentication.SignOut();
		}
	}
}