using System;
using Microsoft.Ajax.Utilities;

namespace Alicargo.ViewModels.Application
{
	internal sealed class ApplicationModelHelper
	{
		public static string GetDisplayNumber(long id, long? count)
		{
			id = id % 1000;

			return String.Format("{0:000}{1}", id, count.HasValue && count > 0 ? "/" + count.Value : "");
		}

		public static string GetSorter(string referenceBill, long dateOfArrivalUtcTicks, long dateOfDepartureUtcTicks)
		{
			var noBill = referenceBill.IsNullOrWhiteSpace();

			if (noBill && dateOfArrivalUtcTicks == 0 && dateOfDepartureUtcTicks == 0)
				return "";

			return String.Format("{0}_{1}_{2}_{3}", noBill ? "0" : "1", dateOfArrivalUtcTicks / 10000000000,
				dateOfDepartureUtcTicks / 10000000000, referenceBill);
		}

		public static int GetDaysInWork(DateTimeOffset dateTimeOffset)
		{
			unchecked // todo: fix and test
			{
				return (DateTimeOffset.UtcNow - dateTimeOffset.ToUniversalTime()).Days;
			}
		}
	}
}