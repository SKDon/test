using System;
using System.Data.SqlClient;
using System.Transactions;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.TestHelpers
{
	public sealed class DbTestContext
    {
        private readonly TransactionScope _transactionScope;
        private readonly SqlConnection _connection;

        public IUnitOfWork UnitOfWork { get; private set; }

	    public DbTestContext(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
			_connection.Open();

            _transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew);

			UnitOfWork = new UnitOfWork(_connection);
        }

        public void Cleanup()
        {
            _transactionScope.Dispose();
			_connection.Close();
        }

        public byte[] RandomBytes()
        {
            return Guid.NewGuid().ToByteArray();
        }
    }
}