using Alicargo.ViewModels;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.Abstract
{
	public interface IAwbPresenter
	{
		ListCollection<AirWaybillListItem> List(int take, int skip);
		AirWaybillEditModel Get(long id);
	}
}