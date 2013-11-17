using System.Collections.Generic;
using Alicargo.Contracts.Contracts;

namespace Alicargo.Services.Abstract
{
	public interface IStateService
	{
		long[] GetStateAvailabilityToSet();
		long[] GetStateVisibility();
		Dictionary<long, string> GetLocalizedDictionary(long[] stateIds = null);
		Dictionary<long, StateData> GetDictionary();
		bool HasPermissionToSetState(long stateId);
		long[] ApplyBusinessLogicToStates(ApplicationData applicationData, long[] StateAvailability);
		long[] FilterByPosition(long[] states, int position);
	}
}