using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.Abstract
{
	public interface IAwbUpdateManager
	{
		void Update(long id, AirWaybillEditModel model);
		void Update(long id, BrokerAwbModel model);
		void Update(long id, SenderAwbModel model);
	}
}
