using Alicargo.DataAccess.Contracts.Contracts.User;

namespace Alicargo.DataAccess.Contracts.Repositories.User
{
	public interface IClientRepository
	{
		long Add(ClientEditData client, long userId, long transitId);
		ClientData Get(long clientId);
		ClientData[] GetAll();
		ClientData GetByUserId(long userId);
		void Update(long clientId, ClientEditData client);
	}
}