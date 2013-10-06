using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
{
	public interface ICalculationRepository
	{
		void Add(CalculationData data);
		VersionedData<CalculationState, CalculationData>[] Get(CalculationState state);
		VersionData<CalculationState> SetState(long id, byte[] rowVersion, CalculationState state);
	}
}
