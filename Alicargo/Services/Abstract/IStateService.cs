using System.Collections.Generic;
using Alicargo.Core.Contracts;

namespace Alicargo.Services.Abstract
{
	public interface IStateService
	{
		long[] GetAvailableStatesToSet();
		long[] GetVisibleStates();
		Dictionary<long, string> GetLocalizedDictionary(IEnumerable<long> stateIds = null);
		Dictionary<long, StateData> GetDictionary();
		bool HasPermissionToSetState(long stateId);
		long[] ApplyBusinessLogicToStates(ApplicationData applicationData, long[] availableStates);
		long[] FilterByPosition(long[] states, int position);
	}
}