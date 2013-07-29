using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;
using Alicargo.App_Start;
using Alicargo.Controllers;
using Alicargo.DataAccess.DbContext;
using Alicargo.Services.Abstract;
using Alicargo.Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ploeh.AutoFixture;

namespace Alicargo.Tests.Controllers
{
	[TestClass]
	public class ApplicationControllerTests
	{
		private IFixture _fixture;
		private TransactionScope _transactionScope;
		private SqlConnection _connection;
		private AlicargoDataContext _db;
		private StandardKernel _kernel;
		private ApplicationController _controller;

		[TestInitialize]
		public void TestInitialize()
		{
			_kernel = new StandardKernel();
			_fixture = new Fixture();			

			_connection = new SqlConnection(Settings.Default.MainConnectionString);
			_connection.Open();
			_db = new AlicargoDataContext(_connection);
			_transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew);

			CompositionRoot.BindServices(_kernel);
			CompositionRoot.BindDataAccess(_kernel, Settings.Default.MainConnectionString);

			//new Mock
			//_kernel.Bind<IIdentityService>().To()
			_kernel.Bind<ApplicationController>().ToSelf();

			_controller = _kernel.Get<ApplicationController>();
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_transactionScope.Dispose();
			_connection.Close();
			_kernel.Dispose();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_List()
		{
			var result = _controller.List(10, 0, 1, 10, null);
		}
	}
}
