using System;
using System.Collections.Generic;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.User;

namespace Alicargo.Contracts.Repositories
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
	}
}
