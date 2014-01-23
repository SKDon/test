using System.Collections.Generic;
using Alicargo.DataAccess.Contracts.Contracts.User;

namespace Alicargo.DataAccess.Contracts.Repositories.User
{
	public interface ISenderRepository
	{
		long? GetByUserId(long userId);
		SenderData Get(long senderId);
		Dictionary<long, decimal> GetTariffs(params long[] ids);
		long Add(SenderData data, string password);
		void Update(long senderId, SenderData data);
		long GetUserId(long senderId);
		UserData[] GetAll();
		long[] GetByCountry(long countryId);
	}
}