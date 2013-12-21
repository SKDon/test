using Alicargo.Contracts.Contracts.User;

namespace Alicargo.Contracts.Repositories.User
{
	public interface IBrokerRepository
	{
		BrokerData Get(long brokerId);
		BrokerData GetByUserId(long userId);
		BrokerData[] GetAll();
		void UpdateBroker(long entityId, string name, string login, string email);
		void AddBroker(long userId, string name, string login, string email, string twoLetterISOLanguageName);
	}
}