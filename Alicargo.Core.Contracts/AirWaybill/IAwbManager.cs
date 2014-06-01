using Alicargo.DataAccess.Contracts.Contracts.Awb;

namespace Alicargo.Core.Contracts.AirWaybill
{
	public interface IAwbManager
	{
		long Create(long? applicationId, AirWaybillEditData data);
		void Delete(long awbId);
	}
}