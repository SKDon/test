using Alicargo.DataAccess.Contracts.Contracts.Awb;
using Alicargo.DataAccess.Contracts.Contracts.User;

namespace Alicargo.DataAccess.Contracts.Repositories.Application
{
	public interface IAwbRepository
	{
		long Add(AirWaybillEditData data, long stateId);

		long Count(long? brokerId = null);

		void Delete(long id);

		AirWaybillData[] Get(params long[] ids);

		AirWaybillAggregate[] GetAggregate(
			long[] awbIds, long? clientId = null, long? senderId = null,
			long? forwarderId = null, long? carrierId = null);

		EmailData[] GetCarrierEmails(long awbId);
		EmailData[] GetClientEmails(long awbId);
		EmailData[] GetForwarderEmails(long awbId);

		AirWaybillData[] GetRange(int take, long skip, long? brokerId = null);
		EmailData[] GetSenderEmails(long awbId);

		int GetTotalCountWithouAwb(
			long? clientId = null, long? senderId = null,
			long? forwarderId = null, long? carrierId = null);

		decimal GetTotalValueWithouAwb(
			long? clientId = null, long? senderId = null,
			long? forwarderId = null, long? carrierId = null);

		float GetTotalVolumeWithouAwb(
			long? clientId = null, long? senderId = null,
			long? forwarderId = null, long? carrierId = null);

		float GetTotalWeightWithouAwb(
			long? clientId = null, long? senderId = null,
			long? forwarderId = null, long? carrierId = null);

		void SetAdditionalCost(long awbId, decimal? additionalCost);

		void SetState(long airWaybillId, long stateId);

		void Update(long awbId, AirWaybillEditData data);

		void SetActive(long awbId, bool isActive);
	}
}