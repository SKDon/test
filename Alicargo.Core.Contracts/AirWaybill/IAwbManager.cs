using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.Services.Abstract
{
	public interface IAwbManager
	{
		long Create(long? applicationId, AirWaybillData data, byte[] gtdFile, byte[] gtdAdditionalFile, byte[] packingFile,
			byte[] invoiceFile, byte[] awbFile);

		void Delete(long awbId);
	}
}