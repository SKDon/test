using System.Data.SqlClient;
using System.Transactions;
using Alicargo.DataAccess.DbContext;
using Alicargo.Tests.Properties;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.Tests.Controllers
{
	[TestClass, Ignore]
	public class DummyTests
	{
		private Fixture _fixture;
		private CompareObjects _comparer;
		private TransactionScope _transactionScope;
		private SqlConnection _connection;
		private AlicargoDataContext _db;

		[TestInitialize]
		public void TestInitialize()
		{
			_fixture = new Fixture();
			_comparer = new CompareObjects
			{
				MaxDifferences = 10,
				Caching = true,
				AutoClearCache = false
			};

			_connection = new SqlConnection(Settings.Default.MainConnectionString);
			_connection.Open();
			_db = new AlicargoDataContext(_connection);
			_transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_transactionScope.Dispose();
			_connection.Close();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Name()
		{
		}
	}
}
