using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Resources;
using Alicargo.Core.Resources;

namespace Alicargo.ViewModels.Helpers
{
	public static class AwbHelper
	{
		// todo: 3. return decoded text and encode then need
		public static string GetAirWaybillDisplay(AirWaybillData data)
		{
			return string.Format("{0} &plusmn; {1}_{2} &plusmn; {3}_{4}{5}", data.Bill,
								 data.DepartureAirport,
								 data.DateOfDeparture.ToString("ddMMMyyyy").ToUpperInvariant(),
								 data.ArrivalAirport,
								 data.DateOfArrival.ToString("ddMMMyyyy").ToUpperInvariant(),
								 string.IsNullOrWhiteSpace(data.GTD)
									 ? ""
									 : string.Format(" &plusmn; {0}_{1}", Entities.GTD, data.GTD));
		}
	}
}