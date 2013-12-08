using System.Linq;
using Alicargo.BlackBox.Tests.Properties;
using Alicargo.Controllers;
using Alicargo.DataAccess.DbContext;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Alicargo.BlackBox.Tests.Controllers
{
	[TestClass]
	public class TransitControllerTests
	{
		private AlicargoDataContext _db;
		private CompositionHelper _composition;
		private TransitController _controller;

		[TestInitialize]
		public void TestInitialize()
		{
			_db = new AlicargoDataContext(Settings.Default.MainConnectionString);
			_composition = new CompositionHelper(Settings.Default.MainConnectionString, Settings.Default.FilesConnectionString);
			_controller = _composition.Kernel.Get<TransitController>();
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_composition.Dispose();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Edit_Get()
		{
			var data = _db.Transits.First(x => x.Applications.Any());

			var model = _controller.Edit(data.Id).Model;

			data.ShouldBeEquivalentTo(model, options => options.ExcludingMissingProperties());
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
