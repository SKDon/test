using Alicargo.Areas.Admin.Controllers;
using Alicargo.Areas.Admin.Models;
using Alicargo.BlackBox.Tests.Properties;
using Alicargo.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ploeh.AutoFixture;

namespace Alicargo.BlackBox.Tests.Areas.Admin.Controllers
{
	[TestClass]
	public class BillControllerTests
	{
		private CompositionHelper _composition;
		private BillController _controller;
		private Fixture _fixture;

		[TestCleanup]
		public void TestCleanup()
		{
			_composition.Dispose();
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_fixture = new Fixture();
			_composition = new CompositionHelper(Settings.Default.MainConnectionString, Settings.Default.FilesConnectionString);
			_controller = _composition.Kernel.Get<BillController>();
		}

		[TestMethod]
		public void Test_NewPreview()
		{
			var viewResult = _controller.Preview(TestConstants.TestApplicationId);

			var model = (BillModel)viewResult.Model;
		}
	}
}