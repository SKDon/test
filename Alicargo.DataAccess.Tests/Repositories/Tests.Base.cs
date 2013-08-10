using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;
using Alicargo.Core.Repositories;
using Alicargo.Core.Security;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Helpers;
using Alicargo.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.Tests.Repositories
{
	[TestClass]
	public partial class Tests
	{
		private TransactionScope _transactionScope;
		private SqlConnection _connection;

		private Fixture _fixture;

		private IUnitOfWork _unitOfWork;
		private IApplicationRepository _applicationRepository;
		private IApplicationUpdateRepository _applicationUpater;
		private IClientRepository _clientRepository;
		private IAuthenticationRepository _authenticationRepository;
		private IPasswordConverter _passwordConverter;
		private ITransitRepository _transitRepository;
		private ICarrierRepository _carrierRepository;
		private IBrockerRepository _brockerRepository;
		private IReferenceRepository _referenceRepository;
		private IStateRepository _stateRepository;

		[TestInitialize]
		public void TestInitialize()
		{
			_fixture = new Fixture();

			var connectionString = ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString;
			_connection = new SqlConnection(connectionString);
			_connection.Open();
			_transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew);

			_unitOfWork = new UnitOfWork(_connection);

			_applicationRepository = new ApplicationRepository(_unitOfWork, new ApplicationRepositoryOrderer());
			_applicationUpater = new ApplicationUpdateRepository(_unitOfWork);
			_clientRepository = new ClientRepository(_unitOfWork);
			_passwordConverter = new PasswordConverter();

			_authenticationRepository = new AuthenticationRepository(_unitOfWork, _passwordConverter);
			_transitRepository = new TransitRepository(_unitOfWork);
			_carrierRepository = new CarrierRepository(_unitOfWork);
			_brockerRepository = new BrockerRepository(_unitOfWork);
			_referenceRepository = new ReferenceRepository(_unitOfWork);
			_stateRepository = new StateRepository(_unitOfWork);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_transactionScope.Dispose();
			_connection.Close();
		}

		private static string RandomString()
		{
			return Guid.NewGuid().ToString();
		}

		private static byte[] RandomBytes()
		{
			return Guid.NewGuid().ToByteArray();
		}
	}
}
