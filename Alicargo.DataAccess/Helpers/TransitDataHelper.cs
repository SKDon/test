using Alicargo.Core.Models;

namespace Alicargo.DataAccess.Helpers
{
	// todo: test
	internal static class TransitDataHelper
	{
		public static void CopyTo(this TransitData from, DbContext.Transit to)
		{
			to.City = from.City;
			to.Address = from.Address;
			to.RecipientName = from.RecipientName;
			to.Phone = from.Phone;
			to.MethodOfTransitId = from.MethodOfTransitId;
			to.DeliveryTypeId = from.DeliveryTypeId;
			to.CarrierId = from.CarrierId;
			to.WarehouseWorkingTime = from.WarehouseWorkingTime;
		}
	}
}