using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.Application;

namespace Alicargo.Services.Abstract
{
	public interface IStateFilter
	{
		long[] GetStateAvailabilityToSet();
		long[] GetStateVisibility();

		long[] FilterByBusinessLogic(ApplicationData applicationData, long[] stateAvailability);
		long[] FilterByPosition(long[] states, int position);
	}
}