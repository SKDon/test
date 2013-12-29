using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
{
	public interface IEventRepository
	{
		void Add(int partitionId, EventType type, EventState state, byte[] data);
		EventData GetNext(EventState state, int partitionId);
		void SetState(long id, EventState state);
		void Delete(long id);
	}
}