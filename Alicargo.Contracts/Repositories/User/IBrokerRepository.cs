using Alicargo.Contracts.Contracts.User;

namespace Alicargo.Contracts.Repositories.User
{
	public interface IBrokerRepository
	{
		BrokerData Get(long brokerId);
		BrokerData GetByUserId(long userId);
		BrokerData[] GetAll();
		void Update(long brokerId, string name, string login, string email);
		void Add(string name, string login, string email, string language);
	}
}