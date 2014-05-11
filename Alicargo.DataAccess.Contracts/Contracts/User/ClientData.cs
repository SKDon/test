namespace Alicargo.DataAccess.Contracts.Contracts.User
{
	public sealed class ClientData : ClientEditData
	{
		public long ClientId { get; set; }
		public long TransitId { get; set; }
		public long UserId { get; set; }
		public string Language { get; set; }
		public string Login { get; set; }
	}
}