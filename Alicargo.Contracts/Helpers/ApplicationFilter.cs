using System.Collections.Generic;

namespace Alicargo.Contracts.Helpers
{
	public sealed class ApplicationFilter
	{
		public ApplicationFilter(int take, int skip, IEnumerable<long> stateIds)
		{
			Take = take;
			StateIds = stateIds;
			Skip = skip;
		}

		public int Take { get; set; }
		public int Skip { get; set; }
		public IEnumerable<long> StateIds { get; set; }
		public Order[] Orders { get; set; }
		public long? ClientUserId { get; set; }
	}
}