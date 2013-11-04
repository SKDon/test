using Alicargo.Core.Localization;
using Alicargo.Core.Resources;

namespace Alicargo.ViewModels.AirWaybill
{
	public sealed class AwbBrokerModel
	{
		[DisplayNameLocalized(typeof(Entities), "GTD")]
		public string GTD { get; set; }

		[DisplayNameLocalized(typeof(Entities), "GTD")]
		public byte[] GTDFile { get; set; }
		public string GTDFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "GTDAdditional")]
		public byte[] GTDAdditionalFile { get; set; }
		public string GTDAdditionalFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Invoice")]
		public byte[] InvoiceFile { get; set; }
		public string InvoiceFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Packing")]
		public byte[] PackingFile { get; set; }
		public string PackingFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "CustomCost")]
		public decimal? CustomCost { get; set; }

		[DisplayNameLocalized(typeof(Entities), "BrokerCost")]
		public decimal? BrokerCost { get; set; }
	}
}