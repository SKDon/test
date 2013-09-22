namespace Alicargo.ViewModels.Calculation
{
	public sealed class CalculationListCollection
	{
		public long Total { get; set; }
		public CalculationItem[] Data { get; set; }
		public CalculationInfo[] Info { get; set; }
	}
}
