using Alicargo.Utilities.Localization;

namespace Alicargo.Core.Enums
{
	public enum DeliveryType
	{
		[DisplayNameLocalized(typeof(Resources.Enums), "ToDoor")]
		ToDoor = 1,

		[DisplayNameLocalized(typeof(Resources.Enums), "ToWarehouse")]
		ToWarehouse = 2
	}
}