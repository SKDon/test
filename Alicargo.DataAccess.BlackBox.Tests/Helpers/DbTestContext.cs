using System;
using System.Data.SqlClient;
using System.Transactions;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.BlackBox.Tests.Helpers
{
    internal sealed class DbTestContext
    {
        private readonly TransactionScope _transactionScope;
        private readonly SqlConnection _connection;

        public IUnitOfWork UnitOfWork { get; private set; }

	    public SqlConnection Connection
	    {
		    get { return _connection; }
	    }

	    public DbTestContext()
        {
            _connection = new SqlConnection(Settings.Default.MainConnectionString);
            Connection.Open();

            _transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew);

            UnitOfWork = new UnitOfWork(Connection);
        }

        public void Cleanup()
        {
            _transactionScope.Dispose();
            Connection.Close();
        }

        public byte[] RandomBytes()
        {
            return Guid.NewGuid().ToByteArray();
        }
    }
}
