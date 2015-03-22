using System;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.DataAccess.Contracts.Contracts.Calculation
{
	public sealed class RegistryOfPaymentsData
	{
		public DateTimeOffset Timestamp { get; set; }
		public EventType EventType { get; set; }
		public decimal Balance { get; set; }
		public decimal Money { get; set; }
		public string ClientNic { get; set; }
		public string Comment { get; set; }
	}
}