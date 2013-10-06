using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.DbContext;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Helpers
{
    internal sealed class DbTestContext
    {
        private readonly TransactionScope _transactionScope;
        private readonly SqlConnection _connection;

		public IFixture Fixture { get; private set; }

        public IUnitOfWork UnitOfWork { get; private set; }

        public DbTestContext()
        {
            Fixture = new Fixture();

            _connection = new SqlConnection(Settings.Default.MainConnectionString);
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
            return Fixture.CreateMany<byte>().ToArray();
        }
    }
}
