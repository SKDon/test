using System.Collections.ObjectModel;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.DataAccess.Contracts.Repositories.Application
{
	public interface IAwbFileRepository
	{
		ReadOnlyCollection<FileInfo> GetNames(long awbId, AwbFileType type);
		ReadOnlyDictionary<long, ReadOnlyCollection<FileInfo>> GetInfo(long[] awbIds, AwbFileType type);
		FileHolder Get(long fileId);
		long Add(long awbId, AwbFileType type, string fileName, byte[] bytes);
		void Delete(long fileId);

		FileHolder GTDAdditionalFile(long awbId);
		FileHolder GetAWBFile(long awbId);
		FileHolder GetGTDFile(long awbId);
		FileHolder GetInvoiceFile(long awbId);
		FileHolder GetPackingFile(long awbId);
		FileHolder GetDrawFile(long awbId);
	}
}