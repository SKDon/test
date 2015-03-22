using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.Jobs.Helpers.Abstract
{
	public interface ITemplateRepositoryHelper
	{
		long? GetTemplateId(EventType type);
		EmailTemplateLocalizationData GetLocalization(long templateId, string language);
	}
}