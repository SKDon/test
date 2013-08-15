using System;
using Alicargo.Contracts.Contracts;

namespace Alicargo.Core.Repositories
{
	public interface IAWBRepository
	{
		Func<long> Add(AirWaybillData data, byte[] gtdFile, byte[] gtdAdditionalFile, byte[] packingFile, byte[] invoiceFile, byte[] awbFile);
		long Count(long? brockerId = null);

		AirWaybillData[] Get(params long[] ids);
		AirWaybillData[] GetAll();
		AirWaybillData[] GetRange(long skip, int take, long? brockerId = null);
		AirWaybillAggregate[] GetAggregate(params long[] ids);
		string[] GetClientEmails(long id);

		void Delete(long id);
		void SetState(long airWaybillId, long stateId);
		void Update(AirWaybillData data, byte[] gtdFile, byte[] gtdAdditionalFile, byte[] packingFile, byte[] invoiceFile, byte[] awbFile);
		
		FileHolder GetAWBFile(long id);
		FileHolder GetGTDFile(long id);
		FileHolder GetPackingFile(long id);
		FileHolder GTDAdditionalFile(long id);
		FileHolder GetInvoiceFile(long id);
	}
}