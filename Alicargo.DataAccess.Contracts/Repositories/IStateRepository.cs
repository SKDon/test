using System.Collections.Generic;
using Alicargo.DataAccess.Contracts.Contracts.State;

namespace Alicargo.DataAccess.Contracts.Repositories
{
	public interface IStateRepository
	{
		long Add(StateEditData data);
		void Delete(long id);
		IReadOnlyDictionary<long, StateData> Get(string language, params long[] ids);
		void Update(long id, StateEditData data);
	}
}