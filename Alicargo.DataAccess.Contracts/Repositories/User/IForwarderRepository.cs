using Alicargo.DataAccess.Contracts.Contracts.User;

namespace Alicargo.DataAccess.Contracts.Repositories.User
{
	public interface IForwarderRepository
	{
		long Update(long forwarderId, string name, string login, string email);
		long Add(string name, string login, string email, string twoLetterISOLanguageName);
		UserData[] GetAll();
	}
}