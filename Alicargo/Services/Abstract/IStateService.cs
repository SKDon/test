using System.Collections.Generic;
using Alicargo.Core.Contracts;

namespace Alicargo.Services.Abstract
{
	public interface IStateService
	{
		long[] GetAvailableStates();
		long[] GetVisibleStates();
		Dictionary<long, string> GetLocalizedDictionary(IEnumerable<long> stateIds = null);
		Dictionary<long, StateData> GetDictionary();
		bool HasPermissionToSetState(long stateId);
	}
}