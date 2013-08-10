using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Core.Contracts;
using Alicargo.Core.Enums;

namespace Alicargo.Core.Repositories
{
	public interface IAuthenticationRepository
	{
		bool IsInRole(RoleType role, long userId); // todo: tests
		
		AuthenticationData GetById(long value);
		AuthenticationData GetByLogin(string login);

		Func<long> Add(string login, string password, string twoLetterISOLanguageName);
		void Update(long userId, string newLogin, string newPassword);
		void SetTwoLetterISOLanguageName(long id, string twoLetterISOLanguageName);
	}
}