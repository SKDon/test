using System;
using Alicargo.Core.Models;

namespace Alicargo.Core.Repositories
{
	public interface IClientRepository
	{
		long Count();
		Client[] GetRange(long skip, int take);
		Func<long> Add(IClientData client);
		Client GetByUserId(long userId);
		Client GetById(long clientId);
		void Delete(long id);
		Client[] GetAll();
		void Update(long id, IClientData client);

		ClientData[] Get(params long[] clinentIds);
	}
}
