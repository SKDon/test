using System.Linq;
using Alicargo.Core.Contracts;
using Alicargo.Core.Localization;
using Alicargo.DataAccess.DbContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alicargo.DataAccess.Tests.Repositories
{
	public partial class Tests
	{
		private AuthenticationData CreateTestUser()
		{
			var id = _authenticationRepository.Add(RandomString(), RandomString(), TwoLetterISOLanguageName.Russian);
			_unitOfWork.SaveChanges();

			return _authenticationRepository.GetById(id());
		}

		[TestMethod]
		public void Test_UserRepository_AddUser_GetById()
		{
			var login = RandomString();
			var password = RandomString();

			var id = _authenticationRepository.Add(login, password, TwoLetterISOLanguageName.Russian);
			_unitOfWork.SaveChanges();

			var actual = _authenticationRepository.GetById(id());

			CheckUser(actual, password, login);
		}

		private void CheckUser(IAuthenticationData actual, string password, string login)
		{
			Assert.IsNotNull(actual.PasswordSalt);
			var passwordHash = _passwordConverter.GetPasswordHash(password, actual.PasswordSalt.ToArray());
			Assert.AreEqual(login, actual.Login);
			Assert.AreEqual(TwoLetterISOLanguageName.Russian, actual.TwoLetterISOLanguageName);
			Assert.IsNotNull(actual.PasswordHash);
			Assert.IsTrue(passwordHash.SequenceEqual(actual.PasswordHash.ToArray()));
		}

		[TestMethod]
		public void Test_UserRepository_GetByLogin()
		{
			var exprected = CreateTestUser();

			var actual = _authenticationRepository.GetByLogin(exprected.Login);

			Assert.AreEqual(exprected.Id, actual.Id);
		}

		[TestMethod]
		public void Test_UserRepository_Update()
		{
			var user = CreateTestUser();

			var newLogin = RandomString();
			var newPassword = RandomString();

			_authenticationRepository.Update(user.Id, newLogin, newPassword);
			_unitOfWork.SaveChanges();

			var actual = _authenticationRepository.GetById(user.Id);

			CheckUser(actual, newPassword, newLogin);
		}

		[TestMethod, Ignore]
		public void UpdatePasswords()
		{
			_transactionScope.Dispose();

			var users = _unitOfWork.Context.GetTable<User>().ToArray();
			foreach (var user in users)
			{
				var salt = _passwordConverter.GenerateSalt();
				var hash = _passwordConverter.GetPasswordHash(user.Login, salt);

				user.PasswordHash = hash;
				user.PasswordSalt = salt;

				_unitOfWork.SaveChanges();
			}
		}
	}
}
