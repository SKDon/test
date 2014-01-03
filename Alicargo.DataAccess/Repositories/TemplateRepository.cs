using System.Data;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.Helpers;

namespace Alicargo.DataAccess.Repositories
{
	public sealed class TemplateRepository : ITemplateRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public TemplateRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public void SetForState(long stateId, string language, bool enableEmailSend, bool useEventTemplate,
			EmailTemplateLocalizationData localization)
		{
			_executor.Execute("[dbo].[EmailTemplate_MergeState]", new
			{
				StateId = stateId,
				localization.Body,
				localization.IsBodyHtml,
				localization.Subject,
				TwoLetterISOLanguageName = language,
				enableEmailSend,
				useEventTemplate
			});
		}

		public void SetForEvent(EventType eventType, string language, bool enableEmailSend,
			RoleType[] recipients, EmailTemplateLocalizationData localization)
		{
			var table = TableParameters.GeIdsTable("Recipients", recipients.Select(x => (long)x).ToArray());

			_executor.Execute("[dbo].[EmailTemplate_MergeEvent]", new
			{
				EventTypeId = eventType,
				localization.Body,
				localization.IsBodyHtml,
				localization.Subject,
				TwoLetterISOLanguageName = language,
				enableEmailSend
			});

			_executor.Execute("[dbo].[EventEmailRecipient_Set]", new TableParameters(new { EventTypeId = eventType }, table));
		}

		public EventTemplateData GetByEventType(EventType eventType)
		{
			return _executor.Query<EventTemplateData>("[dbo].[EmailTemplate_GetByEvent]",
				new { EventTypeId = (int)eventType });
		}

		public StateEmailTemplateData GetByStateId(long stateId)
		{
			return _executor.Query<StateEmailTemplateData>("[dbo].[EmailTemplate_GetByStateId]", new { stateId });
		}

		public RoleType[] GetRecipientRoles(EventType eventType)
		{
			return _executor.Array<RoleType>("[dbo].[EventEmailRecipient_Get]", new { EventTypeId = (int)eventType });
		}

		public EmailTemplateLocalizationData GetLocalization(long templateId, string language)
		{
			var table = new DataTable("Localizations");
			table.Columns.Add("Value");
			table.Rows.Add(language);

			return _executor.Array<EmailTemplateLocalizationData>(
				"[dbo].[EmailTemplateLocalization_Get]",
				new TableParameters(new { TemplateId = templateId }, table))
				.Single();
		}
	}
}