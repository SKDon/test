using System;
using System.Collections.Generic;
using Alicargo.DataAccess.Contracts.Contracts.User;

namespace Alicargo.DataAccess.Contracts.Repositories.User
{
	public interface IClientRepository
	{
		long Count();
		Func<long> Add(ClientData client);
		void Delete(long id);
		void Update(ClientData client);

		string GetLanguage(long clientId);
		ClientData GetByUserId(long userId);
		ClientData Get(long clientId);
		ClientData[] GetRange(int take, long skip);
		ClientData[] GetAll();
		IDictionary<long, string> GetNicByApplications(params long[] appIds);
		long GetUserId(long clientId);
	}
}
