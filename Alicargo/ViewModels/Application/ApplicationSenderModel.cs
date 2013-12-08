using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Localization;
using Alicargo.Core.Resources;

namespace Alicargo.ViewModels.Application
{
	public sealed class ApplicationSenderModel
	{
		public ApplicationSenderModel()
		{
			Currency = new CurrencyModel();
		}

		[Required, DisplayNameLocalized(typeof (Entities), "FactoryName")]
		public string FactoryName { get; set; }

		[Required, DisplayNameLocalized(typeof (Entities), "Mark")]
		public string MarkName { get; set; }

		[Required, DisplayNameLocalized(typeof (Entities), "Invoice")]
		public string Invoice { get; set; }

		[DisplayNameLocalized(typeof (Entities), "Weight")]
		public float? Weight { get; set; }

		[DisplayNameLocalized(typeof (Entities), "Count")]
		public int? Count { get; set; }

		[DisplayNameLocalized(typeof (Entities), "Volume"), Required]
		public float Volume { get; set; }

		[Required, DisplayNameLocalized(typeof (Entities), "Value")]
		public CurrencyModel Currency { get; set; }

		[DisplayNameLocalized(typeof (Entities), "FactureCost")]
		public decimal? FactureCost { get; set; }

		[DisplayNameLocalized(typeof (Entities), "PickupCost")]
		public decimal? PickupCost { get; set; }

		[DisplayNameLocalized(typeof (Entities), "Country")]
		public long CountryId { get; set; }
	}
}