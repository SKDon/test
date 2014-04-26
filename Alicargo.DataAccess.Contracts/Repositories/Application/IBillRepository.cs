using Alicargo.DataAccess.Contracts.Contracts.Application;

namespace Alicargo.DataAccess.Contracts.Repositories.Application
{
	public interface IBillRepository
	{
		void AddOrReplace(long applicationId, BillEditData data);
		BillEditData Get(long applicationId);
	}
}