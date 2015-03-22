using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using Alicargo.DataAccess.Contracts.Contracts.State;
using Alicargo.DataAccess.Contracts.Repositories;
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

		public long Add(StateEditData data)
		{
			using(var scope = new TransactionScope())
			{
				var id = _executor.Query<long>("[dbo].[State_Add]",
					new
					{
						data.Name,
						data.Position,
						IsSystem = false,
					});

				_executor.Execute("[dbo].[StateLocalization_Merge]",
					new
					{
						Name = data.LocalizedName,
						TwoLetterISOLanguageName = data.Language,
						StateId = id
					});

				scope.Complete();

				return id;
			}
		}

		public void Update(long id, StateEditData data)
		{
			using(var scope = new TransactionScope())
			{
				_executor.Execute("[dbo].[State_Update]",
					new
					{
						data.Name,
						data.Position,
						Id = id
					});

				_executor.Execute("[dbo].[StateLocalization_Merge]",
					new
					{
						Name = data.LocalizedName,
						TwoLetterISOLanguageName = data.Language,
						StateId = id
					});

				scope.Complete();
			}
		}

		public void Delete(long id)
		{
			_executor.Execute("[dbo].[State_Delete]", new { Id = id });
		}

		public IReadOnlyDictionary<long, StateData> Get(string language, params long[] ids)
		{
			var idsTable = TableParameters.GeIdsTable("Ids", ids.Distinct().ToArray());

			var list = _executor.Array<dynamic>("[dbo].[State_GetOrderedList]", new TableParameters(idsTable));

			var table = new DataTable("Localizations");
			table.Columns.Add("Value");
			table.Rows.Add(language);

			var localizations = _executor.Array<dynamic>(
				"[dbo].[StateLocalization_Get]",
				new TableParameters(idsTable, table))
				.GroupBy(x => x.StateId)
				.ToDictionary(x => x.Key, x => x.ToDictionary(y => y.TwoLetterISOLanguageName, y => y.Name));

			return list.ToDictionary(x => (long)x.Id,
				x => new StateData
				{
					Name = x.Name,
					Position = x.Position,
					LocalizedName = localizations[x.Id][language],
					IsSystem = x.IsSystem
				});
		}
	}
}