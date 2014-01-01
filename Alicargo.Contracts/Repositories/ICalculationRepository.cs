using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
{
	public interface ICalculationRepository
	{
		void Add(CalculationData data, long applicationId);
		CalculationData[] Get(CalculationState state);
		CalculationData[] GetByClientId(long clientId);
		void RemoveByApplication(long applicationId);
	}
}