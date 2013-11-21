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

		public long Add(StateData data)
		{
			using (var scope = new TransactionScope())
			{
				var id = _executor.Query<long>("[dbo].[State_Add]", new { data.Name, data.Position, IsSystem = false });

				foreach (var item in data.Localization)
				{
					_executor.Execute("[dbo].[StateLocalization_Merge]", new
					{
						Name = item.Value,
						TwoLetterISOLanguageName = item.Key,
						StateId = id
					});
				}

				scope.Complete();

				return id;
			}
		}

		public IReadOnlyDictionary<long, StateData> Get(params long[] ids)
		{
			var idsTable = TableParameters.GeIdsTable("Ids", ids.Distinct().ToArray());

			var list = _executor.Array<StateListItem>("[dbo].[State_GetList]", new TableParameters(idsTable));

			var localizations = GetLocalization(idsTable);

			return list.ToDictionary(x => x.Id,
				x => new StateData(localizations[x.Id])
				{
					Name = x.Name,
					Position = x.Position
				});
		}

		private Dictionary<long, Dictionary<string, string>> GetLocalization(DataTable idsTable)
		{
			var table = TableParameters.GetLocalizationTable();

			var localizations = _executor.Array<StateLocalization>("[dbo].[StateLocalization_Get]", new TableParameters(idsTable, table));

			return localizations.GroupBy(x => x.StateId)
				.ToDictionary(x => x.Key, x => x.ToDictionary(y => y.TwoLetterISOLanguageName, y => y.Name));
		}

		public StateListItem[] All()
		{
			return _executor.Array<StateListItem>("[dbo].[State_GetList]"); // todo: test for order by position
		}

		public void Edit(long id, StateData data)
		{
			using (var scope = new TransactionScope())
			{
				_executor.Execute("[dbo].[State_Update]", new { data.Name, data.Position, Id = id });

				foreach (var item in data.Localization)
				{
					_executor.Execute("[dbo].[StateLocalization_Merge]", new
					{
						Name = item.Value,
						TwoLetterISOLanguageName = item.Key,
						StateId = id
					});
				}

				scope.Complete();
			}
		}

		public void Delete(long id)
		{
			throw new NotImplementedException();
		}

		private class StateLocalization
		{
			// ReSharper disable UnusedAutoPropertyAccessor.Local
			public long StateId { get; set; }
			public string Name { get; set; }
			public string TwoLetterISOLanguageName { get; set; } // ReSharper restore UnusedAutoPropertyAccessor.Local
		}
	}
}