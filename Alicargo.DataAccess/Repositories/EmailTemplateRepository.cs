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

		public EmailTemplateData GetByStateId(long stateId, string language)
		{
			var template = _executor.Query<Settings>("[dbo].[EmailTemplate_GetByStateId]", new { StateId = stateId });

			return GetEmailTemplateData(template, language);
		}

		public void Set(long stateId, string language, EmailTemplateLocalizationData data, bool enableEmailSend)
		{
			_executor.Execute("[dbo].[EmailTemplate_Merge]", new
			{
				StateId = stateId,
				data.Body,
				data.IsBodyHtml,
				data.Subject,
				TwoLetterISOLanguageName = language,
				enableEmailSend
			});
		}

		public EmailTemplateData GetByApplicationEvent(ApplicationEventType eventType, string language)
		{
			var template = _executor.Query<Settings>("[dbo].[EmailTemplate_GetByApplicationEvent]",
				new { EventTypeId = (int)eventType });

			return GetEmailTemplateData(template, language);
		}

		private EmailTemplateData GetEmailTemplateData(Settings template, string language)
		{
			if (template == null)
			{
				return null;
			}

			return new EmailTemplateData
			{
				EnableEmailSend = template.EnableEmailSend,
				Localization = GetLocalization(template.EmailTemplateId, language)
			};
		}

		private EmailTemplateLocalizationData GetLocalization(long templId, string language)
		{
			var table = new DataTable("Localizations");
			table.Columns.Add("Value");
			table.Rows.Add(language);

			return _executor.Array<EmailTemplateLocalizationData>(
				"[dbo].[EmailTemplateLocalization_Get]",
				new TableParameters(new { TemplateId = templId }, table))
				.First();
		}

		private sealed class Settings
		{
			public long EmailTemplateId { get; set; }
			public bool EnableEmailSend { get; set; }
		}
	}
}