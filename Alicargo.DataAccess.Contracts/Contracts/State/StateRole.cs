using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Contracts.State
{
	public sealed class StateRole
	{
		public long StateId { get; set; }
		public RoleType Role { get; set; }
	}
}