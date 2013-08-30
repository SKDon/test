using System.Linq;
using System.Transactions;
using Alicargo.App_Start;
using Alicargo.BlackBox.Tests.Properties;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Controllers;
using Alicargo.Services.Abstract;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;

namespace Alicargo.BlackBox.Tests.Controllers
{
    [TestClass]
    public class ApplicationControllerTests
    {
        private TransactionScope _transactionScope;
        private StandardKernel _kernel;
        private ApplicationController _controller;

        [TestInitialize]
        public void TestInitialize()
        {
            _kernel = new StandardKernel();
            _transactionScope = new TransactionScope();

            BindServices();

            BindIdentityService();

            _controller = _kernel.Get<ApplicationController>();
        }

        private void BindIdentityService()
        {
            var identityService = new Mock<IIdentityService>(MockBehavior.Strict);

            identityService.Setup(x => x.IsInRole(RoleType.Admin)).Returns(true);
            identityService.Setup(x => x.IsInRole(RoleType.Client)).Returns(false);
            identityService.Setup(x => x.TwoLetterISOLanguageName).Returns(TwoLetterISOLanguageName.English);

            _kernel.Rebind<IIdentityService>().ToConstant(identityService.Object);
        }

        private void BindServices()
        {
            CompositionRoot.BindServices(_kernel);
            CompositionRoot.BindDataAccess(_kernel, Settings.Default.MainConnectionString);

            _kernel.Bind<ApplicationController>().ToSelf();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _transactionScope.Dispose();
            _kernel.Dispose();
        }

        // todo: 1.5. fix
        [TestMethod, TestCategory("black-box")]
        public void Test_Create_Get()
        {
            var clientRepository = _kernel.Get<IClientRepository>();

            var clientData = clientRepository.Get(TestConstants.TestClientId1).First();

            var result = _controller.Create(TestConstants.TestClientId1);

            clientData.Nic.ShouldBeEquivalentTo((string)result.ViewBag.ClientNic);

            ((object)result.ViewBag.ApplicationId).Should().BeNull();

            ((object)result.ViewBag.Countries).Should().NotBeNull();

        }
    }
}
