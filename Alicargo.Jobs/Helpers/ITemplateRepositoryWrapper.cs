using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Jobs.Helpers
{
	public interface ITemplateRepositoryWrapper
	{
		long? GetTemplateId(EventType type);
		EmailTemplateLocalizationData GetLocalization(long templateId, string language);
	}
}