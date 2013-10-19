using Alicargo.Contracts.Contracts;

namespace Alicargo.Contracts.Repositories
{
	public interface IClientFileRepository
	{
		FileHolder GetCalculationFile(long clientId);
		void SetCalculationExcel(long clientId, byte[] bytes);

		FileHolder GetClientDocument(long clientId);
	}
}
