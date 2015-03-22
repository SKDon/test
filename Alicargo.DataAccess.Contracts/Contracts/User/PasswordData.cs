namespace Alicargo.DataAccess.Contracts.Contracts.User
{
	public sealed class PasswordData
	{
		public byte[] PasswordSalt { get; set; }
		public byte[] PasswordHash { get; set; }
		public long UserId { get; set; }
	}
}