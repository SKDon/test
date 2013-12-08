using System.Collections.Generic;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
{
	public interface IApplicationFileRepository
	{
		#region Obsolete

		FileHolder GetInvoiceFile(long id);
		FileHolder GetSwiftFile(long id);
		FileHolder GetCPFile(long id);
		FileHolder GetDeliveryBillFile(long id);
		FileHolder GetTorg12File(long id);
		FileHolder GetPackingFile(long id);

		#endregion


		IReadOnlyDictionary<long, string> GetFileNames(long applicationId, ApplicationFileType type);
		IReadOnlyDictionary<long, FileInfo[]> GetFileNames(long[] applicationIds, ApplicationFileType type);
		FileHolder Get(long id);
		long Add(long applicationId, ApplicationFileType type, string fileName, byte[] bytes);
		void Delete(long id);
	}
}