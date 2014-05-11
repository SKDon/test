using System;
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

			_repository.GetLanguage(id).ShouldBeEquivalentTo(language);
			var passwordData = _repository.GetPasswordData(login);

			passwordData.UserId.ShouldBeEquivalentTo(id);
			passwordData.PasswordHash.ShouldAllBeEquivalentTo(_converter.GetPasswordHash(password, passwordData.PasswordSalt));
		}
	}
}
