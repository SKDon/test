using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
{
	public interface IEmailTemplateRepository
	{
		EmailTemplateData GetByStateId(long stateId, string language);
		void Set(long stateId, string language, EmailTemplateLocalizationData data, bool enableEmailSend);
		EmailTemplateData GetByApplicationEvent(ApplicationEventType eventType, string language);
	}
}