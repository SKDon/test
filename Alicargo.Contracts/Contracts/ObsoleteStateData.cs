using System.Collections.Generic;

namespace Alicargo.Contracts.Contracts
{
	public sealed class ObsoleteStateData
	{
		public ObsoleteStateData()
		{
			Localization = new Dictionary<string, string>();
		}

		public IDictionary<string, string> Localization { get; private set; }

		public long Id { get; set; }

		public string Name { get; set; }

		public int Position { get; set; }
	}
}