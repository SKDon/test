//using System.Transactions;
//using Alicargo.App_Start;
//using Alicargo.Controllers;
//using Alicargo.Core.Enums;
//using Alicargo.Core.Localization;
//using Alicargo.Services.Abstract;
//using Alicargo.Tests.Properties;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using Ninject;

//namespace Alicargo.Tests.Controllers
//{
//	[TestClass]
//	public class ApplicationControllerTests
//	{
//		private TransactionScope _transactionScope;
//		private StandardKernel _kernel;
//		private ApplicationController _controller;

//		[TestInitialize]
//		public void TestInitialize()
//		{
//			_kernel = new StandardKernel();
//			_transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew);

//			BindServices();

//			BindIdentityService();

//			_controller = _kernel.Get<ApplicationController>();
//		}

//		private void BindIdentityService()
//		{
//			var identityService = new Mock<IIdentityService>(MockBehavior.Strict);

//			identityService.Setup(x => x.IsInRole(RoleType.Admin)).Returns(true);
//			identityService.Setup(x => x.IsInRole(RoleType.Client)).Returns(false);
//			identityService.Setup(x => x.TwoLetterISOLanguageName).Returns(TwoLetterISOLanguageName.English);

//			_kernel.Rebind<IIdentityService>().ToConstant(identityService.Object);
//		}

//		private void BindServices()
//		{
//			CompositionRoot.BindServices(_kernel);
//			CompositionRoot.BindDataAccess(_kernel, Settings.Default.MainConnectionString);
//			_kernel.Bind<ApplicationController>().ToSelf();
//		}

//		[TestCleanup]
//		public void TestCleanup()
//		{
//			_transactionScope.Dispose();
//			_kernel.Dispose();
//		}

//		[TestMethod, TestCategory("black-box")]
//		public void Test_List()
//		{
//			var result = _controller.List(10, 0, 1, 10, null);
//		}
//	}
//}
