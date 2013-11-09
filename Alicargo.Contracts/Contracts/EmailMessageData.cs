namespace Alicargo.Contracts.Contracts
{
    public sealed class EmailMessageData
	{
		public long Id { get; set; }

		public string From { get; set; }
		public string To { get; set; }
		public string CopyTo { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public byte[] Files { get; set; }
	}
}