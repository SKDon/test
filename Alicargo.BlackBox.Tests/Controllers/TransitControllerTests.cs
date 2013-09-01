using System.Linq;
using System.Net;
using System.Net.Http;
using Alicargo.BlackBox.Tests.Properties;
using Alicargo.DataAccess.DbContext;
using Alicargo.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.BlackBox.Tests.Controllers
{
	[TestClass]
	public class TransitControllerTests
	{
		private HttpClient _client;
		private AlicargoDataContext _db;
		private WebTestContext _context;

		[TestInitialize]
		public void TestInitialize()
		{
			_db = new AlicargoDataContext(Settings.Default.MainConnectionString);
			_context = new WebTestContext(Settings.Default.BaseAddress, Settings.Default.ClientLogin, Settings.Default.ClientPassword);
			_client = _context.HttpClient;
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Edit_Get()
		{
			var first = _db.Transits.First(x => x.Applications.Any());

			_client.GetAsync("Transit/Edit/" + first.Id)
				   .ContinueWith(task => Assert.AreEqual(HttpStatusCode.OK, task.Result.StatusCode))
				   .Wait();
		}

		//[TestMethod, TestCategory("black-box")]
		//public void Test_Edit_Post()
		//{
		//	var first = _db.Transits.First(x => x.Applications.Any());

		//	_client.PostAsJsonAsync("Transit/Edit", first)
		//		   .ContinueWith(task => Assert.AreEqual(HttpStatusCode.OK, task.Result.StatusCode))
		//		   .Wait();
		//}
	}
}
