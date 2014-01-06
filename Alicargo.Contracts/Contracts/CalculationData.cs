﻿using System;

namespace Alicargo.Contracts.Contracts
{
	public sealed class CalculationData
	{
		public DateTimeOffset CreationTimestamp { get; set; }
		public long ClientId { get; set; }
		public string AirWaybillDisplay { get; set; }
		public string ApplicationDisplay { get; set; }
		public string MarkName { get; set; }
		public string FactoryName { get; set; }
		public float Weight { get; set; }
		public decimal TariffPerKg { get; set; }
		public decimal ScotchCost { get; set; }
		public decimal InsuranceCost { get; set; }
		public decimal FactureCost { get; set; }
		public decimal TransitCost { get; set; }
		public decimal PickupCost { get; set; }
	}
}