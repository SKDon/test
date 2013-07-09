using System;
using Alicargo.Core.Contracts;

namespace Alicargo.Core.Repositories
{
	public interface IReferenceRepository
	{
		Func<long> Add(ReferenceData data, byte[] gtdFile, byte[] gtdAdditionalFile,  byte[] packingFile, byte[] invoiceFile, byte[] awbFile);
		long Count(long? brockerId = null);

		ReferenceData[] Get(params long[] ids);
		ReferenceData[] GetAll();
		ReferenceData[] GetRange(long skip, int take, long? brockerId = null);
		ReferenceAggregate[] GetAggregate(params long[] ids);
		string[] GetClientEmails(long id);

		void Delete(long id);
		void SetState(long referenceId, long stateId);
		void Update(ReferenceData data, byte[] gtdFile, byte[] gtdAdditionalFile, byte[] packingFile, byte[] invoiceFile, byte[] awbFile);
		
		FileHolder GetAWBFile(long id);
		FileHolder GetGTDFile(long id);
		FileHolder GetPackingFile(long id);
		FileHolder GTDAdditionalFile(long id);
		FileHolder GetInvoiceFile(long id);
	}
}