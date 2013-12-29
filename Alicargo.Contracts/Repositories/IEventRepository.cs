using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
{
	public interface IEventRepository
	{
		void Add(long applicationId, EventType eventType, byte[] data);
		EventData GetNext(EventState state, int shardIndex, int shardCount);
		void SetState(long id, EventState state);
		void Delete(long id);
	}
}