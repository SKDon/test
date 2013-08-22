using Alicargo.ViewModels;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.Abstract
{
	public interface IAwbUpdateManager
	{
		void Update(long id, AirWaybillEditModel model);
		void Update(long id, BrockerAWBModel model);
		void SetState(long airWaybillId, long stateId);
	}
}
