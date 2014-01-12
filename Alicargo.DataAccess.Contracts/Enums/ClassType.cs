using Alicargo.Utilities.Localization;

namespace Alicargo.Contracts.Enums
{
	using Resources = DataAccess.Contracts.Resources;

	public enum ClassType
	{
		[DisplayNameLocalized(typeof(Resources.Enums), "Econom")]
		Econom = 1,

		[DisplayNameLocalized(typeof(Resources.Enums), "Comfort")]
		Comfort = 2,

		[DisplayNameLocalized(typeof(Resources.Enums), "Lux")]
		Lux = 3
	}
}