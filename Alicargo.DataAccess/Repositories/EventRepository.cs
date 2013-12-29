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

		public void Add(long applicationId, EventType eventType, byte[] data)
		{
			_executor.Execute("[dbo].[Event_Add]", new { applicationId, eventType, data, State = EventState.ApplicationEmailing });
		}

		public EventData GetNext(EventState state, int shardIndex, int shardCount)
		{
			return _executor.Query<EventData>("[dbo].[Event_GetNext]", new { state, shardIndex, shardCount });
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