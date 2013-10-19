using System.Data;
using System.Data.SqlClient;
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

		public T Get<T>(string sql, object param = null, IDbTransaction transaction = null)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				return connection.Query<T>(sql, param, transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();
			}
		}
	}
}
