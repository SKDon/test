using System;
using System.Globalization;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Resources;

namespace Alicargo.Core.Helpers
{
	public static class LocalizationHelper
	{
		public static string GetDate(DateTimeOffset? date, CultureInfo cultureInfo, TimeSpan? timeZone = null)
		{
			// todo: apply timezone
			return date.HasValue ? date.Value.Date.ToString("d", cultureInfo) : null;
		}

		public static string GetMethodOfDelivery(MethodOfDelivery methodOfDelivery, CultureInfo cultureInfo)
		{
			return Entities.ResourceManager.GetString("MethodOfDelivery", cultureInfo);
		}

		public static string GetMethodOfTransit(MethodOfTransit methodOfTransit, CultureInfo cultureInfo)
		{
			return Entities.ResourceManager.GetString("MethodOfTransit", cultureInfo);
		}

		public static string GetDeliveryType(DeliveryType deliveryType, CultureInfo cultureInfo)
		{
			return Entities.ResourceManager.GetString("DeliveryType", cultureInfo);
		}

		public static string GetValueString(decimal value, CurrencyType currency, CultureInfo cultureInfo)
		{
			if (value == 0)
			{
				return String.Empty;
			}

			return value.ToString("### ### ### ###", cultureInfo) + CurrencyName.Names[currency];
		}
	}
}