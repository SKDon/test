using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Resources;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Utilities.Localization;

namespace Alicargo.ViewModels.Application
{
	public sealed class ApplicationAdminModel
	{
		public ApplicationAdminModel()
		{
			Currency = new CurrencyModel();
		}

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Value")]
		public CurrencyModel Currency { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Invoice")]
		public string Invoice { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Characteristic")]
		public string Characteristic { get; set; }

		[DisplayNameLocalized(typeof(Entities), "AddressLoad")]
		public string AddressLoad { get; set; }

		[DisplayNameLocalized(typeof(Entities), "WarehouseWorkingTime")]
		public string WarehouseWorkingTime { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Weight")]
		public float? Weight { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Count")]
		public int? Count { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Volume")]
		public float Volume { get; set; }

		[DisplayNameLocalized(typeof(Entities), "TermsOfDelivery")]
		public string TermsOfDelivery { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Country")]
		public long CountryId { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Sender")]
		public long? SenderId { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Forwarder")]
		public long? ForwarderId { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Carrier")]
		public long? CarrierId { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "FactoryName")]
		public string FactoryName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "FactoryPhone")]
		public string FactoryPhone { get; set; }

		[MaxLength(320)]
		[DataType(DataType.EmailAddress)]
		[DisplayNameLocalized(typeof(Entities), "FactoryEmail")]
		public string FactoryEmail { get; set; }

		[DataType(DataType.Text)]
		[DisplayNameLocalized(typeof(Entities), "FactoryContact")]
		public string FactoryContact { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Mark")]
		public string MarkName { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "MethodOfDelivery")]
		public MethodOfDelivery MethodOfDelivery { get; set; }

		[DisplayNameLocalized(typeof(Entities), "FactureCost")]
		public decimal? FactureCost { get; set; }

		[DisplayNameLocalized(typeof(Entities), "FactureCostEx")]
		public decimal? FactureCostEx { get; set; }

		[DisplayNameLocalized(typeof(Entities), "PickupCost")]
		public decimal? PickupCost { get; set; }

		[DisplayNameLocalized(typeof(Entities), "FactureCostEdited")]
		public decimal? FactureCostEdited { get; set; }

		[DisplayNameLocalized(typeof(Entities), "FactureCostExEdited")]
		public decimal? FactureCostExEdited { get; set; }

		[DisplayNameLocalized(typeof(Entities), "TransitCostEdited")]
		public decimal? TransitCostEdited { get; set; }

		[DisplayNameLocalized(typeof(Entities), "ScotchCostEdited")]
		public decimal? ScotchCostEdited { get; set; }

		[DisplayNameLocalized(typeof(Entities), "PickupCostEdited")]
		public decimal? PickupCostEdited { get; set; }

		[DisplayNameLocalized(typeof(Entities), "TransitCost")]
		public decimal? TransitCost { get; set; }

		[DisplayNameLocalized(typeof(Entities), "TariffPerKg")]
		public decimal? TariffPerKg { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "InsuranceRate")]
		public decimal InsuranceRate { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "InsuranceRateForClient")]
		public decimal InsuranceRateForClient { get; set; }
	}
}