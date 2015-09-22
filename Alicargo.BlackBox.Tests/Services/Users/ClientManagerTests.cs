using System.Linq;
using Alicargo.BlackBox.Tests.Properties;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Services.Abstract;
using Alicargo.TestHelpers;
using Alicargo.Utilities;
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
		private IClientRepository _clientRepository;
		private CompositionHelper _context;
		private Fixture _fixture;
		private IClientManager _manager;
		private ITransitRepository _transitRepository;
		private IUserRepository _userRepository;

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Dispose();
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new CompositionHelper(Settings.Default.MainConnectionString, Settings.Default.FilesConnectionString);
			_fixture = new Fixture();

			_manager = _context.Kernel.Get<IClientManager>();
			_userRepository = _context.Kernel.Get<IUserRepository>();
			_clientRepository = _context.Kernel.Get<IClientRepository>();
			_transitRepository = _context.Kernel.Get<ITransitRepository>();
		}

		[TestMethod]
		public void Test_Add()
		{
			var clientModel = _fixture.Build<ClientModel>()
				.With(x => x.ContractDate, DateTimeProvider.Now.ToString())
				.With(x => x.DefaultSenderId, TestConstants.TestSenderId)
				.Create();
			var transitEditModel = _fixture.Create<TransitEditModel>();
			transitEditModel.CityId = TestConstants.TestCityId1;

			var clientId = _manager.Add(clientModel, transitEditModel);

			var clientData = _clientRepository.Get(clientId);
			var transitData = _transitRepository.Get(clientData.TransitId).Single();

			clientData.ShouldBeEquivalentTo(
				clientModel,
				options => options.ExcludingMissingProperties()
					.Excluding(x => x.ContractDate)
					.Excluding(x => x.InsuranceRate));
			clientData.ContractDate.ToString().ShouldBeEquivalentTo(clientModel.ContractDate);
			clientData.InsuranceRate.ShouldBeEquivalentTo(clientModel.InsuranceRate / 100);
			transitData.ShouldBeEquivalentTo(transitEditModel, options => options.ExcludingMissingProperties());
		}

		[TestMethod]
		public void Test_Update()
		{
			const long clientId = TestConstants.TestClientId1;
			var clientModel = _fixture.Create<ClientModel>();
			clientModel.ContractDate = DateTimeProvider.Now.ToString();
			var transitEditModel = _fixture.Create<TransitEditModel>();
			transitEditModel.CityId = TestConstants.TestCityId1;

			_manager.Update(clientId, clientModel, transitEditModel);

			var clientData = _clientRepository.Get(clientId);
			var transitData = _transitRepository.Get(clientData.TransitId).Single();
			var passwordData = _userRepository.GetPasswordData(clientModel.Authentication.Login);
			var converter = _context.Kernel.Get<IPasswordConverter>();

			clientData.ShouldBeEquivalentTo(
				clientModel,
				options => options.ExcludingMissingProperties()
					.Excluding(x => x.ContractDate)
					.Excluding(x => x.InsuranceRate));
			clientData.InsuranceRate.ShouldBeEquivalentTo(clientModel.InsuranceRate / 100);
			clientData.ContractDate.ToString().ShouldBeEquivalentTo(clientModel.ContractDate);
			transitData.ShouldBeEquivalentTo(transitEditModel, options => options.ExcludingMissingProperties());
			passwordData.PasswordHash.ShouldAllBeEquivalentTo(
				converter.GetPasswordHash(
					clientModel.Authentication.NewPassword,
					passwordData.PasswordSalt));
		}
	}
}