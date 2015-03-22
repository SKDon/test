using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.DataAccess.Contracts.Contracts.State
{
	public sealed class StateRole
	{
		public long StateId { get; set; }
		public RoleType Role { get; set; }
	}
}