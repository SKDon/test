using System.Data;
using Dapper;

namespace Alicargo.DataAccess.Helpers
{
	internal sealed class TableParameters : SqlMapper.IDynamicParameters
	{
		private readonly DataTable[] _tables;

		public TableParameters(params DataTable[] tables)
		{
			_tables = tables;
		}

		public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
		{
			foreach (var table in _tables)
			{
				var parameter = command.CreateParameter();

				parameter.ParameterName = table.TableName;
				parameter.Value = table;

				command.Parameters.Add(parameter);
			}
		}

		public static DataTable GeIdsTable(string name, long[] ids)
		{
			var table = new DataTable(name);
			table.Columns.Add("Id", typeof(long));
			foreach (var id in ids) { table.Rows.Add(id); }

			return table;
		}
	}
}
