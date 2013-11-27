using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Jobs.Entities;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	internal sealed class TemplatesHelper
	{
		private readonly ISerializer _serializer;
		private readonly IEmailTemplateRepository _templates;

		public TemplatesHelper(
			ISerializer serializer,
			IEmailTemplateRepository templates)
		{
			_serializer = serializer;
			_templates = templates;
		}

		public long? GetTemplateId(ApplicationEventType type, byte[] data)
		{
			var eventTemplate = _templates.GetByEventType(type);
			if (eventTemplate == null || !eventTemplate.EnableEmailSend)
			{
				return null;
			}

			if (type != ApplicationEventType.SetState)
				return eventTemplate.EmailTemplateId;

			var stateEventData = _serializer.Deserialize<ApplicationSetStateEventData>(data);
			var stateTemplate = _templates.GetByStateId(stateEventData.StateId);
			if (stateTemplate != null && stateTemplate.EnableEmailSend && !stateTemplate.UseApplicationEventTemplate)
			{
				return stateTemplate.EmailTemplateId;
			}

			return eventTemplate.EmailTemplateId;
		}

		public EmailTemplateLocalizationData GetLocalization(RecipientData recipient, long templateId)
		{
			return _templates.GetLocalization(templateId, recipient.Culture);
		}

		
	}
}