using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Alicargo.Contracts.Repositories;
using Dapper;

namespace Alicargo.DataAccess.DbContext
{
	public sealed class SqlProcedureExecutor : ISqlProcedureExecutor
	{
		private static T Action<T>(string connectionString, Func<IDbConnection, T> action)
		{
			return SqlExceptionsHelper.Action(() =>
			{
				using (var connection = new SqlConnection(connectionString))
				{
					return action(connection);
				}
			});
		}

		private readonly string _connectionString;

		public SqlProcedureExecutor(string connectionString)
		{
			_connectionString = connectionString;
		}


		public T Query<T>(string sql, object param = null, IDbTransaction transaction = null)
		{
			return Action(_connectionString, connection =>
				connection.Query<T>(sql, param, transaction, commandType: CommandType.StoredProcedure)
						  .FirstOrDefault());
		}

		public T[] Array<T>(string sql, object param = null, IDbTransaction transaction = null)
		{
			return Action(_connectionString, connection =>
				connection.Query<T>(sql, param, transaction, commandType: CommandType.StoredProcedure)
						  .ToArray());
		}

		public int Execute(string sql, object param = null, IDbTransaction transaction = null)
		{
			return Action(_connectionString, connection =>
				connection.Execute(sql, param, transaction, commandType: CommandType.StoredProcedure));
		}
	}
}