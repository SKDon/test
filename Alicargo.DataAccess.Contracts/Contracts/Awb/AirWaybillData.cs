using System;

namespace Alicargo.DataAccess.Contracts.Contracts.Awb
{
	public sealed class AirWaybillData : AirWaybillEditData
	{
		public long Id { get; set; }
		public long StateId { get; set; }
		public DateTimeOffset CreationTimestamp { get; set; }
		public DateTimeOffset StateChangeTimestamp { get; set; }
		public bool IsActive { get; set; }
		public long CreatorUserId { get; set; }
	}
}