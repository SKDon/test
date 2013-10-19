using Alicargo.Contracts.Contracts;

namespace Alicargo.Contracts.Repositories
{
	public interface IClientFileRepository
	{
		FileHolder GetClientDocument(long clientId);
	}
}
