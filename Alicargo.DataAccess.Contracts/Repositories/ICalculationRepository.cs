using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.DataAccess.Contracts.Repositories
{
	public interface ICalculationRepository
	{
		void Add(CalculationData data, long applicationId);
		void RemoveByApplication(long applicationId);
		CalculationData GetByApplication(long applicationId);
		CalculationData[] GetByClient(long clientId);
	}
}