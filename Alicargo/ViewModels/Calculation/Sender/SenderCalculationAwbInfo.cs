namespace Alicargo.ViewModels.Calculation.Sender
{
	public sealed class SenderCalculationAwbInfo
	{
		public long? AirWaybillId { get; set; }

		public decimal? CostPerKgOfSender { get; set; }

		public decimal TotalCostOfSenderForWeight { get; set; }

		public decimal TotalScotchCost { get; set; }

		public decimal TotalSenderRate { get; set; }

		public decimal TotalFactureCost { get; set; }

		public decimal TotalPickupCost { get; set; }

		public decimal TotalOfSender
		{
			get { return TotalSenderRate + TotalScotchCost + TotalFactureCost + TotalPickupCost; }
		}

		public decimal Total
		{
			get { return TotalOfSender + FlightCost; }
		}

		public decimal FlightCost { get; set; }

		public decimal? FlightCostPerKg { get; set; }
	}
}