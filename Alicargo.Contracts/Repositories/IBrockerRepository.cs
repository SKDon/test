using Alicargo.Contracts.Contracts;

namespace Alicargo.Contracts.Repositories
{
	public interface IBrockerRepository
	{
		BrockerData Get(long brockerId);
		BrockerData GetByUserId(long userId);
		BrockerData[] GetAll();
	}
}