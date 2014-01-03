using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Jobs.Helpers
{
	public interface ITemplateRepositoryWrapper
	{
		EventTemplateData GetByEventType(EventType type);
		EmailTemplateLocalizationData GetLocalization(long templateId, string language);
	}
}