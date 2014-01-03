using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Jobs.Helpers;

namespace Alicargo.Jobs.Balance.Helpers
{
	internal sealed class MessageBuilder : IMessageBuilder
	{
		private readonly IRecipientsFacade _recipients;
		private readonly ISerializer _serializer;
		private readonly IEventRepository _events;
		private readonly ITemplateRepositoryWrapper _templates;

		public MessageBuilder(IEventRepository events, IRecipientsFacade recipients, ISerializer serializer, ITemplateRepositoryWrapper templates)
		{
			_events = events;
			_recipients = recipients;
			_serializer = serializer;
			_templates = templates;
		}


		public EmailMessage[] Get()
		{
			//var templateId = _templates.GetTemplateId(type);
			//if (!templateId.HasValue)
			//{
			//	return;
			//}

			//var recipients = _recipients.GetRecipients(type, eventData.EntityId);
			//foreach (var recipient in recipients)
			//{
			//	var localization = _templates.GetLocalization(templateId.Value, recipient.Culture);
			//}

			return null;
		}
	}
}