using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.User;

namespace Alicargo.Contracts.Repositories
{
	public interface IBrokerRepository
	{
		BrokerData Get(long brokerId);
		BrokerData GetByUserId(long userId);
		BrokerData[] GetAll();
	}
}