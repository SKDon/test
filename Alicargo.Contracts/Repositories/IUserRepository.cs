using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
{
	public interface IUserRepository
	{
		UserData[] GetByRole(RoleType role);

		void UpdateAdmin(long entityId, string name, string login, string email, string newPassword);
		void UpdateBroker(long entityId, string name, string login, string email, string newPassword);
		void UpdateForwarder(long entityId, string name, string login, string email, string newPassword);

		void AddForwarder(long userId, string name, string login, string email, string newPassword, string twoLetterISOLanguageName);
		void AddBroker(long userId, string name, string login, string email, string newPassword, string twoLetterISOLanguageName);
		void AddAdmin(long userId, string name, string login, string email, string newPassword, string twoLetterISOLanguageName);
	}
}
