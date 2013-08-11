using System;
using Alicargo.Contracts.Contracts;

namespace Alicargo.Core.Repositories
{
	public interface IClientRepository
	{
		long Count();
		ClientData[] GetRange(long skip, int take);
		Func<long> Add(ClientData client);
		ClientData GetByUserId(long userId);
		ClientData GetById(long clientId);
		void Delete(long id);
		ClientData[] GetAll();
		void Update(ClientData client);
		ClientData[] Get(params long[] clinentIds);
	}
}
