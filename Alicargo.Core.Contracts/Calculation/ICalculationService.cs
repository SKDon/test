using Alicargo.DataAccess.Contracts.Contracts.Calculation;

namespace Alicargo.Core.Contracts.Calculation
{
	public interface ICalculationService
	{
		CalculationData Calculate(long applicationId);
		void CancelCalculatation(long applicationId);
	}
}