﻿using System.Linq;
using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.DataAccess.Repositories.User;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories.User
{
	[TestClass]
	public class AdminRepositoryTests
	{
		private DbTestContext _context;
		private Fixture _fixture;
		private IAdminRepository _repository;

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);
			_fixture = new Fixture();
			_repository = new AdminRepository(_context.Connection);
		}

		[TestMethod]
		public void Test_AuthenticationRepository_Add_GetById()
		{
			var login = _fixture.Create<string>();
			var name = _fixture.Create<string>();
			var email = _fixture.Create<string>();

			_repository.Add(name, login, email, TwoLetterISOLanguageName.Russian);

			var actual = _repository.GetAll().Last();

			CheckUser(actual, login, name, email, TwoLetterISOLanguageName.Russian);
		}

		[TestMethod]
		public void Test_AuthenticationRepository_Update()
		{
			var login = _fixture.Create<string>();
			var name = _fixture.Create<string>();
			var email = _fixture.Create<string>();

			_repository.Add(name, login, email, TwoLetterISOLanguageName.Russian);
			var actual = _repository.GetAll().Last();

			login = _fixture.Create<string>();
			name = _fixture.Create<string>();
			email = _fixture.Create<string>();
			_repository.Update(actual.EntityId, name, login, email);

			actual = _repository.GetAll().Last();
			CheckUser(actual, login, name, email, TwoLetterISOLanguageName.Russian);
		}

		private static void CheckUser(UserEntityData actual, string login, string name, string email, string language)
		{
			actual.EntityId.Should().BeGreaterThan(0);
			actual.UserId.Should().BeGreaterThan(0);
			Assert.AreEqual(login, actual.Login);
			Assert.AreEqual(name, actual.Name);
			Assert.AreEqual(email, actual.Email);
			Assert.AreEqual(language, actual.Language);
		}

		// do not remove. this is not test
		//[TestMethod, Ignore] 
		//public void UpdatePasswords()
		//{
		//	_transactionScope.Dispose();

		//	var users = _context.UnitOfWork.Context.GetTable<User>().ToArray();
		//	foreach (var user in users)
		//	{
		//		var salt = _passwordConverter.GenerateSalt();
		//		var hash = _passwordConverter.GetPasswordHash(user.Login, salt);

		//		user.PasswordHash = hash;
		//		user.PasswordSalt = salt;

		//		_context.UnitOfWork.SaveChanges();
		//	}
		//}
	}
}