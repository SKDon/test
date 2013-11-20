using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;

namespace Alicargo.DataAccess.Repositories
{
	public sealed class StateSettingsRepository : IStateSettingsRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public StateSettingsRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public StateRole[] GetStateAvailabilities()
		{
			return _executor.Array<StateRole>("[dbo].[State_GetStateAvailabilities]");
		}

		public StateRole[] GetStateVisibilities()
		{
			return _executor.Array<StateRole>("[dbo].[State_GetStateVisibilities]");
		}

		public StateRole[] GetStateEmailRecipients()
		{
			return _executor.Array<StateRole>("[dbo].[State_GetStateEmailRecipients]");
		}
	}
}