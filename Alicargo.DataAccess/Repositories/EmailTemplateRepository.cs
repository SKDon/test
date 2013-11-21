using Alicargo.Contracts.Contracts;
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

		public StateEmailTemplateData GetByStateId(long stateId)
		{
			var template = _executor.Query<dynamic>("[dbo].[EmailTemplate_GetByStateId]", new { StateId = stateId });

			if (template == null)
			{
				return null;
			}

			var localizations = GetLocalization(template.Id);

			return new StateEmailTemplateData
			{
				EnableEmailSend = template.EnableEmailSend,
				Localizations = localizations
			};
		}

		private EmailTemplateLocalizationData[] GetLocalization(long templId)
		{
			var table = TableParameters.GetLocalizationTable();

			return _executor.Array<EmailTemplateLocalizationData>(
				"[dbo].[EmailTemplateLocalization_Get]",
				new TableParameters(new { TemplateId = templId }, table));
		}
	}
}