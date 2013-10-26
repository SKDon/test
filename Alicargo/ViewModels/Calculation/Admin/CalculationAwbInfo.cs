namespace Alicargo.ViewModels.Calculation.Admin
{
	public sealed class CalculationAwbInfo
	{
		public long AirWaybillId { get; set; }

		public decimal? CostPerKgOfSender { get; set; }

		public decimal TotalCostOfSenderForWeight { get; set; }

		public decimal TotalScotchCost { get; set; }

		public decimal TotalSenderRate { get; set; }

		public decimal TotalFactureCost { get; set; }

		public decimal TotalWithdrawCost { get; set; }

		public decimal TotalTransitCost { get; set; }

		public decimal TotalInsuranceCost { get; set; }

		public decimal TotalOfSender
		{
			get { return TotalSenderRate + TotalScotchCost + TotalFactureCost + TotalWithdrawCost; }
		}

		public decimal? AdditionalCost { get; set; }

		public decimal TotalExpenses
		{
			get
			{
				return TotalOfSender + FlightCost + CustomCost + BrokerCost + TotalInsuranceCost
					   + TotalTransitCost + (AdditionalCost ?? 0);
			}
		}

		public decimal Profit { get; set; }

		public decimal? ProfitPerKg { get; set; }

		public decimal FlightCost { get; set; }

		public decimal? FlightCostPerKg { get; set; }

		public decimal CustomCost { get; set; }

		public decimal? CustomCostPerKg { get; set; }

		public decimal BrokerCost { get; set; }

		public decimal? BrokerCostPerKg { get; set; }
	}
}