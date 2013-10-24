using Alicargo.BlackBox.Tests.Properties;
using Alicargo.Controllers;
using Alicargo.TestHelpers;
using Alicargo.ViewModels;
using Alicargo.ViewModels.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ploeh.AutoFixture;

namespace Alicargo.BlackBox.Tests.Controllers
{
	[TestClass]
	public class ClientControllerTests
	{
		private ClientController _controller;
		private CompositionHelper _composition;
		private MockContainer _mock;

		[TestInitialize]
		public void TestInitialize()
		{
			_composition = new CompositionHelper(Settings.Default.MainConnectionString);
			_mock = new MockContainer();
			_controller = _composition.Kernel.Get<ClientController>();
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_composition.Dispose();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Create()
		{
			var password = _mock.Create<string>();

			var authenticationModel = _mock.Build<AuthenticationModel>()
											  .With(x => x.ConfirmPassword, password)
											  .With(x => x.NewPassword, password)
											  .Create();

			var model = _mock.Build<ClientModel>().Create();
			var transitModel = _mock.Build<TransitEditModel>().Create();
			var carrierModel = _mock.Build<CarrierSelectModel>().Create();

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
