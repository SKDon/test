using Alicargo.DataAccess.Contracts.Contracts.Application;

namespace Alicargo.DataAccess.Contracts.Repositories.Application
{
	public interface IBillRepository
	{
		void AddOrReplace(long applicationId, BillData data);
		BillData Get(long applicationId);
	}
}