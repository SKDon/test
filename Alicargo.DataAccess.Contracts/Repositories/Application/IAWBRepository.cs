using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.User;

namespace Alicargo.DataAccess.Contracts.Repositories.Application
{
	public interface IAwbRepository
	{
		long Add(AirWaybillData data, byte[] gtdFile, byte[] gtdAdditionalFile, byte[] packingFile, byte[] invoiceFile,
			byte[] awbFile, byte[] drawFile);

		long Count(long? brokerId = null);

		void Delete(long id);

		AirWaybillData[] Get(params long[] ids);

		AirWaybillAggregate[] GetAggregate(long[] awbIds, long? clientId = null, long? senderId = null,
			long? forwarderId = null, long? carrierId = null);

		EmailData[] GetCarrierEmails(long awbId);
		EmailData[] GetClientEmails(long awbId);
		EmailData[] GetForwarderEmails(long awbId);

		AirWaybillData[] GetRange(int take, long skip, long? brokerId = null);
		EmailData[] GetSenderEmails(long awbId);

		int? GetTotalCountWithouAwb();

		decimal GetTotalValueWithouAwb();

		float GetTotalVolumeWithouAwb();

		float? GetTotalWeightWithouAwb();

		void SetAdditionalCost(long awbId, decimal? additionalCost);

		void SetState(long airWaybillId, long stateId);

		void Update(AirWaybillData data, byte[] gtdFile, byte[] gtdAdditionalFile, byte[] packingFile, byte[] invoiceFile,
			byte[] awbFile, byte[] drawFile);
	}
}