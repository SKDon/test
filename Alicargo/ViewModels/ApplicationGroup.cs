namespace Alicargo.ViewModels
{
	public sealed class ApplicationGroup
	{
		// ReSharper disable InconsistentNaming
		public object aggregates { get; set; }
		public string field { get; set; }
		public string value { get; set; }
		public bool hasSubgroups { get; set; }
		public object[] items { get; set; }
		// ReSharper restore InconsistentNaming
	}
}