using System.Linq;
using System.Data.SqlClient;
using Alicargo.BlackBox.Tests.Properties;
using Alicargo.Contracts.Enums;
using Alicargo.Core.Enums;
using Alicargo.Services.Application;
using Alicargo.TestHelpers;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Alicargo.BlackBox.Tests.Services.Application
{
	[TestClass]
	public class ApplicationListPresenterTests
	{
		private CompositionHelper _context;
		private ApplicationListPresenter _presenter;


		[TestInitialize]
		public void TestInitialize()
		{
			_context = new CompositionHelper(Settings.Default.MainConnectionString, RoleType.Forwarder);
			_context.Kernel.Bind<ApplicationListPresenter>().ToSelf();

			_presenter = _context.Kernel.Get<ApplicationListPresenter>();
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Dispose();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_FilterByCargoReceivedDaysShow()
		{
			var application = _presenter.List(isForwarder: true).Data.First(x => x.StateId != TestConstants.CargoReceivedStateId);

			using (var connection = new SqlConnection(Settings.Default.MainConnectionString))
			{
				connection.Execute("update [dbo].[Application] set [StateId] = @StateId, [StateChangeTimestamp] = GETUTCDATE() where [Id] = @Id",
					new { StateId = TestConstants.CargoReceivedStateId, application.Id });
			}

			Assert.IsTrue(_presenter.List(isForwarder: true).Data.Any(x => x.Id == application.Id && x.StateId == TestConstants.CargoReceivedStateId));

			using (var connection = new SqlConnection(Settings.Default.MainConnectionString))
			{
				connection.Execute("update [dbo].[Application] set [StateChangeTimestamp] = GETUTCDATE() - 20 where [Id] = @Id",
					new { application.Id });
			}

			Assert.IsFalse(_presenter.List(isForwarder: true).Data.Any(x => x.Id == application.Id));
		}
	}
}
