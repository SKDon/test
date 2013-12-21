using System.Collections.Generic;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.User;

namespace Alicargo.Contracts.Repositories
{
	public interface ISenderRepository
	{
		long? GetByUserId(long userId);
		SenderData Get(long senderId);
		Dictionary<long, decimal> GetTariffs(params long[] ids);
		long Add(SenderData data, string password);
		void Update(long senderId, SenderData data);
		long GetUserId(long senderId);
	}
}