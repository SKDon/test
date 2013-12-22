namespace Alicargo.Contracts.Contracts.User
{
	public sealed class BrokerData
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string TwoLetterISOLanguageName { get; set; }
		public long UserId { get; set; }
		public string Login { get; set; }
	}
}
