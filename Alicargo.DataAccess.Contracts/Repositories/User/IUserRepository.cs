using Alicargo.DataAccess.Contracts.Contracts.User;

namespace Alicargo.DataAccess.Contracts.Repositories.User
{
	public interface IUserRepository
	{
		long Add(string login, string password, string language);
		string Get(long userId);
		PasswordData GetPasswordData(string login);
		long? GetUserIdByEmail(string email); // todo: 182, 159
		void SetLanguage(long userId, string language);
		void SetLogin(long userId, string login);
		void SetPassword(long userId, string password);
	}
}