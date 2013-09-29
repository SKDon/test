using System.Linq;
using System.Web.Mvc;
using Alicargo.BlackBox.Tests.Properties;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Controllers;
using Alicargo.TestHelpers;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ploeh.AutoFixture;

namespace Alicargo.BlackBox.Tests.Controllers
{
	[TestClass]
	public class ApplicationControllerTests
	{
		private IApplicationRepository _applicationRepository;
		private IClientRepository _clientRepository;
		private CompositionHelper _context;
		private ApplicationController _controller;
		private Fixture _fixture;


		[TestInitialize]
		public void TestInitialize()
		{
			_fixture = new Fixture();
			_context = new CompositionHelper(Settings.Default.MainConnectionString);
			_context.Kernel.Bind<ApplicationController>().ToSelf();

			_controller = _context.Kernel.Get<ApplicationController>();
			_clientRepository = _context.Kernel.Get<IClientRepository>();
			_applicationRepository = _context.Kernel.Get<IApplicationRepository>();
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Dispose();
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
		public void Test_Edit_Post()
		{
			var model = _fixture.Create<ApplicationAdminModel>();
			var transitModel = _fixture.Create<TransitEditModel>();
			var newCarrierName = _fixture.Create<string>();
			var old = _applicationRepository.List(1).First();

			var result = _controller.Edit(old.Id, model, new CarrierSelectModel
			{
				NewCarrierName = newCarrierName
			}, transitModel);

			result.Should().BeOfType<RedirectToRouteResult>();
			var data = _applicationRepository.Get(old.Id);
			data.ShouldBeEquivalentTo(model, options => options.ExcludingMissingProperties());
			data.CurrencyId.ShouldBeEquivalentTo(model.Currency.CurrencyId);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Create_Post()
		{
			var clientData = _clientRepository.Get(TestConstants.TestClientId1).First();
			var model = _fixture.Create<ApplicationAdminModel>();
			var transitModel = _fixture.Create<TransitEditModel>();
			var newCarrierName = _fixture.Create<string>();

			var result = _controller.Create(clientData.Id, model, new CarrierSelectModel
			{
				NewCarrierName = newCarrierName
			}, transitModel);

			Validate(result, clientData, model, transitModel, newCarrierName);
		}

		private void Validate(ActionResult result, ClientData clientData, ApplicationAdminModel model,
							  TransitEditModel transitModel, string newCarrierName)
		{
			result.Should().BeOfType<RedirectToRouteResult>();

			var data = _applicationRepository.List(1,
												   stateIds: new[]
												   {
													   TestConstants.DefaultStateId
												   },
												   orders: new[]
												   {
													   new Order
													   {
														   Desc = true,
														   OrderType = OrderType.Id
													   }
												   }, clientId: clientData.Id).First();

			Validate(clientData, model, transitModel, newCarrierName, data);
		}

		private static void Validate(ClientData clientData, ApplicationAdminModel model, TransitEditModel transitModel,
									 string newCarrierName, ApplicationListItemData data)
		{
			data.ShouldBeEquivalentTo(model, options => options.ExcludingMissingProperties()
															   .Excluding(x => x.FactureCost)
															   .Excluding(x => x.WithdrawCost)
															   .Excluding(x => x.ScotchCost));
			data.FactureCost.ShouldBeEquivalentTo(model.FactureCostEdited);
			data.WithdrawCost.ShouldBeEquivalentTo(model.WithdrawCostEdited);
			data.ScotchCost.ShouldBeEquivalentTo(model.ScotchCostEdited);
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