using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
{
	public interface IEmailTemplateRepository
	{
		EmailTemplateData GetByStateId(long stateId, string language);
		void SetForState(long stateId, string language, bool enableEmailSend, EmailTemplateLocalizationData data);
		void SetForApplicationEvent(ApplicationEventType eventType, string language, bool enableEmailSend,
			EmailTemplateLocalizationData data);
		EmailTemplateData GetByApplicationEvent(ApplicationEventType eventType, string language);
	}
}