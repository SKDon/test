using System;
using System.Data.SqlClient;
using System.Transactions;
using Alicargo.App_Start;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;
using Moq;
using Ninject;

namespace Alicargo.TestHelpers
{
	public sealed class CompositionHelper : IDisposable
	{
		private readonly string _mainConnectionString;
		private readonly string _filesConnectionString;
		private readonly StandardKernel _kernel = new StandardKernel();
		private readonly IUnitOfWork _unitOfWork;
		private readonly SqlConnection _connection;
		private readonly TransactionScope _transactionScope;
		private readonly RoleType _type;

		public CompositionHelper(string mainConnectionString, string filesConnectionString, RoleType type = RoleType.Admin)
		{
			_type = type;
			_mainConnectionString = mainConnectionString;
			_filesConnectionString = filesConnectionString;
			_connection = new SqlConnection(_mainConnectionString);
			_transactionScope = new TransactionScope();
			_unitOfWork = new UnitOfWork(_connection);

			Init();
		}

		private void Init()
		{
			_connection.Open();

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

			foreach (RoleType item in Enum.GetValues(typeof(RoleType)))
			{
				var item1 = item;
				identityService.Setup(x => x.IsInRole(item1)).Returns(false);			
			}

			identityService.Setup(x => x.IsInRole(_type)).Returns(true);

			identityService.Setup(x => x.Language).Returns(TwoLetterISOLanguageName.English);

			switch(_type)
			{
				case RoleType.Admin:
					identityService.Setup(x => x.Id).Returns(TestConstants.TestAdminUserId);
					break;

				case RoleType.Sender:
					identityService.Setup(x => x.Id).Returns(TestConstants.TestSenderUserId);
					break;

				case RoleType.Broker:
					break;

				case RoleType.Forwarder:
					identityService.Setup(x => x.Id).Returns(TestConstants.TestForwarderUserId1);
					break;

				case RoleType.Client:
					identityService.Setup(x => x.Id).Returns(TestConstants.TestClientUserId);
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}

			Kernel.Rebind<IIdentityService>().ToConstant(identityService.Object).InSingletonScope();
		}

		private void BindServices()
		{
			CompositionRoot.BindDataAccess(Kernel, _mainConnectionString, _filesConnectionString, context => this);

			Kernel.Rebind<IUnitOfWork>().ToConstant(_unitOfWork).InSingletonScope();

			CompositionRoot.BindServices(Kernel, new ConsoleLogger());
		}

		public void Dispose()
		{
			_transactionScope.Dispose();
			_connection.Close();
			Kernel.Dispose();
		}
	}
}
