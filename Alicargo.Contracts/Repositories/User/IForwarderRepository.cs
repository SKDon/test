using Alicargo.Contracts.Contracts.User;

namespace Alicargo.Contracts.Repositories.User
{
	public interface IForwarderRepository
	{
		void Update(long forwarderId, string name, string login, string email);
		void Add(string name, string login, string email, string twoLetterISOLanguageName);
		UserData[] GetAll();
	}
}