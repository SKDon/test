using Alicargo.DataAccess.Contracts.Contracts;
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

		public decimal? FlightCost { get; set; }

		public decimal? CustomCost { get; set; }

		public decimal? BrokerCost { get; set; }

		public decimal? AdditionalCost { get; set; }

		public decimal? TotalCostOfSenderForWeight { get; set; }

		public bool IsActive { get; set; }

		public FileInfo[] GTDFiles { get; set; }

		public FileInfo[] GTDAdditionalFiles { get; set; }

		public FileInfo[] PackingFiles { get; set; }

		public FileInfo[] InvoiceFiles { get; set; }

		public FileInfo[] AWBFiles { get; set; }

		public FileInfo[] DrawFiles { get; set; }

		public FileInfo[] OtherFiles { get; set; }
	}
}