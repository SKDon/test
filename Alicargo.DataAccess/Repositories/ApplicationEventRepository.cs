using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;

namespace Alicargo.DataAccess.Repositories
{
	public sealed class ApplicationEventRepository : IApplicationEventRepository
	{
		private readonly ISqlProcedureExecutor _executor;
		private readonly ISerializer _serializer;

		public ApplicationEventRepository(ISqlProcedureExecutor executor, ISerializer serializer)
		{
			_executor = executor;
			_serializer = serializer;
		}

		public void Add<T>(long applicationId, ApplicationEventType eventType, T obj)
		{
			var data = _serializer.Serialize(obj);

			_executor.Execute("[dbo].[ApplicationEvent_Add]", new { applicationId, eventType, data, State = ApplicationEventState.New });
		}

		public ApplicationEventData GetNext(ApplicationEventState state, int index, int total)
		{
			return _executor.Query<ApplicationEventData>("[dbo].[ApplicationEvent_GetNext]", new { state, index, total });
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