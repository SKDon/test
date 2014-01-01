using Alicargo.Contracts.Contracts;

namespace Alicargo.Contracts.Repositories
{
	public interface ICalculationRepository
	{
		void Add(CalculationData data, long applicationId);
		CalculationData[] GetByClientId(long clientId);
		void RemoveByApplication(long applicationId);
	}
}