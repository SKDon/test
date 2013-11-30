using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Jobs.ApplicationEvents.Abstract;
using Alicargo.Jobs.ApplicationEvents.Entities;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public sealed class TemplatesFacade : ITemplatesFacade
	{
		private readonly ISerializer _serializer;
		private readonly IEmailTemplateRepository _templates;

		public TemplatesFacade(
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

		public EmailTemplateLocalizationData GetLocalization(long templateId, string language)
		{
			var priority = new List<string>
			{
				TwoLetterISOLanguageName.Russian,
				TwoLetterISOLanguageName.English,
				TwoLetterISOLanguageName.Italian
			};

			priority.Remove(language);

			return GetLocalization(templateId, language, priority);
		}

		private EmailTemplateLocalizationData GetLocalization(long templateId, string language, ICollection<string> priority)
		{
			while (true)
			{
				var localization = _templates.GetLocalization(templateId, language);

				var haveBoby = !string.IsNullOrWhiteSpace(localization.Body);
				var haveSubject = !string.IsNullOrWhiteSpace(localization.Subject);
				if (haveBoby || haveSubject)
					return localization;

				priority.Remove(language);
				if (priority.Count == 0) 
					return null;

				language = priority.First();
			}
		}
	}
}