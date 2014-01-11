using Alicargo.Contracts.Contracts.User;

namespace Alicargo.Contracts.Repositories.User
{
	public interface IUserRepository
	{
		void SetLanguage(long userId, string language);
		string GetLanguage(long userId);
		long? GetUserIdByEmail(string email);

		void SetPassword(long userId, string password);
		PasswordData GetPasswordData(string login);
	}
}