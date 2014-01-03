using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Core.Calculation;
using Alicargo.Jobs.Helpers.Abstract;

namespace Alicargo.Jobs.Balance
{
	internal sealed class MessageBuilder : IMessageBuilder
	{
		private readonly IClientCalculationPresenter _calculationPresenter;
		private readonly string _defaultFrom;
		private readonly IExcelClientCalculation _excel;
		private readonly IRecipientsFacade _recipients;
		private readonly ISerializer _serializer;
		private readonly ITemplateRepositoryWrapper _templates;
		private readonly ITextBuilder<TextLocalizedData> _textBuilder;

		public MessageBuilder(
			string defaultFrom,
			IRecipientsFacade recipients,
			IClientCalculationPresenter calculationPresenter,
			IExcelClientCalculation excel,
			ISerializer serializer,
			ITextBuilder<TextLocalizedData> textBuilder,
			ITemplateRepositoryWrapper templates)
		{
			_defaultFrom = defaultFrom;
			_recipients = recipients;
			_calculationPresenter = calculationPresenter;
			_excel = excel;
			_serializer = serializer;
			_textBuilder = textBuilder;
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
			var languages = recipients.Select(x => x.Culture).Distinct().ToArray();

			var files = GetFiles(clientId, languages);

			var localizedData = new TextLocalizedData();

			var localizations = languages.ToDictionary(x => x, x => _templates.GetLocalization(templateId.Value, x));
			var subjects = localizations.ToDictionary(x => x.Key,
				x => _textBuilder.GetText(x.Value.Subject, x.Key, localizedData));
			var bodies = localizations.ToDictionary(x => x.Key,
				x => _textBuilder.GetText(x.Value.Body, x.Key, localizedData));

			return recipients.Select(x => GetEmailMessage(x.Email, subjects[x.Culture], bodies[x.Culture], files[x.Culture]))
				.ToArray();
		}

		private Dictionary<string, FileHolder> GetFiles(long clientId, IEnumerable<string> languages)
		{
			var list = _calculationPresenter.List(clientId, int.MaxValue, 0);
			var files = languages
				.ToDictionary(
					x => x,
					x =>
					{
						using (var stream = _excel.Get(list.Groups, x))
						{
							return new FileHolder
							{
								Data = stream.ToArray(),
								Name = "calculation.xlsx"
							};
						}
					});
			return files;
		}

		private EmailMessage GetEmailMessage(string email, string subject, string body, FileHolder file)
		{
			return new EmailMessage(subject, body, _defaultFrom, email)
			{
				Files = new[] { file },
				IsBodyHtml = false
			};
		}
	}
}