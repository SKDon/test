using System;
using System.Globalization;
using Alicargo.Core.Enums;
using Alicargo.Core.Localization;

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
			unchecked // 3. todo: fix and test
			{
				return (DateTimeOffset.UtcNow - dateTimeOffset.ToUniversalTime()).Days;
			}
		}

		public static string GetValueString(decimal value, int currencyId)
		{
			var currency = ((CurrencyType)currencyId);

			return GetValueString(value, currency, CultureContext.Current.GetTwoLetterISOLanguageName());
		}

		public static string GetValueString(decimal value, CurrencyType currency, string culture)
		{
			if (value == 0)
			{
				return string.Empty;
			}

			return value.ToString("### ### ### ###", CultureInfo.GetCultureInfo(culture)) + CurrencyName.Names[currency];
		}
	}
}