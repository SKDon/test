using Alicargo.Contracts.Contracts;

namespace Alicargo.Contracts.Repositories
{
	public interface IApplicationFileRepository
	{
		FileHolder GetInvoiceFile(long id);
		FileHolder GetSwiftFile(long id);
		FileHolder GetCPFile(long id);
		FileHolder GetDeliveryBillFile(long id);
		FileHolder GetTorg12File(long id);
		FileHolder GetPackingFile(long id);
	}
}
