using System;
using System.Data;
using System.Data.SqlClient;

namespace Alicargo.DataAccess.DbContext
{
	public static class SqlHelper
	{
		public static T Action<T>(string connectionString, Func<IDbConnection, T> action)
		{
			return SqlExceptionsHelper.Action(() =>
			{
				using (var connection = new SqlConnection(connectionString))
				{
					return action(connection);
				}
			});
		}
	}
}
