namespace Alicargo.DataAccess.Contracts.Contracts.User
{
	public sealed class ForwarderData
	{
		public long Id { get; set; }
		public long UserId { get; set; }

		public long CityId { get; set; }
		public string Email { get; set; }
		public string Language { get; set; }
		public string Login { get; set; }
		public string Name { get; set; }
	}
}