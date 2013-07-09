using System.Data.Linq;

namespace Alicargo.Core.Contracts
{
	public sealed class AuthenticationData : IAuthenticationData
	{
		public long Id { get; set; }
		public string Login { get; set; }
		public Binary PasswordSalt { get; set; }
		public Binary PasswordHash { get; set; }
		public string TwoLetterISOLanguageName { get; set; }
	}
}