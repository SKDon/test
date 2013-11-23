using System;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.Helpers;

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

		public void SetStateAvailabilities(long stateId, RoleType[] roles)
		{
			if (roles ==null)
			{
				throw new ArgumentNullException("roles");
			}

			var table = TableParameters.GeIdsTable("Roles", roles.Select(x => (long)x).ToArray());

			_executor.Execute("[dbo].[State_SetStateAvailabilities]", new TableParameters(new { stateId }, table));
		}

		public void SetStateVisibilities(long stateId, RoleType[] roles)
		{
			if (roles == null)
			{
				throw new ArgumentNullException("roles");
			}

			var table = TableParameters.GeIdsTable("Roles", roles.Select(x => (long)x).ToArray());

			_executor.Execute("[dbo].[State_SetStateVisibilities]", new TableParameters(new { stateId }, table));
		}

		public void SetStateEmailRecipients(long stateId, RoleType[] roles)
		{
			if (roles == null)
			{
				throw new ArgumentNullException("roles");
			}

			var table = TableParameters.GeIdsTable("Roles", roles.Select(x => (long)x).ToArray());

			_executor.Execute("[dbo].[State_SetStateEmailRecipients]", new TableParameters(new { stateId }, table));
		}
	}
}