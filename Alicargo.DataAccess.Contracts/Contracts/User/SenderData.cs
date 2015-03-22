namespace Alicargo.DataAccess.Contracts.Contracts.User
{
	public sealed class SenderData
	{
		public string Login { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public decimal TariffOfTapePerBox { get; set; }
		public string Language { get; set; }
		public string Contact { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
	}
}