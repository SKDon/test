using System.Collections.Generic;
using Alicargo.Contracts.Contracts;

namespace Alicargo.Contracts.Repositories
{
	public interface IStateRepository
	{
		long Add(string twoLetterISOLanguageName, StateData data);
		IReadOnlyDictionary<long, StateData> Get(string twoLetterISOLanguageName, params long[] ids);
		StateListItem[] All();
		void Update(long id, string twoLetterISOLanguageName, StateData data);
		void Delete(long id);
	}
}