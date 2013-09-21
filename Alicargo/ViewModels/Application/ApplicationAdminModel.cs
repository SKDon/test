using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Localization;
using Resources;

namespace Alicargo.ViewModels.Application
{
	public sealed class ApplicationAdminModel
	{
		public ApplicationAdminModel()
		{
			Currency = new CurrencyModel();
		}

		[Required, DisplayNameLocalized(typeof(Entities), "Value")]
		public CurrencyModel Currency { get; set; }

		#region Files

		[DisplayNameLocalized(typeof(Entities), "Invoice")]
		public byte[] InvoiceFile { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Packing")]
		public byte[] PackingFile { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Swift")]
		public byte[] SwiftFile { get; set; }

		[DisplayNameLocalized(typeof(Entities), "DeliveryBill")]
		public byte[] DeliveryBillFile { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Torg12")]
		public byte[] Torg12File { get; set; }

		[DisplayNameLocalized(typeof(Entities), "CP")]
		public byte[] CPFile { get; set; }

		#endregion

		#region Data

		[Required, DisplayNameLocalized(typeof(Entities), "Invoice")]
		public string Invoice { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Invoice")]
		public string InvoiceFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Swift")]
		public string SwiftFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Packing")]
		public string PackingFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "DeliveryBill")]
		public string DeliveryBillFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Torg12")]
		public string Torg12FileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "CP")]
		public string CPFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Characteristic")]
		public string Characteristic { get; set; }

		[DisplayNameLocalized(typeof(Entities), "AddressLoad")]
		public string AddressLoad { get; set; }

		[DisplayNameLocalized(typeof(Entities), "WarehouseWorkingTime")]
		public string WarehouseWorkingTime { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Weigth")]
		public float? Weigth { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Count")]
		public int? Count { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Volume"), Required]
		public float Volume { get; set; }

		[DisplayNameLocalized(typeof(Entities), "TermsOfDelivery")]
		public string TermsOfDelivery { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Country")]
		public long? CountryId { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Sender")]
		public long? SenderId { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "FactoryName")]
		public string FactoryName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "FactoryPhone")]
		public string FactoryPhone { get; set; }

		[DataType(DataType.EmailAddress), MaxLength(320), DisplayNameLocalized(typeof(Entities), "FactoryEmail")]
		public string FactoryEmail { get; set; }

		[DataType(DataType.MultilineText), DisplayNameLocalized(typeof(Entities), "FactoryContact")]
		public string FactoryContact { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Mark")]
		public string MarkName { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "MethodOfDelivery")]
		public int MethodOfDeliveryId { get; set; }

		[DisplayNameLocalized(typeof(Entities), "ScotchCost")]
		public decimal? ScotchCost { get; set; }

		[DisplayNameLocalized(typeof(Entities), "FactureCost")]
		public decimal? FactureCost { get; set; }

		[DisplayNameLocalized(typeof(Entities), "WithdrawCost")]
		public decimal? WithdrawCost { get; set; }

		[DisplayNameLocalized(typeof(Entities), "FactureCostEdited")]
		public decimal? FactureCostEdited { get; set; }

		[DisplayNameLocalized(typeof(Entities), "ScotchCostEdited")]
		public decimal? ScotchCostEdited { get; set; }

		[DisplayNameLocalized(typeof(Entities), "WithdrawCostEdited")]
		public decimal? WithdrawCostEdited { get; set; }

		[DisplayNameLocalized(typeof(Entities), "TransitCost")]
		public decimal? TransitCost { get; set; }

		[DisplayNameLocalized(typeof(Entities), "ForwarderCost")]
		public decimal? ForwarderCost { get; set; }

		[DisplayNameLocalized(typeof(Entities), "TariffPerKg")]
		public decimal? TariffPerKg { get; set; }

		#endregion
	}
}