﻿using System.Linq;
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

namespace Alicargo.BlackBox.Tests.Controllers.Application
{
	[TestClass]
	public class ApplicationControllerTests
	{
		private IApplicationRepository _applicationRepository;
		private IClientRepository _clientRepository;
		private CompositionHelper _context;
		private ApplicationController _controller;
		private Fixture _fixture;

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Dispose();
		}

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

		[TestMethod]
		public void Test_Create_Get()
		{
			var clientData = _clientRepository.Get(TestConstants.TestClientId1);

			var result = _controller.Create(TestConstants.TestClientId1);

			clientData.Nic.ShouldBeEquivalentTo((string)result.ViewBag.ClientNic);

			((object)result.ViewBag.ApplicationId).Should().BeNull();

			((object)result.ViewBag.Countries).Should().NotBeNull();
		}

		[TestMethod]
		public void Test_Create_Post()
		{
			var clientData = _clientRepository.Get(TestConstants.TestClientId1);
			var model = _fixture.Create<ApplicationAdminModel>();
			model.SenderId = TestConstants.TestSenderId;
			model.ForwarderId = TestConstants.TestForwarderId1;
			model.CarrierId = null;
			var transitModel = _fixture.Create<TransitEditModel>();
			transitModel.CityId = TestConstants.TestCityId1;

			var result = _controller.Create(clientData.ClientId, model, transitModel);

			Validate(result, clientData, model, transitModel);
		}

		[TestMethod]
		public void Test_Edit_Post()
		{
			var model = _fixture.Create<ApplicationAdminModel>();
			model.SenderId = TestConstants.TestSenderId;
			model.CountryId = TestConstants.TestCountryId;
			model.ForwarderId = TestConstants.TestForwarderId2;
			model.CarrierId = null;
			var transitModel = _fixture.Create<TransitEditModel>();
			transitModel.CityId = TestConstants.TestCityId1;
			var old = _applicationRepository.List(new[] { TestConstants.DefaultStateId },
				new[] { new Order { OrderType = OrderType.Id } },
				1).First();

			var result = _controller.Edit(old.Id, model, transitModel);

			result.Should().BeOfType<RedirectToRouteResult>();
			var data = _applicationRepository.Get(old.Id);

			data.ShouldBeEquivalentTo(
				model,
				options => options.ExcludingMissingProperties()
					.Excluding(x => x.InsuranceRate)
					.Excluding(x => x.CarrierId));
			data.CarrierId.ShouldBeEquivalentTo(TestConstants.TestCarrierId1);
			data.CurrencyId.ShouldBeEquivalentTo(model.Currency.CurrencyId);
			data.InsuranceRate.ShouldBeEquivalentTo(model.InsuranceRate / 100);
		}

		private void Validate(
			ActionResult result, ClientData clientData, ApplicationAdminModel model,
			TransitEditModel transitModel)
		{
			result.Should().BeOfType<RedirectToRouteResult>();

			var data = _applicationRepository.List(new[]
			{
				TestConstants.DefaultStateId
			},
				new[]
				{
					new Order
					{
						Desc = true,
						OrderType = OrderType.Id
					}
				},
				1,
				clientId: clientData.ClientId).First();

			Validate(clientData, model, transitModel, data);
		}

		private static void Validate(
			ClientData clientData, ApplicationAdminModel model, TransitEditModel transitModel,
			ApplicationData data)
		{
			data.ShouldBeEquivalentTo(model,
				options => options.ExcludingMissingProperties()
					.Excluding(x => x.PickupCost)
					.Excluding(x => x.CarrierId)
					.Excluding(x => x.CarrierName)
					.Excluding(x => x.InsuranceRate));
			data.InsuranceRate.ShouldBeEquivalentTo(model.InsuranceRate / 100);
			data.GetAdjustedFactureCost().ShouldBeEquivalentTo(model.FactureCostEdited);
			data.GetAdjustedFactureCostEx().ShouldBeEquivalentTo(model.FactureCostExEdited);
			data.GetAdjustedTransitCost().ShouldBeEquivalentTo(model.TransitCostEdited);
			data.GetAdjustedPickupCost().ShouldBeEquivalentTo(model.PickupCostEdited);
			data.GetAdjustedScotchCost().ShouldBeEquivalentTo(model.ScotchCostEdited);
			data.CurrencyId.ShouldBeEquivalentTo(model.Currency.CurrencyId);
			data.ClientLegalEntity.ShouldBeEquivalentTo(clientData.LegalEntity);
			data.ClientNic.ShouldBeEquivalentTo(clientData.Nic);
			data.TransitAddress.ShouldBeEquivalentTo(transitModel.Address);
			data.TransitCityId.ShouldBeEquivalentTo(TestConstants.TestCityId1);
			data.CarrierId.ShouldBeEquivalentTo(TestConstants.TestCarrierId1);
			data.TransitDeliveryType.ShouldBeEquivalentTo(transitModel.DeliveryType);
			data.TransitMethodOfTransit.ShouldBeEquivalentTo(transitModel.MethodOfTransit);
			data.TransitPhone.ShouldBeEquivalentTo(transitModel.Phone);
			data.TransitRecipientName.ShouldBeEquivalentTo(transitModel.RecipientName);
			data.TransitWarehouseWorkingTime.ShouldBeEquivalentTo(transitModel.WarehouseWorkingTime);
		}
	}
}