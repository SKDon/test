using Alicargo.DataAccess.Contracts.Contracts.User;

namespace Alicargo.DataAccess.Contracts.Repositories.User
{
	public interface IForwarderRepository
	{
		void Update(long id, string name, string login, string email);
		long Add(string name, string login, string password, string email, string language);
		ForwarderData[] GetAll();
		ForwarderData Get(long id);

		long[] GetCities(long forwarderId);
		void SetCities(long forwarderId, long[] cities);
		long[] GetByCity(long cityId);
		long? GetByUserId(long userId);
	}
}