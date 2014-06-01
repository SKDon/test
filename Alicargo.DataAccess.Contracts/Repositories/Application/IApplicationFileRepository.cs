using System.Collections.ObjectModel;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.DataAccess.Contracts.Repositories.Application
{
	public interface IApplicationFileRepository
	{
		ReadOnlyCollection<FileInfo> GetNames(long applicationId, ApplicationFileType type);
		ReadOnlyDictionary<long, ReadOnlyCollection<FileInfo>> GetInfo(long[] applicationIds, ApplicationFileType type);
		FileHolder Get(long id);
		long Add(long applicationId, ApplicationFileType type, string fileName, byte[] bytes);
		void Delete(long id);
	}
}