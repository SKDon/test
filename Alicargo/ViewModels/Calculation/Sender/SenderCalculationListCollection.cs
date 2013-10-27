namespace Alicargo.ViewModels.Calculation.Sender
{
	public sealed class SenderCalculationListCollection
	{
		public long Total { get; set; }
		public SenderCalculationGroup[] Groups { get; set; }
		public SenderCalculationAwbInfo[] Info { get; set; }
	}
}
