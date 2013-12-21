using Alicargo.Contracts.Contracts.User;

namespace Alicargo.Contracts.Repositories.User
{
	public interface IUserRepository
	{
		void SetLanguage(long userId, string language);
		void SetPassword(long userId, string password);
		PasswordData GetPasswordData(string login);
	}
}