using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface IAwbManager
	{
		void Create(long applicationId, ReferenceModel model);
		void SetAwb(long applicationId, long? awbId);
		void Update(ReferenceModel model);
		void Delete(long id);
		void SetState(long referenceId, long stateId);
	}
}
