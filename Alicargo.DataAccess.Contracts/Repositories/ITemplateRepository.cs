using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.DataAccess.Contracts.Repositories
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