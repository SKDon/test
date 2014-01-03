using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Core.Calculation;
using Alicargo.Jobs.Helpers;
using Alicargo.Jobs.Helpers.Abstract;

namespace Alicargo.Jobs.Balance
{
	internal sealed class MessageBuilder : IMessageBuilder
	{
		private readonly IClientCalculationPresenter _calculationPresenter;
		private readonly IExcelClientCalculation _excel;
		private readonly IRecipientsFacade _recipients;
		private readonly ISerializer _serializer;
		private readonly ITemplateRepositoryWrapper _templates;

		public MessageBuilder(
			IRecipientsFacade recipients,
			IClientCalculationPresenter calculationPresenter,
			IExcelClientCalculation excel,
			ISerializer serializer,
			ITemplateRepositoryWrapper templates)
		{
			_recipients = recipients;
			_calculationPresenter = calculationPresenter;
			_excel = excel;
			_serializer = serializer;
			_templates = templates;
		}

		public EmailMessage[] Get(EventType type, EventData eventData)
		{
			var eventDataForEntity = _serializer.Deserialize<EventDataForEntity>(eventData.Data);
			var clientId = eventDataForEntity.EntityId;

			var templateId = _templates.GetTemplateId(type);
			if (!templateId.HasValue)
			{
				return null;
			}

			var recipients = _recipients.GetRecipients(type, clientId);

			var list = _calculationPresenter.List(clientId, int.MaxValue, 0);
			var files = recipients.Select(x => x.Culture)
				.Distinct()
				.Select(x => new
				{
					Stream = _excel.Get(list.Groups, x),
					Language = x
				})
				.ToDictionary(
					x => x.Language,
					x => new FileHolder
					{
						Data = x.Stream.ToArray(),
						Name = "calculation.xlsx"
					})
				.ToArray();

			foreach (var recipient in recipients)
			{
				var localization = _templates.GetLocalization(templateId.Value, recipient.Culture);

				 
			}

			return null;
		}
	}
}