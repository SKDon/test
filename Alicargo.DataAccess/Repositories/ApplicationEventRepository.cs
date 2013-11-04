using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;

namespace Alicargo.DataAccess.Repositories
{
	public sealed class ApplicationEventRepository : IApplicationEventRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public ApplicationEventRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public void Add(long applicationId, ApplicationEventType eventType)
		{
			_executor.Execute("");
		}

		public ApplicationEventData GetNext()
		{
			return _executor.Query<ApplicationEventData>("[dbo].[ApplicationEvent_GetNext]");
		}
	}
}
