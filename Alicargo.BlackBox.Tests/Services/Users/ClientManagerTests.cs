using System.Linq;
using Alicargo.BlackBox.Tests.Properties;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Services.Abstract;
using Alicargo.TestHelpers;
using Alicargo.ViewModels;
using Alicargo.ViewModels.User;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ploeh.AutoFixture;

namespace Alicargo.BlackBox.Tests.Services.Users
{
	[TestClass]
	public class ClientManagerTests
	{
		private CompositionHelper _context;
		private IClientManager _manager;
		private Fixture _fixture;


		[TestCleanup]
		public void TestCleanup()
		{
			_context.Dispose();
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new CompositionHelper(Settings.Default.MainConnectionString, Settings.Default.FilesConnectionString,
				RoleType.Forwarder);
			_fixture = new Fixture();

			_manager = _context.Kernel.Get<IClientManager>();
		}

		[TestMethod]
		public void TestAdd()
		{
			var clientModel = _fixture.Create<ClientModel>();

			var transitEditModel = _fixture.Create<TransitEditModel>();
			transitEditModel.CityId = TestConstants.TestCityId1;
			var userId = _manager.Add(clientModel,
				transitEditModel,
				_fixture.Create<AuthenticationModel>());

			var clientData = _context.Kernel.Get<IClientRepository>().Get(userId);
			var transitData = _context.Kernel.Get<ITransitRepository>().Get(clientData.TransitId).Single();

			clientData.ShouldBeEquivalentTo(clientModel, options => options.ExcludingMissingProperties().Excluding(x=>x.Emails));
			clientData.Emails.ShouldAllBeEquivalentTo(EmailsHelper.SplitAndTrimEmails(clientModel.Emails));
			transitData.ShouldBeEquivalentTo(transitEditModel, options => options.ExcludingMissingProperties());
		}
	}
}
