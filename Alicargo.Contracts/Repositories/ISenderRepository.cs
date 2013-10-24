using Alicargo.Contracts.Contracts;

namespace Alicargo.Contracts.Repositories
{
	public interface ISenderRepository
	{
		long? GetByUserId(long userId);
		SenderData Get(long id);
		long Add(SenderData data, string password);
		void Update(long id, SenderData data);
		long GetUserId(long id);
	}
}
