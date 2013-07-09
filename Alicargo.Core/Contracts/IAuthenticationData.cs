using System.Data.Linq;

namespace Alicargo.Core.Contracts
{
	public interface IAuthenticationData
	{
		long Id { get; set; }
		string Login { get; set; }
		Binary PasswordHash { get; set; }
		Binary PasswordSalt { get; set; }
		string TwoLetterISOLanguageName { get; set; }
	}
}