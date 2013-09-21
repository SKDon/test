namespace Alicargo.ViewModels.Calculation
{
	public sealed class CalculationGroup
	{
		// ReSharper disable InconsistentNaming

		public string field { get; set; }
		public string value { get; set; }
		public bool hasSubgroups { get; set; }
		public CalculationDetailsItem[] items { get; set; }

		// ReSharper restore InconsistentNaming
	}
}