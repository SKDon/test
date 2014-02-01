using Alicargo.DataAccess.Contracts.Contracts;

namespace Alicargo.ViewModels
{
	public sealed class TransitMapper
	{
		public static void Map(TransitEditModel @from, TransitData to, long carrierId)
		{
			to.Address = @from.Address;
			to.CarrierId = carrierId;
			to.WarehouseWorkingTime = @from.WarehouseWorkingTime;
			to.CityId = @from.CityId;
			to.DeliveryType = @from.DeliveryType;
			to.MethodOfTransit = @from.MethodOfTransit;
			to.Phone = @from.Phone;
			to.RecipientName = @from.RecipientName;
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