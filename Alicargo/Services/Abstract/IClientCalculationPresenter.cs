using Alicargo.ViewModels.Calculation;
using Alicargo.ViewModels.Calculation.Client;

namespace Alicargo.Services.Abstract
{
	public interface IClientCalculationPresenter
	{
		ClientCalculationListCollection List(long clientId, int take, long skip);
	}
}
