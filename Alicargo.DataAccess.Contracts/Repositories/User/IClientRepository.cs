using System.Collections.Generic;
using Alicargo.DataAccess.Contracts.Contracts.User;

namespace Alicargo.DataAccess.Contracts.Repositories.User
{
	public interface IClientRepository
	{
		long Add(ClientData client);
		long Count();
		void Delete(long id);
		ClientData Get(long clientId);
		ClientData[] GetAll();
		ClientData GetByUserId(long userId);
		string GetLanguage(long clientId);
		IDictionary<long, string> GetNicByApplications(params long[] appIds);
		ClientData[] GetRange(int take, long skip);
		long GetUserId(long clientId);
		void Update(ClientData client);
	}
}