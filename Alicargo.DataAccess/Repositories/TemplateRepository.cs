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

		public EventTemplateData GetByEventType(EventType eventType)
		{
			return _executor.Query<EventTemplateData>("[dbo].[EmailTemplate_GetByEvent]",
				new { EventTypeId = (int)eventType });
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

		public void SetForEvent(EventType eventType, string language, bool enableEmailSend, EmailTemplateLocalizationData localization)
		{
			_executor.Execute("[dbo].[EmailTemplate_MergeEvent]", new
			{
				EventTypeId = eventType,
				localization.Body,
				localization.IsBodyHtml,
				localization.Subject,
				TwoLetterISOLanguageName = language,
				enableEmailSend
			});
		}
	}
}