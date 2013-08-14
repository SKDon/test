using Alicargo.Core.Localization;
using Resources;

namespace Alicargo.ViewModels
{
	public sealed class BrockerAWBModel
	{
		[DisplayNameLocalized(typeof(Entities), "GTD")]
		public string GTD { get; set; }

		[DisplayNameLocalized(typeof(Entities), "GTD")]
		public string GTDFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "GTDAdditional")]
		public string GTDAdditionalFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Invoice")]
		public string InvoiceFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Packing")]
		public string PackingFileName { get; set; }

		#region // todo: remove file fields

		[DisplayNameLocalized(typeof (Entities), "GTD")]
		public byte[] GTDFile { get; set; }

		[DisplayNameLocalized(typeof (Entities), "GTDAdditional")]
		public byte[] GTDAdditionalFile { get; set; }

		[DisplayNameLocalized(typeof (Entities), "Invoice")]
		public byte[] InvoiceFile { get; set; }

		[DisplayNameLocalized(typeof (Entities), "Packing")]
		public byte[] PackingFile { get; set; }

		#endregion

	}
}