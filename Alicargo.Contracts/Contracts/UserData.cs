namespace Alicargo.Core.Contracts
{
	public sealed class UserData
	{
		public long Id { get; set; }
		public long UserId { get; set; }
		public string Name { get; set; }
		public string Login { get; set; }
		public string Email { get; set; }
		public string TwoLetterISOLanguageName { get; set; }
	}
}
