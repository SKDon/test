namespace Alicargo.DataAccess.Contracts.Contracts
{
	public sealed class TransitData
	{	
		public long Id { get; set; }

		public string City { get; set; }

		public string Address { get; set; }

		public string RecipientName { get; set; }

		public string Phone { get; set; }

		public int MethodOfTransitId { get; set; }

		public int DeliveryTypeId { get; set; }

		public long CarrierId { get; set; }

		public string WarehouseWorkingTime { get; set; }
	}
}