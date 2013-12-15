using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Enums;
using Alicargo.Core.Localization;
using Alicargo.Core.Resources;

namespace Alicargo.ViewModels.Application
{
	public sealed class ApplicationClientModel
	{
		public ApplicationClientModel()
		{
			Currency = new CurrencyModel();
		}

		[Required, DisplayNameLocalized(typeof(Entities), "Value")]
		public CurrencyModel Currency { get; set; }

		#region Data

		[Required, DisplayNameLocalized(typeof(Entities), "Invoice")]
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

		[DisplayNameLocalized(typeof(Entities), "Volume"), Required]
		public float Volume { get; set; }

		[DisplayNameLocalized(typeof(Entities), "TermsOfDelivery")]
		public string TermsOfDelivery { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Country")]
		public long CountryId { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "FactoryName")]
		public string FactoryName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "FactoryPhone")]
		public string FactoryPhone { get; set; }

		[DataType(DataType.EmailAddress), MaxLength(320), DisplayNameLocalized(typeof(Entities), "FactoryEmail")]
		public string FactoryEmail { get; set; }

		[DataType(DataType.Text), DisplayNameLocalized(typeof(Entities), "FactoryContact")]
		public string FactoryContact { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Mark")]
		public string MarkName { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "MethodOfDelivery")]
		public MethodOfDelivery MethodOfDelivery { get; set; }

		#endregion
	}
}