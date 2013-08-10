using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Localization;
using Resources;

namespace Alicargo.Core.Models
{
	public class TransitData
	{
		public TransitData() { }

		public TransitData(TransitData transit)
		{
			if (Id == 0)
				Id = transit.Id;

			City = transit.City;
			Address = transit.Address;
			RecipientName = transit.RecipientName;
			Phone = transit.Phone;
			MethodOfTransitId = transit.MethodOfTransitId;
			DeliveryTypeId = transit.DeliveryTypeId;
			CarrierId = transit.CarrierId;
			WarehouseWorkingTime = transit.WarehouseWorkingTime;
		}

		public long Id { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "City")]
		public string City { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Address")]
		public string Address { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "RecipientName")]
		public string RecipientName { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Phone")]
		public string Phone { get; set; }

		public int MethodOfTransitId { get; set; }

		public int DeliveryTypeId { get; set; }

		public long CarrierId { get; set; }

		[DisplayNameLocalized(typeof(Entities), "WarehouseWorkingTime")]
		public string WarehouseWorkingTime { get; set; }
	}
}