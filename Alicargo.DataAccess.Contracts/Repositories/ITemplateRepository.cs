using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.DataAccess.Contracts.Repositories
{
	public interface ITemplateRepository
	{
		EventTemplateData GetByEventType(EventType eventType);
		EmailTemplateLocalizationData GetLocalization(long templateId, string language);
		void SetForEvent(EventType eventType, string language, bool enableEmailSend,
			EmailTemplateLocalizationData localization);
	}
}