using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Contracts
{
	public sealed class StateRole
	{
		public long StateId { get; set; }
		public RoleType Role { get; set; }
	}
}