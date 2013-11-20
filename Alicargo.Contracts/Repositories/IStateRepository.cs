using System.Collections.Generic;
using Alicargo.Contracts.Contracts;

namespace Alicargo.Contracts.Repositories
{
	public interface IStateRepository
	{
		long Add(StateData data);
		IReadOnlyDictionary<long, StateData> Get(params long[] ids);
		StateListItem[] All();
		void Edit(long id, StateData data);
		void Delete(long id);
	}
}