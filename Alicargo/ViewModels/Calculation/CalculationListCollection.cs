namespace Alicargo.ViewModels.Calculation
{
	public sealed class CalculationListCollection
	{
		public CalculationGroup[] Groups { get; set; }
		public long Total { get; set; }
		public CalculationDetailsItem[] Data { get; set; }
	}
}
