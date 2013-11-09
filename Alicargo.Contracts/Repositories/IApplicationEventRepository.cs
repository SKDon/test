using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
{
	public interface IApplicationEventRepository
	{
		void Add(long applicationId, ApplicationEventType eventType, byte[] data);
		ApplicationEventData GetNext(ApplicationEventState state, int index, int total);
		void SetState(long id, ApplicationEventState state);
		void Delete(long id);
	}
}