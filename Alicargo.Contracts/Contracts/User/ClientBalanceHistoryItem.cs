using System;

namespace Alicargo.Contracts.Contracts.User
{
	public sealed class ClientBalanceHistoryItem
	{
		public DateTimeOffset Timestamp { get; set; }
		public decimal Balance { get; set; }
		public decimal Input { get; set; }
		public string Comment { get; set; }
	}
}