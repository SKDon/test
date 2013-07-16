using System.Linq;
using System.Net;
using System.Net.Http;
using Alicargo.Core.Contracts;
using Alicargo.Core.Helpers;
using Alicargo.Core.Models;
using Alicargo.DataAccess.DbContext;
using Alicargo.Tests.Properties;
using Alicargo.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.Tests.Controllers
{
	[TestClass]
	public class ClientControllerTests
	{
		private WebTestContext _context;
		private AlicargoDataContext _db;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new WebTestContext();
			_db = new AlicargoDataContext(Settings.Default.MainConnectionString);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Create()
		{
			var password = _context.Create<string>();

			var model = _context.Build<Core.Models.Client>()
				.With(x => x.AuthenticationModel, _context.Build<AuthenticationModel>()
					.With(x => x.ConfirmPassword, password)
					.With(x => x.NewPassword, password)
					.Create())
				.Create();

			var carrierSelectModel = _context.Build<CarrierSelectModel>()
				.With(x => x.Carriers, null)
				.Create();

			_context.HttpClient.PostAsJsonAsync("Client/Create", new { model, carrierSelectModel })
				.ContinueWith(task =>
				{
					Assert.AreEqual(HttpStatusCode.OK, task.Result.StatusCode);

					var clientData = _db.Clients.First(x => x.LegalEntity == model.LegalEntity);
					var carrierData = _db.Carriers.First(x => x.Name == carrierSelectModel.NewCarrierName);
					var transitData = clientData.Transit;
					var userData = clientData.User;

					var expectedClient = new ClientData();
					var actualClient = new ClientData();
					model.Id = clientData.Id;
					model.UserId = clientData.UserId;
					model.TransitId = clientData.TransitId;
					model.CopyTo(expectedClient);
					clientData.CopyTo(actualClient);

					var actualTransit = new Core.Models.Transit
					{
						Address = transitData.Address,
						Id = transitData.Id,
						CarrierName = transitData.Carrier.Name,
						CarrierId = transitData.CarrierId,
						City = transitData.City,
						DeliveryTypeId = transitData.DeliveryTypeId,
						MethodOfTransitId = transitData.MethodOfTransitId,
						Phone = transitData.Phone,
						RecipientName = transitData.RecipientName,
						WarehouseWorkingTime = transitData.WarehouseWorkingTime						
					};

					model.Transit.Id = actualTransit.Id;
					model.Transit.CarrierName = actualTransit.CarrierName;
					model.Transit.CarrierId = actualTransit.CarrierId;
					
					_db.Clients.DeleteOnSubmit(clientData);
					_db.Users.DeleteOnSubmit(userData);
					_db.Transits.DeleteOnSubmit(transitData);
					_db.Carriers.DeleteOnSubmit(carrierData);
					_db.SubmitChanges();

					_context.AreEquals(expectedClient, actualClient);
					_context.AreEquals(model.Transit, actualTransit);
					Assert.AreEqual(model.AuthenticationModel.Login, userData.Login);
				})
				.Wait();
		}
	}
}
