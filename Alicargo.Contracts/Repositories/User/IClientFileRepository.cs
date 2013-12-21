using Alicargo.Contracts.Contracts;

namespace Alicargo.Contracts.Repositories
{
	public interface IClientFileRepository
	{
		FileHolder GetClientContract(long clientId);
		string GetClientContractFileName(long clientId);
		void SetClientContract(long clientId, string name, byte[] data);
	}
}
