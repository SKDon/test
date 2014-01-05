using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Jobs.Helpers.Abstract
{
	public interface ITemplateRepositoryHelper
	{
		long? GetTemplateId(EventType type);
		EmailTemplateLocalizationData GetLocalization(long templateId, string language);
	}
}