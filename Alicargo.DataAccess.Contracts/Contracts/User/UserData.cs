namespace Alicargo.Contracts.Contracts.User
{
	public sealed class UserData
	{
		public long EntityId { get; set; }
		public long UserId { get; set; }
		public string Name { get; set; }
		public string Login { get; set; }
		public string Email { get; set; }
		public string Language { get; set; }
	}
}