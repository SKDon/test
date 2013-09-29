using System;
using System.Data.SqlClient;
using System.Transactions;
using Alicargo.App_Start;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;
using Alicargo.Services.Abstract;
using Moq;
using Ninject;

namespace Alicargo.TestHelpers
{
	public sealed class CompositionHelper : IDisposable
	{
		private readonly StandardKernel _kernel;
		private readonly IUnitOfWork _unitOfWork;
		private readonly SqlConnection _connection;
		private readonly TransactionScope _transactionScope;

		public CompositionHelper(string connectionString)
		{
			_kernel = new StandardKernel();
			_connection = new SqlConnection(connectionString);
			_connection.Open();
			_transactionScope = new TransactionScope();
			_unitOfWork = new UnitOfWork(_connection);

			BindServices();
			BindIdentityService();			
		}

		public IKernel Kernel
		{
			get { return _kernel; }
		}

		private void BindIdentityService()
		{
			var identityService = new Mock<IIdentityService>(MockBehavior.Strict);

			identityService.Setup(x => x.IsInRole(RoleType.Admin)).Returns(true);
			identityService.Setup(x => x.IsInRole(RoleType.Client)).Returns(false);
			identityService.Setup(x => x.TwoLetterISOLanguageName).Returns(TwoLetterISOLanguageName.English);

			Kernel.Rebind<IIdentityService>().ToConstant(identityService.Object).InSingletonScope();
		}

		private void BindServices()
		{
			CompositionRoot.BindDataAccess(Kernel, null);

			Kernel.Rebind<IUnitOfWork>().ToConstant(_unitOfWork).InSingletonScope();

			CompositionRoot.BindServices(Kernel);
		}

		public void Dispose()
		{
			_transactionScope.Dispose();
			_connection.Close();
			Kernel.Dispose();
		}
	}
}
