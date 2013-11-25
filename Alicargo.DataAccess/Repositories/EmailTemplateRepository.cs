using System.Data;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.Helpers;

namespace Alicargo.DataAccess.Repositories
{
	public sealed class EmailTemplateRepository : IEmailTemplateRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public EmailTemplateRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public void SetForState(long stateId, string language, bool enableEmailSend, bool useApplicationEventTemplate,
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
				useApplicationEventTemplate
			});
		}

		public void SetForApplicationEvent(ApplicationEventType eventType, string language, bool enableEmailSend,
			RoleType[] recipients, EmailTemplateLocalizationData localization)
		{
			var table = TableParameters.GeIdsTable("Recipients", recipients.Select(x => (long)x).ToArray());

			_executor.Execute("[dbo].[EmailTemplate_MergeApplicationEvent]", new
			{
				EventTypeId = eventType,
				localization.Body,
				localization.IsBodyHtml,
				localization.Subject,
				TwoLetterISOLanguageName = language,
				enableEmailSend
			});

			_executor.Execute("[dbo].[ApplicationEventEmailRecipient_Set]", new TableParameters(new { EventTypeId = eventType }, table));
		}

		public ApplicationEventTemplateData GetByEventType(ApplicationEventType eventType)
		{
			return _executor.Query<ApplicationEventTemplateData>("[dbo].[EmailTemplate_GetByApplicationEvent]",
				new { EventTypeId = (int)eventType });
		}

		public StateEmailTemplateData GetByStateId(long stateId)
		{
			return _executor.Query<StateEmailTemplateData>("[dbo].[EmailTemplate_GetByStateId]", new { stateId });
		}

		public RoleType[] GetRecipientRoles(ApplicationEventType eventType)
		{
			return _executor.Array<RoleType>("[dbo].[ApplicationEventEmailRecipient_Get]", new { EventTypeId = (int)eventType });
		}

		public EmailTemplateLocalizationData GetLocalization(long templateId, string language)
		{
			var table = new DataTable("Localizations");
			table.Columns.Add("Value");
			table.Rows.Add(language);

			return _executor.Array<EmailTemplateLocalizationData>(
				"[dbo].[EmailTemplateLocalization_Get]",
				new TableParameters(new { TemplateId = templateId }, table))
				.First();
		}
	}
}