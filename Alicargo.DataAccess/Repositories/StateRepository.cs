using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.Helpers;

namespace Alicargo.DataAccess.Repositories
{
	public sealed class StateRepository : IStateRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public StateRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public long Add(string twoLetterISOLanguageName, StateData data)
		{
			using (var scope = new TransactionScope())
			{
				var id = _executor.Query<long>("[dbo].[State_Add]", new { data.Name, data.Position, IsSystem = false });

				_executor.Execute("[dbo].[StateLocalization_Merge]", new
				{
					Name = data.LocalizedName,
					TwoLetterISOLanguageName = twoLetterISOLanguageName,
					StateId = id
				});

				scope.Complete();

				return id;
			}
		}

		public StateListItem[] All()
		{
			return _executor.Array<StateListItem>("[dbo].[State_GetList]"); // todo: test for order by position
		}

		public void Update(long id, string twoLetterISOLanguageName, StateData data)
		{
			using (var scope = new TransactionScope())
			{
				_executor.Execute("[dbo].[State_Update]", new { data.Name, data.Position, Id = id });

				_executor.Execute("[dbo].[StateLocalization_Merge]", new
				{
					Name = data.LocalizedName,
					TwoLetterISOLanguageName = twoLetterISOLanguageName,
					StateId = id
				});

				scope.Complete();
			}
		}

		public void Delete(long id)
		{
			_executor.Execute("[dbo].[State_Delete]", new { Id = id });
		}

		public IReadOnlyDictionary<long, StateData> Get(string twoLetterISOLanguageName, params long[] ids)
		{
			var idsTable = TableParameters.GeIdsTable("Ids", ids.Distinct().ToArray());

			var list = _executor.Array<StateListItem>("[dbo].[State_GetList]", new TableParameters(idsTable));

			var table = new DataTable("Localizations");
			table.Columns.Add("Value");
			table.Rows.Add(twoLetterISOLanguageName);

			var localizations = _executor.Array<dynamic>(
				"[dbo].[StateLocalization_Get]", new TableParameters(idsTable, table))
				.GroupBy(x => x.StateId)
				.ToDictionary(x => x.Key, x => x.ToDictionary(y => y.TwoLetterISOLanguageName, y => y.Name));

			return list.ToDictionary(x => x.Id,
				x => new StateData
				{
					Name = x.Name,
					Position = x.Position,
					LocalizedName = localizations[x.Id][twoLetterISOLanguageName]
				});
		}
	}
}