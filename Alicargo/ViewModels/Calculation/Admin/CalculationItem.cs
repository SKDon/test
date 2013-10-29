﻿using Alicargo.Core.Enums;

namespace Alicargo.ViewModels.Calculation.Admin
{
	public sealed class CalculationItem
	{
		public string ClientNic { get; set; }
		public string DisplayNumber { get; set; }
		public decimal TotalTariffCost { get; set; }
		public decimal TotalSenderRate { get; set; }
		public decimal InsuranceCost { get; set; }
		public decimal Profit { get; set; }
		public bool IsCalculated { get; set; }

		#region Application data

		public long ApplicationId { get; set; }
		public string Factory { get; set; }
		public string Mark { get; set; }
		public int? Count { get; set; }
		public float? Weight { get; set; }
		public string Invoice { get; set; }
		public decimal Value { get; set; }
		public int ValueCurrencyId { get; set; }
		public decimal? ScotchCost { get; set; }
		public decimal? FactureCost { get; set; }
		public decimal? PickupCost { get; set; }
		public decimal? TransitCost { get; set; }
		public decimal? TariffPerKg { get; set; }
		public decimal? SenderRate { get; set; }
		public long AirWaybillId { get; set; }

		public ClassType? ClassId { get; set; }

		#endregion
	}
}