using System.Collections.Generic;

namespace Alicargo.Core.Contracts
{
	public class StateData
	{
		public StateData()
		{
			Localization = new Dictionary<string, string>();
		}

		public IDictionary<string, string> Localization { get; private set; }

		public long Id { get; set; }

		public string Name { get; set; }

		public int Position { get; set; }
	}
}
