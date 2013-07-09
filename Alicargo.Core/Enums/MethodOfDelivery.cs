using Alicargo.Core.Localization;

namespace Alicargo.Core.Enums
{
    public enum MethodOfDelivery
    {
		[DisplayNameLocalized(typeof(Resources.Enums), "Avia")]
        Avia = 0,

		[DisplayNameLocalized(typeof(Resources.Enums), "Auto")]
        Auto = 1
    }
}