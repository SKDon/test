using Alicargo.DataAccess.Contracts.Contracts.Application;

namespace Alicargo.Core.Contracts.State
{
	public interface IStateFilter
	{
		long[] GetStateAvailabilityToSet();
		long[] GetStateVisibility();

		long[] FilterByBusinessLogic(ApplicationEditData applicationData, long[] stateAvailability);
		long[] FilterByPosition(long[] states, int position);
	}
}