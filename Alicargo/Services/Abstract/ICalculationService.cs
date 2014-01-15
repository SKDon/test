using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.Services.Abstract
{
	public interface ICalculationService
	{
		CalculationData Calculate(long applicationId);
		void CancelCalculatation(long applicationId);
	}
}