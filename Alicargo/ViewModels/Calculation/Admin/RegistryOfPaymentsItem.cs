namespace Alicargo.ViewModels.Calculation.Admin
{
	public sealed class RegistryOfPaymentsItem
	{
		public string Timestamp { get; set; }
		public string EventType { get; set; }
		public decimal Balance { get; set; }
		public decimal Money { get; set; }
		public string ClientNic { get; set; }
		public string Comment { get; set; }
	}
}