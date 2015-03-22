using Alicargo.DataAccess.Contracts.Contracts.Awb;

namespace Alicargo.Core.Contracts.AirWaybill
{
	public interface IAwbManager
	{
		long Create(long? applicationId, AirWaybillEditData data, long creatorUserId);
		void Delete(long awbId);
	}
}