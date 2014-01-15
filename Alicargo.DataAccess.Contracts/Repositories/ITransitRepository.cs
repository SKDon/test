using System;
using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.DataAccess.Contracts.Repositories
{
	public interface ITransitRepository
	{
		Func<long> Add(TransitData transit);
		void Update(TransitData transit);
		TransitData[] Get(params long[] ids);
		long? GetaApplicationId(long id);
		TransitData GetByApplication(long id);
		TransitData GetByClient(long clientId);
		void Delete(long transitId);
	}
}
