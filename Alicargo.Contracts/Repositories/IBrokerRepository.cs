using Alicargo.Contracts.Contracts;

namespace Alicargo.Contracts.Repositories
{
	public interface IBrokerRepository
	{
		BrokerData Get(long brokerId);
		BrokerData GetByUserId(long userId);
		BrokerData[] GetAll();
	}
}