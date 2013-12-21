using System;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.User;

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

		public void SetLanguage(long userId, string language)
		{
			_executor.Execute("[dbo].[User_SetLanguage]", new { userId, language });
		}
	}
}