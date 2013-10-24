using System.Data.SqlClient;
using Alicargo.BlackBox.Tests.Properties;
using Alicargo.Contracts.Contracts;
using Alicargo.Controllers;
using Alicargo.TestHelpers;
using Alicargo.ViewModels.User;
using Dapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Alicargo.BlackBox.Tests.Controllers
{
	[TestClass]
	public class SenderControllerTests
	{
		private CompositionHelper _composition;
		private SenderController _controller;
		private MockContainer _mock;

		[TestInitialize]
		public void TestInitialize()
		{
			_composition = new CompositionHelper(Settings.Default.MainConnectionString);
			_controller = _composition.Kernel.Get<SenderController>();
			_mock = new MockContainer();
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_composition.Dispose();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Create()
		{
			var model = _mock.Create<SenderModel>();

			_controller.Create(model);

			using (var connection = new SqlConnection(Settings.Default.MainConnectionString))
			{
				var actual = connection.Query<SenderData>("select u.Login, s.Name, s.Email, s.TariffOfTapePerBox from sender s join user u on s.userid = u.id where u.login = @login", new { model.Authentication.Login });

				actual.ShouldBeEquivalentTo(model);
			}
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Edit()
		{
			var model = _mock.Create<SenderModel>();

			_controller.Edit(TestConstants.TestSenderId, model);

			using (var connection = new SqlConnection(Settings.Default.MainConnectionString))
			{
				var actual = connection.Query<SenderData>("select u.Login, s.Name, s.Email, s.TariffOfTapePerBox from sender s join user u on s.userid = u.id where u.login = @login", new { model.Authentication.Login });

				actual.ShouldBeEquivalentTo(model);
			}
		}
	}
}
