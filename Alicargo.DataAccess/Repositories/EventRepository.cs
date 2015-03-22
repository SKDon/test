using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;

namespace Alicargo.DataAccess.Repositories
{
	public sealed class EventRepository : IEventRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public EventRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public void Add(int partitionId, EventType type, EventState state, byte[] data)
		{
			_executor.Execute("[dbo].[Event_Add]", new { EventTypeId = type, data, StateId = state, partitionId });
		}

		public EventData GetNext(EventType type, int partitionId)
		{
			return _executor.Query<EventData>("[dbo].[Event_GetNext]", new { EventTypeId = type, partitionId });
		}

		public void SetState(long id, EventState state)
		{
			_executor.Execute("[dbo].[Event_SetState]", new { id, state });
		}

		public void Delete(long id)
		{
			_executor.Execute("[dbo].[Event_Delete]", new { id });
		}
	}
}