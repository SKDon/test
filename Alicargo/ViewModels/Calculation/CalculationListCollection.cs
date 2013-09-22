namespace Alicargo.ViewModels.Calculation
{
	public sealed class CalculationListCollection
	{
		public long Total { get; set; }
		public CalculationGroup[] Groups { get; set; }
		public CalculationInfo[] Info { get; set; }
	}
}
