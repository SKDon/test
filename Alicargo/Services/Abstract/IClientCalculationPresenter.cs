using Alicargo.ViewModels.Calculation;

namespace Alicargo.Services.Abstract
{
	public interface IClientCalculationPresenter
	{
		ClientCalculationListCollection List(int take, long skip);
	}
}
