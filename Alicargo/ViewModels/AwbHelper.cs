using Alicargo.Contracts.Contracts;
using Resources;

namespace Alicargo.ViewModels
{
	public static class AwbHelper
	{
		public static string GetAirWayBillDisplay(AirWaybillData airWaybillModel)
		{
			return string.Format("{0} &plusmn; {1}_{2} &plusmn; {3}_{4}{5}", airWaybillModel.Bill,
								 airWaybillModel.DepartureAirport,
								 airWaybillModel.DateOfDeparture.ToString("ddMMMyyyy").ToUpperInvariant(),
								 airWaybillModel.ArrivalAirport,
								 airWaybillModel.DateOfArrival.ToString("ddMMMyyyy").ToUpperInvariant(),
								 string.IsNullOrWhiteSpace(airWaybillModel.GTD)
									 ? ""
									 : string.Format(" &plusmn; {0}_{1}", Entities.GTD, airWaybillModel.GTD));
		}
	}
}