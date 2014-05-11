using Alicargo.DataAccess.Contracts.Contracts.User;

namespace Alicargo.DataAccess.Contracts.Repositories.User
{
	public interface IUserRepository
	{
		long Add(string login, string password, string language);
		void SetLanguage(long userId, string language);
		string GetLanguage(long userId);
		// todo: 182, 159
		long? GetUserIdByEmail(string email);
		void SetPassword(long userId, string password);
		PasswordData GetPasswordData(string login);
	}
}