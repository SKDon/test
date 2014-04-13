using System;
using System.Globalization;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Utilities;
using Alicargo.Utilities.Localization;

namespace Alicargo.Core.Helpers
{
	public sealed class ApplicationHelper
	{
		public static string GetApplicationDisplay(int number, long? count)
		{
			return String.Format("{0:000}{1}", number % 1000, count.HasValue && count > 0 ? "/" + count.Value : "");
		}

		public static int GetDaysInWork(DateTimeOffset creationTimestamp)
		{
			return (DateTimeProvider.Now - creationTimestamp.ToUniversalTime()).Days;
		}

		public static string GetValueString(decimal value, int currencyId)
		{
			var currency = ((CurrencyType)currencyId);

			var culture = CultureProvider.Get();

			return LocalizationHelper.GetValueString(value, currency, CultureInfo.GetCultureInfo(culture));
		}
	}
}