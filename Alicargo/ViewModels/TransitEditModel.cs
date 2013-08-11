using System.ComponentModel.DataAnnotations;
using Alicargo.Contracts.Contracts;
using Alicargo.Core.Enums;
using Alicargo.Core.Localization;
using Resources;

namespace Alicargo.ViewModels
{
	public sealed class TransitEditModel
	{
		[Required, DisplayNameLocalized(typeof(Entities), "MethodOfTransit")]
		public MethodOfTransit MethodOfTransit { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "DeliveryType")]
		public DeliveryType DeliveryType { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "City")]
		public string City { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Address")]
		public string Address { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "RecipientName")]
		public string RecipientName { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Phone")]
		public string Phone { get; set; }

		[DisplayNameLocalized(typeof(Entities), "WarehouseWorkingTime")]
		public string WarehouseWorkingTime { get; set; }

		public static TransitData GetData(TransitEditModel model, long carrierId)
		{
			return new TransitData
			{
				Address = model.Address,
				CarrierId = carrierId,
				WarehouseWorkingTime = model.WarehouseWorkingTime,
				City = model.City,
				DeliveryTypeId = (int)model.DeliveryType,
				MethodOfTransitId = (int)model.MethodOfTransit,
				Phone = model.Phone,
				RecipientName = model.RecipientName
			};
		}

		public static TransitEditModel GetModel(TransitData data)
		{
			return new TransitEditModel
			{
				Address = data.Address,
				WarehouseWorkingTime = data.WarehouseWorkingTime,
				City = data.City,
				DeliveryType = (DeliveryType)data.DeliveryTypeId,
				MethodOfTransit = (MethodOfTransit)data.MethodOfTransitId,
				Phone = data.Phone,
				RecipientName = data.RecipientName				
			};
		}
	}
}