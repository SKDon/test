using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Alicargo.Core.Enums
{
	public static class CurrencyName
	{
		public const string Pound = "£";
		public const string Dollar = "$";
		public const string Euro = "€";

		static CurrencyName()
		{
			Names = new ReadOnlyDictionary<CurrencyType, string>(
				new Dictionary<CurrencyType, string>
				{
					{CurrencyType.Dollar, Dollar},
					{CurrencyType.Pound, Pound},
					{CurrencyType.Euro, Euro}
				});
		}

		public static IReadOnlyDictionary<CurrencyType, string> Names { get; private set; }
	}
}