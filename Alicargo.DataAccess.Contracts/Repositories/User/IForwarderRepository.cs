using Alicargo.DataAccess.Contracts.Contracts.User;

namespace Alicargo.DataAccess.Contracts.Repositories.User
{
	public interface IForwarderRepository
	{
		void Update(long id, string name, string login, string email, long cityId);
		long Add(string name, string login, string password, string email, string language, long cityId);
		ForwarderData[] GetAll();
		ForwarderData Get(long id);
	}
}