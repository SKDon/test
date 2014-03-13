using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.DataAccess.Contracts.Repositories.Application
{
	public interface IAwbFileRepository
	{
		FileHolder GTDAdditionalFile(long awbId);
		FileHolder GetAWBFile(long awbId);
		FileHolder GetGTDFile(long awbId);
		FileHolder GetInvoiceFile(long awbId);
		FileHolder GetPackingFile(long awbId);
		FileHolder GetDrawFile(long awbId);
	}
}