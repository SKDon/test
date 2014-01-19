using Alicargo.Core.Calculation.Entities;

namespace Alicargo.Services.Abstract
{
	public interface IClientCalculationPresenter
	{
		ClientCalculationListCollection List(long clientId, int take, long skip);
	}
}
