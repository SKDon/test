using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Jobs.Core;
using Alicargo.Jobs.Helpers;

namespace Alicargo.Jobs.Balance
{
	public sealed class BalanceEmailCreatorProcessor : IEventProcessor
	{
		private readonly IRecipientsFacade _recipients;
		private readonly ISerializer _serializer;
		private readonly IEventRepository _events;
		private readonly ITemplateRepositoryWrapper _templates;

		public BalanceEmailCreatorProcessor(
			IEventRepository events,
			ITemplateRepositoryWrapper templates,
			IRecipientsFacade recipients,
			ISerializer serializer)
		{
			_events = events;
			_templates = templates;
			_recipients = recipients;
			_serializer = serializer;
		}

		public void ProcessEvent(EventType type, EventData data)
		{
			var eventData = _serializer.Deserialize<EventDataForEntity>(data.Data);

			var templateId = _templates.GetTemplateId(type);

			if (!templateId.HasValue)
			{
				return;
			}

			var recipients = _recipients.GetRecipients(type, eventData.EntityId);

			foreach (var recipient in recipients)
			{
				var localization = _templates.GetLocalization(templateId.Value, recipient.Culture);
			}

			_events.Delete(data.Id);
		}
	}
}