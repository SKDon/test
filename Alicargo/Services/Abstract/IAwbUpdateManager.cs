using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.Abstract
{
	public interface IAwbUpdateManager
	{
		void Update(long id, AirWaybillEditModel model);
		void Update(long id, BrockerAwbModel model);
		void Update(long id, SenderAwbModel model);
	}
}
