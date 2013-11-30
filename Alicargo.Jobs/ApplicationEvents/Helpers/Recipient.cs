using Alicargo.Contracts.Enums;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public sealed class Recipient
	{
		public string Email { get; set; }
		public string Culture { get; set; }
		public RoleType Role { get; set; }
	}
}