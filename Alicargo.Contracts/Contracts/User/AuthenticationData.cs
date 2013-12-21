namespace Alicargo.Contracts.Contracts.User
{
	public sealed class AuthenticationData
	{
		public long Id { get; set; }
		public string Login { get; set; }
		public byte[] PasswordSalt { get; set; }
		public byte[] PasswordHash { get; set; }
		public string TwoLetterISOLanguageName { get; set; }
	}
}