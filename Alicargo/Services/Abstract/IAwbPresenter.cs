using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface IAwbPresenter
	{		
		ListCollection<AirWaybillModel> List(int take, int skip);
		AirWaybillModel Get(long id);		
	}
}