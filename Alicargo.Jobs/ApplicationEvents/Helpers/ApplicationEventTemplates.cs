using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Jobs.ApplicationEvents.Abstract;
using Alicargo.Jobs.ApplicationEvents.Entities;
using Alicargo.Jobs.Helpers;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public sealed class ApplicationEventTemplates : IApplicationEventTemplates
	{
		private readonly ITemplateRepositoryWrapper _wrapper;
		private readonly ISerializer _serializer;
		private readonly IEmailTemplateRepository _templates;

		public ApplicationEventTemplates(
			ITemplateRepositoryWrapper wrapper,
			IEmailTemplateRepository templates,
			ISerializer serializer)
		{
			_wrapper = wrapper;
			_templates = templates;
			_serializer = serializer;
		}

		public long? GetTemplateId(EventType type, byte[] applicationEventData)
		{
			var eventTemplate = _wrapper.GetByEventType(type);

			if (type != EventType.ApplicationSetState)
				return eventTemplate.EmailTemplateId;

			var stateEventData = _serializer.Deserialize<ApplicationSetStateEventData>(applicationEventData);
			var stateTemplate = _templates.GetByStateId(stateEventData.StateId);
			if (stateTemplate != null && stateTemplate.EnableEmailSend && !stateTemplate.UseEventTemplate)
			{
				return stateTemplate.EmailTemplateId;
			}

			return eventTemplate.EmailTemplateId;
		}

		public EmailTemplateLocalizationData GetLocalization(long templateId, string language)
		{
			return _wrapper.GetLocalization(templateId, language);
		}
	}
}