using System.Linq;
using System.Web.Mvc;
using Alicargo.BlackBox.Tests.Properties;
using Alicargo.Controllers.Application;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.Contracts.Repositories.User;
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
			_context = new CompositionHelper(Settings.Default.MainConnectionString, Settings.Default.FilesConnectionString);
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
			var clientData = _clientRepository.Get(TestConstants.TestClientId1);

			var result = _controller.Create(TestConstants.TestClientId1);

			clientData.Nic.ShouldBeEquivalentTo((string)result.ViewBag.ClientNic);

			((object)result.ViewBag.ApplicationId).Should().BeNull();

			((object)result.ViewBag.Countries).Should().NotBeNull();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Edit_Post()
		{
			var model = _fixture.Create<ApplicationAdminModel>();
			model.SenderId = TestConstants.TestSenderId;
			var transitModel = _fixture.Create<TransitEditModel>();
			transitModel.CityId = TestConstants.TestCityId1;
			var newCarrierName = _fixture.Create<string>();
			var old = _applicationRepository.List(new[] { TestConstants.DefaultStateId },
					new[] { new Order { OrderType = OrderType.Id } }, 1).First();

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
			var clientData = _clientRepository.Get(TestConstants.TestClientId1);
			var model = _fixture.Create<ApplicationAdminModel>();
			model.SenderId = TestConstants.TestSenderId;
			var transitModel = _fixture.Create<TransitEditModel>();
			transitModel.CityId = TestConstants.TestCityId1;
			var newCarrierName = _fixture.Create<string>();

			var result = _controller.Create(clientData.ClientId, model, new CarrierSelectModel
			{
				NewCarrierName = newCarrierName
			}, transitModel);

			Validate(result, clientData, model, transitModel, newCarrierName);
		}

		private void Validate(ActionResult result, ClientData clientData, ApplicationAdminModel model,
			TransitEditModel transitModel, string newCarrierName)
		{
			result.Should().BeOfType<RedirectToRouteResult>();

			var data = _applicationRepository.List(new[]
            {
                TestConstants.DefaultStateId
            }, new[]
            {
                new Order
                {
                    Desc = true,
                    OrderType = OrderType.Id
                }
            }, 1, clientId: clientData.ClientId).First();

			Validate(clientData, model, transitModel, newCarrierName, data);
		}

		private static void Validate(ClientData clientData, ApplicationAdminModel model, TransitEditModel transitModel,
			string newCarrierName, ApplicationListItemData data)
		{
			data.ShouldBeEquivalentTo(model, options => options.ExcludingMissingProperties()
				.Excluding(x => x.FactureCost)
				.Excluding(x => x.TransitCost)
				.Excluding(x => x.PickupCost)
				.Excluding(x => x.ScotchCost));
			data.FactureCost.ShouldBeEquivalentTo(model.FactureCostEdited);
			data.TransitCost.ShouldBeEquivalentTo(model.TransitCostEdited);
			data.PickupCost.ShouldBeEquivalentTo(model.PickupCostEdited);
			data.ScotchCost.ShouldBeEquivalentTo(model.ScotchCostEdited);
			data.CurrencyId.ShouldBeEquivalentTo(model.Currency.CurrencyId);
			data.ClientLegalEntity.ShouldBeEquivalentTo(clientData.LegalEntity);
			data.ClientNic.ShouldBeEquivalentTo(clientData.Nic);
			data.TransitAddress.ShouldBeEquivalentTo(transitModel.Address);
			data.TransitCarrierName.ShouldBeEquivalentTo(newCarrierName);
			data.TransitCity.ShouldBeEquivalentTo("Москва");
			data.TransitDeliveryType.ShouldBeEquivalentTo(transitModel.DeliveryType);
			data.TransitMethodOfTransit.ShouldBeEquivalentTo(transitModel.MethodOfTransit);
			data.TransitPhone.ShouldBeEquivalentTo(transitModel.Phone);
			data.TransitRecipientName.ShouldBeEquivalentTo(transitModel.RecipientName);
			data.TransitWarehouseWorkingTime.ShouldBeEquivalentTo(transitModel.WarehouseWorkingTime);
		}

		//[TestMethod, TestCategory("black-box")]
		//public void Test_SetState()
		//{
		//	var entity = _db.AirWaybills.FirstOrDefault(
		//		x => x.Applications.Count() > 1 && x.Applications.All(y => y.State.Id != DefaultStateId));
		//	if (entity == null)
		//		Assert.Inconclusive("Cant find AirWaybill for test");

		//	var oldStateId = entity.Applications.First().StateId;


		//	_controller.SetState(entity.Id, DefaultStateId);


		//	_db.Refresh(RefreshMode.OverwriteCurrentValues, entity);
		//	foreach (var application in entity.Applications)
		//	{
		//		_db.Refresh(RefreshMode.OverwriteCurrentValues, application);
		//	}

		//	Assert.IsTrue(entity.Applications.All(x => x.StateId == DefaultStateId));

		//	foreach (var application in entity.Applications)
		//	{
		//		application.StateId = oldStateId;
		//	}
		//	entity.StateId = oldStateId;

		//	_db.SubmitChanges();
		//}
	}
}