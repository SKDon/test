using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Resources;
using Alicargo.Utilities.Localization;

namespace Alicargo.ViewModels.Application
{
	public sealed class ApplicationSenderModel
	{
		public ApplicationSenderModel()
		{
			Currency = new CurrencyModel();
		}

		[Required]
		[DisplayNameLocalized(typeof(Entities), "FactoryName")]
		public string FactoryName { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Mark")]
		public string MarkName { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Invoice")]
		public string Invoice { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Weight")]
		public float? Weight { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Count")]
		public int? Count { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Volume")]
		[Required]
		public float Volume { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Value")]
		public CurrencyModel Currency { get; set; }

		[DisplayNameLocalized(typeof(Entities), "FactureCost")]
		public decimal? FactureCost { get; set; }

		[DisplayNameLocalized(typeof(Entities), "FactureCostEx")]
		public decimal? FactureCostEx { get; set; }

		[DisplayNameLocalized(typeof(Entities), "PickupCost")]
		public decimal? PickupCost { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Country")]
		public long CountryId { get; set; }

		[DisplayNameLocalized(typeof(Entities), "FactoryPhone")]
		public string FactoryPhone { get; set; }

		[MaxLength(320)]
		[DataType(DataType.EmailAddress)]
		[DisplayNameLocalized(typeof(Entities), "FactoryEmail")]
		public string FactoryEmail { get; set; }

		[DataType(DataType.Text)]
		[DisplayNameLocalized(typeof(Entities), "FactoryContact")]
		public string FactoryContact { get; set; }

		[DisplayNameLocalized(typeof(Entities), "AddressLoad")]
		public string AddressLoad { get; set; }

		[DisplayNameLocalized(typeof(Entities), "WarehouseWorkingTime")]
		public string WarehouseWorkingTime { get; set; }

		[DisplayNameLocalized(typeof(Entities), "MRN")]
		public string MRN { get; set; }

		[DisplayNameLocalized(typeof(Entities), "CountInInvoce")]
		public int? CountInInvoce { get; set; }

		[DisplayNameLocalized(typeof(Entities), "DocumentWeight")]
		public float? DocumentWeight { get; set; }

		[DisplayNameLocalized(typeof(Entities), "TransitCost")]
		public decimal? TransitCost { get; set; }
	}
}