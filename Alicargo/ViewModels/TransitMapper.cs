using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.ViewModels
{
	public sealed class TransitMapper
	{
		public static TransitData Map(TransitEditModel model, long carrierId)
		{
			return new TransitData
			{
				Address = model.Address,
				CarrierId = carrierId,
				WarehouseWorkingTime = model.WarehouseWorkingTime,
				CityId = model.CityId,
				DeliveryType = model.DeliveryType,
				MethodOfTransit = model.MethodOfTransit,
				Phone = model.Phone,
				RecipientName = model.RecipientName
			};
		}

		public static TransitEditModel Map(TransitData data)
		{
			return new TransitEditModel
			{
				Address = data.Address,
				WarehouseWorkingTime = data.WarehouseWorkingTime,
				CityId = data.CityId,
				DeliveryType = data.DeliveryType,
				MethodOfTransit = data.MethodOfTransit,
				Phone = data.Phone,
				RecipientName = data.RecipientName
			};
		}
	}
}