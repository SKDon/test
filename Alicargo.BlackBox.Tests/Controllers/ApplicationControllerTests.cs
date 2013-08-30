using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using Alicargo.App_Start;
using Alicargo.BlackBox.Tests.Properties;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Controllers;
using Alicargo.DataAccess.DbContext;
using Alicargo.Services.Abstract;
using Alicargo.TestHelpers;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;
using Ploeh.AutoFixture;

namespace Alicargo.BlackBox.Tests.Controllers
{
    [TestClass]
    public class ApplicationControllerTests
    {
        private IApplicationRepository _applicationRepository;
        private IClientRepository _clientRepository;
        private SqlConnection _connection;
        private ApplicationController _controller;
        private Fixture _fixture;
        private StandardKernel _kernel;
        private TransactionScope _transactionScope;
        private IUnitOfWork _unitOfWork;

        [TestInitialize]
        public void TestInitialize()
        {
            _kernel = new StandardKernel();
            _fixture = new Fixture();

            _connection = new SqlConnection(Settings.Default.MainConnectionString);
            _connection.Open();
            _transactionScope = new TransactionScope();
            _unitOfWork = new UnitOfWork(_connection);

            BindServices();
            BindIdentityService();

            _controller = _kernel.Get<ApplicationController>();
            _clientRepository = _kernel.Get<IClientRepository>();
            _applicationRepository = _kernel.Get<IApplicationRepository>();
        }

        private void BindIdentityService()
        {
            var identityService = new Mock<IIdentityService>(MockBehavior.Strict);

            identityService.Setup(x => x.IsInRole(RoleType.Admin)).Returns(true);
            identityService.Setup(x => x.IsInRole(RoleType.Client)).Returns(false);
            identityService.Setup(x => x.TwoLetterISOLanguageName).Returns(TwoLetterISOLanguageName.English);

            _kernel.Rebind<IIdentityService>().ToConstant(identityService.Object).InSingletonScope();
        }

        private void BindServices()
        {
            CompositionRoot.BindDataAccess(_kernel, null);

            _kernel.Rebind<IUnitOfWork>().ToConstant(_unitOfWork).InSingletonScope();

            CompositionRoot.BindServices(_kernel);

            _kernel.Bind<ApplicationController>().ToSelf();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _transactionScope.Dispose();
            _connection.Close();
            _kernel.Dispose();
        }

        [TestMethod, TestCategory("black-box")]
        public void Test_Create_Get()
        {
            var clientData = _clientRepository.Get(TestConstants.TestClientId1).First();

            var result = _controller.Create(TestConstants.TestClientId1);

            clientData.Nic.ShouldBeEquivalentTo((string)result.ViewBag.ClientNic);

            ((object)result.ViewBag.ApplicationId).Should().BeNull();

            ((object)result.ViewBag.Countries).Should().NotBeNull();
        }

        [TestMethod, TestCategory("black-box")]
        public void Test_Create_Post()
        {
            var clientData = _clientRepository.Get(TestConstants.TestClientId1).First();
            var model = _fixture.Create<ApplicationEditModel>();
            var transitModel = _fixture.Create<TransitEditModel>();
            var newCarrierName = _fixture.Create<string>();

            var result = _controller.Create(clientData.Id, model, new CarrierSelectModel
                {
                    NewCarrierName = newCarrierName
                }, transitModel);

            result.Should().BeOfType<RedirectToRouteResult>();

            var data = _applicationRepository.List(1, 0, new[]
                {
                    TestConstants.DefaultStateId
                }, new[]
                    {
                        new Order
                            {
                                Desc = true,
                                OrderType = OrderType.Id
                            }
                    }, clientData.UserId).First();

            data.ShouldBeEquivalentTo(model, options => options.ExcludingMissingProperties());
            data.CurrencyId.ShouldBeEquivalentTo(model.Currency.CurrencyId);
            data.ClientLegalEntity.ShouldBeEquivalentTo(clientData.LegalEntity);
            data.ClientNic.ShouldBeEquivalentTo(clientData.Nic);
            data.TransitAddress.ShouldBeEquivalentTo(transitModel.Address);
            data.TransitCarrierName.ShouldBeEquivalentTo(newCarrierName);
            data.TransitCity.ShouldBeEquivalentTo(transitModel.City);
            data.TransitDeliveryTypeId.ShouldBeEquivalentTo((int)transitModel.DeliveryType);
            data.TransitMethodOfTransitId.ShouldBeEquivalentTo((int)transitModel.MethodOfTransit);
            data.TransitPhone.ShouldBeEquivalentTo(transitModel.Phone);
            data.TransitRecipientName.ShouldBeEquivalentTo(transitModel.RecipientName);
            data.TransitWarehouseWorkingTime.ShouldBeEquivalentTo(transitModel.WarehouseWorkingTime);
        }
    }
}