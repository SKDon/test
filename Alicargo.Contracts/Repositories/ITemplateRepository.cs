using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
{
	public interface ITemplateRepository
	{
		void SetForState(long stateId, string language, bool enableEmailSend,
			bool useEventTemplate, EmailTemplateLocalizationData localization);

		void SetForEvent(EventType eventType, string language, bool enableEmailSend,
			RoleType[] recipients, EmailTemplateLocalizationData localization);

		EventTemplateData GetByEventType(EventType eventType);
		StateEmailTemplateData GetByStateId(long stateId);
		RoleType[] GetRecipientRoles(EventType eventType);
		EmailTemplateLocalizationData GetLocalization(long templateId, string language);
	}
}