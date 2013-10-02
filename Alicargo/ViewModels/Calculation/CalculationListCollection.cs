namespace Alicargo.ViewModels.Calculation
{
	public sealed class CalculationListCollection
	{
		public long Total { get; set; }
		public CalculationGroup[] Groups { get; set; }
		public CalculationAwbInfo[] Info { get; set; }
	}
}
