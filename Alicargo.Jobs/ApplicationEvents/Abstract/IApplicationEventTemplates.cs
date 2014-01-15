using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.Jobs.ApplicationEvents.Abstract
{
	public interface IApplicationEventTemplates
	{
		long? GetTemplateId(EventType type, byte[] applicationEventData);
		EmailTemplateLocalizationData GetLocalization(long templateId, string language);
	}
}