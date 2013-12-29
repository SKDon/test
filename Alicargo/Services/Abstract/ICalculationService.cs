namespace Alicargo.Services.Abstract
{
	public interface ICalculationService
	{
		void Calculate(long applicationId);
		void CancelCalculatation(long applicationId);
	}
}
