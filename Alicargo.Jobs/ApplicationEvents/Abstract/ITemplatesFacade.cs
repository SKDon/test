using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Jobs.ApplicationEvents.Abstract
{
	public interface ITemplatesFacade
	{
		long? GetTemplateId(ApplicationEventType type, byte[] data);
		EmailTemplateLocalizationData GetLocalization(long templateId, string language);
	}
}