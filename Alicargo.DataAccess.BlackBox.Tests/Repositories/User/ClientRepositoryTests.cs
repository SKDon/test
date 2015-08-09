using System.Linq;
using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories.User;
using Alicargo.TestHelpers;
using Alicargo.Utilities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories.User
{
	[TestClass]
	public class ClientRepositoryTests
	{
		private ClientRepository _clientRepository;
		private DbTestContext _context;
		private Fixture _fixture;
		private UserRepository _userRepository;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);
			_fixture = new Fixture();

			var executor = new SqlProcedureExecutor(Settings.Default.MainConnectionString);
			_userRepository = new UserRepository(new PasswordConverter(), executor);
			_clientRepository = new ClientRepository(executor);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		private ClientData CreateTestClient()
		{
			var db = new AlicargoDataContext(_context.Connection);

			var user = _fixture.Build<DbContext.User>()
				.Without(x => x.Admins)
				.Without(x => x.Clients)
				.Without(x => x.Brokers)
				.Without(x => x.Forwarders)
				.Without(x => x.Carriers)
				.Without(x => x.Senders)
				.With(x => x.TwoLetterISOLanguageName, TwoLetterISOLanguageName.Russian)
				.Create();
			db.Users.InsertOnSubmit(user);
			db.SubmitChanges();

			var client = _fixture.Build<Client>()
				.Without(x => x.Applications)
				.Without(x => x.Calculations)
				.Without(x => x.Transit)
				.With(x => x.TransitId, TestConstants.TestTransitId)
				.With(x => x.User, user)
				.Create();
			db.Clients.InsertOnSubmit(client);
			db.SubmitChanges();

			return new ClientData
			{
				Emails = client.Emails,
				LegalEntity = client.LegalEntity,
				BIC = client.BIC,
				Nic = client.Nic,
				Contacts = client.Contacts,
				Phone = client.Phone,
				INN = client.INN,
				KPP = client.KPP,
				OGRN = client.OGRN,
				Bank = client.Bank,
				LegalAddress = client.LegalAddress,
				MailingAddress = client.MailingAddress,
				RS = client.RS,
				KS = client.KS,
				TransitId = client.TransitId,
				ClientId = client.Id,
				Login = user.Login,
				Language = user.TwoLetterISOLanguageName,
				UserId = user.Id,
				ContractDate = client.ContractDate,
				ContractNumber = client.ContractNumber,
				FactureCost = client.FactureCost,
				FactureCostEx = client.FactureCostEx,
				InsuranceRate = client.InsuranceRate,
				PickupCost = client.PickupCost,
				TransitCost = client.TransitCost
			};
		}

		[TestMethod]
		public void Test_ClientRepository_GetAll()
		{
			var all1 = _clientRepository.GetAll();
			var expected = CreateTestClient();
			var all2 = _clientRepository.GetAll();
			var actual = all2.Single(x => x.ClientId == expected.ClientId);

			all1.Should().NotContain(expected);
			actual.ShouldBeEquivalentTo(expected);
			all1.Length.ShouldBeEquivalentTo(all2.Length - 1);
		}

		[TestMethod]
		public void Test_ClientRepository_Add_GetByUserId_GetById()
		{
			var client = _fixture.Create<ClientEditData>();
			var userId = _userRepository.Add(_fixture.Create<string>(), _fixture.Create<string>(), TwoLetterISOLanguageName.English);

			var clientId = _clientRepository.Add(client, userId, TestConstants.TestTransitId);

			var byId = _clientRepository.Get(clientId);

			client.ShouldBeEquivalentTo(byId);

			var byUserId = _clientRepository.GetByUserId(userId);

			Assert.IsNotNull(byUserId);

			client.ShouldBeEquivalentTo(byUserId);
		}

		[TestMethod]
		public void Test_ClientRepository_Update()
		{
			var client = CreateTestClient();
			var newData = _fixture.Create<ClientEditData>();

			_clientRepository.Update(client.ClientId, newData);

			var byId = _clientRepository.Get(client.ClientId);

			newData.ShouldBeEquivalentTo(byId);
		}
	}
}