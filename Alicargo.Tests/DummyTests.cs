using System.Data.Linq;
using System.Net.Http;
using Alicargo.DataAccess.DbContext;
using Alicargo.TestHelpers;
using Alicargo.Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.Tests
{
	[TestClass, Ignore]
	public class DummyTests
	{
		private HttpClient _client;
		private AlicargoDataContext _db;
		private WebTestContext _context;

		[TestInitialize]
		public void TestInitialize()
		{
			_db = new AlicargoDataContext(Settings.Default.MainConnectionString);
			_context = new WebTestContext(Settings.Default.BaseAddress, Settings.Default.AdminLogin, Settings.Default.AdminPassword);
			_client = _context.HttpClient;
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Name()
		{
			_client.PostAsJsonAsync("Url", 1)
				.ContinueWith(task =>
				{
					_db.Refresh(RefreshMode.OverwriteCurrentValues, 1);
					_db.Refresh(RefreshMode.OverwriteCurrentValues, 2);
				})
				.Wait();
		}
	}
}
