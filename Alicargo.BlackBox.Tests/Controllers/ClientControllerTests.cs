using System.Linq;
using Alicargo.BlackBox.Tests.Properties;
using Alicargo.Controllers.User;
using Alicargo.DataAccess.DbContext;
using Alicargo.TestHelpers;
using Alicargo.ViewModels;
using Alicargo.ViewModels.User;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ploeh.AutoFixture;

namespace Alicargo.BlackBox.Tests.Controllers
{
	[TestClass]
	public class ClientControllerTests
	{
		private CompositionHelper _composition;
		private ClientController _controller;
		private MockContainer _mock;

		[TestInitialize]
		public void TestInitialize()
		{
			_composition = new CompositionHelper(Settings.Default.MainConnectionString, Settings.Default.FilesConnectionString);
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
			var authentication = _mock.Build<AuthenticationModel>()
				.With(x => x.ConfirmPassword, password)
				.With(x => x.NewPassword, password)
				.Create();
			var model = _mock.Build<ClientModel>().Create();
			var transit = _mock.Build<TransitEditModel>().Create();
			var carrier = _mock.Build<CarrierSelectModel>().Create();

			_controller.Create(model, transit, carrier, authentication);

			var context = new AlicargoDataContext(Settings.Default.MainConnectionString);
			var client = context.Clients.First(x => x.User.Login == authentication.Login);

			client.User.PasswordHash.Should().NotBe(new byte[0]);
			client.User.PasswordSalt.Should().NotBe(new byte[0]);
			client.ShouldBeEquivalentTo(model, options => options.ExcludingMissingProperties());
			client.Transit.ShouldBeEquivalentTo(transit, options => options.ExcludingMissingProperties());
			client.Transit.ShouldBeEquivalentTo(carrier, options => options.ExcludingMissingProperties().Excluding(x => x.CarrierId));
		}
	}
}