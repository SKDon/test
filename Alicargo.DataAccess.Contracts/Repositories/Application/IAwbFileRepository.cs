using System.Collections.Generic;
using System.Collections.ObjectModel;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.DataAccess.Contracts.Repositories.Application
{
	public interface IAwbFileRepository
	{
		ReadOnlyCollection<FileInfo> GetNames(long awbId, AwbFileType type);
		IReadOnlyDictionary<long, FileInfo[]> GetInfo(long[] awbIds, AwbFileType type);
		FileHolder Get(long fileId);
		long Add(long awbId, AwbFileType type, string fileName, byte[] bytes);
		void Delete(long fileId);
	}
}