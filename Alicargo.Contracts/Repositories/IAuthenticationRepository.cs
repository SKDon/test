using System;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
{
	public interface IAuthenticationRepository
	{
		bool IsInRole(RoleType role, long userId);
		
		AuthenticationData GetById(long id);
		AuthenticationData GetByLogin(string login);

		Func<long> Add(string login, string password, string twoLetterISOLanguageName);
		void Update(long userId, string newLogin, string newPassword);
		void SetTwoLetterISOLanguageName(long id, string twoLetterISOLanguageName);
		AuthenticationData GetByClientId(long clientId);
	}
}