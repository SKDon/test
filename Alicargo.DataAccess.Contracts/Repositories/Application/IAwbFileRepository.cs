using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.DataAccess.Contracts.Repositories.Application
{
	public interface IAwbFileRepository
	{
		FileHolder GTDAdditionalFile(long id);
		FileHolder GetAWBFile(long id);
		FileHolder GetGTDFile(long id);
		FileHolder GetInvoiceFile(long id);
		FileHolder GetPackingFile(long id);
		FileHolder GetDrawFile(long id);
	}
}