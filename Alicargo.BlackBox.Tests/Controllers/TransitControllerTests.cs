using Alicargo.BlackBox.Tests.Properties;
using Alicargo.Controllers;
using Alicargo.DataAccess.DbContext;
using Alicargo.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Alicargo.BlackBox.Tests.Controllers
{
	[TestClass]
	[Ignore]
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
	}
}