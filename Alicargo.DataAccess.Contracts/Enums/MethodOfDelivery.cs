using Alicargo.Utilities.Localization;

namespace Alicargo.DataAccess.Contracts.Enums
{
    public enum MethodOfDelivery
    {
		[DisplayNameLocalized(typeof(DataAccess.Contracts.Resources.Enums), "Avia")]
        Avia = 0,

		[DisplayNameLocalized(typeof(DataAccess.Contracts.Resources.Enums), "Auto")]
        Auto = 1
    }
}