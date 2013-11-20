using Alicargo.Contracts.Contracts;

namespace Alicargo.Services.Abstract
{
	public interface IStateService
	{
		long[] GetStateAvailabilityToSet();
		long[] GetStateVisibility();

		long[] FilterByBusinessLogic(ApplicationData applicationData, long[] stateAvailability);
		long[] FilterByPosition(long[] states, int position);
	}
}