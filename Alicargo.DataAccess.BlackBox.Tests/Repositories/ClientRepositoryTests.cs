using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Helpers;
using Alicargo.DataAccess.BlackBox.Tests.Helpers;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories
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
			_context = new DbTestContext();
			_fixture = new Fixture();

			_clientRepository = new ClientRepository(_context.UnitOfWork);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		private ClientData CreateTestClient()
		{
			var db = (AlicargoDataContext)_context.UnitOfWork.Context;

			var carrier = _fixture.Build<Carrier>()
				.Without(x => x.Transits)
				.Create();
			db.Carriers.InsertOnSubmit(carrier);
			db.SubmitChanges();

			var transit = _fixture.Build<Transit>()
				.Without(x => x.Applications)
				.Without(x => x.Clients)
				.With(x => x.Carrier, carrier)
				.Create();
			db.Transits.InsertOnSubmit(transit);
			db.SubmitChanges();

			var user = _fixture.Build<User>()
				.Without(x => x.Admins)
				.Without(x => x.Clients)
				.Without(x => x.Brokers)
				.Without(x => x.Forwarders)
				.Without(x => x.Senders)
				.With(x => x.TwoLetterISOLanguageName, "ru")
				.Create();
			db.Users.InsertOnSubmit(user);
			db.SubmitChanges();

			var client = _fixture.Build<Client>()
				.Without(x => x.Applications)
				.Without(x => x.Calculations)
				.With(x => x.Transit, transit)
				.With(x => x.User, user)
				.Create();
			db.Clients.InsertOnSubmit(client);
			db.SubmitChanges();

			return new ClientData
			{
				Emails = EmailsHelper.SplitEmails(client.Emails),
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
				UserId = client.UserId,
				Id = client.Id
			};
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_ClientRepository_Count()
		{
			var all = _clientRepository.GetAll();

			var count = _clientRepository.Count();

			Assert.AreEqual(all.Length, count);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_ClientRepository_GetRange()
		{
			var count = (int)_clientRepository.Count();

			var take = count - count / 2;

			var range = _clientRepository.GetRange(take, 0);

			Assert.AreEqual(range.Length, take);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_ClientRepository_Add_GetByUserId_GetById_Delete()
		{
			var client = CreateTestClient();

			var byId = _clientRepository.Get(client.Id);

			Assert.IsNotNull(byId);

			client.ShouldBeEquivalentTo(byId);

			var byUserId = _clientRepository.GetByUserId(client.UserId);

			Assert.IsNotNull(byUserId);

			client.ShouldBeEquivalentTo(byUserId);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_ClientRepository_Delete()
		{
			var client = CreateTestClient();

			_clientRepository.Delete(client.Id);

			_context.UnitOfWork.SaveChanges();

			var byId = _clientRepository.Get(client.Id);

			Assert.IsNull(byId);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_ClientRepository_Update()
		{
			var client = CreateTestClient();
			var newData = _fixture.Create<ClientData>();
			newData.Id = client.Id;
			newData.UserId = client.UserId;
			newData.TransitId = client.TransitId;

			_clientRepository.Update(newData);

			_context.UnitOfWork.SaveChanges();

			var byId = _clientRepository.Get(client.Id);

			newData.ShouldBeEquivalentTo(byId);
		}
	}
}