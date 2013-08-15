using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using Alicargo.App_Start;
using Alicargo.Contracts.Enums;
using Alicargo.Controllers;
using Alicargo.DataAccess.DbContext;
using Alicargo.Services.Abstract;
using Alicargo.TestHelpers;
using Alicargo.Tests.Properties;
using Alicargo.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;
using Ploeh.AutoFixture;

namespace Alicargo.Tests.Controllers
{
	[TestClass]
	public class ClientControllerTests
	{
		private WebTestContext _context;
		private AlicargoDataContext _db;

		private StandardKernel _kernel;
		private ClientController _controller;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new WebTestContext(Settings.Default.BaseAddress, Settings.Default.AdminLogin, Settings.Default.AdminPassword);
			_db = new AlicargoDataContext(Settings.Default.MainConnectionString);

			_kernel = new StandardKernel();

			BindServices();

			BindIdentityService();

			_controller = _kernel.Get<ClientController>();
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

		[TestMethod, TestCategory("black-box")]
		public void Test_Create()
		{
			var password = _context.Create<string>();

			var authenticationModel = _context.Build<AuthenticationModel>()
											  .With(x => x.ConfirmPassword, password)
											  .With(x => x.NewPassword, password)
											  .Create();

			var model = _context.Build<ClientModel>().Create();
			var transitModel = _context.Build<TransitEditModel>().Create();
			var carrierModel = _context.Build<CarrierSelectModel>().Create();

			var result = _controller.Create(model, transitModel, carrierModel, authenticationModel);

			//_context.HttpClient.PostAsJsonAsync("Client/Create", new { model, transitModel, carrierModel, authenticationModel })
			//	.ContinueWith(task =>
			//	{
			//		Assert.AreEqual(HttpStatusCode.OK, task.Result.StatusCode);

			//		var clientData = _db.Clients.First(x => x.LegalEntity == model.LegalEntity);
			//		var carrierData = _db.Carriers.First(x => x.Name == carrierModel.NewCarrierName);
			//		var transitData = clientData.Transit;
			//		var userData = clientData.User;

			//		//var expectedClient = new ClientData();
			//		//var actualClient = new ClientData();
			//		//model.Id = clientData.Id;
			//		//model.UserId = clientData.UserId;
			//		//model.TransitId = clientData.TransitId;
			//		//model.CopyTo(expectedClient);
			//		//clientData.CopyTo(actualClient);

			//		//var actualTransit = new TransitEditModel
			//		//{
			//		//	Address = transitData.Address,
			//		//	City = transitData.City,
			//		//	DeliveryType = (DeliveryType)transitData.DeliveryTypeId,
			//		//	MethodOfTransit = (MethodOfTransit)transitData.MethodOfTransitId,
			//		//	Phone = transitData.Phone,
			//		//	RecipientName = transitData.RecipientName,
			//		//	WarehouseWorkingTime = transitData.WarehouseWorkingTime
			//		//};

			//		_db.Clients.DeleteOnSubmit(clientData);
			//		_db.Users.DeleteOnSubmit(userData);
			//		_db.Transits.DeleteOnSubmit(transitData);
			//		_db.Carriers.DeleteOnSubmit(carrierData);
			//		_db.SubmitChanges();

			//		//expectedClient.ShouldBeEquivalentTo(actualClient);
			//		//model.Transit.ShouldBeEquivalentTo(actualTransit);
			//		//Assert.AreEqual(model.AuthenticationModel.Login, userData.Login);
			//	})
			//	.Wait();
		}
	}
}
