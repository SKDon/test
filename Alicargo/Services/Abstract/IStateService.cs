using System.Collections.Generic;
using Alicargo.Contracts.Contracts;

namespace Alicargo.Services.Abstract
{
	public interface IStateService
	{
		long[] GetStateAvailabilityToSet();
		long[] GetStateVisibility();
		Dictionary<long, string> GetLocalizedDictionary(long[] stateIds = null);
		Dictionary<long, ObsoleteStateData> GetDictionary();
		bool HasPermissionToSetState(long stateId);
		long[] ApplyBusinessLogicToStates(ApplicationData applicationData, long[] stateAvailability);
		long[] FilterByPosition(long[] states, int position);
	}
}