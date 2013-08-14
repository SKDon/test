using Alicargo.ViewModels.Application;

namespace Alicargo.ViewModels.AirWaybill
{
	public sealed class AirWaybillListItem
	{
		public long Id { get; set; }

		public string CreationTimestampLocalString { get; set; }

		public ApplicationStateModel State { get; set; }

		public string Bill { get; set; }

		public string ArrivalAirport { get; set; }

		public string DepartureAirport { get; set; }

		public string DateOfDepartureLocalString { get; set; }

		public string DateOfArrivalLocalString { get; set; }

		public int? TotalCount { get; set; }

		public float? TotalWeight { get; set; }

		public string StateChangeTimestampLocalString { get; set; }

		public string GTD { get; set; }

		public string GTDFileName { get; set; }

		public string GTDAdditionalFileName { get; set; }

		public string PackingFileName { get; set; }

		public string InvoiceFileName { get; set; }

		public string AWBFileName { get; set; }
	}
}