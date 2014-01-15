using System.Data;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
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

		public EventTemplateData GetByEventType(EventType eventType)
		{
			return _executor.Query<EventTemplateData>("[dbo].[EmailTemplate_GetByEvent]",
				new { EventTypeId = (int)eventType });
		}

		public StateEmailTemplateData GetByStateId(long stateId)
		{
			return _executor.Query<StateEmailTemplateData>("[dbo].[EmailTemplate_GetByStateId]", new { stateId });
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