using Alicargo.Utilities.Localization;

namespace Alicargo.Core.Enums
{
	public enum CurrencyType
	{
		[DisplayNameLocalized(typeof(Resources.Enums), CurrencyName.Pound)]
		Pound = 1,

		[DisplayNameLocalized(typeof(Resources.Enums), CurrencyName.Dollar)]
		Dollar = 2,

		[DisplayNameLocalized(typeof(Resources.Enums), CurrencyName.Euro)]
		Euro = 3
	}
}
