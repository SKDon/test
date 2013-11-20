using System.Data;
using System.Linq;
using Alicargo.Contracts.Repositories;
using Dapper;

namespace Alicargo.DataAccess.DbContext
{
	public sealed class SqlProcedureExecutor : ISqlProcedureExecutor
	{
		private readonly string _connectionString;

		public SqlProcedureExecutor(string connectionString)
		{
			_connectionString = connectionString;
		}

		public T Query<T>(string sql, object param = null)
		{
			return SqlHelper.Action(_connectionString, connection =>
				connection.Query<T>(sql, param, commandType: CommandType.StoredProcedure).FirstOrDefault());
		}

		public T[] Array<T>(string sql, object param = null)
		{
			return SqlHelper.Action(_connectionString, connection =>
				connection.Query<T>(sql, param, commandType: CommandType.StoredProcedure).ToArray());
		}

		public int Execute(string sql, object param = null)
		{
			return SqlHelper.Action(_connectionString, connection =>
				connection.Execute(sql, param, commandType: CommandType.StoredProcedure));
		}
	}
}