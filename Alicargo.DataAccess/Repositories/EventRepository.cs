using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;

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

		public EventData GetNext(EventState state, int partitionId)
		{
			return _executor.Query<EventData>("[dbo].[Event_GetNext]", new { state, partitionId });
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