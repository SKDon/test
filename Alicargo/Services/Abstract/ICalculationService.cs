using Alicargo.ViewModels.Calculation;

namespace Alicargo.Services.Abstract
{
	public interface ICalculationService
	{
		CalculationAwb[] List(int take, long skip);
		CalculationAwb Row(long awbId);
	}
}
