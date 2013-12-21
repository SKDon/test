namespace Alicargo.Contracts.Contracts.User
{
	public sealed class PasswordData
	{
		public byte[] PasswordSalt { get; set; }
		public byte[] PasswordHash { get; set; }
	}
}