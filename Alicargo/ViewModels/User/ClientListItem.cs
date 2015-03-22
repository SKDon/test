namespace Alicargo.ViewModels.User
{
	public sealed class ClientListItem
	{
		public string Nic { get; set; }
		public string LegalEntity { get; set; }
		public decimal Balance { get; set; }
		public long ClientId { get; set; }
	}
}