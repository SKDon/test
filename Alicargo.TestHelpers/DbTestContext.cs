using System.Data.SqlClient;
using System.Transactions;

namespace Alicargo.TestHelpers
{
	public sealed class DbTestContext
	{
		private readonly SqlConnection _connection;
		private readonly TransactionScope _transactionScope;

		public DbTestContext(string connectionString)
		{
			_connection = new SqlConnection(connectionString);
			Connection.Open();

			_transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew);
		}

		public SqlConnection Connection
		{
			get { return _connection; }
		}

		public void Cleanup()
		{
			_transactionScope.Dispose();
			Connection.Close();
		}
	}
}