using Alicargo.ViewModels.Calculation;

namespace Alicargo.Services.Abstract
{
	public interface ICalculationService
	{
		CalculationListCollection List(int take, long skip);
		CalculationListCollection Row(long id);
	}
}
