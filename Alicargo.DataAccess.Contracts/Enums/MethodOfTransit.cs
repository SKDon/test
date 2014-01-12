using Alicargo.Utilities.Localization;

namespace Alicargo.Contracts.Enums
{
	public enum MethodOfTransit
	{
		[DisplayNameLocalized(typeof(DataAccess.Contracts.Resources.Enums), "Avia")]
		Avia = 0,

		[DisplayNameLocalized(typeof(DataAccess.Contracts.Resources.Enums), "Auto")]
		Auto = 1,

		[DisplayNameLocalized(typeof(DataAccess.Contracts.Resources.Enums), "Self")]
		Self = 2
	}
}