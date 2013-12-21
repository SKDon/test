using Alicargo.Contracts.Contracts.User;

namespace Alicargo.Contracts.Repositories.User
{
	public interface IForwarderRepository
	{
		void UpdateForwarder(long entityId, string name, string login, string email);
		void AddForwarder(long userId, string name, string login, string email, string twoLetterISOLanguageName);
		UserData[] GetAll();
	}
}