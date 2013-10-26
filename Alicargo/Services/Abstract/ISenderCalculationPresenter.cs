using Alicargo.ViewModels.Calculation.Sender;

namespace Alicargo.Services.Abstract
{
	public interface ISenderCalculationPresenter
	{
		SenderCalculationListCollection List(long senderId, int take, long skip);
	}
}
