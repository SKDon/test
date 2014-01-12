using Alicargo.Core.Calculation.Entities;

namespace Alicargo.Core.Calculation
{
	public interface IClientCalculationPresenter
	{
		ClientCalculationListCollection List(long clientId, int take, long skip);
	}
}
