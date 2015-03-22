using Alicargo.DataAccess.Contracts.Contracts.User;

namespace Alicargo.DataAccess.Contracts.Repositories.User
{
	public interface IManagerRepository
	{
		long Update(long managerId, string name, string login, string email);
		long Add(string name, string login, string email, string language);
		UserEntityData[] GetAll();
	}
}