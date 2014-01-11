using Alicargo.Contracts.Contracts.User;

namespace Alicargo.Contracts.Repositories.User
{
	public interface IAdminRepository
	{
		long Update(long adminId, string name, string login, string email);
		long Add(string name, string login, string email, string language);
		UserData[] GetAll();
	}
}