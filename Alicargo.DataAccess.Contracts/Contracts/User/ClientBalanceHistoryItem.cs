using System;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.DataAccess.Contracts.Contracts.User
{
	public sealed class ClientBalanceHistoryItem
	{
		public DateTimeOffset CreationTimestamp { get; set; }
		public DateTimeOffset Timestamp { get; set; }
		public EventType EventType { get; set; }
		public decimal Balance { get; set; }
		public decimal Money { get; set; }
		public string Comment { get; set; }
		public bool IsCalculation { get; set; }
	}
}