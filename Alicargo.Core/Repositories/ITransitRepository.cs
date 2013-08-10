using System;
using Alicargo.Core.Contracts;
using Alicargo.Core.Models;

namespace Alicargo.Core.Repositories
{
	public interface ITransitRepository
	{
		Func<long> Add(TransitData transit);
		void Update(TransitData transit);
		TransitData[] Get(params long[] ids);
		void Delete(long transitId);
	}
}
