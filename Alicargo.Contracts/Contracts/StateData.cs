using System;
using System.Collections.Generic;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Contracts
{
	public sealed class StateData
	{
		public StateData(IReadOnlyDictionary<string, string> localization)
		{
			Localization = localization;

#if DEBUG
			if (!localization.ContainsKey(TwoLetterISOLanguageName.English)
				|| !localization.ContainsKey(TwoLetterISOLanguageName.Russian)
				|| !localization.ContainsKey(TwoLetterISOLanguageName.Italian))
			{
				throw new ArgumentException("Localization dictionary should contains all cultures");
			}
#endif
		}

		public IReadOnlyDictionary<string, string> Localization { get; private set; }

		public string Name { get; set; }

		public int Position { get; set; }
	}
}