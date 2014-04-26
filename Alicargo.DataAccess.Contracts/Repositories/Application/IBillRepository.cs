using Alicargo.DataAccess.Contracts.Contracts.Application;

namespace Alicargo.DataAccess.Contracts.Repositories.Application
{
	public interface IBillRepository
	{
		void AddOrReplace(BillEditData data);
		BillEditData Get(long applicationId);
	}
}