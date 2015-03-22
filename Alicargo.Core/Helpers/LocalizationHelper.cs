using System;
using System.Globalization;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Resources;

namespace Alicargo.Core.Helpers
{
	public static class LocalizationHelper
	{
		public static string GetDate(DateTimeOffset? date, CultureInfo cultureInfo, TimeSpan? timeZone = null)
		{
			return date.HasValue && !date.Value.Equals(DateTimeOffset.MinValue)
				? date.Value.Date.ToString("d", cultureInfo)
				: null;
		}

		public static string GetDeliveryType(DeliveryType deliveryType, CultureInfo cultureInfo)
		{
			return Enums.ResourceManager.GetString(deliveryType.ToString(), cultureInfo);
		}

		public static string GetMethodOfDelivery(MethodOfDelivery methodOfDelivery, CultureInfo cultureInfo)
		{
			return Enums.ResourceManager.GetString(methodOfDelivery.ToString(), cultureInfo);
		}

		public static string GetMethodOfTransit(MethodOfTransit methodOfTransit, CultureInfo cultureInfo)
		{
			return Enums.ResourceManager.GetString(methodOfTransit.ToString(), cultureInfo);
		}

		public static string GetValueString(decimal value, CurrencyType currency, CultureInfo cultureInfo)
		{
			if(value == 0)
			{
				return null;
			}

			return value.ToString("### ### ### ###", cultureInfo) + CurrencyName.Names[currency];
		}
	}
}