using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;

namespace Alicargo.DataAccess.Repositories.Application
{
	public sealed class ApplicationEventRepository : IApplicationEventRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public ApplicationEventRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public void Add(long applicationId, ApplicationEventType eventType, byte[] data)
		{
			_executor.Execute("[dbo].[ApplicationEvent_Add]", new { applicationId, eventType, data, State = ApplicationEventState.New });
		}

		public ApplicationEventData GetNext(ApplicationEventState state, int shardIndex, int shardCount)
		{
			return _executor.Query<ApplicationEventData>("[dbo].[ApplicationEvent_GetNext]", new { state, shardIndex, shardCount });
		}

		public void SetState(long id, ApplicationEventState state)
		{
			_executor.Execute("[dbo].[ApplicationEvent_SetState]", new { id, state });
		}

		public void Delete(long id)
		{
			_executor.Execute("[dbo].[ApplicationEvent_Delete]", new { id });
		}
	}
}