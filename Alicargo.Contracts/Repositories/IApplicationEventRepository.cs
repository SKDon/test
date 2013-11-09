using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
{
	public interface IApplicationEventRepository
	{
		void Add<T>(long applicationId, ApplicationEventType eventType, T data);
		ApplicationEventData GetNext(ApplicationEventState state, int index, int total);
		void SetState(long id, ApplicationEventState state);
		void Delete(long id);
	}
}