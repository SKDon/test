using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Contracts
{
	public sealed class RecipientData
	{
		public RecipientData(string email, string culture, RoleType role)
		{
			Culture = culture;
			Email = email;
			Role = role;
		}

		public string Email { get; private set; }
		public string Culture { get; private set; }
		public RoleType Role { get; private set; }
	}
}