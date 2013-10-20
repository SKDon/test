using Alicargo.ViewModels.Calculation;

namespace Alicargo.Services.Abstract
{
	public interface IClientCalculationPresenter
	{
		ClientCalculationListCollection List(long clientId, int take, long skip);
	}
}
