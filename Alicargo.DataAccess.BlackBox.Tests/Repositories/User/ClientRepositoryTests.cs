using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories.User;
using Alicargo.TestHelpers;
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

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);
			_fixture = new Fixture();

			_clientRepository = new ClientRepository(_context.Connection);
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
				Emails = EmailsHelper.SplitAndTrimEmails(client.Emails),
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
				Language = user.TwoLetterISOLanguageName
			};
		}

		[TestMethod]
		public void Test_ClientRepository_Count()
		{
			var all = _clientRepository.GetAll();

			var count = _clientRepository.Count();

			Assert.AreEqual(all.Length, count);
		}

		[TestMethod]
		public void Test_ClientRepository_GetRange()
		{
			var count = (int)_clientRepository.Count();

			var take = count - count / 2;

			var range = _clientRepository.GetRange(take, 0);

			Assert.AreEqual(range.Length, take);
		}

		[TestMethod]
		public void Test_ClientRepository_Add_GetByUserId_GetById_Delete()
		{
			var client = _fixture.Create<ClientData>();
			client.TransitId = TestConstants.TestTransitId;
			client.Language = TwoLetterISOLanguageName.English;
			var clientId = _clientRepository.Add(client);
			client.ClientId = clientId;

			var byId = _clientRepository.Get(client.ClientId);

			Assert.IsNotNull(byId);

			client.ShouldBeEquivalentTo(byId);

			var userId = _clientRepository.GetUserId(client.ClientId);

			var byUserId = _clientRepository.GetByUserId(userId);

			Assert.IsNotNull(byUserId);

			client.ShouldBeEquivalentTo(byUserId);
		}

		[TestMethod]
		public void Test_ClientRepository_Delete()
		{
			var client = CreateTestClient();

			_clientRepository.Delete(client.ClientId);

			var byId = _clientRepository.Get(client.ClientId);

			Assert.IsNull(byId);
		}

		[TestMethod]
		public void Test_ClientRepository_Update()
		{
			var client = CreateTestClient();
			var newData = _fixture.Create<ClientData>();
			newData.ClientId = client.ClientId;
			newData.TransitId = client.TransitId;
			newData.Language = TwoLetterISOLanguageName.Italian;

			_clientRepository.Update(newData);

			var byId = _clientRepository.Get(client.ClientId);

			newData.ShouldBeEquivalentTo(byId);
		}
	}
}