using Alicargo.Contracts.Contracts.User;

namespace Alicargo.Contracts.Repositories.User
{
	public interface IAdminRepository
	{
		void Update(long adminId, string name, string login, string email);
		void Add(string name, string login, string email, string language);
		UserData[] GetAll();
	}
}