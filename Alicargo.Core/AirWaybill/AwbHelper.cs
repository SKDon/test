using Alicargo.Core.Resources;
using Alicargo.DataAccess.Contracts.Contracts.Awb;

namespace Alicargo.Core.AirWaybill
{
	public static class AwbHelper
	{
		public static string GetAirWaybillDisplay(AirWaybillData data)
		{
			return string.Format("{0} ± {1}_{2} ± {3}_{4}{5}",
				data.Bill,
				data.DepartureAirport,
				data.DateOfDeparture.ToString("ddMMMyyyy").ToUpperInvariant(),
				data.ArrivalAirport,
				data.DateOfArrival.ToString("ddMMMyyyy").ToUpperInvariant(),
				string.IsNullOrWhiteSpace(data.GTD)
					? ""
					: string.Format(" ± {0}_{1}", Entities.GTD, data.GTD));
		}
	}
}