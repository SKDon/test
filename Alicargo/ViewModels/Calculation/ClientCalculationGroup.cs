using System;

namespace Alicargo.ViewModels.Calculation
{
	public sealed class ClientCalculationGroup
	{
		public long AirWaybillId { get; set; }

		// ReSharper disable InconsistentNaming

		public string field { get { return "AirWaybillId"; } }
		public string value { get; set; }
		public bool hasSubgroups { get { return false; } }
		public ClientCalculationItem[] items { get; set; }
		public object aggregates { get { return new Object(); } }		

		// ReSharper restore InconsistentNaming		
	}
}