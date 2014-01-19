using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Resources;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Utilities.Localization;

namespace Alicargo.ViewModels
{
	public sealed class TransitEditModel
	{
		[Required, DisplayNameLocalized(typeof(Entities), "MethodOfTransit")]
		public MethodOfTransit MethodOfTransit { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "DeliveryType")]
		public DeliveryType DeliveryType { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "City")]
		public long CityId { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Address")]
		public string Address { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "RecipientName")]
		public string RecipientName { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Phone")]
		public string Phone { get; set; }

		[DisplayNameLocalized(typeof(Entities), "WarehouseWorkingTime")]
		public string WarehouseWorkingTime { get; set; }
	}
}