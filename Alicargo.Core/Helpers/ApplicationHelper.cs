using System;
using System.Globalization;
using Alicargo.Core.Enums;
using Alicargo.Core.Localization;
using Alicargo.Core.Services;

namespace Alicargo.Core.Helpers
{
	public sealed class ApplicationHelper
	{
		public static string GetDisplayNumber(long id, long? count)
		{
			id = id % 1000;

			return String.Format("{0:000}{1}", id, count.HasValue && count > 0 ? "/" + count.Value : "");
		}

		public static int GetDaysInWork(DateTimeOffset dateTimeOffset)
		{
			return (DateTimeOffset.UtcNow - dateTimeOffset.ToUniversalTime()).Days;
		}

		public static string GetValueString(decimal value, int currencyId)
		{
			var currency = ((CurrencyType)currencyId);

			string culture = CultureContext.Current.GetTwoLetterISOLanguageName();
			return LocalizationHelper.GetValueString(value, currency, CultureInfo.GetCultureInfo(culture));
		}
	}
}