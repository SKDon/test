using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.DataAccess.Contracts.Repositories
{
	public interface IEventRepository
	{
		void Add(int partitionId, EventType type, EventState state, byte[] data);
		EventData GetNext(EventType type, int partitionId);
		void SetState(long id, EventState state);
		void Delete(long id);
	}
}