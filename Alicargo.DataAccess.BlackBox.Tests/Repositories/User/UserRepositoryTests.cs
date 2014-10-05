using Alicargo.DataAccess.BlackBox.Tests.Properties;
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
	public class UserRepositoryTests
	{
		private DbTestContext _context;
		private UserRepository _repository;
		private Fixture _fixture;
		private PasswordConverter _converter;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);
			_fixture = new Fixture();

			_converter = new PasswordConverter();
			_repository = new UserRepository(_converter, new SqlProcedureExecutor(Settings.Default.MainConnectionString));
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod]
		public void TestAdd()
		{
			var language = _fixture.Create<string>().Substring(0, 2);
			var login = _fixture.Create<string>();
			var password = _fixture.Create<string>();

			var id = _repository.Add(login, password, language);

			var userData = _repository.Get(id);
			userData.Language.ShouldBeEquivalentTo(language);
			userData.Name.ShouldBeEquivalentTo(login);
			userData.Id.ShouldBeEquivalentTo(id);

			var passwordData = _repository.GetPasswordData(login);
			passwordData.UserId.ShouldBeEquivalentTo(id);
			passwordData.PasswordHash.ShouldAllBeEquivalentTo(_converter.GetPasswordHash(password, passwordData.PasswordSalt));
		}

		[TestMethod]
		public void TestGet()
		{
			_repository.Get(12).Name.ShouldBeEquivalentTo("CarrierName1");
			_repository.Get(2).Name.ShouldBeEquivalentTo("BrokerName1");
			_repository.Get(1).Name.ShouldBeEquivalentTo("AdminName");
			_repository.Get(3).Name.ShouldBeEquivalentTo("ForwarderName1");
			_repository.Get(4).Name.ShouldBeEquivalentTo("SenderName1");
			_repository.Get(6).Name.ShouldBeEquivalentTo("Nic 1");
			_repository.Get(15).Name.ShouldBeEquivalentTo("Manager1Name");
		}

		[TestMethod]
		public void Test_SetLogin()
		{
			var login1 = _fixture.Create<string>();
			var login2 = _fixture.Create<string>();

			var id = _repository.Add(login1, _fixture.Create<string>(), _fixture.Create<string>().Substring(0, 2));
			var one = _repository.GetPasswordData(login1);

			_repository.SetLogin(id, login2);
			
			var other = _repository.GetPasswordData(login2);
			one.ShouldBeEquivalentTo(other);
		}
	}
}
