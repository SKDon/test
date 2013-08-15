using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
{
	public  interface IUserRepository
	{
		UserData[] GetByRole(RoleType role);

		void UpdateAdmin(long entityId, string name, string login, string email, string newPassword);
		void UpdateSender(long entityId, string name, string login, string email, string newPassword);
		void UpdateBrocker(long entityId, string name, string login, string email, string newPassword);
		void UpdateForwarder(long entityId, string name, string login, string email, string newPassword);

		void AddForwarder(long userId, string name, string login, string email, string newPassword, string twoLetterISOLanguageName);
		void AddBrocker(long userId, string name, string login, string email, string newPassword, string twoLetterISOLanguageName);
		void AddSender(long userId, string name, string login, string email, string newPassword, string twoLetterISOLanguageName);
		void AddAdmin(long userId, string name, string login, string email, string newPassword, string twoLetterISOLanguageName);
	}
}
