using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.DataAccess.Contracts.Repositories
{
	public interface IEventEmailRecipient
	{
		RoleType[] GetRecipientRoles(EventType eventType);
		void Set(EventType eventType, RoleType[] recipients);
	}
}