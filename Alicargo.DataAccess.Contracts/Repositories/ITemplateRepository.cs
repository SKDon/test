using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
{
	public interface ITemplateRepository
	{
		void SetForState(long stateId, string language, bool enableEmailSend,
			bool useEventTemplate, EmailTemplateLocalizationData localization);

		EventTemplateData GetByEventType(EventType eventType);
		StateEmailTemplateData GetByStateId(long stateId);
		EmailTemplateLocalizationData GetLocalization(long templateId, string language);
	}
}