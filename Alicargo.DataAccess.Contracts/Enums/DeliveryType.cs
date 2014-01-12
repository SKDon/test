using Alicargo.Utilities.Localization;

namespace Alicargo.Contracts.Enums
{
	public enum DeliveryType
	{
		[DisplayNameLocalized(typeof(DataAccess.Contracts.Resources.Enums), "ToDoor")]
		ToDoor = 1,

		[DisplayNameLocalized(typeof(DataAccess.Contracts.Resources.Enums), "ToWarehouse")]
		ToWarehouse = 2
	}
}