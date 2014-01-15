using System.Collections.Generic;
using Alicargo.DataAccess.Contracts.Contracts.State;

namespace Alicargo.DataAccess.Contracts.Repositories
{
	public interface IStateRepository
	{
		long Add(string language, StateData data);
		IReadOnlyDictionary<long, StateData> Get(string language, params long[] ids);
		StateListItem[] All();
		void Update(long id, string language, StateData data);
		void Delete(long id);
	}
}