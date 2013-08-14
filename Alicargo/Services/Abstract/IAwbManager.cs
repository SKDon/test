using Alicargo.ViewModels;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.Abstract
{
	public interface IAwbManager
	{
		long Create(long applicationId, AirWaybillEditModel model);
		void SetAwb(long applicationId, long? awbId);
		void Update(long id, AirWaybillEditModel model);
		void Update(long id, BrockerAWBModel model);
		void Delete(long id);
		void SetState(long airWaybillId, long stateId);
	}
}
