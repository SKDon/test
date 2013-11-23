using Alicargo.Contracts.Contracts;

namespace Alicargo.Contracts.Repositories
{
	public interface IEmailTemplateRepository
	{
		StateEmailTemplateData GetByStateId(long stateId);
		void Set(long stateId, EmailTemplateLocalizationData data, bool enableEmailSend);
	}
}
