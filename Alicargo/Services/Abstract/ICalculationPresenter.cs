using Alicargo.ViewModels.Calculation;

namespace Alicargo.Services.Abstract
{
	public interface ICalculationPresenter
	{
		CalculationListCollection List(int take, long skip);
		CalculationListCollection Row(long awbId);
		object ClientCalculationList(int take, long skip);
	}
}
