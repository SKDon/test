using System.Collections.Generic;

namespace Alicargo.DataAccess.Contracts.Contracts
{
	public sealed class CountryData
	{
		public CountryData(long id, IDictionary<string, string> name)
		{
			Id = id;
			Name = name;
		}

		public long Id { get; private set; }

		public IDictionary<string, string> Name { get; private set; }
	}
}
