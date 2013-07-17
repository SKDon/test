using System.Data.SqlClient;
using System.Transactions;
using Alicargo.Core.BlackBox.Tests.Properties;
using Alicargo.Core.Enums;
using Alicargo.Core.Security;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using Alicargo.Services;
using Alicargo.Services.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

namespace Alicargo.Core.BlackBox.Tests
{
	[TestClass]
	public class StateServiceTests
	{
		private TransactionScope _transactionScope;
		private SqlConnection _connection;
		private StateService _stateService;
		private Mock<IIdentityService> _identityService;
		private StateConfig _stateConfig;

		[TestInitialize]
		public void TestInitialize()
		{
			_connection = new SqlConnection(Settings.Default.MainConnectionString);
			_connection.Open();
			_transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew);
			var unitOfWork = new UnitOfWork(_connection);

			_identityService = new Mock<IIdentityService>(MockBehavior.Strict);
			_stateConfig = new StateConfig();

			_stateService = new StateService(
				new StateRepository(unitOfWork),
				_identityService.Object,
				_stateConfig);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_transactionScope.Dispose();
			_connection.Close();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_HasPermissionToSetState()
		{
			_identityService.Setup(x => x.IsInRole(RoleType.Admin)).Returns(true);
			_identityService.Setup(x => x.IsInRole(RoleType.Sender)).Returns(true);
			_identityService.Setup(x => x.IsInRole(RoleType.Brocker)).Returns(true);

			//_stateService.HasPermissionToSetState(_sta);

			_identityService.Verify(x => x.IsInRole(RoleType.Admin));
			_identityService.Verify(x => x.IsInRole(RoleType.Sender));
			_identityService.Verify(x => x.IsInRole(RoleType.Brocker));

		}
	}
}
