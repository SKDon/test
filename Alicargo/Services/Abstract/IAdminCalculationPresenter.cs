using Alicargo.ViewModels.Calculation.Admin;

namespace Alicargo.Services.Abstract
{
	public interface IAdminCalculationPresenter
	{
		CalculationListCollection List(int take, long skip);
		CalculationListCollection Row(long awbId);
		decimal GetTotalBalance();
	}
}
