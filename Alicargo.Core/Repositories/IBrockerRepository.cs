using Alicargo.Contracts.Contracts;
using Alicargo.Core.Models;

namespace Alicargo.Core.Repositories
{
	public interface IBrockerRepository
	{
		BrockerData Get(long brockerId);
		BrockerData GetByUserId(long userId);
		BrockerData[] GetAll();
	}
}