using System;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Utilities;

namespace Alicargo.DataAccess.Repositories.User
{
	public sealed class UserRepository : IUserRepository
	{
		private readonly IPasswordConverter _converter;
		private readonly ISqlProcedureExecutor _executor;

		public UserRepository(IPasswordConverter converter, ISqlProcedureExecutor executor)
		{
			_converter = converter;
			_executor = executor;
		}

		public void SetLanguage(long userId, string language)
		{
			_executor.Execute("[dbo].[User_SetLanguage]", new { userId, language });
		}

		public string GetLanguage(long userId)
		{
			return _executor.Query<string>("[dbo].[User_GetLanguage]", new { userId });
		}

		public long? GetUserIdByEmail(string email)
		{
			return _executor.Query<long?>("[dbo].[User_GetUserIdByEmail]", new { email });
		}

		public void SetPassword(long userId, string password)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}

			var salt = _converter.GenerateSalt();
			var hash = _converter.GetPasswordHash(password, salt);

			_executor.Execute("[dbo].[User_SetPassword]", new { userId, salt, hash });
		}

		public PasswordData GetPasswordData(string login)
		{
			return _executor.Query<PasswordData>("[dbo].[User_GetPasswordData]", new { login });
		}
	}
}