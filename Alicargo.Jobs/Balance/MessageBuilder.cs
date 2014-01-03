using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Jobs.Helpers.Abstract;

namespace Alicargo.Jobs.Balance
{
	internal sealed class MessageBuilder : IMessageBuilder
	{
		private readonly IRecipientsFacade _recipients;
		private readonly ISerializer _serializer;
		private readonly ITemplateRepositoryWrapper _templates;

		public MessageBuilder(IRecipientsFacade recipients, ISerializer serializer, ITemplateRepositoryWrapper templates)
		{
			_recipients = recipients;
			_serializer = serializer;
			_templates = templates;
		}

		public EmailMessage[] Get(EventType type, EventData eventData)
		{
			var eventDataForEntity = _serializer.Deserialize<EventDataForEntity>(eventData.Data);

			var templateId = _templates.GetTemplateId(type);
			if (!templateId.HasValue)
			{
				return null;
			}

			// get excel file

			var recipients = _recipients.GetRecipients(type, eventDataForEntity.EntityId);
			foreach (var recipient in recipients)
			{
				var localization = _templates.GetLocalization(templateId.Value, recipient.Culture);

				// TextBulderHelper
			}
		}
	}
}