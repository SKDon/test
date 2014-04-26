using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.ViewModels.Calculation.Sender
{
	public sealed class SenderCalculationItem
	{
		public long ApplicationId { get; set; }
		public long? AirWaybillId { get; set; }
		public string ClientNic { get; set; }
		public string DisplayNumber { get; set; }
		public string Factory { get; set; }
		public string Mark { get; set; }
		public int? Count { get; set; }
		public float? Weight { get; set; }
		public string Invoice { get; set; }
		public decimal Value { get; set; }
		public CurrencyType ValueCurrencyId { get; set; }
		public decimal? SenderRate { get; set; }
		public decimal TotalSenderRate { get; set; }
		public decimal? SenderScotchCost { get; set; }
		public decimal? FactureCost { get; set; }
		public decimal? FactureCostEx { get; set; }
		public decimal? PickupCost { get; set; }
		public decimal Profit { get; set; }
	}
}