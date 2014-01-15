using System;
using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.DataAccess.Contracts.Repositories
{
	public interface IAwbRepository
	{
		Func<long> Add(AirWaybillData data, byte[] gtdFile, byte[] gtdAdditionalFile, byte[] packingFile, byte[] invoiceFile, byte[] awbFile);
		long Count(long? brokerId = null);

		AirWaybillData[] Get(params long[] ids);
		AirWaybillData[] GetRange(int take, long skip, long? brokerId = null);

		AirWaybillAggregate[] GetAggregate(params long[] ids);
		int? GetTotalCountWithouAwb();
		float? GetTotalWeightWithouAwb();
		decimal GetTotalValueWithouAwb();
		float GetTotalVolumeWithouAwb();
		string[] GetClientEmails(long id);

		void Delete(long id);
		void SetState(long airWaybillId, long stateId);
		void SetAdditionalCost(long awbId, decimal? additionalCost);
		void Update(AirWaybillData data, byte[] gtdFile, byte[] gtdAdditionalFile, byte[] packingFile, byte[] invoiceFile, byte[] awbFile);
		
		FileHolder GetAWBFile(long id);
		FileHolder GetGTDFile(long id);
		FileHolder GetPackingFile(long id);
		FileHolder GTDAdditionalFile(long id);
		FileHolder GetInvoiceFile(long id);
	}
}