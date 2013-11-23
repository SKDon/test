using System;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Services;
using Alicargo.DataAccess.BlackBox.Tests.Helpers;
using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories
{
	[TestClass]
	public class AuthenticationRepositoryTests
	{
		private DbTestContext _context;
		private PasswordConverter _passwordConverter;
		private IAuthenticationRepository _repository;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext();

			_passwordConverter = new PasswordConverter();
			_repository = new AuthenticationRepository(_context.UnitOfWork, _passwordConverter, new SqlProcedureExecutor(Settings.Default.MainConnectionString));
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_AuthenticationRepository_IsInRole()
		{
			var userRepository = new UserRepository(_context.UnitOfWork, _passwordConverter);

			var roleTypes = Enum.GetValues(typeof(RoleType)).Cast<RoleType>().ToArray();

			var users = roleTypes
				.Select(x => new { Role = x, Data = userRepository.GetByRole(x).First() })
				.ToDictionary(x => x.Role, x => x.Data);

			users.Count().ShouldBeEquivalentTo(roleTypes.Count());

			foreach (var user in users)
			{
				_repository.IsInRole(user.Key, user.Value.UserId).ShouldBeEquivalentTo(true);
			}
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_AuthenticationRepository_Add_GetById()
		{
			var login = RandomString();
			var password = RandomString();

			var id = _repository.Add(login, password, TwoLetterISOLanguageName.Russian);
			_context.UnitOfWork.SaveChanges();

			var actual = _repository.GetById(id());

			CheckUser(actual, password, login);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_AuthenticationRepository_GetByLogin()
		{
			var exprected = CreateTestUser();

			var actual = _repository.GetByLogin(exprected.Login);

			Assert.AreEqual(exprected.Id, actual.Id);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_AuthenticationRepository_Update()
		{
			var user = CreateTestUser();

			var newLogin = RandomString();
			var newPassword = RandomString();

			_repository.Update(user.Id, newLogin, newPassword);
			_context.UnitOfWork.SaveChanges();

			var actual = _repository.GetById(user.Id);

			CheckUser(actual, newPassword, newLogin);
		}

		private AuthenticationData CreateTestUser()
		{
			var id = _repository.Add(RandomString(), RandomString(), TwoLetterISOLanguageName.Russian);
			_context.UnitOfWork.SaveChanges();

			return _repository.GetById(id());
		}

		private void CheckUser(AuthenticationData actual, string password, string login)
		{
			Assert.IsNotNull(actual.PasswordSalt);
			var passwordHash = _passwordConverter.GetPasswordHash(password, actual.PasswordSalt.ToArray());
			Assert.AreEqual(login, actual.Login);
			Assert.AreEqual(TwoLetterISOLanguageName.Russian, actual.TwoLetterISOLanguageName);
			Assert.IsNotNull(actual.PasswordHash);
			Assert.IsTrue(passwordHash.SequenceEqual(actual.PasswordHash.ToArray()));
		}

		private static string RandomString()
		{
			return Guid.NewGuid().ToString();
		}

		//[TestMethod, Ignore] // this is not test
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