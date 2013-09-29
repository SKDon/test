using System;
using System.Collections.Generic;
using Alicargo.Contracts.Contracts;

namespace Alicargo.Contracts.Repositories
{
	public interface IClientRepository
	{
		long Count();
		Func<long> Add(ClientData client);
		void Delete(long id);
		void Update(ClientData client);

		ClientData GetByUserId(long userId);
		ClientData GetById(long clientId);
		ClientData[] GetRange(int take, long skip);
		ClientData[] GetAll();
		ClientData[] Get(params long[] clinentIds);
		IDictionary<long, string> GetNicByApplications(params long[] appIds);
		void SetCalculationExcel(long clientId, byte[] bytes);
	}
}
