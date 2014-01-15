using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.DataAccess.Contracts.Repositories
{
	public interface IEventEmailRecipient
	{
		RoleType[] GetRecipientRoles(EventType eventType);
		void SetForEvent(EventType eventType, string language, bool enableEmailSend,
			RoleType[] recipients, EmailTemplateLocalizationData localization);
	}
}