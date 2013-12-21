namespace Alicargo.Contracts.Contracts.User
{
	public sealed class SenderData
	{
		public string Login { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public decimal TariffOfTapePerBox { get; set; }
		public string TwoLetterISOLanguageName { get; set; }
	}
}
