using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.DataAccess.Contracts.Contracts
{
	public sealed class TransitData
	{	
		public long Id { get; set; }

		public long CityId { get; set; }

		public long CarrierId { get; set; }

		public string Address { get; set; }

		public string RecipientName { get; set; }

		public string Phone { get; set; }

		public MethodOfTransit MethodOfTransit { get; set; }

		public DeliveryType DeliveryType { get; set; }

		public string WarehouseWorkingTime { get; set; }
	}
}