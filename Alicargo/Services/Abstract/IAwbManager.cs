using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface IAwbManager
	{
		void Create(long applicationId, AirWaybillModel model);
		void SetAwb(long applicationId, long? awbId);
		void Update(AirWaybillModel model);
		void Delete(long id);
		void SetState(long AirWaybillId, long stateId);
	}
}
