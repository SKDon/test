using Alicargo.Utilities.Localization;

namespace Alicargo.Core.Enums
{
	public enum MethodOfTransit
	{
		[DisplayNameLocalized(typeof(Resources.Enums), "Avia")]
		Avia = 0,

		[DisplayNameLocalized(typeof(Resources.Enums), "Auto")]
		Auto = 1,

		[DisplayNameLocalized(typeof(Resources.Enums), "Self")]
		Self = 2
	}
}